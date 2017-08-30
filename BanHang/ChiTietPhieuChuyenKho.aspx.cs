using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ChiTietPhieuChuyenKho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                string IDPhieuChuyenKho = Request.QueryString["IDPhieuChuyenKho"];
                if (IDPhieuChuyenKho != null)
                {
                    LoadGrid(IDPhieuChuyenKho);
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }
        private void LoadGrid(string IDPhieuChuyenKho)
        {
            dtPhieuChuyenKho data = new dtPhieuChuyenKho();
            DataTable da = data.DanhSachChiTietPhieuChuyenKho(IDPhieuChuyenKho);
            gridDanhSachHangHoa.DataSource = da;
            gridDanhSachHangHoa.DataBind();

            int kt = data.TrangThaiPhieuChuyenKho(IDPhieuChuyenKho);
            if(kt > 1)
                gridDanhSachHangHoa.Columns["actionbtn"].Visible = false;

            DataTable dat = data.DanhSachPhieuChuyenKho_Kho(IDPhieuChuyenKho);
            if (dat.Rows.Count != 0)
            {
                string KhoXuat = dat.Rows[0]["IDKhoXuat"].ToString();
                string KhoNhan = dat.Rows[0]["IDKhoNhap"].ToString();

                if (KhoXuat.CompareTo(Session["IDKho"].ToString()) == 0)
                {
                    btnDuyetNhap.Enabled = false;
                    if (Session["IDKho"].ToString().CompareTo("1") == 0)
                        btnDuyetPhieuChuyenKho.Enabled = true;
                    else btnDuyetPhieuChuyenKho.Enabled = false;
                }
                else if (KhoNhan.CompareTo(Session["IDKho"].ToString()) == 0)
                {
                    btnDuyetXuat.Enabled = false;
                    if (Session["IDKho"].ToString().CompareTo("1") == 0)
                        btnDuyetPhieuChuyenKho.Enabled = true;
                    else btnDuyetPhieuChuyenKho.Enabled = false;
                }
                else
                {
                    if (Session["IDKho"].ToString().CompareTo("1") == 0)
                    {
                        btnDuyetNhap.Enabled = false;
                        btnDuyetXuat.Enabled = false;
                    }
                }
            }
        }

        protected void gridDanhSachHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys[0].ToString());
            string IDPhieuChuyenKho = Request.QueryString["IDPhieuChuyenKho"];
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
            dt.XoaChiTietPhieuChuyenKho(ID + "");

            DataTable da = dt.ChiTietTongSoLuongHangHoa_2(IDPhieuChuyenKho);
            if (da.Rows.Count != 0)
            {
                string tongTrongLuong = da.Rows[0]["TongTrongLuong"].ToString();
                string tongTien = da.Rows[0]["TongTien"].ToString();
                string tongSoLuong = da.Rows[0]["TongSoLuong"].ToString();

                dt.CapNhatPhieuChuyenKho_2(IDPhieuChuyenKho, tongSoLuong, tongTrongLuong, tongTien);

                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Chi tiết phiếu chuyển kho", Session["IDKho"].ToString(), "Nhập xuất tồn", "Xóa");
            }

            e.Cancel = true;
            gridDanhSachHangHoa.CancelEdit();
            LoadGrid(IDPhieuChuyenKho);
        }

        protected void gridDanhSachHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys[0].ToString());
            int sLuongMoi = Int32.Parse(e.NewValues["SoLuong"].ToString());
            string IDPhieuChuyenKho = Request.QueryString["IDPhieuChuyenKho"];

            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
            dataHangHoa data = new dataHangHoa();
            int sLuongTon = 0;
            float trongLuong = 0;
            float giaBan = 0;
            DataTable da = data.getDanhSachHangHoa_TonKho_ID(ID + "");
            if (da.Rows.Count != 0)
            {
                DataRow dr = da.Rows[0];
                sLuongTon = Int32.Parse(dr["SoLuongCon"].ToString());
                trongLuong = float.Parse(dr["TrongLuong"].ToString());
                giaBan = float.Parse(dr["GiaBan"].ToString());
            }

            if (sLuongMoi > sLuongTon)
                Response.Write("<script language='JavaScript'> alert('Hàng hóa trong kho không đủ số lượng.'); </script>");
            else
            {
                float trongLuongMoi = sLuongMoi * trongLuong;
                float tongTienMoi = sLuongMoi * giaBan;
                dt.CapNhatChiTietPhieuChuyenKho(IDPhieuChuyenKho, ID + "", sLuongMoi + "", trongLuongMoi + "", tongTienMoi + "");

                DataTable dax = dt.ChiTietTongSoLuongHangHoa_2(IDPhieuChuyenKho);
                if (dax.Rows.Count != 0)
                {
                    string tongTrongLuong = dax.Rows[0]["TongTrongLuong"].ToString();
                    string tongTien = dax.Rows[0]["TongTien"].ToString();
                    string tongSoLuong = dax.Rows[0]["TongSoLuong"].ToString();

                    dt.CapNhatPhieuChuyenKho_2(IDPhieuChuyenKho, tongSoLuong, tongTrongLuong, tongTien);
                }
            }

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Chi tiết phiếu chuyển kho", Session["IDKho"].ToString(), "Nhập xuất tồn", "Cập nhật");
            e.Cancel = true;
            gridDanhSachHangHoa.CancelEdit();
            LoadGrid(IDPhieuChuyenKho);
        }

        protected void btnDuyetXuat_Click(object sender, EventArgs e)
        {
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
            string IDPhieuChuyenKho = Request.QueryString["IDPhieuChuyenKho"];
            int kt = dt.TrangThaiPhieuChuyenKho(IDPhieuChuyenKho);
            if (kt == 1)
            {
                DataTable da = dt.DanhSachChiTietPhieuChuyenKho(IDPhieuChuyenKho);
                if (da.Rows.Count != 0)
                {
                    for (int i = 0; i < da.Rows.Count; i++)
                    {
                        DataRow dr = da.Rows[i];
                        string IDHangHoa = dr["IDHangHoa"].ToString();
                        string soLuong = dr["SoLuong"].ToString();
                        dtCapNhatTonKho.TruTonKho(IDHangHoa, soLuong, Session["IDKho"].ToString());
                    }

                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Chi tiết phiếu chuyển kho", Session["IDKho"].ToString(), "Nhập xuất tồn", "Duyệt xuất hàng");
                    dt.DuyetChuyenKho_Xuat(IDPhieuChuyenKho, 2 + "", Session["IDNhanVien"].ToString());
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Phiếu chuyển này đã được duyệt.'); </script>");
            } 
        }

        protected void btnDuyetNhap_Click(object sender, EventArgs e)
        {
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
            string IDPhieuChuyenKho = Request.QueryString["IDPhieuChuyenKho"];
            int kt = dt.TrangThaiPhieuChuyenKho(IDPhieuChuyenKho);
            if (kt == 2)
            {
                DataTable da = dt.DanhSachChiTietPhieuChuyenKho(IDPhieuChuyenKho);
                if (da.Rows.Count != 0)
                {
                    for (int i = 0; i < da.Rows.Count; i++)
                    {
                        DataRow dr = da.Rows[i];
                        string IDHangHoa = dr["IDHangHoa"].ToString();
                        string soLuong = dr["SoLuong"].ToString();
                        dtCapNhatTonKho.CongTonKho(IDHangHoa, soLuong, Session["IDKho"].ToString());
                    }

                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Chi tiết phiếu chuyển kho", Session["IDKho"].ToString(), "Nhập xuất tồn", "Duyệt nhập hàng");
                    dt.DuyetChuyenKho_Nhap(IDPhieuChuyenKho, 3 + "", Session["IDNhanVien"].ToString());
                }
            }
            else
            {
                if(kt < 2)
                    Response.Write("<script language='JavaScript'> alert('Phiếu chuyển này chưa được xuất hàng.'); </script>");
                else
                    Response.Write("<script language='JavaScript'> alert('Phiếu chuyển này đã duyệt nhập hàng.'); </script>");
            } 
        }

        protected void btnDuyetPhieuChuyenKho_Click(object sender, EventArgs e)
        {
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
            string IDPhieuChuyenKho = Request.QueryString["IDPhieuChuyenKho"];
            int kt = dt.TrangThaiPhieuChuyenKho(IDPhieuChuyenKho);
            if (kt == 3)
            {
                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Chi tiết phiếu chuyển kho", Session["IDKho"].ToString(), "Nhập xuất tồn", "Duyệt hoàn tất chuyển kho.");
                dt.DuyetChuyenKho(IDPhieuChuyenKho, 4 + "");
            }
            else
            {
                if (kt == 1)
                    Response.Write("<script language='JavaScript'> alert('Phiếu chuyển này chưa được xuất hàng.'); </script>");
                else if (kt == 2)
                    Response.Write("<script language='JavaScript'> alert('Phiếu chuyển này chưa nhận được hàng.'); </script>");
                else Response.Write("<script language='JavaScript'> alert('Phiếu chuyển này đã hoàn tất.'); </script>");
            } 
        }
    }
}