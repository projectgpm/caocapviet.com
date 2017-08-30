using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachKhachHangTraHang : System.Web.UI.Page
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
                    btnThemPhieuTraHang.Enabled = false;
                if (dtSetting.LayTrangThaiMenu(Session["IDNhom"].ToString(), 18) == 1)
                {
                    LoadGrid();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void LoadGrid()
        {
            dtPhieuKhachHangTraHang data = new dtPhieuKhachHangTraHang();
            gridPhieuKhachHangTraHang.DataSource = data.DanhSachPhieuKhachHangTraHang(Session["IDKho"].ToString());
            gridPhieuKhachHangTraHang.DataBind();
        }

    }
}