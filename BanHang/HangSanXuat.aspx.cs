using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class HangSanXuat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["KTDangNhap"] != "GPM")
            //{
            //    Response.Redirect("DangNhap.aspx");
            //}
            //else
            //{
            //    if (dtSetting.LayTrangThaiMenu_ChucNang(Session["IDNhom"].ToString(), 10) == 1)
            //        gridDanhSach.Columns["chucnang"].Visible = false;
            //    if (dtSetting.LayTrangThaiMenu(Session["IDNhom"].ToString(), 10) == 1)
            //    {
                    loadGrid();
                //}
                //else
                //{
                //    Response.Redirect("Default.aspx");
                //}
            //}
        }

        private void loadGrid()
        {
            dataHangSanXuat data = new dataHangSanXuat();
            gridDanhSach.DataSource = data.getDanhSachHangSX();
            gridDanhSach.DataBind();

        }

        protected void gridDanhSach_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            dataHangSanXuat data = new dataHangSanXuat();
            string TenHangSanXuat = e.NewValues["TenHangSanXuat"].ToString();
            if (dtSetting.kiemTraChuyenDoiDau() == 1)
                TenHangSanXuat = dtSetting.convertDauSangKhongDau(TenHangSanXuat).ToUpper();

            string SDT = e.NewValues["SDT"] == null ? "" : e.NewValues["SDT"].ToString();
            string DiaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            string Email = e.NewValues["Email"] == null ? "" : e.NewValues["Email"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            data.insertHangSX(TenHangSanXuat, SDT, DiaChi, Email, GhiChu);
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            loadGrid();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hãng SX", Session["IDKho"].ToString(), "Danh mục", "Thêm: " + TenHangSanXuat); 
        }

        protected void gridDanhSach_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            dataHangSanXuat data = new dataHangSanXuat();
            string ID = e.Keys[0].ToString();
            data.deleteHangSX(ID);
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            loadGrid();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hãng SX", Session["IDKho"].ToString(), "Danh mục", "Xóa: ID = " + ID); 
        }

        protected void gridDanhSach_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            dataHangSanXuat data = new dataHangSanXuat();
            string TenHangSanXuat = e.NewValues["TenHangSanXuat"].ToString();
            if (dtSetting.kiemTraChuyenDoiDau() == 1)
                TenHangSanXuat = dtSetting.convertDauSangKhongDau(TenHangSanXuat).ToUpper();

            string SDT = e.NewValues["SDT"] == null ? "" : e.NewValues["SDT"].ToString();
            string DiaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            string Email = e.NewValues["Email"] == null ? "" : e.NewValues["Email"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            data.updateHangSX(ID, TenHangSanXuat, SDT, DiaChi, Email, GhiChu);
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            loadGrid();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hãng SX", Session["IDKho"].ToString(), "Danh mục", "Cập nhật: " + TenHangSanXuat); 
        }
    }
}