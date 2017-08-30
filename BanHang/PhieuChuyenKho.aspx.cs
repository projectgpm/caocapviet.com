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
    public partial class PhieuChuyenKho : System.Web.UI.Page
    {
        private string strFileExcel = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayTrangThaiMenu_ChucNang(Session["IDNhom"].ToString(), 19) == 1)
                    Response.Redirect("Default.aspx");
                if (dtSetting.LayTrangThaiMenu(Session["IDNhom"].ToString(), 19) == 1)
                {
                    if (!IsPostBack)
                    {
                        dtPhieuChuyenKho data = new dtPhieuChuyenKho();
                        object IDPhieuChuyenKho = data.ThemPhieuChuyenKho(Session["IDKho"].ToString());
                        IDPhieuChuyenKho_Temp.Value = IDPhieuChuyenKho.ToString();
                        cmbNguoiLapPhieu.Text = Session["IDNhanVien"].ToString();
                        cmbTrangThaiPhieu.SelectedIndex = 0;
                    }
                    LoadGrid(IDPhieuChuyenKho_Temp.Value.ToString());
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }

            if (Session["IDKho"].ToString().CompareTo("1") != 0)
            {
                Response.Redirect("DanhSachPhieuChuyenKho.aspx");
            }
        }

        private void LoadGrid(string IDPhieuChuyenKho)
        {
            dtPhieuChuyenKho data = new dtPhieuChuyenKho();
            gridDanhSachHangHoa_Temp.DataSource = data.DanhSachChiTietPhieuChuyenKho_Temp(IDPhieuChuyenKho);
            gridDanhSachHangHoa_Temp.DataBind();

        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
            if (cmbHangHoa.Value != null)
            {
                string IDHH = cmbHangHoa.Value + "";
                if (Int32.Parse(txtSoLuong.Value + "") <= Int32.Parse(txtTonKho.Value + ""))
                {
                    DataTable dx = dt.KiemTraHangHoa_Temp(IDPhieuChuyenKho_Temp.Value.ToString(), IDHH);
                    if (dx.Rows.Count == 0)
                    {
                        dt.ThemChiTietPhieuChuyenKho_Temp(IDPhieuChuyenKho_Temp.Value.ToString(), IDHH, txtSoLuong.Value + "", txtHHTrongLuong.Value + "", txtGiaBan.Value + "", txtTongTienHH.Value + "");
                        DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho_Temp.Value.ToString());
                        if (da.Rows.Count != 0)
                        {
                            txtTrongLuong.Value = float.Parse(da.Rows[0]["TongTrongLuong"].ToString());
                            txtTongTien.Value = float.Parse(da.Rows[0]["TongTien"].ToString());
                            txtSoMatHang.Value = Int32.Parse(da.Rows[0]["TongSoLuong"].ToString());
                        }
                    }
                    else
                    {
                        dt.CapNhatChiTietPhieuChuyenKho_Temp(IDPhieuChuyenKho_Temp.Value.ToString(), IDHH, (Int32.Parse(dx.Rows[0]["SoLuong"].ToString()) + Int32.Parse(txtSoLuong.Value + "")) + "", (float.Parse(dx.Rows[0]["TrongLuong"].ToString()) + float.Parse(txtHHTrongLuong.Value + "")) + "", txtGiaBan.Value + "", ((Int32.Parse(dx.Rows[0]["SoLuong"].ToString()) + Int32.Parse(txtSoLuong.Value + "")) * float.Parse(txtGiaBan.Value + "")) + "");
                        DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho_Temp.Value.ToString());
                        if (da.Rows.Count != 0)
                        {
                            txtTrongLuong.Value = float.Parse(da.Rows[0]["TongTrongLuong"].ToString());
                            txtTongTien.Value = float.Parse(da.Rows[0]["TongTien"].ToString());
                            txtSoMatHang.Value = Int32.Parse(da.Rows[0]["TongSoLuong"].ToString());
                        }
                    }
                    LoadGrid(IDPhieuChuyenKho_Temp.Value.ToString());
                }
                else
                {
                    txtSoLuong.Value = txtTonKho.Value;
                    Response.Write("<script language='JavaScript'> alert('Hàng hóa không đủ số lượng.'); </script>");
                }
            }
            else if (!string.IsNullOrEmpty(fileUpload.FileName))
            {
                Import();
            }
            else
                Response.Write("<script language='JavaScript'> alert('Chọn hàng hóa hoặc file Import.'); </script>");
        }

        protected void gridDanhSachHangHoa_Temp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys[0].ToString());
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
            dt.XoaChiTietPhieuChuyenKho_Temp(ID + "");
            e.Cancel = true;
            gridDanhSachHangHoa_Temp.CancelEdit();
            LoadGrid(IDPhieuChuyenKho_Temp.Value.ToString());

            DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho_Temp.Value.ToString());
            if (da.Rows.Count != 0)
            {
                txtTrongLuong.Value = float.Parse(da.Rows[0]["TongTrongLuong"].ToString());
                txtTongTien.Value = float.Parse(da.Rows[0]["TongTien"].ToString());
                txtSoMatHang.Value = Int32.Parse(da.Rows[0]["TongSoLuong"].ToString());
            }
        }

        protected void cmbHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string IDHH = cmbHangHoa.Value.ToString();
            dataHangHoa data = new dataHangHoa();
            DataTable da = data.getDanhSachHangHoa_TonKho_ID(IDHH);
            if (da.Rows.Count != 0)
            {
                DataRow dr  = da.Rows[0];
                string SoLuongCon = dr["SoLuongCon"].ToString();
                string IDDọnViTinh = dr["IDDonViTinh"].ToString();
                string TrongLuong = dr["TrongLuong"].ToString();
                string giaBan = dr["GiaBan"].ToString();
                txtHHTrongLuong.Value = TrongLuong;
                cmbDonViTinh.Value = IDDọnViTinh;
                txtSoLuong.Value = 1; ;
                txtTonKho.Value = SoLuongCon;
                txtGiaBan.Value = giaBan;
                txtTongTienHH.Value = giaBan;
            }
        }

        protected void btnThemPhieuChuyenKho_Click(object sender, EventArgs e)
        {
            string ID = IDPhieuChuyenKho_Temp.Value.ToString();
            dtPhieuChuyenKho data = new dtPhieuChuyenKho();
            if (cmbKhoNhap.Value != null && cmbKhoXuat.Value != null)
            {
                DataTable da = data.DanhSachChiTietPhieuChuyenKho_Temp(ID);
                if (da.Rows.Count != 0)
                {
                    string IDKhoXuat = cmbKhoXuat.Value + "";
                    string IDKhoNhap = cmbKhoNhap.Value + "";
                    string IDNhanVienLap = Session["IDNhanVien"].ToString();
                    string SoMatHang = txtSoMatHang.Value + "";
                    string TrongLuong = txtTrongLuong.Value + "";
                    string tongTien = txtTongTien.Value + "";
                    data.CapNhatPhieuChuyenKho(ID, IDKhoXuat, IDKhoNhap, IDNhanVienLap, SoMatHang, TrongLuong, tongTien);

                    for (int i = 0; i < da.Rows.Count; i++)
                    {
                        DataRow dr = da.Rows[i];
                        string IDHangHoa = dr["IDHangHoa"].ToString();
                        string SoLuong = dr["SoLuong"].ToString();
                        string TrongLuongHH = dr["TrongLuong"].ToString();
                        string GiaBan = dr["GiaBan"].ToString();
                        string TongTien = dr["TongTien"].ToString();
                        data.ThemChiTietPhieuChuyenKho(ID, IDHangHoa, SoLuong, TrongLuongHH, GiaBan, TongTien);
                    }

                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Phiếu chuyển kho", Session["IDKho"].ToString(), "Nhập xuất tồn", "Thêm");
                    Response.Redirect("DanhSachPhieuChuyenKho.aspx");
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa không được rỗng.'); </script>");
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Chọn kho nhận.'); </script>");
            }
        }

        protected void cmbNgayLapPhieu_Init(object sender, EventArgs e)
        {
            cmbNgayLapPhieu.Date = DateTime.Today;
        }

        protected void btnHuyPhieuChuyenKho_Click(object sender, EventArgs e)
        {
            dtPhieuChuyenKho data = new dtPhieuChuyenKho();
            data.XoaPhieuChuyenKho_Temp(IDPhieuChuyenKho_Temp.Value.ToString());
            Response.Redirect("DanhSachPhieuChuyenKho.aspx");
        }

        protected void txtSoLuong_NumberChanged(object sender, EventArgs e)
        {
            string IDHH = cmbHangHoa.Value.ToString();
            dataHangHoa data = new dataHangHoa();
            DataTable da = data.getDanhSachHangHoa_TonKho_ID(IDHH);
            if (da.Rows.Count != 0)
            {
                DataRow dr = da.Rows[0];
                float TrongLuong = float.Parse(dr["TrongLuong"].ToString());
                float giaban = float.Parse(dr["GiaBan"].ToString());
                int soLuongMoi = Int32.Parse(txtSoLuong.Value + "");
                txtHHTrongLuong.Value = (soLuongMoi * TrongLuong);
                txtTongTienHH.Value = (soLuongMoi * giaban);
            }
        }

        private void Import()
        {
            if (string.IsNullOrEmpty(fileUpload.FileName))
            {
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
            int kt = 0;
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
            int intRow = dataTable.Rows.Count;
            if (intRow != 0)
            {
                for (int i = 0; i <= intRow - 1; i++)
                {
                    DataRow dr = dataTable.Rows[i];
                    string MaHang = dr["Ma Hang"].ToString();
                    string soLuong = dr["So Luong"].ToString();
                    dataHangHoa d = new dataHangHoa();
                    DataTable dataHH = d.getDanhSachHangHoa_MaHang(MaHang);
                    if (dataHH.Rows.Count != 0)
                    {
                        if (Int32.Parse(soLuong) <= Int32.Parse(dataHH.Rows[0]["SoLuongCon"].ToString()))
                        {
                            DataTable dataChiTiet_Temp = dt.KiemTraHangHoa_Temp(IDPhieuChuyenKho_Temp.Value.ToString(), dataHH.Rows[0]["ID"].ToString());
                            if (dataChiTiet_Temp.Rows.Count == 0)
                            {
                                dt.ThemChiTietPhieuChuyenKho_Temp(IDPhieuChuyenKho_Temp.Value.ToString(), dataHH.Rows[0]["ID"].ToString(), soLuong, (float.Parse(dataHH.Rows[0]["TrongLuong"].ToString()) * Int32.Parse(soLuong)) + "", dataHH.Rows[0]["GiaBan"].ToString(), (Int32.Parse(soLuong) * float.Parse(dataHH.Rows[0]["GiaBan"].ToString())) + "");
                                DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho_Temp.Value.ToString());
                                if (da.Rows.Count != 0)
                                {
                                    txtTrongLuong.Value = float.Parse(da.Rows[0]["TongTrongLuong"].ToString());
                                    txtTongTien.Value = float.Parse(da.Rows[0]["TongTien"].ToString());
                                    txtSoMatHang.Value = Int32.Parse(da.Rows[0]["TongSoLuong"].ToString());
                                }
                            }
                            else
                            {
                                dt.CapNhatChiTietPhieuChuyenKho_Temp(IDPhieuChuyenKho_Temp.Value.ToString(), dataHH.Rows[0]["ID"].ToString(), (Int32.Parse(dataChiTiet_Temp.Rows[0]["SoLuong"].ToString()) + Int32.Parse(soLuong)) + "", ((Int32.Parse(dataChiTiet_Temp.Rows[0]["SoLuong"].ToString()) + Int32.Parse(soLuong)) * float.Parse(dataHH.Rows[0]["TrongLuong"].ToString())) + "", dataHH.Rows[0]["GiaBan"].ToString(), ((Int32.Parse(dataChiTiet_Temp.Rows[0]["SoLuong"].ToString()) + Int32.Parse(soLuong)) * float.Parse(dataHH.Rows[0]["GiaBan"].ToString())) + "");
                                DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho_Temp.Value.ToString());
                                if (da.Rows.Count != 0)
                                {
                                    txtTrongLuong.Value = float.Parse(da.Rows[0]["TongTrongLuong"].ToString());
                                    txtTongTien.Value = float.Parse(da.Rows[0]["TongTien"].ToString());
                                    txtSoMatHang.Value = Int32.Parse(da.Rows[0]["TongSoLuong"].ToString());
                                }
                            }
                        }
                        else
                        {
                            kt = 1;
                            soLuong = dataHH.Rows[0]["SoLuongCon"].ToString();

                            DataTable dataChiTiet_Temp = dt.KiemTraHangHoa_Temp(IDPhieuChuyenKho_Temp.Value.ToString(), dataHH.Rows[0]["ID"].ToString());
                            if (dataChiTiet_Temp.Rows.Count == 0)
                            {
                                dt.ThemChiTietPhieuChuyenKho_Temp(IDPhieuChuyenKho_Temp.Value.ToString(), dataHH.Rows[0]["ID"].ToString(), soLuong, (float.Parse(dataHH.Rows[0]["TrongLuong"].ToString()) * Int32.Parse(soLuong)) + "", dataHH.Rows[0]["GiaBan"].ToString(), (Int32.Parse(soLuong) * float.Parse(dataHH.Rows[0]["GiaBan"].ToString())) + "");
                                DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho_Temp.Value.ToString());
                                if (da.Rows.Count != 0)
                                {
                                    txtTrongLuong.Value = float.Parse(da.Rows[0]["TongTrongLuong"].ToString());
                                    txtTongTien.Value = float.Parse(da.Rows[0]["TongTien"].ToString());
                                    txtSoMatHang.Value = Int32.Parse(da.Rows[0]["TongSoLuong"].ToString());
                                }
                            }
                            else
                            {
                                dt.CapNhatChiTietPhieuChuyenKho_Temp(IDPhieuChuyenKho_Temp.Value.ToString(), dataHH.Rows[0]["ID"].ToString(), (Int32.Parse(dataChiTiet_Temp.Rows[0]["SoLuong"].ToString()) + Int32.Parse(soLuong)) + "", ((Int32.Parse(dataChiTiet_Temp.Rows[0]["SoLuong"].ToString()) + Int32.Parse(soLuong)) * float.Parse(dataHH.Rows[0]["TrongLuong"].ToString())) + "", dataHH.Rows[0]["GiaBan"].ToString(), ((Int32.Parse(dataChiTiet_Temp.Rows[0]["SoLuong"].ToString()) + Int32.Parse(soLuong)) * float.Parse(dataHH.Rows[0]["GiaBan"].ToString())) + "");
                                DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho_Temp.Value.ToString());
                                if (da.Rows.Count != 0)
                                {
                                    txtTrongLuong.Value = float.Parse(da.Rows[0]["TongTrongLuong"].ToString());
                                    txtTongTien.Value = float.Parse(da.Rows[0]["TongTien"].ToString());
                                    txtSoMatHang.Value = Int32.Parse(da.Rows[0]["TongSoLuong"].ToString());
                                }
                            }
                        }
                    }
                }

                if(kt == 1)
                    Response.Write("<script language='JavaScript'> alert('Có hàng hóa không đủ số lượng trong kho. Hệ thống lấy số lượng tối đa để cập nhật.'); </script>");
                LoadGrid(IDPhieuChuyenKho_Temp.Value.ToString());
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

            if (fileUpload.HasFile)
            {
                strFileExcel = Guid.NewGuid().ToString();
                string theExtension = Path.GetExtension(fileUpload.FileName);
                strFileExcel += theExtension;

                filein = folder + strFileExcel;
                fileUpload.SaveAs(filein);
                strFileExcel = ThangNam + "/" + strFileExcel;
            }
        }
    }
}