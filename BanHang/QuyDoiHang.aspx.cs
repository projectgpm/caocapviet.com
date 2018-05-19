using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class QuyDoiHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Loading();
        }

        public void Loading()
        {
            gridDanhSach.DataSource = dtHangHoa.DanhSachHangHoaCoQuyDoi();
            gridDanhSach.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbHH1.Value == null || cmbHH2.Value == null) return;

            int iD1 = Int32.Parse(cmbHH1.Value + "");
            int iD2 = Int32.Parse(cmbHH2.Value + "");

            cmbHH1.SelectedIndex = -1;
            cmbHH2.SelectedIndex = -1;

            dtHangHoa.UpdateHangQuyDoi(iD1, iD2);

            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cập nhật thành công!.');", true);
            Loading();
        }
    }
}