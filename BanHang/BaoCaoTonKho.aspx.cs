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
    public partial class BaoCaoTonKho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string IDNhanVien = "1"; // Session["IDThuNgan"].ToString();
                if (Session["IDThuNgan"] != null)
                    IDNhanVien = Session["IDThuNgan"].ToString();
                if (Session["IDNhanVien"] != null)
                    IDNhanVien = Session["IDNhanVien"].ToString();

                dtKho dt = new dtKho();
                DataTable da = dt.LayDanhSachKho();
                da.Rows.Add(-1, "", "Tất cả cửa hàng", null, null, null, null, null, null, null, null, null);

                cmbKho.DataSource = da;
                cmbKho.TextField = "TenCuaHang";
                cmbKho.ValueField = "ID";
                cmbKho.DataBind();
                cmbKho.SelectedIndex = da.Rows.Count;

                dataNganhHang dtNH = new dataNganhHang();
                DataTable daNH = dtNH.getDanhSachNganhHang();
                daNH.Rows.Add(-1, null, "Tất cả ngành hàng", null, null, null);
                cmbNganhHang.DataSource = daNH;
                cmbNganhHang.TextField = "TenNganhHang";
                cmbNganhHang.ValueField = "ID";
                cmbNganhHang.DataBind();
                cmbNganhHang.SelectedIndex = daNH.Rows.Count;

                dataNhomHang dtNhomH = new dataNhomHang();
                DataTable daNhomH = dtNhomH.getDanhSachNhomHang();
                daNhomH.Rows.Add(-1, null, "Tất cả ngành hàng", null, null, null);

            }
        }

        protected void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            string IDKho = cmbKho.Value + "";

            //popup.ContentUrl = "~/BaoCaoChuyenKho_In.aspx?ngayBD=" + ngayBD + "&ngayKT=" + ngayKT + "&IDKho=" + IDKho;
            //popup.ShowOnPageLoad = true;
        }

        protected void cmbNganhHang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cmbNhomHang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}