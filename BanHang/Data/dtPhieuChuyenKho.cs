using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtPhieuChuyenKho
    {
        public DataTable DanhSachPhieuChuyenKho_Tong()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_PhieuChuyenKho] WHERE IDKhoXuat is not null AND IDKhoNhap is not null AND SoMatHang is not null ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachChiTietPhieuChuyenKho(string IDPhieuChuyenKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GPM_HangHoa.IDDonViTinh,GPM_HangHoa.TenHangHoa,GPM_HangHoa.MaHang,GPM_ChiTietPhieuChuyenKho.* FROM GPM_ChiTietPhieuChuyenKho,GPM_HangHoa WHERE GPM_ChiTietPhieuChuyenKho.IDHangHoa = GPM_HangHoa.ID AND GPM_ChiTietPhieuChuyenKho.IDPhieuChuyenKho = '" + IDPhieuChuyenKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachPhieuChuyenKho_Kho(string IDPhieuChuyenKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT IDKhoXuat,IDKhoNhap FROM GPM_PhieuChuyenKho WHERE ID = '" + IDPhieuChuyenKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }


        public int TrangThaiPhieuChuyenKho(string IDPhieuChuyenKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT IDTrangThai FROM GPM_PhieuChuyenKho WHERE ID = '" + IDPhieuChuyenKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return Int32.Parse(tb.Rows[0]["IDTrangThai"].ToString());
                    }
                    return 1;
                }
            }
        }

        public DataTable ChiTietTongSoLuongHangHoa(string IDPhieuChuyenKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select IDPhieuChuyenKho, SUM(SoLuong) as TongSoLuong, SUM(TrongLuong) as TongTrongLuong, SUM(TongTien) as TongTien from GPM_ChiTietPhieuChuyenKho_Temp where IDPhieuChuyenKho = '" + IDPhieuChuyenKho + "' group by IDPhieuChuyenKho";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable ChiTietTongSoLuongHangHoa_2(string IDPhieuChuyenKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select IDPhieuChuyenKho, SUM(SoLuong) as TongSoLuong, SUM(TrongLuong) as TongTrongLuong, SUM(TongTien) as TongTien from GPM_ChiTietPhieuChuyenKho where IDPhieuChuyenKho = '" + IDPhieuChuyenKho + "' group by IDPhieuChuyenKho";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachPhieuChuyenKho_Client(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_PhieuChuyenKho] WHERE IDKhoXuat is not null AND IDKhoNhap is not null AND SoMatHang is not null AND (IDKhoXuat = '" + IDKho + "' OR IDKhoNhap = '" + IDKho + "') ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachChiTietPhieuChuyenKho_Temp(string IDPhieuChuyenKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GPM_HangHoa.IDDonViTinh,GPM_HangHoa.TenHangHoa,GPM_HangHoa.MaHang,GPM_ChiTietPhieuChuyenKho_Temp.* FROM GPM_ChiTietPhieuChuyenKho_Temp,GPM_HangHoa WHERE GPM_ChiTietPhieuChuyenKho_Temp.IDHangHoa = GPM_HangHoa.ID AND GPM_ChiTietPhieuChuyenKho_Temp.IDPhieuChuyenKho = '" + IDPhieuChuyenKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable KiemTraHangHoa_Temp(string IDPhieuChuyenKho, string IDHH)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM GPM_ChiTietPhieuChuyenKho_Temp WHERE IDPhieuChuyenKho = '" + IDPhieuChuyenKho + "' AND IDHangHoa = '" + IDHH + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public object ThemPhieuChuyenKho(string IDKho)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    object IDPhieuChuyenKho = null;
                    string cmdText = "INSERT INTO [GPM_PhieuChuyenKho] ([NgayLap],[IDKhoXuat]) OUTPUT INSERTED.ID VALUES (getdate(),'" + IDKho + "')";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        IDPhieuChuyenKho = myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                    return IDPhieuChuyenKho;
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }

        public void XoaPhieuChuyenKho_Temp(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_PhieuChuyenKho] WHERE [ID] = '" + ID + "'";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang.");
                }
            }
        }

        public void XoaChiTietPhieuChuyenKho_Temp(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_ChiTietPhieuChuyenKho_Temp] WHERE [ID] = '" + ID + "'";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang.");
                }
            }
        }

        public void XoaChiTietPhieuChuyenKho(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_ChiTietPhieuChuyenKho] WHERE [ID] = '" + ID + "'";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang.");
                }
            }
        }

        public void CapNhatPhieuChuyenKho(string ID, string IDKhoXuat, string IDKhoNhap, string IDNhanVienLap, string SoMatHang, string TrongLuong, string TongTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_PhieuChuyenKho set [IDKhoXuat] = @IDKhoXuat, [IDKhoNhap] = @IDKhoNhap, [IDNhanVienLap] = @IDNhanVienLap, [IDNhanVienXuat] = @IDNhanVienXuat, [IDNhanVienNhap] = @IDNhanVienNhap, [SoMatHang] = @SoMatHang, [TrongLuong] = @TrongLuong,[TongTien] = @TongTien where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@IDKhoXuat", IDKhoXuat);
                        myCommand.Parameters.AddWithValue("@IDKhoNhap", IDKhoNhap);
                        myCommand.Parameters.AddWithValue("@IDNhanVienLap", IDNhanVienLap);
                        myCommand.Parameters.AddWithValue("@IDNhanVienXuat", "0");
                        myCommand.Parameters.AddWithValue("@IDNhanVienNhap", "0");
                        myCommand.Parameters.AddWithValue("@SoMatHang", SoMatHang);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@TongTien", TongTien);
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

        public void CapNhatPhieuChuyenKho_2(string ID, string SoMatHang, string TrongLuong, string TongTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_PhieuChuyenKho set [SoMatHang] = @SoMatHang, [TrongLuong] = @TrongLuong,[TongTien] = @TongTien where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@SoMatHang", SoMatHang);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@TongTien", TongTien);
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

        public void DuyetChuyenKho(string IDPhieuChuyenKho, string TrangThai)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_PhieuChuyenKho set [IDTrangThai] = @IDTrangThai where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDTrangThai", TrangThai);
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

        public void DuyetChuyenKho_Xuat(string IDPhieuChuyenKho, string TrangThai, string IDNhanVienXuat)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_PhieuChuyenKho set [IDTrangThai] = @IDTrangThai,[IDNhanVienXuat] = @IDNhanVienXuat,[NgayXuat] = getDATE() where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDTrangThai", TrangThai);
                        myCommand.Parameters.AddWithValue("@IDNhanVienXuat", IDNhanVienXuat);
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

        public void DuyetChuyenKho_Nhap(string IDPhieuChuyenKho, string TrangThai, string IDNhanVienNhap)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_PhieuChuyenKho set [IDTrangThai] = @IDTrangThai,[IDNhanVienNhap] = @IDNhanVienNhap,[NgayNhap] = getDATE() where ID = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDTrangThai", TrangThai);
                        myCommand.Parameters.AddWithValue("@IDNhanVienNhap", IDNhanVienNhap);
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

        public void CapNhatChiTietPhieuChuyenKho_Temp(string IDPhieuChuyenKho, string IDHangHoa, string SoLuong, string TrongLuong, string GiaBan, string TongTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_ChiTietPhieuChuyenKho_Temp set [SoLuong] = @SoLuong, [TrongLuong] = @TrongLuong, [GiaBan] = @GiaBan, [TongTien] = @TongTien  where IDPhieuChuyenKho = @IDPhieuChuyenKho AND IDHangHoa = @IDHangHoa";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDPhieuChuyenKho", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        myCommand.Parameters.AddWithValue("@TongTien", TongTien);
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

        public void CapNhatChiTietPhieuChuyenKho(string IDPhieuChuyenKho, string IDHangHoa, string SoLuong, string TrongLuong, string TongTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_ChiTietPhieuChuyenKho set [SoLuong] = @SoLuong, [TongTien] = @TongTien, [TrongLuong] = @TrongLuong where IDPhieuChuyenKho = @IDPhieuChuyenKho AND IDHangHoa = @IDHangHoa";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDPhieuChuyenKho", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@TongTien", TongTien);
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

        public void ThemChiTietPhieuChuyenKho_Temp(string IDPhieuChuyenKho, string IDHangHoa, string SoLuong, string TrongLuong, string GiaBan, string TongTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_ChiTietPhieuChuyenKho_Temp] ([IDPhieuChuyenKho],[IDHangHoa],[SoLuong],[TrongLuong],[GiaBan],[TongTien]) VALUES (@IDPhieuChuyenKho,@IDHangHoa,@SoLuong,@TrongLuong,@GiaBan,@TongTien)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDPhieuChuyenKho", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        myCommand.Parameters.AddWithValue("@TongTien", TongTien);
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

        public void ThemChiTietPhieuChuyenKho(string IDPhieuChuyenKho, string IDHangHoa, string SoLuong, string TrongLuong, string GiaBan, string TongTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_ChiTietPhieuChuyenKho] ([IDPhieuChuyenKho],[IDHangHoa],[SoLuong],[TrongLuong],[GiaBan],[TongTien]) VALUES (@IDPhieuChuyenKho,@IDHangHoa,@SoLuong,@TrongLuong,@GiaBan,@TongTien)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDPhieuChuyenKho", IDPhieuChuyenKho);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        myCommand.Parameters.AddWithValue("@TongTien", TongTien);
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
    }
}