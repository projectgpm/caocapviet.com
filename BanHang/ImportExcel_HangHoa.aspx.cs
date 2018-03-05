using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ImportExcel_HangHoa : System.Web.UI.Page
    {
        private string strFileExcel = "";
        dtImportHangHoa data = new dtImportHangHoa();
        protected void Page_Load(object sender, EventArgs e)
        {
            
                //if (Session["KTDangNhap"] == "GPM")
                //{
                //    if (dtSetting.LayTrangThaiMenu_ChucNang(Session["IDNhom"].ToString(), 7) != 1)
                //    {
                        //if (!IsPostBack)
                        //{
                        //    // xóa dữ liệu bảng temp
                        //    data = new dtImportHangHoa();
                        //    data.XoaDuLieuTemp();

                        //}
                        //LoadGrid();
                //    }
                //    else
                //    {
                //        Response.Redirect("Default.aspx");
                //    }
                //}
                //else
                //{
                //    Response.Redirect("DangNhap.aspx");
                //}
           

        }

        private void LoadGrid()
        {
            data = new dtImportHangHoa();
            gridHangHoa_Temp.DataSource = data.DanhSachHangHoa_Import_Temp();
            gridHangHoa_Temp.DataBind();
        }

        protected void gridHangHoa_Temp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys[0].ToString());
            data = new dtImportHangHoa();
            data.XoaDuLieuTemp_ID(ID);
            e.Cancel = true;
            gridHangHoa_Temp.CancelEdit();
            LoadGrid();
        }

        protected void btnNhap_Click(object sender, EventArgs e)
        {
            Import();
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            
            
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            data.XoaDuLieuTemp();
            //Response.Redirect("HangHoa.aspx");
        }
        private void Import()
        {
            if (string.IsNullOrEmpty(UploadFileExcel.FileName))
            {
                Response.Write("<script language='JavaScript'> alert('Chưa chọn file.'); </script>");
                return;
            }

            UploadFile();
            string Excel = Server.MapPath("~/Uploads/") + strFileExcel;

            string excelConnectionString = string.Empty;
            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Excel + ";Extended Properties=Excel 8.0;";

            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
            OleDbCommand cmd = new OleDbCommand("Select * from [Sheet$]", excelConnection);
            excelConnection.Open();
            OleDbDataReader dReader = default(OleDbDataReader);
            dReader = cmd.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(dReader);
            Import_Temp(dataTable);

        }

        private void Import_Temp(DataTable dataTable)
        {
            int intRow = dataTable.Rows.Count;
            if (intRow != 0)
            {
                for (int i = 1400; i <= intRow - 1; i++)
                {
                    try
                    {
                        DataRow dr = dataTable.Rows[i];
                        dataNhomHang d = new dataNhomHang();
                        string maHang = dr["MaHang"].ToString();
                        int IDHangHoa = Int32.Parse(maHang);
                        int SoLuong1 = Int32.Parse(dr["KHO 01"].ToString());
                        int SoLuong2 = Int32.Parse(dr["AN THOI 24"].ToString());
                        int SoLuong3 = Int32.Parse(dr["CAU DINH 43"].ToString());
                        int SoLuong4 = Int32.Parse(dr["HUNG VUONG 35"].ToString());
                        int SoLuong5 = Int32.Parse(dr["MY QUI 11"].ToString());
                        int SoLuong6 = Int32.Parse(dr["MY XUYEN 20"].ToString());
                        int SoLuong7 = Int32.Parse(dr["TAN PHU DONG 42"].ToString());
                        int SoLuong8 = Int32.Parse(dr["THANH AN 18"].ToString());
                        int SoLuong9 = Int32.Parse(dr["TRAN QUANG DIEU 32"].ToString());

                        int IDKho1 = 1;
                        int IDKho2 = 43;
                        int IDKho3 = 62;
                        int IDKho4 = 53;
                        int IDKho5 = 22;
                        int IDKho6 = 38;
                        int IDKho7 = 61;
                        int IDKho8 = 35;
                        int IDKho9 = 51;
                        d.updateHangHoa(IDHangHoa, SoLuong1, IDKho1);
                        d.updateHangHoa(IDHangHoa, SoLuong2, IDKho2);
                        d.updateHangHoa(IDHangHoa, SoLuong3, IDKho3);
                        d.updateHangHoa(IDHangHoa, SoLuong4, IDKho4);
                        d.updateHangHoa(IDHangHoa, SoLuong5, IDKho5);
                        d.updateHangHoa(IDHangHoa, SoLuong6, IDKho6);
                        d.updateHangHoa(IDHangHoa, SoLuong7, IDKho7);
                        d.updateHangHoa(IDHangHoa, SoLuong8, IDKho8);
                        d.updateHangHoa(IDHangHoa, SoLuong9, IDKho9);

                    }catch(Exception e){}
                    //data = new dtImportHangHoa();
                    //data.insertHangHoa_temp(IDNhomHang, MaHang, TenHangHoa, IDDonViTinh, HeSo, IDHangSanXuat, IDThue, HangQuyDoi, IDNhomDatHang, GiaMuaTruocThue, GiaBanTruocThue, GiaMuaSauThue, GiaBanSauThue, GiaBan1, GiaBan2, GiaBan3, GiaBan4, GiaBan5, TrongLuong, HanSuDung, IDTrangThaiHang, GhiChu, IDTrangThaiBarcode, Barcode);
                }
                //LoadGrid();
            }
        }

        private void UploadFile()
        {
            string folder = null;
            string filein = null;
            string ThangNam = null;

            ThangNam = string.Concat(System.DateTime.Now.Month.ToString(), System.DateTime.Now.Year.ToString());
            if (!Directory.Exists(Server.MapPath("~/Uploads/") + ThangNam))
            {
                Directory.CreateDirectory(Server.MapPath("~/Uploads/") + ThangNam);
            }
            folder = Server.MapPath("~/Uploads/" + ThangNam + "/");

            if (UploadFileExcel.HasFile)
            {
                strFileExcel = Guid.NewGuid().ToString();
                string theExtension = Path.GetExtension(UploadFileExcel.FileName);
                strFileExcel += theExtension;

                filein = folder + strFileExcel;
                UploadFileExcel.SaveAs(filein);
                strFileExcel = ThangNam + "/" + strFileExcel;
            }
        }

    }
}