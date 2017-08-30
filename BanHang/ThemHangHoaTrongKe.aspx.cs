﻿using BanHang.Data;
using DevExpress.Web;
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
    public partial class ThemHangHoaTrongKe : System.Web.UI.Page
    {
        dtKe data = new dtKe();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                txtNhanVien.Text = Session["TenDangNhap"].ToString();
            }
        }

        protected void cmbHangHoa_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaBanSauThue, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh
                                        WHERE (GPM_HangHoa.ID = @ID) AND  (GPM_HangHoa.IDTrangThaiHang = 1 OR GPM_HangHoa.IDTrangThaiHang = 3 OR GPM_HangHoa.IDTrangThaiHang = 6)";
            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void cmbHangHoa_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;

            dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaBanSauThue] , [TenDonViTinh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa,GPM_HangHoa.GiaBanSauThue, GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                              
	                                        WHERE ((GPM_HangHoa.TenHangHoa LIKE @TenHang) OR (GPM_HangHoa.MaHang LIKE @MaHang)) AND (GPM_HangHoa.DaXoa = 0) AND  (GPM_HangHoa.IDTrangThaiHang = 1 OR GPM_HangHoa.IDTrangThaiHang = 3 OR GPM_HangHoa.IDTrangThaiHang = 6)
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";

            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("MaHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
            dsHangHoa.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            dsHangHoa.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void btnThem_Temp_Click(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "" && UploadFileExcel.FileName.ToString() != "")
            {
                 Response.Write("<script language='JavaScript'> alert('Vui lòng chỉ chọn 1 hình thức thêm hàng hóa.'); </script>");
                 return;
            }
            else if (UploadFileExcel.FileName.ToString() != "")
            {
                Import();
            }
            else if (cmbHangHoa.Text != "")
            {
                string IDHangHoa = cmbHangHoa.Value.ToString();
                string IDKe = cmbKe.Value.ToString();
                string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                string MaHang = dtHangHoa.LayMaHang(IDHangHoa);
                data = new dtKe();
                DataTable db = dtKe.KTHangTrongKe_Temp(IDHangHoa, IDKe);
                if (db.Rows.Count == 0)
                {
                    data.ThemHangVaoKe_Temp(IDHangHoa, IDKe, IDDonViTinh, MaHang);
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Hàng hóa đã tồn tại .'); </script>");
                }
                cmbHangHoa.Text = "";
                LoadGrid(IDKe);
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn hàng hóa.'); </script>");
                return;
            }
        }

        private void LoadGrid(string IDKe)
        {
            data = new dtKe();
            gridDanhSachHangHoa.DataSource = data.DanhSachKe_Temp(IDKe);
            gridDanhSachHangHoa.DataBind();
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
            int r = dataTable.Rows.Count;
            Import_Temp(dataTable);
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
        private void Import_Temp(DataTable datatable)
        {
            int intRow = datatable.Rows.Count;
            if (datatable.Columns.Contains("MaHang") && datatable.Columns.Contains("TenHangHoa"))
            {
                if (intRow != 0)
                {
                    for (int i = 0; i <= intRow - 1; i++)
                    {
                        DataRow dr = datatable.Rows[i];
                        string MaHang = dr["MaHang"].ToString().Trim();
                        string TenHangHoa = dr["TenHangHoa"].ToString();
                        string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(MaHang.Trim());
                        string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                        string IDKe = cmbKe.Value.ToString();
                        DataTable db = dtKe.KTHangTrongKe_Temp(IDHangHoa, IDKe);
                        if (db.Rows.Count == 0)
                        {
                            data = new dtKe();
                            data.ThemHangVaoKe_Temp(IDHangHoa, IDKe, IDDonViTinh, MaHang);
                            cmbHangHoa.Text = "";
                        }
                    }
                    LoadGrid(cmbKe.Value.ToString());
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Dữ liệu không chính xác? Vui lòng kiểm tra lại.'); </script>");
            }

        }
        public string strFileExcel { get; set; }

        protected void cmbKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKe.Text != "")
            {
                cmbHangHoa.Enabled = true;
                btnThem_Temp.Enabled = true;
                UploadFileExcel.Enabled = true;
            }
        }

        protected void gridDanhSachHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtKe();
            data.XoaKe_Temp(ID);
            e.Cancel = true;
            gridDanhSachHangHoa.CancelEdit();
            LoadGrid(cmbKe.Value.ToString());
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            if (cmbKe.Text != "")
            {
                data = new dtKe();
                DataTable db = data.DanhSachKe_Temp(cmbKe.Value.ToString());
                if (db.Rows.Count > 0)
                {
                    foreach (DataRow dr in db.Rows)
                    {
                        string MaHang = dr["MaHang"].ToString();
                        string IDHangHoa = dr["IDHangHoa"].ToString();
                        string IDDonViTinh = dr["IDonViTinh"].ToString();
                        DataTable dt = dtKe.KTHangTrongKe(IDHangHoa, cmbKe.Value.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            data = new dtKe();
                            data.ThemHangVaoKe(IDHangHoa, cmbKe.Value.ToString(), IDDonViTinh, MaHang, Session["IDNhanVien"].ToString());
                        }
                    }
                    data = new dtKe();
                    data.XoaKe_IDke_Temp(cmbKe.Value.ToString());
                    LoadGrid(cmbKe.Value.ToString());

                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Danh sách kệ", Session["IDKho"].ToString(), "Danh mục", "Thêm");
                    Response.Redirect("DanhSachKe.aspx");
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa trống.'); </script>");
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn kệ.'); </script>");
            }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            Response.Redirect("DanhSachKe.aspx");
        }
    }
}