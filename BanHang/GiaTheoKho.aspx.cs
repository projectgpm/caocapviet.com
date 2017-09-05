using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class GiaTheoVung : System.Web.UI.Page
    {
        dtGiaTheoVung data = new dtGiaTheoVung();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayTrangThaiMenu_ChucNang(Session["IDNhom"].ToString(), 9) == 1)
                    Response.Redirect("Default.aspx");
                if (dtSetting.LayTrangThaiMenu(Session["IDNhom"].ToString(), 9) == 1)
                {
                    if (!IsPostBack)
                    {
                        DanhSachVung();
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
                if (cmbKho.Text != "")
                {
                    LoadGrid(cmbKho.Value.ToString());
                }
            }
        }

        private void DanhSachVung()
        {
            data = new dtGiaTheoVung();
            DanhSachKho.DataSource = data.LayDanhSachKho();
            DanhSachKho.ValueField = "ID";
            DanhSachKho.TextField = "TenCuaHang";
            DanhSachKho.DataBind();
            DanhSachKho.SelectAll();

            dtVung dt1 = new dtVung();
            DataTable d2x = dt1.LayDanhSach();
            d2x.Rows.Add(-1,0 ,"Tất cả Vùng", 0);
            cmbVung.DataSource = d2x;
            cmbVung.ValueField = "ID";
            cmbVung.TextField = "TenVung";
            cmbVung.DataBind();
            cmbVung.SelectedIndex = cmbVung.Items.Count;
           
           
        }

        private void LoadGrid(string IDKho)
        {
              data = new dtGiaTheoVung();
              gridHangHoa.DataSource = data.DanhSachHangHoa_IDKho(IDKho);
              gridHangHoa.DataBind();
        }
        protected void cmbVung_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVung.Text != "")
            {
                int ID = Int32.Parse(cmbVung.Value.ToString());
                data = new dtGiaTheoVung();
                DanhSachKho.DataSource = data.LayDanhSachKho_IDVung(ID);
                DanhSachKho.ValueField = "ID";
                DanhSachKho.TextField = "TenCuaHang";
                DanhSachKho.DataBind();
                DanhSachKho.SelectAll();
            }
        }
        protected void gridHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string IDDonViTinh = e.NewValues["IDDonViTinh"].ToString();
            string IDKho1 = cmbKho.Value.ToString();

            if (DanhSachKho.SelectedItems.Count != 0)
            {
                string IDHangHoa = dtCapNhatTonKho.LayIDHangHoa_HangHoaTonKho(ID).ToString();
                float giacu0 = dtCapNhatTonKho.GiaBan_KhoChiNhanh(IDHangHoa, IDKho1);
                float giacu1 = dtCapNhatTonKho.GiaBan1_KhoChiNhanh(IDHangHoa, IDKho1);
                float giacu2 = dtCapNhatTonKho.GiaBan2_KhoChiNhanh(IDHangHoa, IDKho1);
                float giacu3 = dtCapNhatTonKho.GiaBan3_KhoChiNhanh(IDHangHoa, IDKho1);
                float giacu4 = dtCapNhatTonKho.GiaBan4_KhoChiNhanh(IDHangHoa, IDKho1);
                float giacu5 = dtCapNhatTonKho.GiaBan5_KhoChiNhanh(IDHangHoa, IDKho1);
                float GiaBan0 = float.Parse(e.NewValues["GiaBan"].ToString());
                float GiaBan1 = float.Parse(e.NewValues["GiaBan1"].ToString());
                float GiaBan2 = float.Parse(e.NewValues["GiaBan2"].ToString());
                float GiaBan3 = float.Parse(e.NewValues["GiaBan3"].ToString());
                float GiaBan4 = float.Parse(e.NewValues["GiaBan4"].ToString());
                float GiaBan5 = float.Parse(e.NewValues["GiaBan5"].ToString());
                for (int i = DanhSachKho.Items.Count - 1; i >= 0; i--)
                {
                    if (DanhSachKho.Items[i].Selected == true)
                    {
                       
                        // chưa lấy được giá bán củ
                        string IDKhoMoi = DanhSachKho.Items[i].Value.ToString();
                        if (giacu0 != GiaBan0)
                        {
                            data = new dtGiaTheoVung();
                            data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKhoMoi, GiaBan0.ToString(), "GiaBan");
                        }
                        if (giacu1 != GiaBan1)
                        {
                            data = new dtGiaTheoVung();
                            data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKhoMoi, GiaBan1.ToString(), "GiaBan1");
                        }
                        if (giacu2 != GiaBan2)
                        {
                            data = new dtGiaTheoVung();
                            data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKhoMoi, GiaBan2.ToString(), "GiaBan2");
                        }
                        if (giacu3 != GiaBan3)
                        {
                            data = new dtGiaTheoVung();
                            data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKhoMoi, GiaBan3.ToString(), "GiaBan3");
                        }
                        if (giacu4 != GiaBan4)
                        {
                            data = new dtGiaTheoVung();
                            data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKhoMoi, GiaBan4.ToString(), "GiaBan4");
                        }
                        if (giacu5 != GiaBan5)
                        {
                            data = new dtGiaTheoVung();
                            data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKhoMoi, GiaBan5.ToString(), "GiaBan5");
                        }
                      // ghi lịch sử cụ thể giá mấy thay đổi
                    }
                }
                Session["updated"] = true;
                e.Cancel = true;
                gridHangHoa.CancelEdit();
                LoadGrid(cmbKho.Value.ToString());
            }
            else
            {
                throw new Exception("Lỗi: Chưa chọn kho để áp dụng giá mới?");
            }
        }

        protected void gridHangHoa_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            if (Session["updated"] != null)
            {
                if (Session["updated"].ToString() == "True")
                {
                    e.Properties["cpUpdated"] = true;
                    Session["updated"] = null;
                }
            }
        }

        protected void cmbKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKho.Text != "")
            {
                string IDKho = cmbKho.Value.ToString();
                LoadGrid(IDKho);
            }
        }
        
    }
}