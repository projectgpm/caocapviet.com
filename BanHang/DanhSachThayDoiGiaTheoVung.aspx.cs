using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                if (dtSetting.LayChucNang_HienThi(Session["IDNhom"].ToString()) == true)
                {
                    Loadgrid(cmbHienThi.Value.ToString());
                    if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                        btnThemThayDoiGia.Enabled = false;
                }
                else
                    Response.Redirect("Default.aspx");
               
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

        protected void gridHangHoa_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            double GiaMuaSauThue = Convert.ToDouble(e.GetValue("GiaMuaSauThue"));
            double GiaBan = Convert.ToDouble(e.GetValue("GiaBan"));
            double GiaBan1 = Convert.ToDouble(e.GetValue("GiaBan1"));
            double GiaBan2 = Convert.ToDouble(e.GetValue("GiaBan2"));
            double GiaBan3 = Convert.ToDouble(e.GetValue("GiaBan3"));
            double GiaBan4 = Convert.ToDouble(e.GetValue("GiaBan4"));
            double GiaBan5 = Convert.ToDouble(e.GetValue("GiaBan5"));
            if (GiaMuaSauThue > GiaBan || GiaMuaSauThue > GiaBan1 || GiaMuaSauThue > GiaBan2 || GiaMuaSauThue > GiaBan3 || GiaMuaSauThue > GiaBan4 || GiaMuaSauThue > GiaBan5)
                e.Row.BackColor = color;
        }
    }
}