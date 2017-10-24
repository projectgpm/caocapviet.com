using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtDonHangChiNhanh
    {
        public static string LayIDKhoTheoDonHang(string IDDonHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT IDKho FROM [GPM_DonHangChiNhanh] WHERE ID = '" + IDDonHang + "' ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return dr["IDKho"].ToString();
                    }
                    else return "-1";
                }
            }
        }
        public static string TongSoXuatTrongThang(string NgayBD, string NgayKT, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select [IDKho], count(IDKho) as Tong from [GPM_DonHangChiNhanh] where [NgayLap] >= '" + DateTime.Parse(NgayBD).ToString("yyyy-MM-dd hh:mm:ss tt") + "' and [NgayLap] <= '" + DateTime.Parse(NgayKT).ToString("yyyy-MM-dd hh:mm:ss tt") + "' and [IDKho] = '" + IDKho + "' group by IDKho";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                        return (Int32.Parse(tb.Rows[0]["Tong"].ToString()) + 1).ToString();
                    return "1";
                }
            }
        }
        public static string SoLuongDatHang(string IDHangHoa, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT [GPM_DonHangChiNhanh_ChiTiet].IDHangHoa, SUM([GPM_DonHangChiNhanh_ChiTiet].SoLuong)  AS SoLuongDat FROM GPM_DonHangChiNhanh,[GPM_DonHangChiNhanh_ChiTiet] WHERE GPM_DonHangChiNhanh.idkho = '" + IDKho + "' AND GPM_DonHangChiNhanh.trangthai = 0 AND [GPM_DonHangChiNhanh_ChiTiet].IDDonHangChiNhanh = GPM_DonHangChiNhanh.ID AND  [GPM_DonHangChiNhanh_ChiTiet].IDHangHoa =  '" + IDHangHoa + "' GROUP BY IDHangHoa";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return dr["SoLuongDat"].ToString();
                    }
                    else return "0";
                }
            }
        }
        public static int TuanSuatBanHang(DateTime NgayHomNay, string IDHangHoa, int SoNgay, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SUM(SoLuong) as Tong FROM [GPM_ChiTietHoaDon] WHERE [NgayBan] >= '" + NgayHomNay.AddDays(SoNgay).ToString("yyyy-MM-dd hh:mm:ss tt") + "' AND [NgayBan] <= '" + NgayHomNay.ToString("yyyy-MM-dd hh:mm:ss tt") + "'  AND [IDHangHoa] = '" + IDHangHoa + "' AND [IDKho] = '" + IDKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        if (dr["Tong"].ToString() != "")
                        {
                            return Int32.Parse(dr["Tong"].ToString());
                        }
                        return 0;
                    }
                    else return 0;
                }
            }
        }
        public DataTable LayDanhSachDonHang(string IDKho, string HienThi, string NgayBD, string NgayKT)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TOP " + HienThi + " * FROM [GPM_DonHangChiNhanh] WHERE [NgayDat] < '" + NgayKT + "' AND NgayDat >= '" + NgayBD + "' AND [TrangThai] = 0 AND [IDKho] = '" + IDKho + "' ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void ThemChiTietDonHangClient(object IDDonHangChiNhanh, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string SoLuong, string TonKho, string GhiChu, string TrangThai, string IDKho, string DonGia, string ThanhTien, string SoLuongDatTruoc, string TanSuatBanHang, string SoLuongDeNghi)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_DonHangChiNhanh_ChiTiet] ([IDDonHangChiNhanh],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[TonKho],[GhiChu],[IDKho],[TrangThai],[DonGia],[ThanhTien],[SoLuongDatTruoc],[TanSuatBanHang],[SoLuongDeNghi]) VALUES (@IDDonHangChiNhanh,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@TonKho,@GhiChu,@IDKho,@TrangThai,@DonGia,@ThanhTien,@SoLuongDatTruoc,@TanSuatBanHang,@SoLuongDeNghi)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@SoLuongDeNghi", SoLuongDeNghi);
                        myCommand.Parameters.AddWithValue("@TanSuatBanHang", TanSuatBanHang);
                        myCommand.Parameters.AddWithValue("@SoLuongDatTruoc", SoLuongDatTruoc);
                        myCommand.Parameters.AddWithValue("@IDDonHangChiNhanh", IDDonHangChiNhanh);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@DonGia", DonGia);
                        myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TonKho", TonKho);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public void ThemChiTietDonHangClientGiamSat(object IDDonHangChiNhanh, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string SoLuong, string TonKho, string GhiChu, string TrangThai, string IDKho, string DonGia, string ThanhTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_DonHangChiNhanh_ChiTiet] ([IDDonHangChiNhanh],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[TonKho],[GhiChu],[IDKho],[TrangThai],[DonGia],[ThanhTien],[TrangThaiThem]) VALUES (@IDDonHangChiNhanh,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@TonKho,@GhiChu,@IDKho,@TrangThai,@DonGia,@ThanhTien,@TrangThaiThem)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDDonHangChiNhanh", IDDonHangChiNhanh);
                        myCommand.Parameters.AddWithValue("@TrangThaiThem", 1);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@DonGia", DonGia);
                        myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TonKho", TonKho);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public void CapNhatDonDatHangClient(object ID, string SoDonHang, string IDNguoiLap, DateTime NgayLap, string TongTrongLuong, string IDKho, string GhiChu, DateTime NgayDat, DateTime NgayGiaoDuKien, string MucDoUuTien, float TongTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();

                    string cmdText = "UPDATE [GPM_DonHangChiNhanh] SET [TongTien] = @TongTien ,[MucDoUuTien] = @MucDoUuTien,[NgayGiaoDuKien] = @NgayGiaoDuKien,[SoDonHang] = @SoDonHang,[IDNguoiLap] = @IDNguoiLap,[NgayLap] = @NgayLap,[TongTrongLuong] = @TongTrongLuong,[NgayDat] = @NgayDat,[IDKho] = @IDKho,[GhiChu] = @GhiChu WHERE ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@SoDonHang", SoDonHang);
                        myCommand.Parameters.AddWithValue("@TongTien", TongTien);
                        myCommand.Parameters.AddWithValue("@IDNguoiLap", IDNguoiLap);
                        myCommand.Parameters.AddWithValue("@NgayLap", NgayLap);
                        myCommand.Parameters.AddWithValue("@TongTrongLuong", TongTrongLuong);
                        myCommand.Parameters.AddWithValue("@NgayDat", NgayDat);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@NgayGiaoDuKien", NgayGiaoDuKien);
                        myCommand.Parameters.AddWithValue("@MucDoUuTien", MucDoUuTien);
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public void CapNhatChiTietDonHang_temp(string IDDonHangChiNhanh, string IDHangHoa, int SoLuong, string TrongLuong, string TonKho, string GhiChu, float DonGia)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_DonHangChiNhanh_ChiTiet_Temp] SET [DonGia] = @DonGia,[ThanhTien] = @ThanhTien,[TrongLuong] = @TrongLuong,[SoLuong] = @SoLuong,[TonKho] = @TonKho,[GhiChu] = @GhiChu WHERE [IDHangHoa] = @IDHangHoa AND [IDDonHangChiNhanh] = @IDDonHangChiNhanh";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@DonGia", DonGia);
                        myCommand.Parameters.AddWithValue("@ThanhTien", DonGia * SoLuong);
                        myCommand.Parameters.AddWithValue("@IDDonHangChiNhanh", IDDonHangChiNhanh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TonKho", TonKho);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
                }
            }
        }
        public void ThemChiTietDonHang_Temp(string IDDonHangChiNhanh, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, int SoLuong, string TonKho, string GhiChu, float DonGia, string SoLuongDeNghi, string TanSuatBanHang, string SoLuongDatTruoc)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_DonHangChiNhanh_ChiTiet_Temp] ([IDDonHangChiNhanh],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[TonKho],[GhiChu],[DonGia],[ThanhTien],[SoLuongDeNghi],[TanSuatBanHang],[SoLuongDatTruoc]) VALUES (@IDDonHangChiNhanh,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@TonKho,@GhiChu,@DonGia,@ThanhTien,@SoLuongDeNghi,@TanSuatBanHang,@SoLuongDatTruoc)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@SoLuongDeNghi", SoLuongDeNghi);
                        myCommand.Parameters.AddWithValue("@TanSuatBanHang", TanSuatBanHang);
                        myCommand.Parameters.AddWithValue("@SoLuongDatTruoc", SoLuongDatTruoc);
                        myCommand.Parameters.AddWithValue("@DonGia", DonGia);
                        myCommand.Parameters.AddWithValue("@ThanhTien", DonGia * SoLuong);
                        myCommand.Parameters.AddWithValue("@IDDonHangChiNhanh", IDDonHangChiNhanh);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TonKho", TonKho);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public static DataTable KTChiTietDonHang_Temp(string IDHangHoa, string IDDonHangChiNhanh)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DonHangChiNhanh_ChiTiet_Temp] WHERE [IDHangHoa]= '" + IDHangHoa + "' AND [IDDonHangChiNhanh] = '" + IDDonHangChiNhanh + "' ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void XoaChiTietDonHang_Temp_ID(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_DonHangChiNhanh_ChiTiet_Temp] WHERE ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public void XoaChiTietDonHang_Temp(string IDDonHangChiNhanh)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_DonHangChiNhanh_ChiTiet_Temp] WHERE [IDDonHangChiNhanh] = " + IDDonHangChiNhanh;
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                    strSQL = "DELETE [GPM_DonHangChiNhanh] WHERE [ID] = " + IDDonHangChiNhanh;
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public void XoaChiTietDonHang_Nhap(string IDDonHangChiNhanh)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_DonHangChiNhanh_ChiTiet_Temp] WHERE [IDDonHangChiNhanh] = '" + IDDonHangChiNhanh + "'";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public DataTable DanhSachDonDatHangClient_Temp(string IDDonHangChiNhanh)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DonHangChiNhanh_ChiTiet_Temp] WHERE [IDDonHangChiNhanh] = " + IDDonHangChiNhanh;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public object ThemPhieuDatHangClient()
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    object IDPhieuKiemKho = null;
                    string cmdText = "INSERT INTO [GPM_DonHangChiNhanh] ([NgayCapNhat]) OUTPUT INSERTED.ID VALUES (getdate())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        IDPhieuKiemKho = myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                    return IDPhieuKiemKho;
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
    }
}