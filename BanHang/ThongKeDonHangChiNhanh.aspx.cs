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
    public partial class ThongKeDonHangChiNhanh : System.Web.UI.Page
    {
        dtThongKeDonHangChiNhanh data = new dtThongKeDonHangChiNhanh();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                LoadGrid(cmbHienThi.Value.ToString());
            }
        }

        private void LoadGrid(string HienThi)
        {
            data = new dtThongKeDonHangChiNhanh();
            gridDanhSach.DataSource = data.DanhSachThongKe(Session["IDKho"].ToString(), HienThi);
            gridDanhSach.DataBind();
        }

        protected void btnXuatPDF_Click(object sender, EventArgs e)
        {
            XuatDuLieu.WritePdfToResponse();
        }

        protected void btnXuatExcel_Click(object sender, EventArgs e)
        {
            XuatDuLieu.WriteXlsToResponse();
        }

        protected void gridDanhSach_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
              Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int TonKhoTong = Convert.ToInt32(e.GetValue("TonKho"));// lấy giá trị
            int SoLuongDat = Convert.ToInt32(e.GetValue("SoLuongDat"));
            if (TonKhoTong < SoLuongDat)
                e.Row.BackColor = color;
        }

        protected void cmbHienThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid(cmbHienThi.Value.ToString());
        }

      

       
    }
}