using BanHang.Data;
using BanHang.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class BangKePhieuXuatKhac_In : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string NgayBD = Request.QueryString["ngayBD"];
            string NgayKT = Request.QueryString["NgayKT"];
            string IDKhoXuat = Request.QueryString["IDKhoXuat"];
            string IDLyDoXuat = Request.QueryString["IDLyDoXuat"];
            string strKhoXuat = "Tất cả các kho";
            string strLyDoXuat = "Tất cả";

            dtKho dt = new dtKho();
            if (Int32.Parse(IDKhoXuat) != -1)
                strKhoXuat = dt.LayTenKho_ID(IDKhoXuat);

            dtPhieuXuatKhac dt1 = new dtPhieuXuatKhac();
            if (Int32.Parse(IDLyDoXuat) != -1)
                strLyDoXuat = dt1.LayTenLyDo_ID(IDLyDoXuat);

            string strNgay = DateTime.Parse(NgayBD).ToString("dd-MM-yyyy") + " - " + DateTime.Parse(NgayKT).ToString("dd-MM-yyyy");
            rpBangKePhieuXuatKhac rp = new rpBangKePhieuXuatKhac();
            rp.Parameters["strNgay"].Value = strNgay;
            rp.Parameters["strNgay"].Visible = false;
            rp.Parameters["strKhoXuat"].Value = strKhoXuat;
            rp.Parameters["strKhoXuat"].Visible = false;
            rp.Parameters["strLyDoXuat"].Value = strLyDoXuat;
            rp.Parameters["strLyDoXuat"].Visible = false;

            rp.Parameters["NgayBD"].Value = NgayBD;
            rp.Parameters["NgayBD"].Visible = false;
            rp.Parameters["NgayKT"].Value = NgayKT;
            rp.Parameters["NgayKT"].Visible = false;
            rp.Parameters["IDKhoXuat"].Value = IDKhoXuat;
            rp.Parameters["IDKhoXuat"].Visible = false;
            rp.Parameters["IDLyDoXuat"].Value = IDLyDoXuat;
            rp.Parameters["IDLyDoXuat"].Visible = false;
            viewerReport.Report = rp;
        }
    }
}