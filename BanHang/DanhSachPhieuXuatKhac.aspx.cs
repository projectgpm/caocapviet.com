using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachPhieuXuatKhac : System.Web.UI.Page
    {
        dtPhieuXuatKhac data = new dtPhieuXuatKhac();
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
                        LoadGrid(cmbHienThi.Value.ToString());
                    }
                    //if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                    //    btnThemPhieuXuatKhac.Enabled = false;
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }

        private void LoadGrid(string HienThi)
        {
            data = new dtPhieuXuatKhac();
            string ngayBD = dateTuNgay.Date.ToString("yyyy-MM-dd");
            string ngayKT = dateDenNgay.Date.ToString("yyyy-MM-dd");
            ngayBD = ngayBD + " 00:00:0.000";
            ngayKT = ngayKT + " 23:59:59.999";
            gridPhieuXuatKhac.DataSource = data.DanhSachPhieuXuatKhac(HienThi,ngayBD,ngayKT);
            gridPhieuXuatKhac.DataBind();
        }

        protected void cmbHienThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid(cmbHienThi.Value.ToString());
        }

        protected void btnLoc_Click(object sender, EventArgs e)
        {
            LoadGrid(cmbHienThi.Value.ToString());
        }
    }
}