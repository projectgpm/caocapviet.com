using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ChiNhanhDatHang : System.Web.UI.Page
    {
        dtDonHangChiNhanh data = new dtDonHangChiNhanh();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }

            else
            {
                LoadGrid(Session["IDKho"].ToString());
            }
        }

        private void LoadGrid(string p)
        {
            data = new dtDonHangChiNhanh();
            gridDonDatHang.DataSource = data.LayDanhSachDonHang(p);
            gridDonDatHang.DataBind();
        }
    }
}