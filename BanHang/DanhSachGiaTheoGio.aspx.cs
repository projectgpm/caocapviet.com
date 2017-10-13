using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachGiaTheoGio : System.Web.UI.Page
    {
        dtGiaTheoGio data = new dtGiaTheoGio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                Loadgrid(cmbHienThi.Value.ToString());
            }
        }

        private void Loadgrid(string HienThi)
        {
            data = new dtGiaTheoGio();
            gridHangHoa.DataSource = data.DanhSachHoanTat(HienThi);
            gridHangHoa.DataBind();
        }

        protected void cmbHienThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loadgrid(cmbHienThi.Value.ToString());
        }
    }
}