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
                if (dtSetting.LayTrangThaiMenu_ChucNang(Session["IDNhom"].ToString(), 18) == 1)
                    btnThemPhieuChuyenKho.Enabled = false;
                if (dtSetting.LayTrangThaiMenu(Session["IDNhom"].ToString(), 18) == 1)
                {
                    LoadGrid();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }

            if (Session["IDKho"].ToString().CompareTo("1") != 0)
            {
                btnThemPhieuChuyenKho.Visible = false;
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
    }
}