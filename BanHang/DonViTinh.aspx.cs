using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DonViTinh : System.Web.UI.Page
    {
        dtDonViTinh data = new dtDonViTinh();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadGrid();
        }
        public void LoadGrid()
        {
            gridDonViTinh.DataSource = data.DanhSachDonViTinh();
            gridDonViTinh.DataBind();
        }

        protected void gridDonViTinh_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            data = new dtDonViTinh();
            string TenDonViTinh = e.NewValues["TenDonViTinh"].ToString();
            string MoTa = e.NewValues["MoTa"] == null ? "" : e.NewValues["MoTa"].ToString();
            if (dtSetting.kiemTraChuyenDoiDau() == 1)
                TenDonViTinh = dtSetting.convertDauSangKhongDau(TenDonViTinh).ToUpper();

            if (dtDonViTinh.KiemTraTen(TenDonViTinh) == 1)
            {
                data.ThemDonViTinh(TenDonViTinh, MoTa);
                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Đơn vị tính:" + TenDonViTinh, Session["IDKho"].ToString(), "Danh Mục", "Thêm"); 
                e.Cancel = true;
                gridDonViTinh.CancelEdit();
                LoadGrid();
            }
            else
            {
                throw new Exception("Lỗi: Tên đơn vị tính đã tồn tại: " + TenDonViTinh);
            }
        }

        protected void gridDonViTinh_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtDonViTinh();
            data.XoaDonViTinh(Int32.Parse(ID));
            e.Cancel = true;
            gridDonViTinh.CancelEdit();
            LoadGrid();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Đơn vị tính:" + ID, Session["IDKho"].ToString(), "Danh Mục", "Xóa"); 
        }

        protected void gridDonViTinh_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys["ID"].ToString());
            string TenDonViTinh = e.NewValues["TenDonViTinh"].ToString();
            string MoTa = e.NewValues["MoTa"] == null ? "" : e.NewValues["MoTa"].ToString();
            if (dtSetting.kiemTraChuyenDoiDau() == 1)
                TenDonViTinh = dtSetting.convertDauSangKhongDau(TenDonViTinh).ToUpper();

            data.SuaThongTinDonViTinh(ID, TenDonViTinh, MoTa);
            e.Cancel = true;
            gridDonViTinh.CancelEdit();
            LoadGrid();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Đơn vị tính:" + TenDonViTinh, Session["IDKho"].ToString(), "Danh Mục", "Cập Nhật"); 
        }
    }
}