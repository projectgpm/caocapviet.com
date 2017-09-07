using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtDuyetDonHangChiNhanh
    {
        public static int LayTrangThai(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TrangThai FROM [GPM_DuyetHangChiNhanh_Temp] WHERE [ID] = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["TrangThai"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public DataTable DanhSachChiTietDuyet(string IDDuyetHangChiNhanh)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DuyetHangChiNhanh_ChiTiet] WHERE IDDuyetHangChiNhanh =" + IDDuyetHangChiNhanh;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable LayDanhSachDonHangDuyet(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DuyetHangChiNhanh] WHERE ( '" + IDKho + "' = 1 OR [IDKhoLap] = '" + IDKho + "') ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void CapNhatTrangThaiClient(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_DonHangChiNhanh] SET [TrangThai] = 1 WHERE [ID] = " + ID;
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                    cmdText = " UPDATE [GPM_DonHangChiNhanh_ChiTiet] SET [TrangThai] = 1 WHERE [IDDonHangChiNhanh] = " + ID;
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch(Exception ex)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
                }
            }
        }

        public object ThemDuyetDonHang(string SoDonHang, string IDNguoiLap, string TongTrongLuong, string TongTien, string IDKhoLap, string IDKhoDuyet, string IDNguoiDuyet, string GhiChu, DateTime NgayLap, DateTime NgayDuyet)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    object IDPhieuKiemKho = null;
                    string cmdText = "INSERT INTO [GPM_DuyetHangChiNhanh] ([SoDonHang],[NgayCapNhat],[IDNguoiLap],[TongTrongLuong],[TongTien],[IDKhoLap],[IDKhoDuyet],[IDNguoiDuyet],[GhiChu],[NgayLap],[NgayDuyet]) OUTPUT INSERTED.ID VALUES (@SoDonHang,getdate(),@IDNguoiLap,@TongTrongLuong,@TongTien,@IDKhoLap,@IDKhoDuyet,@IDNguoiDuyet,@GhiChu,@NgayLap,@NgayDuyet)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@SoDonHang", SoDonHang);
                        myCommand.Parameters.AddWithValue("@IDNguoiLap", IDNguoiLap);
                        myCommand.Parameters.AddWithValue("@TongTrongLuong", TongTrongLuong);
                        myCommand.Parameters.AddWithValue("@TongTien", TongTien);
                        myCommand.Parameters.AddWithValue("@IDKhoLap", IDKhoLap);
                        myCommand.Parameters.AddWithValue("@IDKhoDuyet", IDKhoDuyet);
                        myCommand.Parameters.AddWithValue("@IDNguoiDuyet", IDNguoiDuyet);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@NgayLap", NgayLap);
                        myCommand.Parameters.AddWithValue("@NgayDuyet", NgayDuyet);
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
        public void Xoa_ALL_Temp()
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = " DELETE [GPM_DuyetHangChiNhanh_Temp]";
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
        public void Xoa_Temp_ID(string IDTemp)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = " DELETE [GPM_DuyetHangChiNhanh_Temp] WHERE [IDTemp] = '" + IDTemp + "'";
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
        public void CapNhatChiTietDonHang(string ID, string IDHangHoa, int SoLuong, float DonGia, float ThanhTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_DuyetHangChiNhanh_Temp] SET [SoLuong] = @SoLuong,[DonGia] = @DonGia,[ThanhTien] = @ThanhTien WHERE [IDHangHoa] = @IDHangHoa AND [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@DonGia", DonGia);
                        myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
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
        public void ThemChiTietDonHang_Temp(string IDDonHangChiNhanh, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string SoLuong, string TonKho, string GhiChu, string TrangThai, string IDKho, string IDTemp)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_DuyetHangChiNhanh_Temp] ([IDDonHangChiNhanh],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[TonKho],[GhiChu],[TrangThai],[IDKho],[IDTemp]) VALUES (@IDDonHangChiNhanh,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@TonKho,@GhiChu,@TrangThai,@IDKho,@IDTemp)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDDonHangChiNhanh", IDDonHangChiNhanh);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TonKho", TonKho);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@IDTemp", IDTemp);
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
        public void ThemMoiChiTiet(string IDDonHangChiNhanh, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string SoLuong, string TonKho, string GhiChu, string TrangThai, string IDKho, string IDTemp, string ChenhLech, string ThucTe)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_DuyetHangChiNhanh_Temp] ([IDDonHangChiNhanh],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[TonKho],[GhiChu],[TrangThai],[IDKho],[IDTemp],[ChenhLech],[ThucTe]) VALUES (@IDDonHangChiNhanh,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@TonKho,@GhiChu,@TrangThai,@IDKho,@IDTemp,@ChenhLech,@ThucTe)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDDonHangChiNhanh", IDDonHangChiNhanh);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TonKho", TonKho);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@IDTemp", IDTemp);
                        myCommand.Parameters.AddWithValue("@ChenhLech", ChenhLech);
                        myCommand.Parameters.AddWithValue("@ThucTe", ThucTe);
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
        public DataTable KTChiTietDonHang_Temp(string IDHangHoa, string IDDonHangChiNhanh, string IDTemp)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_DuyetHangChiNhanh_Temp] WHERE [IDHangHoa]= '" + IDHangHoa + "' AND [IDDonHangChiNhanh] = '" + IDDonHangChiNhanh + "' AND [IDTemp] = '" + IDTemp + "' ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void ThemChiTietDonHang_Duyet(object IDDuyetHangChiNhanh, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string SoLuong, string DonGia, string ThanhTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_DuyetHangChiNhanh_ChiTiet] ([IDDuyetHangChiNhanh],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[DonGia],[ThanhTien]) VALUES (@IDDuyetHangChiNhanh,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@DonGia,@ThanhTien)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDDuyetHangChiNhanh", IDDuyetHangChiNhanh);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@DonGia", DonGia);
                        myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
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
        public void CapNhat_temp(string IDDonHangChiNhanh, string IDHangHoa, int SoLuong, float DonGia, float ThanhTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_DuyetHangChiNhanh_Temp] SET [SoLuong] = @SoLuong,[DonGia] = @DonGia,[ThanhTien] = @ThanhTien WHERE [IDHangHoa] = @IDHangHoa AND [IDDonHangChiNhanh] = @IDDonHangChiNhanh";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonHangChiNhanh", IDDonHangChiNhanh);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@DonGia", DonGia);
                        myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
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
        public void Xoa_Temp(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "DELETE GPM_DuyetHangChiNhanh_Temp WHERE ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
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
        public DataTable DanhSachChiTiet(string IDDonHangChiNhanh)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DonHangChiNhanh_ChiTiet] WHERE IDDonHangChiNhanh ='" + IDDonHangChiNhanh + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachChiTiet_Temp(string IDDonHangChiNhanh, string IDTemp, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT [GPM_DuyetHangChiNhanh_Temp].*,[GPM_HangHoaTonKho].SoLuongCon As TonKhoTong FROM [GPM_DuyetHangChiNhanh_Temp],[GPM_HangHoaTonKho] WHERE   [GPM_HangHoaTonKho].IDKho = '" + IDKho + "' AND [GPM_HangHoaTonKho].IDHangHoa = [GPM_DuyetHangChiNhanh_Temp].IDHangHoa  AND [GPM_DuyetHangChiNhanh_Temp].IDDonHangChiNhanh ='" + IDDonHangChiNhanh + "' AND [GPM_DuyetHangChiNhanh_Temp].[IDTemp] = '" + IDTemp + "' ORDER BY [GPM_DuyetHangChiNhanh_Temp].TrangThai ASC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable LayDanhSachDonHang()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DonHangChiNhanh] WHERE [TrangThai] = 0 AND [GiamSatDuyet] = 1  AND [IDNguoiLap] is not null ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable LayDanhSachDonHang_ID(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DonHangChiNhanh] WHERE ID = '" + ID + "' ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
    }
}