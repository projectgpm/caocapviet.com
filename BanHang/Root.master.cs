using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using BanHang.Data;
using System.Data;

namespace BanHang {
    public partial class RootMaster : System.Web.UI.MasterPage {
        dtMasterPage data = new dtMasterPage();
        protected void Page_Load(object sender, EventArgs e) {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                //XuLyDonHangChiNhanh();
                HuyDonHangThuMua();
                lblChao.Text = "Xin Chào: " + Session["TenDangNhap"].ToString();
                ASPxLabel2.Text = Server.HtmlDecode("Copyrights &copy;") + DateTime.Now.Year + Server.HtmlDecode(". All Rights Reserved. Designed by GPM.VN");
            }
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
                            object TheKho = dtTheKho.ThemTheKho(dtDuyetDonHangChiNhanh.LaySoDonHang(ID), "Xác nhận đơn hàng tự động ", SoLuong.ToString(), "0", (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKho).ToString()) + SoLuong).ToString(), "1", IDKho, IDHangHoa, "Nhập");
                            if (TheKho != null)
                            {
                                dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong.ToString(), IDKho);
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
                        //cập nhật thành đơn hàng hủy
                        data = new dtMasterPage();
                        data.CapNhatTrangThaiHuyDonHangThuMua(ID);
                    }
                }
            }
        }
    }
}