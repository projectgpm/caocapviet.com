using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class PhanQuyen : System.Web.UI.Page
    {
        dtPhanQuyen data = new dtPhanQuyen();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                string IDNhomNguoiDung = Request.QueryString["IDNhomNguoiDung"];
                if (IDNhomNguoiDung != null)
                {
                    if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 85) == false)
                    {
                        gridPhanQuyen.Columns["chucnang"].Visible = false;
                    }
                    LoadGrid(IDNhomNguoiDung.ToString());
                }
            }
        }

        private void LoadGrid(string IDNhomNguoiDung)
        {
            data = new dtPhanQuyen();
            gridPhanQuyen.DataSource = data.LayDanhSachMenu(IDNhomNguoiDung);
            gridPhanQuyen.DataBind();
        }

        protected void gridPhanQuyen_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys[0].ToString());
            int TrangThai = Int32.Parse(e.NewValues["TrangThai"] == null ? "0" : e.NewValues["TrangThai"].ToString());
            int ChucNang = Int32.Parse(e.NewValues["ChucNang"] == null ? "0" : e.NewValues["ChucNang"].ToString());
            data = new dtPhanQuyen();
            data.CapNhatQuyen(Int32.Parse(Request.QueryString["IDNhomNguoiDung"]), ID, TrangThai, ChucNang);
            e.Cancel = true;
            gridPhanQuyen.CancelEdit();
            LoadGrid(Request.QueryString["IDNhomNguoiDung"]);
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Phân Quyền:" + ID, Session["IDKho"].ToString(), "Hệ Thống", "Cập Nhật"); 
        }

        protected void gridPhanQuyen_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            string TrangThai = Convert.ToString(e.GetValue("Link"));
            if (TrangThai == "")
                e.Row.BackColor = color;
        }
        protected void gridPhanQuyen_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string IDNhomNguoiDung = Request.QueryString["IDNhomNguoiDung"];
            string TrangThai = e.NewValues["TrangThai"] == null ? "0" : e.NewValues["TrangThai"].ToString();
            string ChucNang = e.NewValues["ChucNang"] == null ? "0" : e.NewValues["ChucNang"].ToString();
            string IDMenu = e.NewValues["IDMenu"].ToString();
            data = new dtPhanQuyen();
            if (dtPhanQuyen.KiemTraTonTai(IDNhomNguoiDung, IDMenu) == false)
            {
                data.ThemQuyen(IDNhomNguoiDung, IDMenu, TrangThai, ChucNang);
                e.Cancel = true;
                gridPhanQuyen.CancelEdit();
                LoadGrid(IDNhomNguoiDung);
                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Phân Quyền ", Session["IDKho"].ToString(), "Hệ Thống", "Thêm quyền truy cập");
            }
            else
            {
                throw new Exception("Lỗi: Quyền này đã tồn tại !!!");
            }
        }

        protected void gridPhanQuyen_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string IDNhomNguoiDung = Request.QueryString["IDNhomNguoiDung"];
            string ID = e.Keys[0].ToString();
            data = new dtPhanQuyen();
            data.XoaQuyen(ID);
            e.Cancel = true;
            gridPhanQuyen.CancelEdit();
            LoadGrid(IDNhomNguoiDung);
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Phân Quyền ", Session["IDKho"].ToString(), "Hệ Thống", "Xóa quyền truy cập");
        }

       
    }
}