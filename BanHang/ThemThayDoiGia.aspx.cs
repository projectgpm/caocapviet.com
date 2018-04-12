using BanHang.Data;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ThemThayDoiGia : System.Web.UI.Page
    {
        dtGiaTheoGio dt = new dtGiaTheoGio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 99) == false)
                    Response.Redirect("Default.aspx");
                if (!IsPostBack)
                {
                    ID_temp.Value = Session["IDNhanVien"].ToString();
                    DanhSachVung();
                }
                LoadGrid(ID_temp.Value.ToString());
            }
        }
        private void DanhSachVung()
        {
            dtGiaTheoVung data = new dtGiaTheoVung();
            data = new dtGiaTheoVung();
            DanhSachKho.DataSource = data.LayDanhSachKho();
            DanhSachKho.ValueField = "ID";
            DanhSachKho.TextField = "TenCuaHang";
            DanhSachKho.DataBind();
            

            dtVung dt1 = new dtVung();
            DataTable d2x = dt1.LayDanhSach();
            d2x.Rows.Add(-1, 0, "Tất cả Vùng", 0);
            cmbVung.DataSource = d2x;
            cmbVung.ValueField = "ID";
            cmbVung.TextField = "TenVung";
            cmbVung.DataBind();
            cmbVung.SelectedIndex = cmbVung.Items.Count;


        }

        private void LoadGrid(string IDtemp)
        {
            dt = new dtGiaTheoGio();
            gridHangHoa.DataSource = dt.DanhSachHangHoa(IDtemp);
            gridHangHoa.DataBind();
        }
        protected void cmbVung_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVung.Text != "")
            {
                int ID = Int32.Parse(cmbVung.Value.ToString());
                dtGiaTheoVung data = new dtGiaTheoVung();
                DanhSachKho.DataSource = data.LayDanhSachKho_IDVung(ID);
                DanhSachKho.ValueField = "ID";
                DanhSachKho.TextField = "TenCuaHang";
                DanhSachKho.DataBind();
                KiemTra_Check();
            }
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            if (txtBarcode.Value != null)
            {
                string IDHangHoa = txtBarcode.Value.ToString();
                string IDTemp = ID_temp.Value.ToString();
                string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                DateTime GioThayDoi = DateTime.Parse(txtGioThayDoi.Text.ToString());
                //for (int i = DanhSachKho.Items.Count - 1; i >= 0; i--)
                //{
                //    if (DanhSachKho.Items[i].Selected == true)
                //    {
                //        string IDKhoApDung = DanhSachKho.Items[i].Value.ToString();
                float GiaBan = dtCapNhatTonKho.GiaBan_KhoChiNhanh(IDHangHoa, "1");
                dt = new dtGiaTheoGio();
                DataTable db = dt.KT_ThemChiTiet_Temp(IDHangHoa, IDTemp, "1", GioThayDoi);
                if (db.Rows.Count == 0)
                {
                    dt.ThemChiTiet_Temp(IDTemp, dtHangHoa.LayMaHang(IDHangHoa), IDHangHoa, IDDonViTinh, GiaBan.ToString(), GioThayDoi, "1");
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Mã hàng đã tại trong kho này.'); </script>");
                }
                //}
                //}
                LoadGrid(IDTemp);

            }
            else
            {
                txtBarcode.Text = "";
                txtBarcode.Value = "";
                txtBarcode.Focus();
            }
        }

        protected void gridHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            dt = new dtGiaTheoGio();
            dt.Xoa_temp(ID);
            e.Cancel = true;
            gridHangHoa.CancelEdit();
            LoadGrid(ID_temp.Value.ToString());
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            dt = new dtGiaTheoGio();
            dt.XoaAll_temp(ID_temp.Value.ToString());
            Response.Redirect("GiaTheoGio.aspx");
        }

        protected void gridHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            if (e.NewValues["GiaBan"] != null && e.NewValues["GioThayDoi"] != null)
            {
                string GiaBan0 = e.NewValues["GiaBan"].ToString();
                DateTime GioThayDoi = DateTime.Parse(e.NewValues["GioThayDoi"].ToString());
                dt = new dtGiaTheoGio();
                dt.CapNhatChiTiet_Temp(ID, GiaBan0, GioThayDoi);
            }
            else
            {
                throw new Exception("Lỗi: Giá & giờ không được bỏ trống? Vui lòng kiểm tra lại.");
            }
            e.Cancel = true;
            gridHangHoa.CancelEdit();
            LoadGrid(ID_temp.Value.ToString());
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            dt = new dtGiaTheoGio();
            DataTable db = dt.DanhSachHangHoa(ID_temp.Value.ToString());
            if (db.Rows.Count > 0)
            {
                foreach (DataRow dr in db.Rows)
                {
                    string MaHang = dr["MaHang"].ToString();
                    string IDHangHoa = dr["IDHangHoa"].ToString();
                    string IDDonViTinh = dr["IDDonViTinh"].ToString();
                    string GiaBan = dr["GiaBan"].ToString();
                   
                    string GioThayDoi = dr["GioThayDoi"].ToString();
                  
                    string IDNhanVien = Session["IDNhanVien"].ToString();

                    for (int i = DanhSachKho.Items.Count - 1; i >= 0; i--)
                    {
                        if (DanhSachKho.Items[i].Selected == true)
                        {
                            dt = new dtGiaTheoGio();
                            //kiểm tra
                            string IDKhoApDung = DanhSachKho.Items[i].Value.ToString();
                            DataTable db1 = dt.KT_ThemChiTiet(IDHangHoa, IDKhoApDung, DateTime.Parse(GioThayDoi));
                            if (db1.Rows.Count == 0)
                            {
                                dt.ThemChiTiet(MaHang, IDHangHoa, IDDonViTinh, GiaBan, DateTime.Parse(GioThayDoi), IDKhoApDung, IDNhanVien);
                            }

                        }
                    }
                }
                dt = new dtGiaTheoGio();
                dt.XoaAll_temp(ID_temp.Value.ToString());
                Response.Redirect("GiaTheoGio.aspx");
            }
            else
            {
                txtBarcode.Text = "";
                txtBarcode.Value = "";
                txtBarcode.Focus();
                Response.Write("<script language='JavaScript'> alert('Danh sách thay đổi giá trống.'); </script>");
            }
        }

        protected void ckChonTatCa_CheckedChanged(object sender, EventArgs e)
        {
            KiemTra_Check();
        }
        public void KiemTra_Check()
        {
            if (ckChonTatCa.Checked == true)
            {
                DanhSachKho.SelectAll();
            }
            else
            {
                DanhSachKho.UnselectAll();
            }
        }

        protected void txtBarcode_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa,GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                           INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID 
                                        WHERE (GPM_HangHoa.ID = @ID) AND (GPM_HangHoa.IDTrangThaiHang = 1 OR GPM_HangHoa.IDTrangThaiHang = 3 OR GPM_HangHoa.IDTrangThaiHang = 6  OR GPM_HangHoa.IDTrangThaiHang = 8)";

            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void txtBarcode_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [TenDonViTinh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                               INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID
	                                        WHERE ((GPM_HangHoa.MaHang LIKE @MaHang) OR GPM_HangHoa.TenHangHoa LIKE @TenHang) AND (GPM_HangHoa.IDTrangThaiHang = 1 OR GPM_HangHoa.IDTrangThaiHang = 3 OR GPM_HangHoa.IDTrangThaiHang = 6  OR GPM_HangHoa.IDTrangThaiHang = 8) AND (GPM_HangHoaTonKho.IDKho = @IDKho) AND (GPM_HangHoaTonKho.DaXoa = 0)	
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";
            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("MaHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
            dsHangHoa.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            dsHangHoa.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }
    }
}