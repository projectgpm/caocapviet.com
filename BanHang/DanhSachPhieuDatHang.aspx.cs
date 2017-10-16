using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachPhieuDatHang : System.Web.UI.Page
    {
        dtDuyetDonHangThuMua data = new dtDuyetDonHangThuMua();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }

            else
            {
                //if (Int32.Parse(Session["IDKho"].ToString()) != 1)
                //{
                //    btnDuyetDonHang.Visible = false;
                //}
                if (dtSetting.LayChucNang_HienThi(Session["IDNhom"].ToString()) == true)
                {
                   // LoadGrid(cmbHienThi.Value.ToString());
                    if (!IsPostBack)
                    {
                        dateTuNgay.Date = DateTime.Today.AddDays(-30);
                        dateDenNgay.Date = DateTime.Today;
                    }
                    if (dateTuNgay.Text != "" || dateDenNgay.Text != "")
                    {
                        string ngayBD = dateTuNgay.Date.ToString("yyyy-MM-dd");
                        string ngayKT = dateDenNgay.Date.ToString("yyyy-MM-dd");
                        ngayBD = ngayBD + " 00:00:0.000";
                        ngayKT = ngayKT + " 23:59:59.999";
                        LoadGrid(cmbHienThi.Value.ToString(), ngayBD, ngayKT);
                    }
                    if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                    {
                        btnCapNhatDonHang.Enabled = false;
                        btnDuyetDonHang.Enabled = false;
                    }
                   
                    
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }

        private void LoadGrid(string HienThi, string TuNgay, string DenNgay)
        {
            data = new dtDuyetDonHangThuMua();
            string ngayBD = dateTuNgay.Date.ToString("yyyy-MM-dd");
            string ngayKT = dateDenNgay.Date.ToString("yyyy-MM-dd");
            ngayBD = ngayBD + " 00:00:0.000";
            ngayKT = ngayKT + " 23:59:59.999";
            gridDonDatHang.DataSource = data.DanhSachDuyet_ThuMua(HienThi, ngayBD, ngayKT);
            gridDonDatHang.DataBind();
        }

        protected void cmbHienThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ngayBD = dateTuNgay.Date.ToString("yyyy-MM-dd");
            string ngayKT = dateDenNgay.Date.ToString("yyyy-MM-dd");
            ngayBD = ngayBD + " 00:00:0.000";
            ngayKT = ngayKT + " 23:59:59.999";
            LoadGrid(cmbHienThi.Value.ToString(), ngayBD,ngayKT);
        }

        protected void btnLoc_Click(object sender, EventArgs e)
        {
            string ngayBD = dateTuNgay.Date.ToString("yyyy-MM-dd");
            string ngayKT = dateDenNgay.Date.ToString("yyyy-MM-dd");
            ngayBD = ngayBD + " 00:00:0.000";
            ngayKT = ngayKT + " 23:59:59.999";
            LoadGrid(cmbHienThi.Value.ToString(), ngayBD, ngayKT);
        }
    }
}