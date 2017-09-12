using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachPhieuChuyenKho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                //if (dtSetting.LayTrangThaiMenu(Session["IDNhom"].ToString(), 18) == 1)
                //{
                    LoadGrid();
                //}
                //else
                //{
                //    Response.Redirect("Default.aspx");
                //}
            }

        }

        private void LoadGrid()
        {
            dtPhieuChuyenKho data = new dtPhieuChuyenKho();
            if (Session["IDKho"].ToString().CompareTo("1") == 0)
                gridPhieuChuyenKho.DataSource = data.DanhSachPhieuChuyenKho_Tong();
            else gridPhieuChuyenKho.DataSource = data.DanhSachPhieuChuyenKho_Client(Session["IDKho"].ToString());
            gridPhieuChuyenKho.DataBind();
        }

        protected void gridPhieuChuyenKho_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys[0].ToString());
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
            dt.XoaPhieuChuyenKho_Update(ID + "");

            e.Cancel = true;
            gridPhieuChuyenKho.CancelEdit();
            LoadGrid();
        }
    }
}