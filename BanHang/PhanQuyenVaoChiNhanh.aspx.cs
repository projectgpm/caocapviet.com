using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class PhanQuyenVaoChiNhanh : System.Web.UI.Page
    {
        dtPhanQuyenNguoiDung data = new dtPhanQuyenNguoiDung();
        protected void Page_Load(object sender, EventArgs e)
        {
            string IDNguoiDung = Request.QueryString["IDNguoiDung"];
            if (Session["KTDangNhap"] != "GPM" && IDNguoiDung == null)
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 86) == false)
                {
                    gridDanhSachQuyen.Columns["chucnang"].Visible = false;
                }
                LoadGrid(IDNguoiDung);
            }
             
        }
        public void LoadGrid(string IDNguoiDung)
        {
            data = new dtPhanQuyenNguoiDung();
            gridDanhSachQuyen.DataSource = data.DanhSachQuyen(IDNguoiDung);
            gridDanhSachQuyen.DataBind();
        }
        protected void gridDanhSachQuyen_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string IDKho = e.NewValues["IDKho"].ToString();
            string IDNguoiDung = Request.QueryString["IDNguoiDung"];
            data = new dtPhanQuyenNguoiDung();
            if (dtPhanQuyenNguoiDung.KiemTraTonTai_ID(IDNguoiDung, IDKho, ID) == true)
            {
                data.SuaQuyen(ID, IDKho);
            }
            else
            {
                if (dtPhanQuyenNguoiDung.KiemTraTonTai(IDNguoiDung, IDKho) == false)
                {
                    data.ThemQuyen(IDNguoiDung, IDKho);// kiểm tra tồn tại
                    e.Cancel = true;
                    gridDanhSachQuyen.CancelEdit();
                    LoadGrid(IDNguoiDung);
                }
                else
                {
                    throw new Exception("Lỗi: Quyên truy cập đã tồn tại.");
                }
            }
            e.Cancel = true;
            gridDanhSachQuyen.CancelEdit();
            LoadGrid(IDNguoiDung);
        }

        protected void gridDanhSachQuyen_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string IDKho = e.NewValues["IDKho"].ToString();
            string IDNguoiDung = Request.QueryString["IDNguoiDung"];
            data = new dtPhanQuyenNguoiDung();
            if (dtPhanQuyenNguoiDung.KiemTraTonTai(IDNguoiDung, IDKho) == false)
            {
                data.ThemQuyen(IDNguoiDung, IDKho);// kiểm tra tồn tại
                e.Cancel = true;
                gridDanhSachQuyen.CancelEdit();
                LoadGrid(IDNguoiDung);
            }
            else
            {
                throw new Exception("Lỗi: Quyên truy cập đã tồn tại.");
            }
        }

        protected void gridDanhSachQuyen_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string IDNguoiDung = Request.QueryString["IDNguoiDung"];
            data = new dtPhanQuyenNguoiDung();
            data.XoaQuyen(ID);
            e.Cancel = true;
            gridDanhSachQuyen.CancelEdit();
            LoadGrid(IDNguoiDung);
        }

       
    }
}