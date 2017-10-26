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
    public partial class DuyetDonHangHanMuc : System.Web.UI.Page
    {
        dtDuyetDonHangHanMuc data = new dtDuyetDonHangHanMuc();
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
                    LoadGrid(cmbHienThi.Value.ToString());
                }
            }
        }

        private void LoadGrid(string HienThi)
        {
            data = new dtDuyetDonHangHanMuc();
            string ngayBD = dateTuNgay.Date.ToString("yyyy-MM-dd");
            string ngayKT = dateDenNgay.Date.ToString("yyyy-MM-dd");
            ngayBD = ngayBD + " 00:00:0.000";
            ngayKT = ngayKT + " 23:59:59.999";
            // lấy idnhom để biết gia tiền để hiển thị duyệt đơn hàng

            gridDonDatHang.DataSource = data.LayDanhSachDonHang(Session["IDNhom"].ToString(), HienThi, ngayBD, ngayKT, Session["IDNhanVien"].ToString(), Session["IDKho"].ToString());
            gridDonDatHang.DataBind();
        }
        protected void gridDonDatHang_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int MucDoUuTien = Convert.ToInt32(e.GetValue("MucDoUuTien"));// lấy giá trị
            int TrangThai = Convert.ToInt32(e.GetValue("TrangThai"));
            if (MucDoUuTien == 1 && TrangThai == 0)
                e.Row.BackColor = color;
        }
        protected void btnLoc_Click(object sender, EventArgs e)
        {
            LoadGrid(cmbHienThi.Value.ToString());
        }

        protected void cmbHienThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid(cmbHienThi.Value.ToString());
        }
    }
}