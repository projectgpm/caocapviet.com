using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                    // lấy trangthaiduyet
                    if (dtDuyetDonHangChiNhanh.LayTrangThai(IDDonHangChiNhanh) == 1)
                    {
                        btnChapNhanDonHang.Enabled = false;
                    }
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

        protected void gridChiTiet_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int TrangThai = Convert.ToInt32(e.GetValue("TrangThai"));
            if (TrangThai == 1)
                e.Row.BackColor = color;
        }

        protected void btnChapNhanDonHang_Click(object sender, EventArgs e)
        {

        }
    }
}