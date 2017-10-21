using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class LichSuTruyCap : System.Web.UI.Page
    {
        dtLichSuTruyCap data = new dtLichSuTruyCap();
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
                    if (!IsPostBack)
                    {
                        dateTuNgay.Date = DateTime.Today.AddDays(-30);
                        dateDenNgay.Date = DateTime.Today;
                    }
                    if (dateTuNgay.Text != "" || dateDenNgay.Text != "")
                    {
                        LoadGrid(Session["IDKho"].ToString(), cmbHienThi.Value.ToString());
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void LoadGrid(string IDKho, string HienThi)
        {
            data = new dtLichSuTruyCap();
            string ngayBD = dateTuNgay.Date.ToString("yyyy-MM-dd");
            string ngayKT = dateDenNgay.Date.ToString("yyyy-MM-dd");
            ngayBD = ngayBD + " 00:00:0.000";
            ngayKT = ngayKT + " 23:59:59.999";
            gridLichSuTruyCap.DataSource = data.LayDanhSach(IDKho,HienThi,ngayBD,ngayKT);
            gridLichSuTruyCap.DataBind();
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