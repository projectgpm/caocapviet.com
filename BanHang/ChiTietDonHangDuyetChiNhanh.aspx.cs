using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ChiTietDonHangDuyetChiNhanh : System.Web.UI.Page
    {
        dtDuyetDonHangChiNhanh data = new dtDuyetDonHangChiNhanh();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
                if (IDDonHangChiNhanh != null)
                {
                    LoadGrid(IDDonHangChiNhanh.ToString());
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }

        private void LoadGrid(string IDDuyetHangChiNhanh)
        {
            data = new dtDuyetDonHangChiNhanh();
            gridChiTiet.DataSource = data.DanhSachChiTietDuyet(IDDuyetHangChiNhanh);
            gridChiTiet.DataBind();
        }
    }
}