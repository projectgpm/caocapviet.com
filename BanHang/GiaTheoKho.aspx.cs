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
                    LoadGrid();
                }
                else
                {
                    Response.Redirect("Default.aspx");
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

            dtVung dt1 = new dtVung();
            DataTable d2x = dt1.LayDanhSach();
            d2x.Rows.Add(-1,0 ,"Tất cả Vùng", 0);

            cmbVung.DataSource = d2x;
            cmbVung.ValueField = "ID";
            cmbVung.TextField = "TenVung";
            cmbVung.DataBind();
            cmbVung.SelectedIndex = cmbVung.Items.Count;


            dtGiaTheoVung d2 = new dtGiaTheoVung();
            DataTable d2x1 = d2.NhomHang();
            d2x1.Rows.Add(-1,1,1,"Tất cả Nhóm Hàng","", DateTime.Now.Date, 0);
            cmbNhomHang.DataSource = d2x1;
            cmbNhomHang.TextField = "TenNhomHang";
            cmbNhomHang.ValueField = "ID";
            cmbNhomHang.DataBind();
            cmbNhomHang.SelectedIndex = cmbNhomHang.Items.Count;

        }

        private void LoadGrid()
        {
            data = new dtGiaTheoVung();

            int ID = Int32.Parse(cmbNhomHang.Value.ToString());
            DataTable da = data.LayDanhSachHangHoa_IDNhomHang(ID,1);
            gridHangHoa.DataSource = da;
            gridHangHoa.DataBind();
           
        }
        protected void LayoutGiaTheoVung_E2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNhomHang.Text != "")
            {
                int ID = Int32.Parse(cmbNhomHang.Value.ToString());
                DataTable da = data.LayDanhSachHangHoa_IDNhomHang(ID,1);
                gridHangHoa.DataSource = da;
                gridHangHoa.DataBind();
            }
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

            }
        }
        protected void gridHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string IDHangHoa = e.Keys[0].ToString();
            string IDDonViTinh = e.NewValues["IDDonViTinh"].ToString();
            float GiaMoi = float.Parse(e.NewValues["DaXoa"].ToString());
            if (DanhSachKho.SelectedItems.Count != 0)
            {
                for (int i = DanhSachKho.Items.Count - 1; i >= 0; i--)
                {
                    if (DanhSachKho.Items[i].Selected == true)
                    {
                        string IDKho = DanhSachKho.Items[i].Value.ToString();
                        float GiaCu = dtCapNhatTonKho.GiaBan_KhoChiNhanh(IDHangHoa, IDKho);
                        
                        dtHangHoa.ThemLichSuThayDoiGia(IDHangHoa, IDDonViTinh, GiaCu, GiaMoi, Session["IDNhanVien"].ToString(), e.NewValues["MaHang"].ToString());
                        dtGiaTheoVung.CapNhat_GiaTheoVung(IDHangHoa, IDKho, GiaMoi);
                        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Giá Theo Vùng:" + IDHangHoa + ": " + GiaMoi, Session["IDKho"].ToString(), "Danh Mục", "Cập Nhât");
                    }
                }
                Session["updated"] = true;
                e.Cancel = true;
                gridHangHoa.CancelEdit();
                LoadGrid();
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
        
    }
}