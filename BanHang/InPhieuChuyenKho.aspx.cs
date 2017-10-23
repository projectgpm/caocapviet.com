using BanHang.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class InPhieuChuyenKho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ID = Request.QueryString["ID"];

            rpXuatPhieuChuyenKho rp = new rpXuatPhieuChuyenKho();

            rp.Parameters["ID"].Value = ID;
            rp.Parameters["ID"].Visible = false;

            viewerReport.Report = rp;
        }
    }
}