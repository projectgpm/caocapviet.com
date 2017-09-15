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
    public partial class DanhSachPhieuXuatKhac : System.Web.UI.Page
    {
        dtPhieuXuatKhac data = new dtPhieuXuatKhac();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                //if (dtSetting.LayTrangThaiMenu_ChucNang(Session["IDNhom"].ToString(), 19) == 1)
                //    btnThemPhieuXuatKhac.Enabled = false;
                //if (dtSetting.LayTrangThaiMenu(Session["IDNhom"].ToString(), 19) == 1)
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
            data = new dtPhieuXuatKhac();
            gridPhieuXuatKhac.DataSource = data.DanhSachPhieuXuatKhac();
            gridPhieuXuatKhac.DataBind();
        }
    }
}