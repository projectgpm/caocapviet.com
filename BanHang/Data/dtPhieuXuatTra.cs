using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtPhieuXuatTra
    {

        public DataTable DanhSachPhieuXuatTra()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_PhieuXuatTra] WHERE [IDKho] is not null ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        //public void CapNhatChiTietPhieuXuatTra_ID(int ID, int SoLuong, float GiaMua, float ThanhTien)
        //{
        //    using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        try
        //        {
        //            myConnection.Open();
        //            string strSQL = "UPDATE [GPM_ChiTietPhieuXuatTra] SET [SoLuong] = @SoLuong,[GiaMua] = @GiaMua,[ThanhTien] = @ThanhTien WHERE [ID] = @ID";
        //            using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
        //            {
        //                myCommand.Parameters.AddWithValue("@ID", ID);
        //                myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
        //                myCommand.Parameters.AddWithValue("@GiaMua", GiaMua);
        //                myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
        //                myCommand.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            throw new Exception("Lỗi: Quá trình Xóa dữ liệu gặp lỗi, hãy tải lại trang");
        //        }
        //    }
        //}
        //public void CapNhatTongTienPhieuXuatTra_ID(int ID, float TongTien)
        //{
        //    using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        try
        //        {
        //            myConnection.Open();
        //            string strSQL = "UPDATE [GPM_PhieuXuatTra] SET [TongTien] = @TongTien,[NgayCapNhat] = getdate() WHERE [ID] = @ID";
        //            using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
        //            {
        //                myCommand.Parameters.AddWithValue("@ID", ID);
        //                myCommand.Parameters.AddWithValue("@TongTien", TongTien);
        //                myCommand.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            throw new Exception("Lỗi: Quá trình Xóa dữ liệu gặp lỗi, hãy tải lại trang");
        //        }
        //    }
        //}
        public DataTable DanhSachChiTietPhieuXuatTra_ID(string IDPhieuXuatTra)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT [GPM_ChiTietPhieuXuatTra].* FROM [GPM_ChiTietPhieuXuatTra] WHERE [IDPhieuXuatTra] = '" + IDPhieuXuatTra + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        //public DataTable DanhSachChiTietPhieuXuatTra(int ID)
        //{
        //    using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        con.Open();
        //        string cmdText = " SELECT [GPM_ChiTietPhieuXuatTra].* FROM [GPM_ChiTietPhieuXuatTra] WHERE[GPM_ChiTietPhieuXuatTra].ID = '" + ID + "' ";
        //        using (SqlCommand command = new SqlCommand(cmdText, con))
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            DataTable tb = new DataTable();
        //            tb.Load(reader);
        //            return tb;
        //        }
        //    }
        //}
        //public void XoaPhieuXuatTra_Null()
        //{
        //    using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        try
        //        {
        //            myConnection.Open();
        //            string strSQL = "DELETE [GPM_PhieuXuatTra] WHERE [IDKho] is null AND [IDNhaCungCap] is null AND [NgayLap] is null ";
        //            using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
        //            {
        //                myCommand.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
        //        }
        //    }
        //}
        public object ThemPhieuXuatTra_Temp()
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    object IDPhieuNhapSi = null;
                    string cmdText = "INSERT INTO [GPM_PhieuXuatTra] ([NgayCapNhat]) OUTPUT INSERTED.ID VALUES (getdate())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        IDPhieuNhapSi = myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                    return IDPhieuNhapSi;
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public void XoaPhieuXuatTra_Temp(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_PhieuXuatTra] WHERE [ID] = '" + ID + "'";
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
        public void XoaChiTietPhieuXuatTra_Temp(string IDPhieuXuatTra)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_ChiTietPhieuXuatTra_Temp] WHERE [IDPhieuXuatTra] = '" + IDPhieuXuatTra + "'";
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

            //public DataTable DanhSachPhieuXuatSi(int ID)
            //{
            //    using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            //    {
            //        con.Open();
            //        string cmdText = " SELECT * FROM [GPM_PhieuXuatSi] WHERE [ID] = '" + ID + "'";
            //        using (SqlCommand command = new SqlCommand(cmdText, con))
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            DataTable tb = new DataTable();
            //            tb.Load(reader);
            //            return tb;
            //        }
            //    }
            //}
            //public DataTable DanhSachPhieuNhapSi_IDNhaCungCap(int IDNhaCungCap)
            //{
            //    using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            //    {
            //        con.Open();
            //        string cmdText = "  SELECT [GPM_HangHoaTonKho].[SoLuongCon],[GPM_HangHoa].ID, [GPM_HangHoa].TenHangHoa,[GPM_HangHoa].GiaBan1,[GPM_HangHoa].GiaMua,[GPM_HangHoa].IDDonViTinh FROM [GPM_HangHoa],[GPM_NhaCungCap],[GPM_HangHoaTonKho] WHERE [GPM_HangHoa].IDNhaCungCap = [GPM_NhaCungCap].ID AND [GPM_HangHoaTonKho].IDHangHoa = [GPM_HangHoa].ID AND [GPM_NhaCungCap].ID =  '" + IDNhaCungCap + "' AND IDKho = '" + IDKho + "'";
            //        using (SqlCommand command = new SqlCommand(cmdText, con))
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            DataTable tb = new DataTable();
            //            tb.Load(reader);
            //            return tb;
            //        }
            //    }
            //}
            //public DataTable DanhSachHangHoa(int IDHangHoa)
            //{
            //    using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            //    {
            //        con.Open();
            //        string cmdText = "  SELECT [GPM_HangHoaTonKho].[SoLuongCon],[GPM_HangHoa].ID, [GPM_HangHoa].TenHangHoa,[GPM_HangHoa].GiaMua,[GPM_HangHoa].GiaMua,[GPM_HangHoa].IDDonViTinh FROM [GPM_HangHoa],[GPM_HangHoaTonKho] WHERE [GPM_HangHoaTonKho].IDHangHoa = [GPM_HangHoa].ID AND [GPM_HangHoa].ID = '" + IDHangHoa + "' AND IDKho = '" + IDKho + "'";
            //        using (SqlCommand command = new SqlCommand(cmdText, con))
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            DataTable tb = new DataTable();
            //            tb.Load(reader);
            //            return tb;
            //        }
            //    }
            //}

            //public DataTable DanhSachPhieuNhapSi(int IDNhaCungCap)
            //{
            //    using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            //    {
            //        con.Open();
            //        string cmdText = "SELECT * FROM [GPM_NhaCungCap] WHERE ID = '" + IDNhaCungCap + "'";
            //        using (SqlCommand command = new SqlCommand(cmdText, con))
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            DataTable tb = new DataTable();
            //            tb.Load(reader);
            //            return tb;
            //        }
            //    }
            //}
            //public DataTable DanhSachHangHoa_IDPhieuNhapSi(int IDPhieuNhapSi)
            //{
            //    using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            //    {
            //        con.Open();
            //        string cmdText = "   SELECT [GPM_ChiTietPhieuNhapSi].*,[GPM_HangHoa].TenHangHoa FROM [GPM_ChiTietPhieuNhapSi],[GPM_HangHoa] WHERE [GPM_HangHoa].ID = [GPM_ChiTietPhieuNhapSi].IDHangHoa AND [GPM_ChiTietPhieuNhapSi].IDPhieuNhapSi = '" + IDPhieuNhapSi + "'";
            //        using (SqlCommand command = new SqlCommand(cmdText, con))
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            DataTable tb = new DataTable();
            //            tb.Load(reader);
            //            return tb;
            //        }
            //    }
            //}

            //public DataTable DanhSachChiTietPhieuNhapSi_ID(int ID)
            //{
            //    using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            //    {
            //        con.Open();
            //        string cmdText = "  SELECT [GPM_ChiTietPhieuNhapSi].*,[GPM_HangHoa].TenHangHoa,[GPM_HangHoaTonKho].SoLuongCon FROM [GPM_ChiTietPhieuNhapSi],[GPM_HangHoa],[GPM_HangHoaTonKho] WHERE [GPM_HangHoa].ID = [GPM_ChiTietPhieuNhapSi].IDHangHoa AND [GPM_HangHoaTonKho].IDHangHoa = [GPM_ChiTietPhieuNhapSi].IDHangHoa  AND [GPM_ChiTietPhieuNhapSi].ID = '" + ID + "'";
            //        using (SqlCommand command = new SqlCommand(cmdText, con))
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            DataTable tb = new DataTable();
            //            tb.Load(reader);
            //            return tb;
            //        }
            //    }
            //}
        public DataTable LayDanhSachPhieuXuatTra_Temp(string IDPhieuXuatTra)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_ChiTietPhieuXuatTra_Temp] WHERE [IDPhieuXuatTra] = '" + IDPhieuXuatTra + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void CapNhatPhieuXuatTra_ID(string ID, string IDKho, string IDNhaCungCap, float TongTien, DateTime NgayLap, string GhiChu, string IDNhanVien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_PhieuXuatTra] SET [IDNhanVien] = @IDNhanVien,[IDKho] = @IDKho, [IDNhaCungCap] = @IDNhaCungCap, [TongTien] = @TongTien,[NgayLap] = @NgayLap,[GhiChu] = @GhiChu,[NgayCapNhat] = getdate() WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@IDNhaCungCap", IDNhaCungCap);
                        myCommand.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                        myCommand.Parameters.AddWithValue("@NgayLap", NgayLap);
                        myCommand.Parameters.AddWithValue("@TongTien", TongTien);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@ID", ID);
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
        public DataTable KTChiTietPhieuXuatTra_Temp(string IDHangHoa, string IDPhieuXuatTra)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_ChiTietPhieuXuatTra_Temp] WHERE [IDHangHoa]= '" + IDHangHoa + "' AND [IDPhieuXuatTra] = '" + IDPhieuXuatTra + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void ThemChiTietPhieuXuatTra_Temp(string IDPhieuXuatTra, string IDHangHoa, string IDDonViTinh, int SoLuong, float GiaMua, float ThanhTien,string MaHang)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_ChiTietPhieuXuatTra_Temp] ([IDPhieuXuatTra],[IDHangHoa],[IDDonViTinh],[SoLuong],[GiaMua],[ThanhTien],[MaHang]) VALUES (@IDPhieuXuatTra,@IDHangHoa,@IDDonViTinh,@SoLuong,@GiaMua,@ThanhTien,@MaHang)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDPhieuXuatTra", IDPhieuXuatTra);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@GiaMua", GiaMua);
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
        public void UpdatePhieuXuatTra_temp(string IDPhieuXuatTra, string IDHangHoa, int SoLuong, float GiaMua, float ThanhTien)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_ChiTietPhieuXuatTra_Temp] SET [IDPhieuXuatTra] = @IDPhieuXuatTra,[SoLuong] = @SoLuong,[GiaMua] = @GiaMua,[ThanhTien] = @ThanhTien WHERE [IDHangHoa] = @IDHangHoa";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDPhieuXuatTra", IDPhieuXuatTra);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@GiaMua", GiaMua);
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
        public void XoaChiTietPhieuXuatTra_Temp_ID(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_ChiTietPhieuXuatTra_Temp] WHERE ID = @ID";
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
            //public DataTable DanhSachHangHoa_IDNhaCungCap(int IDNhaCungCap)
            //{
            //    using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            //    {
            //        con.Open();
            //        string cmdText = " SELECT  [GPM_HangHoa].ID,[GPM_HangHoa].TenHangHoa, SUM([GPM_ChiTietPhieuNhapSi].SoLuong) as TongSoLuong FROM [GPM_ChiTietPhieuNhapSi],[GPM_HangHoa] WHERE [GPM_HangHoa].ID = [GPM_ChiTietPhieuNhapSi].IDHangHoa AND  [GPM_ChiTietPhieuNhapSi].IDNhaCungCap = '" + IDNhaCungCap + "' GROUP BY [GPM_HangHoa].TenHangHoa,[GPM_HangHoa].ID";
            //        using (SqlCommand command = new SqlCommand(cmdText, con))
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            DataTable tb = new DataTable();
            //            tb.Load(reader);
            //            return tb;
            //        }
            //    }
            //}

        public void ThemChiTietPhieuXuatTra(string IDPhieuXuatTra, string IDHangHoa, string IDDonViTinh, string SoLuong, string GiaMua, string ThanhTien,string MaHang,string SoLuongCon)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_ChiTietPhieuXuatTra] ([IDPhieuXuatTra],[IDHangHoa],[IDDonViTinh],[SoLuong],[GiaMua],[ThanhTien],[SoLuongCon],[MaHang]) VALUES (@IDPhieuXuatTra,@IDHangHoa,@IDDonViTinh,@SoLuong,@GiaMua,@ThanhTien,@SoLuongCon,@MaHang)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@SoLuongCon", SoLuongCon);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDPhieuXuatTra", IDPhieuXuatTra);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@GiaMua", GiaMua);
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

       
    }
}