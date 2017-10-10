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
    public partial class QLKho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
        private void LoadGrid()
        {
            DataTable da = new DataTable();
            da.Columns.Add("TongBanHang", typeof(float));
            da.Columns.Add("PhieuChuyenKho", typeof(int));
            da.Columns.Add("SoHangTraTrongNgay", typeof(int));
            da.Columns.Add("SoHangBanTrongNgay", typeof(int));

            DateTime date = DateTime.Now;
            string ngayBD = ""; string ngayKT = "";
            ngayBD = date.ToString("yyyy-MM-dd ");
            ngayKT = date.ToString("yyyy-MM-dd ");
            ngayBD = ngayBD + "00:00:0.000";
            ngayKT = ngayKT + "23:59:59.999";

            string IDKho = Session["IDKho"].ToString();

            float TongTienHD = dtQLKho.LayTongHoaDon(IDKho, ngayBD, ngayKT);
            int TongPhieuChuyenKho = dtQLKho.LayTongPhieuChuyenKho(IDKho);
            int TongSLHang = dtQLKho.LayTongSoLuongHangHoaDon(IDKho, ngayBD, ngayKT);
            int TongDLHangTra = dtQLKho.LayTongSoLuongHangDoiTra(IDKho, ngayBD, ngayKT);

            da.Rows.Add(TongTienHD, TongPhieuChuyenKho, TongDLHangTra, TongSLHang);

            gridDanhSach.DataSource = da;
            gridDanhSach.DataBind();
        }
    }
}