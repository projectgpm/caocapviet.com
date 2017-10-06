using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachKiemKho : System.Web.UI.Page
    {
        dtKiemKho data = new dtKiemKho();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayTrangThaiMenu_ChucNang(Session["IDNhom"].ToString(), 21) == 1)
                    btnKiemKho.Enabled = false;
                if (dtSetting.LayTrangThaiMenu(Session["IDNhom"].ToString(), 21) == 1)
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
            data = new dtKiemKho();
            gridDanhSachKiemKho.DataSource = data.DanhSachKiemKho(Session["IDKho"].ToString());
            gridDanhSachKiemKho.DataBind();
        }
    }
}