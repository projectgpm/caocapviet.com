using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachThayDoiGiaTheoVung : System.Web.UI.Page
    {
        dtGiaTheoVung data = new dtGiaTheoVung();
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
            data = new dtGiaTheoVung();
            gridHangHoa.DataSource = data.DanhSachDaThayDoiGiaThanhCong(HienThi);
            gridHangHoa.DataBind();
        }

        protected void cmbHienThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loadgrid(cmbHienThi.Value.ToString());
        }
    }
}