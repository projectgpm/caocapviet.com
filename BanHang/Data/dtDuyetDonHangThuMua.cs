using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtDuyetDonHangThuMua
    {
        public DataTable DanhSachChiTiet_Duyet_ThuMua(string IDDonHangThuMua)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DuyetHangThuMua_ChiTiet] WHERE IDDonHangThuMua =" + IDDonHangThuMua;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachDuyet_ThuMua()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DuyetHangThuMua]  ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void CapNhatChenhLech(object ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_DuyetHangThuMua] SET [TrangThai] = 1 WHERE [ID] = " + ID;
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
                }
            }
        }
        public void CapNhatTrangThaiThuMua(string ID, int IDTrangThaiDonHang)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_ThuMua_DonHang] SET [TrangThai] = 1 , [IDTrangThaiDonHang] = '" + IDTrangThaiDonHang + "' WHERE  [ID] = " + ID;
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
                }
            }
        }
        public DataTable KiemTra_LOG(string SoDonHang, string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_Log_DuyetHangThuMua] WHERE [IDHangHoa] = '" + IDHangHoa + "' AND SoDonHang = N'" + SoDonHang + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void Xoa_LOG(string SoDonHang, string IDHangHoa)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "DELETE [GPM_Log_DuyetHangThuMua]  WHERE [IDHangHoa] = '" + IDHangHoa + "' AND  [SoDonHang] = N'" + SoDonHang + "'";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
                }
            }
        }
        public void ThemChiTietDonHang_LOG(string SoDonHang,string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string SoLuong, string DonGia, float ThanhTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_Log_DuyetHangThuMua] ([SoDonHang],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[DonGia],[ThanhTien]) VALUES (@SoDonHang,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@DonGia,@ThanhTien)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@SoDonHang", SoDonHang);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@DonGia", DonGia);
                        myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
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
        public void CapNhatChiTietDonHang_LOG(string SoDonHang, string IDHangHoa,string SoLuong, string DonGia, float ThanhTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_Log_DuyetHangThuMua] SET [SoLuong] = @SoLuong,[DonGia] = @DonGia ,[ThanhTien] = @ThanhTien WHERE [IDHangHoa] = @IDHangHoa AND [SoDonHang] = @SoDonHang";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@SoDonHang", SoDonHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
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
        public void ThemChiTietDonHang_Duyet(object IDDonHangThuMua, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string SoLuong, string DonGia, string ThanhTien, string ChenhLech, string ThucTe)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_DuyetHangThuMua_ChiTiet] ([IDDonHangThuMua],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[DonGia],[ThanhTien],[ChenhLech],[ThucTe]) VALUES (@IDDonHangThuMua,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@DonGia,@ThanhTien,@ChenhLech,@ThucTe)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ThucTe", ThucTe);
                        myCommand.Parameters.AddWithValue("@ChenhLech", ChenhLech);
                        myCommand.Parameters.AddWithValue("@IDDonHangThuMua", IDDonHangThuMua);
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
        public object ThemDuyetDonHang(string SoDonHang, string IDNguoiLap, string TongTrongLuong, string TongTien, string IDNguoiDuyet, string GhiChu, DateTime NgayLap, DateTime NgayDuyet, string IDNhaCungCap, int IDTrangThaiDonHang)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    object IDPhieuKiemKho = null;
                    string cmdText = "INSERT INTO [GPM_DuyetHangThuMua] ([SoDonHang],[NgayCapNhat],[IDNguoiLap],[TongTrongLuong],[TongTien],[IDNhaCungCap],[IDNguoiDuyet],[GhiChu],[NgayLap],[NgayDuyet],[IDTrangThaiDonHang]) OUTPUT INSERTED.ID VALUES (@SoDonHang,getdate(),@IDNguoiLap,@TongTrongLuong,@TongTien,@IDNhaCungCap,@IDNguoiDuyet,@GhiChu,@NgayLap,@NgayDuyet,@IDTrangThaiDonHang)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@SoDonHang", SoDonHang);
                        myCommand.Parameters.AddWithValue("@IDTrangThaiDonHang", IDTrangThaiDonHang);
                        myCommand.Parameters.AddWithValue("@IDNguoiLap", IDNguoiLap);
                        myCommand.Parameters.AddWithValue("@TongTrongLuong", TongTrongLuong);
                        myCommand.Parameters.AddWithValue("@TongTien", TongTien);
                        myCommand.Parameters.AddWithValue("@IDNhaCungCap", IDNhaCungCap);
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
        public void CapNhatChiTietDonHang(string ID, string IDHangHoa, int ThucTe, float DonGia, float ThanhTien,int ChenhLech)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_DuyetHangThuMua_Temp] SET [ChenhLech] = @ChenhLech,[ThucTe] = @ThucTe,[DonGia] = @DonGia,[ThanhTien] = @ThanhTien WHERE [IDHangHoa] = @IDHangHoa AND [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ChenhLech", ChenhLech);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@ThucTe", ThucTe);
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
        public DataTable DanhSachChiTiet_Temp(string IDDonHangThuMua)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DuyetHangThuMua_Temp] WHERE IDDonHangThuMua =" + IDDonHangThuMua;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        //public DataTable DanhSachChiTiet_LOG_Temp(string SoDonHang)
        //{
        //    using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        con.Open();
        //        string cmdText = "SELECT * FROM [GPM_Log_DuyetHangThuMua] WHERE SoDonHang = '" + SoDonHang + "'";
        //        using (SqlCommand command = new SqlCommand(cmdText, con))
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            DataTable tb = new DataTable();
        //            tb.Load(reader);
        //            return tb;
        //        }
        //    }
        //}
        public void ThemChiTietDonHang_Temp(string IDDonHangThuMua, string MaHang, string IDHangHoa, string IDDonViTinh, string TrongLuong, string SoLuong, string DonGia)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_DuyetHangThuMua_Temp] ([IDDonHangThuMua],[MaHang],[IDHangHoa],[IDDonViTinh],[TrongLuong],[SoLuong],[DonGia]) VALUES (@IDDonHangThuMua,@MaHang,@IDHangHoa,@IDDonViTinh,@TrongLuong,@SoLuong,@DonGia)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDDonHangThuMua", IDDonHangThuMua);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@DonGia", DonGia);
                       // myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
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
        public DataTable DanhSachChiTiet(string IDDonHangThuMua)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_ThuMua_DonHang_ChiTiet] WHERE IDDonHangThuMua =" + IDDonHangThuMua;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachChiTiet_LOG(string SoDonHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_Log_DuyetHangThuMua] WHERE SoDonHang =N'" + SoDonHang + "'";
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
                string cmdText = "SELECT * FROM [GPM_ThuMua_DonHang] WHERE ID = '" + ID + "' ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
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
                    string strSQL = " DELETE [GPM_DuyetHangThuMua_Temp]";
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
    }
}