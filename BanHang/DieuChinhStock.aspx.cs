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
    public partial class DieuChinhStock : System.Web.UI.Page
    {
        string strFileExcel;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
        }

        public void Load()
        {
            dataHangHoa dt = new dataHangHoa();
            gridHangHoa_Temp.DataSource = dt.getDanhSach_Upload_Stock();
            gridHangHoa_Temp.DataBind();
        }

        protected void btnNhap_Click(object sender, EventArgs e)
        {
            Import();
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            dataHangHoa dt = new dataHangHoa();
            DataTable data = dt.getDanhSach_Upload_Stock();
            string IDKho = cmbKhoHang.Value.ToString();
            for (int i = 0; i < data.Rows.Count; i++)
            {

                dt.update_Stock(data.Rows[i]["MaHang"].ToString(), Int32.Parse(data.Rows[i]["SoLuong"].ToString()), Int32.Parse(IDKho), float.Parse(data.Rows[i]["GiaBan"].ToString()));
            }
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa", Session["IDKho"].ToString(), "Điều chỉnh stock", "Điều chỉnh stock");
            Response.Redirect("TonKhoBanDau.aspx");
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            dataHangHoa dt = new dataHangHoa();
            dt.XoaUpload_Stock_All();
            Response.Redirect("TonKhoBanDau.aspx");
        }

        protected void gridHangHoa_Temp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            dataHangHoa dt = new dataHangHoa();
            dt.XoaUpload_Stock(ID);
            e.Cancel = true;
            gridHangHoa_Temp.CancelEdit();
            Load();
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
            dataHangHoa dt = new dataHangHoa();
            int intRow = dataTable.Rows.Count;
            if (intRow != 0)
            {
                for (int i = 0; i <= intRow - 1; i++)
                {
                    DataRow dr = dataTable.Rows[i];
                    dataNhomHang d = new dataNhomHang();
                    string MaHang = dr["Ma Hang"].ToString();
                    string TenHangHoa = dr["Ten Hang Hoa"].ToString();
                    int SoLuong = Int32.Parse(dr["So Luong"].ToString());
                    int giaban = Int32.Parse(dr["Gia Ban"].ToString());

                    dt.themHangHoa_Upload_Stock(MaHang, TenHangHoa, SoLuong, giaban);
                }
                Load();
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