using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DonHangHoanTatThuMua : System.Web.UI.Page
    {
        dtDonHangHoanTatThuMua data = new dtDonHangHoanTatThuMua();
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
                    string ngayBD = dateTuNgay.Date.ToString("yyyy-MM-dd");
                    string ngayKT = dateDenNgay.Date.ToString("yyyy-MM-dd");
                    ngayBD = ngayBD + " 00:00:0.000";
                    ngayKT = ngayKT + " 23:59:59.999";
                    LoadGrid(cmbHienThi.Value.ToString(), ngayBD, ngayKT);
                }
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 64) == false)
                {
                    btnDuyetDonHang.Enabled = false;
                    btncapnhatdonhang.Enabled = false;
                }
                int IDKho = Int32.Parse(Session["IDKho"].ToString());
                if (IDKho != 1)
                {
                    // kiểm tra lại. thu mua k xem chứng từ của kho
                    gridDonDatHang.Columns["ChungTu"].Visible = false;
                    btnDuyetDonHang.Visible = false;
                }
                else
                {
                    gridDonDatHang.Columns["ChungTu"].Visible = true;
                    btnDuyetDonHang.Visible = true;
                }
                //LoadGrid();
            }
        }
        private void LoadGrid(string HienThi, string TuNgay, string DenNgay)
        {
            data = new dtDonHangHoanTatThuMua();
            gridDonDatHang.DataSource = data.LayDanhSachDonHangDuyet();
            gridDonDatHang.DataBind();
        }

        protected void btnLoc_Click(object sender, EventArgs e)
        {

        }

        protected void cmbHienThi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}