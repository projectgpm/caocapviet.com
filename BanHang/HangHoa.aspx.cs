using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.IO;
using DevExpress.XtraRichEdit.API.Native;

namespace BanHang
{
    public partial class HangHoa : System.Web.UI.Page
    {
        //dataHangHoa data = new dataHangHoa();
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
                    string s = "TOP " + cmbSoLuongXem.Value + " ";
                    LoadGrid(s);

                    if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                    {
                        gridHangHoa.Columns["chucnang2"].Visible = false;
                        btnTheMoi.Enabled = false;
                        btnNhapExel.Enabled = false;
                    }
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }
        public void LoadGrid(string s)
        {
            dataHangHoa data = new dataHangHoa();
            gridHangHoa.DataSource = data.getDanhSachHangHoa(s);
            gridHangHoa.DataBind();
        }

        protected void gridHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            
        }

        protected void btnXuatExcel_Click(object sender, EventArgs e)
        {
            dataHangHoa da = new dataHangHoa();
            HangHoa1.DataSource = da.getDanhSachHangHoa_Export(Session["IDKho"].ToString());
            HangHoa1.DataBind();
            HangHoaExport.WriteXlsToResponse();

            //dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa", Session["IDKho"].ToString(), "Danh mục", "Xuất excel");
        }

        protected void btnNhapExcel_Click(object sender, EventArgs e)
        {
            Response.Redirect("QuyDoiHang.aspx");
        }

        protected void XuatFilePDF_Click(object sender, EventArgs e)
        {
            //dataHangHoa data = new dataHangHoa();
            //HangHoaExport1.DataSource = data.getDanhSachHangHoa_Full_Barcode();
            //HangHoaExport1.DataBind();
            //HangHoaExport.WritePdfToResponse();

            //dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa", Session["IDKho"].ToString(), "Danh mục", "Xuất pdf");
        }

        protected void gridHangHoa_RowDeleting1(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            dataHangHoa data = new dataHangHoa();
            int ID = Int32.Parse(e.Keys["ID"].ToString());
            data.XoaHangHoa(ID);
            e.Cancel = true;

            string s = "TOP " + cmbSoLuongXem.Value + " ";
            LoadGrid(s);

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa", Session["IDKho"].ToString(), "Danh mục", "Xóa");
        }

        protected void btnTheMoi_Click(object sender, EventArgs e)
        {
            dataHangHoa data = new dataHangHoa();
            Response.Redirect("HangHoa_Page.aspx");
        }

        protected void cmbSoLuongXem_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = "TOP " + cmbSoLuongXem.Value + " ";
            LoadGrid(s);
        }

    }
}