using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class CauHinhHanMuc : System.Web.UI.Page
    {
        dtCauHinhHanMuc data = new dtCauHinhHanMuc();
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
                    LoadGrid();
                    if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                        gridCauHinhHanMuc.Columns["chucnang"].Visible = false;
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }

        private void LoadGrid()
        {
            data = new dtCauHinhHanMuc();
            gridCauHinhHanMuc.DataSource = data.DanhSach();
            gridCauHinhHanMuc.DataBind();
        }

        protected void gridCauHinhHanMuc_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            data = new dtCauHinhHanMuc();
            string GiaGiamDoc = e.NewValues["GiaGiamDoc"].ToString();
            string GiaKho = e.NewValues["GiaKho"].ToString();
            string GiaGiamSat = e.NewValues["GiaGiamSat"].ToString();
            data.CapNhat(GiaGiamDoc, GiaGiamSat, GiaKho);
            e.Cancel = true;
            gridCauHinhHanMuc.CancelEdit();
            LoadGrid();
        }
    }
}