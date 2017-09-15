using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class TonKhoBanDau : System.Web.UI.Page
    {
        dtKhoHang data = new dtKhoHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                
                if (dtSetting.LayChucNang_HienThi(Session["IDNhom"].ToString()) == true)
                {
                    LoadGrid();
                    if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                    {
                        //gridTonKhoBanDau.Columns["chucnang"].Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
        private void LoadGrid()
        {
            data = new dtKhoHang();
            gridTonKhoBanDau.DataSource = data.LayDanhSachHangTrongKho(Session["IDKho"].ToString());
            gridTonKhoBanDau.DataBind();
        }
    }
}