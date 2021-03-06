﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using BanHang.Data;
using System.Data;

namespace BanHang
{
    public partial class RootMaster : System.Web.UI.MasterPage
    {
        dtMasterPage data = new dtMasterPage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
              
                XuLyThayDoiGiaTheoGio();
                XuLyDonHangChiNhanh();
                HuyDonHangThuMua();
                if (!IsPostBack)
                {
                    data = new dtMasterPage();
                    DataTable dbt = data.DanhSachMemuDuocHienThi(Session["IDNhom"].ToString());
                    if (dbt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dbt.Rows)
                        {
                            string name = dr["Name"].ToString();
                            RibbonItemBase kt = getbyName(name, ribbonMenu);
                            kt.Visible = true;
                        }
                    }
                    lblChao.Text = "Xin Chào: " + Session["TenDangNhap"].ToString();
                    ASPxLabel2.Text = Server.HtmlDecode("Copyrights &copy;") + DateTime.Now.Year + Server.HtmlDecode(". All Rights Reserved. Designed by GPM.VN");
                }
            }
        }
        protected RibbonItemBase getbyName(string name, ASPxRibbon ribbon)
        {
            foreach (RibbonTab tab in ribbon.Tabs)
            {
                foreach (RibbonGroup group in tab.Groups)
                {
                    foreach (RibbonItemBase item in group.Items)
                    {
                        if (item.Name.Trim() == name.Trim())
                            return item;

                        RibbonItemBase subItem = getbyNameSubItem(name, item);
                        if (subItem != null)
                            return subItem;
                    }
                }
            }
            return null;
        }
        private RibbonItemBase getbyNameSubItem(string name, RibbonItemBase item)
        {
            if (item is RibbonDropDownButtonItem)
                foreach (RibbonDropDownButtonItem subItem in ((RibbonDropDownButtonItem)item).Items)
                {
                    if (subItem.Name.Trim() == name.Trim())
                    {
                        return subItem;
                    }
                    var subItemResult = getbyNameSubItem(name, subItem);
                    if (subItemResult != null)
                        return subItemResult;
                }
            return null;
        }
        public void XuLyDonHangChiNhanh()
        {
            data = new dtMasterPage();
            DataTable db = data.DanhSachDonHangChiNhanhChuaXuLy(DateTime.Now);
            if (db.Rows.Count > 0)
            {
                foreach (DataRow dr in db.Rows)
                {
                    string ID = dr["ID"].ToString();
                    string IDKho = dr["IDKhoLap"].ToString();
                    if (ID != "")
                    {

                        DataTable dt = data.DanhSachChiTietDuyet(ID);
                        foreach (DataRow dr1 in dt.Rows)
                        {
                            // tự động xử lý đơn hàng chi nhánh sau 1 ngày
                            string IDHangHoa = dr1["IDHangHoa"].ToString();
                            int SoLuong = Int32.Parse(dr1["ThucTe"].ToString());
                            if (SoLuong > 0)
                            {
                                object TheKho = dtTheKho.ThemTheKho(dtDuyetDonHangChiNhanh.LaySoDonHang(ID), "Xác nhận đơn hàng tự động ", SoLuong.ToString(), "0", (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKho).ToString()) + SoLuong).ToString(), "1", IDKho, IDHangHoa, "Nhập", "0", "0", "0", "0");
                                if (TheKho != null)
                                {
                                    dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong.ToString(), IDKho);
                                }
                            }
                        }
                        data = new dtMasterPage();
                        data.CapNhatDonHangHoanTat(ID);
                    }
                }
            }
        }
        public void HuyDonHangThuMua()
        {
            int SoNgayHuy = dtSetting.SoNgayHuyDonHangThuMua();
            DataTable db = data.DanhSachDonHangThuMua(DateTime.Now, SoNgayHuy);
            if (db.Rows.Count > 0)
            {
                foreach (DataRow dr in db.Rows)
                {
                    string ID = dr["ID"].ToString();
                    if (ID != "")
                    {
                        //cập nhật thành đơn hàng hủy trong 7 ngày, số ngày lấy trong dtsetting
                        data = new dtMasterPage();
                        data.CapNhatTrangThaiHuyDonHangThuMua(ID);
                        // ghi lịch sử
                    }
                }
            }
        }
        public void XuLyThayDoiGiaTheoGio()
        {
            data = new dtMasterPage();
            DataTable db = data.DanhSachHangHoaXuLyTheoGio(DateTime.Now);
            if (db.Rows.Count > 0)
            {
                foreach (DataRow dr in db.Rows)
                {
                    string ID = dr["ID"].ToString(); // id dòng lấy cập nhật trạng thái
                    string IDKho = dr["IDKho"].ToString();
                    float Gia0 = float.Parse(dr["GiaBan"].ToString());
                    string IDHangHoa = dr["IDHangHoa"].ToString();
                    string MaHang = dtHangHoa.LayMaHang(IDHangHoa);
                    string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                    if (ID != "")
                    {
                        if (Gia0 != -1)
                        {
                            dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, dtCapNhatTonKho.GiaBan_KhoChiNhanh(IDHangHoa, IDKho).ToString(), Gia0.ToString(), "1", "Hệ Thống Cập Nhật Giá Theo Giờ(GiaBan): Chi Nhánh " + dtSetting.LayTenKho(IDKho));
                            data = new dtMasterPage();
                            data.CapNhat_GiaTheoGio(IDHangHoa, IDKho, Gia0.ToString(), "GiaBan");
                        }
                        data = new dtMasterPage();
                        data.CapNhatGiaHoanTat(ID);
                    }
                }
            }
        }
    }
}