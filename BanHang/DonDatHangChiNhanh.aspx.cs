using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DonDatHangChiNhanh : System.Web.UI.Page
    {
        dtDuyetDonHangChiNhanh data = new dtDuyetDonHangChiNhanh();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                LoadGrid();
            }
        }

        private void LoadGrid()
        {
            data = new dtDuyetDonHangChiNhanh();
            gridDonDatHang.DataSource = data.LayDanhSachDonHang();
            gridDonDatHang.DataBind();
        }
    }
}