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
                if (!IsPostBack)
                {
                    dateTuNgay.Date = DateTime.Today.AddDays(-30);
                    dateDenNgay.Date = DateTime.Today;
                }
                if (dateTuNgay.Text != "" || dateDenNgay.Text != "")
                {
                    LoadGrid(Session["IDKho"].ToString(),cmbHienThi.Value.ToString());
                }
             
            }
        }

        private void LoadGrid(string IDKho,string HienThi)
        {
            data = new dtDonHangChiNhanh();
            string ngayBD = dateTuNgay.Date.ToString("yyyy-MM-dd");
            string ngayKT = dateDenNgay.Date.ToString("yyyy-MM-dd");
            ngayBD = ngayBD + " 00:00:0.000";
            ngayKT = ngayKT + " 23:59:59.999";
            gridDonDatHang.DataSource = data.LayDanhSachDonHang(IDKho,HienThi,ngayBD,ngayKT);
            gridDonDatHang.DataBind();
        }

        protected void gridDonDatHang_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int MucDoUuTien = Convert.ToInt32(e.GetValue("MucDoUuTien"));// lấy giá trị
            int TrangThai = Convert.ToInt32(e.GetValue("TrangThai"));
            if (MucDoUuTien == 1 && TrangThai ==0)
                e.Row.BackColor = color;
        }

        protected void btnLoc_Click(object sender, EventArgs e)
        {
            LoadGrid(Session["IDKho"].ToString(), cmbHienThi.Value.ToString());
        }

        protected void cmbHienThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid(Session["IDKho"].ToString(), cmbHienThi.Value.ToString());
        }
    }
}