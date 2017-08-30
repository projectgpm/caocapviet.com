using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dataHangHoa
    {
        public DataTable getData(string cmd)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(cmd, con))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
                catch (Exception) { return new DataTable(); }
            }
        }

        public DataTable getDanhSachHangHoa()
        {
            string cmd = "SELECT * FROM [GPM_HANGHOA] WHERE [DAXOA] = 0 AND IDTrangThaiHang < 5";
            return getData(cmd);
        }

        public string getTenHH(string ID)
        {
            string cmd = "SELECT TenHangHoa FROM [GPM_HANGHOA] WHERE ID = '" + ID + "'";
            DataTable da =  getData(cmd);
            if (da.Rows.Count != 0)
            {
                return da.Rows[0]["TenHangHoa"].ToString();
            }
            return "-";
        }

        public DataTable getDanhSachHangHoa_ID(string ID)
        {
            string cmd = "SELECT * FROM [GPM_HANGHOA] WHERE ID = '" + ID + "' AND TenHangHoa is not null";
            return getData(cmd);
        }

        public DataTable getDanhSachHangHoa_MaHang(string MaHang)
        {
            string cmd = "SELECT [GPM_HANGHOA].ID,[GPM_HANGHOA].TrongLuong,[GPM_HangHoaTonKho].GiaBan,[GPM_HangHoaTonKho].SoLuongCon FROM [GPM_HangHoaTonKho],[GPM_HANGHOA] WHERE [GPM_HangHoaTonKho].IDHangHoa = [GPM_HANGHOA].ID AND [GPM_HANGHOA].MaHang = '" + MaHang + "' AND [GPM_HANGHOA].TenHangHoa is not null";
            return getData(cmd);
        }

        public DataTable getChietHangHoaTonKho_ID(string ID)
        {
            string cmd = "SELECT * FROM [GPM_ChiTietHangHoaTonKho] WHERE ID = '" + ID + "'";
            return getData(cmd);
        }

        public DataTable getDanhSachHangHoa_TonKho_ID(string ID)
        {
            string cmd = "SELECT [GPM_HANGHOA].*,[GPM_HANGHOATONKHO].* FROM [GPM_HANGHOA],[GPM_HANGHOATONKHO] WHERE [GPM_HANGHOATONKHO].IDHangHoa = [GPM_HANGHOA].ID AND [GPM_HANGHOA].ID = '" + ID + "' AND [GPM_HANGHOA].TenHangHoa is not null";
            return getData(cmd);
        }

        public DataTable KiemTraHangHoa_MaHang(string MaHang)
        {
            string cmd = "SELECT * FROM [GPM_HANGHOA] WHERE MaHang = '" + MaHang + "'";
            return getData(cmd);
        }

        public DataTable KiemTraBarcode(string ID, string Barcode)
        {
            string cmd = "SELECT * FROM [GPM_HangHoa_Barcode] WHERE Barcode = '" + Barcode + "' AND IDHangHoa = '" + ID + "'";
            return getData(cmd);
        }

        public int KiemTraHangHoa_Null(string ID)
        {
            string cmd = "SELECT * FROM [GPM_HANGHOA] WHERE ID = '" + ID + "' AND TenHangHoa is null";
            DataTable data = getData(cmd);
            if (data.Rows.Count != 0)
            {
                return 0;
            }
            return -1;
        }

        public static string LayIDTrangThaiHang(string Ten)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_TRANGTHAIHANG] WHERE [TenTrangThai] = '" + Ten + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return tb.Rows[0]["ID"].ToString();
                    }
                    return 1 + "";
                }
            }
        }

        public static string LayIDTrangThaiBarcode(string Ten)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_TrangThai_Barcode] WHERE [TenTrangThai] = '" + Ten + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return tb.Rows[0]["ID"].ToString();
                    }
                    return 1 + "";
                }
            }
        }

        public DataTable getDanhSachHangHoa_Full_Barcode()
        {
            dataHangHoa d = new dataHangHoa();
            string cmd = "select GPM_NhomHang.TenNhomHang, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa,GPM_HangHoa.HeSo, GPM_DonViTinh.TenDonViTinh, GPM_HangSanXuat.TenHangSanXuat, GPM_Thue.TenThue, GPM_HangHoa.IDHangQuyDoi, GPM_NhomDatHang.TenNhom, GPM_HangHoa.GiaMuaTruocThue, GPM_HangHoa.GiaBanTruocThue, GPM_HangHoa.GiaMuaSauThue, GPM_HangHoa.GiaBanSauThue, GPM_HangHoa.GiaBan1, GPM_HangHoa.GiaBan2, GPM_HangHoa.GiaBan3, GPM_HangHoa.GiaBan4, GPM_HangHoa.GiaBan5, GPM_HangHoa.TrongLuong, GPM_HangHoa.HanSuDung, GPM_TrangThaiHang.TenTrangThai as TrangThaiHang, GPM_HangHoa.GhiChu, GPM_TrangThai_Barcode.TenTrangThai as TrangThaiBarcode, GPM_HangHoa_Barcode.Barcode from GPM_NhomHang, GPM_HangHoa, GPM_DonViTinh, GPM_HangSanXuat, GPM_Thue, GPM_NhomDatHang, GPM_TrangThaiHang, GPM_HangHoa_Barcode, GPM_TrangThai_Barcode where GPM_HangHoa.IDNhomHang = GPM_NhomHang.ID and GPM_HangHoa.IDDonViTinh = GPM_DonViTinh.ID and GPM_HangHoa.IDHangSanXuat = GPM_HangSanXuat.ID and GPM_HangHoa.IDThue = GPM_Thue.ID and GPM_HangHoa.IDNhomDatHang = GPM_NhomDatHang.ID and GPM_HangHoa.IDTrangThaiHang = GPM_TrangThaiHang.ID and GPM_HangHoa_Barcode.IDHangHoa = GPM_HangHoa.ID and GPM_HangHoa_Barcode.IDTrangThaiBarcode = GPM_TrangThai_Barcode.ID and GPM_HangHoa.DaXoa = 0 and GPM_HangHoa.IDNhomHang < 5";
            DataTable data = getData(cmd);
            DataTable dataNews = new DataTable();
            dataNews.Columns.Add("NhomHang", typeof(string));
            dataNews.Columns.Add("MaHang", typeof(string));
            dataNews.Columns.Add("TenHangHoa", typeof(string));
            dataNews.Columns.Add("DonViTinh", typeof(string));
            dataNews.Columns.Add("HeSo", typeof(string));
            dataNews.Columns.Add("HangSanXuat", typeof(string));
            dataNews.Columns.Add("Thue", typeof(string));
            dataNews.Columns.Add("HangQuyDoi", typeof(string));
            dataNews.Columns.Add("NhomDatHang", typeof(string));
            dataNews.Columns.Add("GiaMuaTruocThue", typeof(string));
            dataNews.Columns.Add("GiaBanTruocThue", typeof(string));
            dataNews.Columns.Add("GiaMuaSauThue", typeof(string));
            dataNews.Columns.Add("GiaBanSauThue", typeof(string));
            dataNews.Columns.Add("GiaBan1", typeof(string));
            dataNews.Columns.Add("GiaBan2", typeof(string));
            dataNews.Columns.Add("GiaBan3", typeof(string));
            dataNews.Columns.Add("GiaBan4", typeof(string));
            dataNews.Columns.Add("GiaBan5", typeof(string));
            dataNews.Columns.Add("TrongLuong", typeof(string));
            dataNews.Columns.Add("HanSuDung", typeof(string));
            dataNews.Columns.Add("TrangThaiHang", typeof(string));
            dataNews.Columns.Add("GhiChu", typeof(string));
            dataNews.Columns.Add("TrangThaiBarcode", typeof(string));
            dataNews.Columns.Add("Barcode", typeof(string));

            if (data.Rows.Count != 0)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    DataRow dr = data.Rows[i];
                    string TenQuyDoi = d.getTenHH(dr["IDHangQuyDoi"].ToString());
                    string NhomHang = dr["TenNhomHang"].ToString();
                    string MaHang = dr["MaHang"].ToString();
                    string TenHangHoa = dr["TenHangHoa"].ToString();
                    string DonViTinh = dr["TenDonViTinh"].ToString();
                    string HeSo = dr["HeSo"].ToString();
                    string HangSanXuat = dr["TenHangSanXuat"].ToString();
                    string Thue = dr["TenThue"].ToString();
                    string HangQuyDoi = TenQuyDoi;
                    string NhomDatHang = dr["TenNhom"].ToString();
                    string GiaMuaTruocThue = dr["GiaMuaTruocThue"].ToString();
                    string GiaBanTruocThue = dr["GiaBanTruocThue"].ToString();
                    string GiaMuaSauThue = dr["GiaMuaSauThue"].ToString();
                    string GiaBanSauThue = dr["GiaBanSauThue"].ToString();
                    string GiaBan1 = dr["GiaBan1"].ToString();
                    string GiaBan2 = dr["GiaBan2"].ToString();
                    string GiaBan3 = dr["GiaBan3"].ToString();
                    string GiaBan4 = dr["GiaBan4"].ToString();
                    string GiaBan5 = dr["GiaBan5"].ToString();
                    string TrongLuong = dr["TrongLuong"].ToString();
                    string HanSuDung = dr["HanSuDung"].ToString();
                    string TrangThaiHang = dr["TrangThaiHang"].ToString();
                    string GhiChu = dr["GhiChu"].ToString();
                    string TrangThaiBarcode = dr["TrangThaiBarcode"].ToString();
                    string Barcode = dr["Barcode"].ToString();

                    dataNews.Rows.Add(NhomHang, MaHang, TenHangHoa, DonViTinh, HeSo, HangSanXuat, Thue, HangQuyDoi, NhomDatHang,
                        GiaMuaTruocThue, GiaBanTruocThue, GiaMuaSauThue, GiaBanSauThue, GiaBan1, GiaBan2, GiaBan3, GiaBan4, GiaBan5,
                        TrongLuong, HanSuDung, TrangThaiHang, GhiChu, TrangThaiBarcode, Barcode);
                }
            }
            return dataNews;
        }

        public void XoaHangHoa(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HANGHOA] SET [DAXOA] = 1 WHERE [ID] = @ID";
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

        public void XoaHangHoa_Delete(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "Delete from [GPM_HANGHOA] WHERE [ID] = @ID and TenHangHoa is null";
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

        public object insertHangHoa_Temp()
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object IDHangHoa = null;
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HANGHOA] ([MaHang])" +
                                     " OUTPUT INSERTED.ID" +
                                     " VALUES (@MaHang)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@MaHang", "00000");
                        IDHangHoa = myCommand.ExecuteScalar();
                    }
                    myConnection.Close();

                    return IDHangHoa;
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }


        public void SuaThongTinHangHoa(string ID, string IDNhomHang, string MaHang, string TenHangHoa, string IDDonViTinh, string HeSo, string IDHangSanXuat, string IDThue, string IDHangQuyDoi, string IDNhomDatHang, string GiaMuaTruocThue, string GiaBanTruocThue, string GiaMuaSauThue, string GiaBanSauThue, string GiaBan1, string GiaBan2, string GiaBan3, string GiaBan4, string GiaBan5, string TrongLuong, string HanSuDung, string IDTrangThaiHang, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HANGHOA] SET [IDNhomHang] = @IDNhomHang,[MaHang] = @MaHang, [TenHangHoa] = @TenHangHoa, [IDDonViTinh] = @IDDonViTinh,[HeSo] = @HeSo, [IDHangSanXuat] = @IDHangSanXuat,[IDThue] = @IDThue,[IDHangQuyDoi] = @IDHangQuyDoi,[IDNhomDatHang] = @IDNhomDatHang,[GiaMuaTruocThue] = @GiaMuaTruocThue,[GiaBanTruocThue] = @GiaBanTruocThue, [GiaMuaSauThue] = @GiaMuaSauThue,[GiaBanSauThue] = @GiaBanSauThue, [GiaBan1] = @GiaBan1, [GiaBan2] = @GiaBan2, [GiaBan3] = @GiaBan3, [GiaBan4] = @GiaBan4, [GiaBan5] = @GiaBan5, [TrongLuong] = @TrongLuong,[HanSuDung] = @HanSuDung,[IDTrangThaiHang] = @IDTrangThaiHang, [GhiChu] = @GhiChu, [NgayCapNhat] = getDATE() WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@IDNhomHang", IDNhomHang);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@TenHangHoa", TenHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@HeSo", HeSo);
                        myCommand.Parameters.AddWithValue("@IDHangSanXuat", IDHangSanXuat);
                        myCommand.Parameters.AddWithValue("@IDThue", IDThue);
                        myCommand.Parameters.AddWithValue("@IDHangQuyDoi", IDHangQuyDoi);
                        myCommand.Parameters.AddWithValue("@IDNhomDatHang", IDNhomDatHang);
                        myCommand.Parameters.AddWithValue("@GiaMuaTruocThue", GiaMuaTruocThue);
                        myCommand.Parameters.AddWithValue("@GiaBanTruocThue", GiaBanTruocThue);
                        myCommand.Parameters.AddWithValue("@GiaMuaSauThue", GiaMuaSauThue);
                        myCommand.Parameters.AddWithValue("@GiaBanSauThue", GiaBanSauThue);
                        myCommand.Parameters.AddWithValue("@GiaBan1", GiaBan1);
                        myCommand.Parameters.AddWithValue("@GiaBan2", GiaBan2);
                        myCommand.Parameters.AddWithValue("@GiaBan3", GiaBan3);
                        myCommand.Parameters.AddWithValue("@GiaBan4", GiaBan4);
                        myCommand.Parameters.AddWithValue("@GiaBan5", GiaBan5);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@HanSuDung", HanSuDung);
                        myCommand.Parameters.AddWithValue("@IDTrangThaiHang", IDTrangThaiHang);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

        public void SuaThongTinHangHoa_HangQuyDoi(string MaHang, string IDHangQuyDoi)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HANGHOA] SET [IDHangQuyDoi] = @IDHangQuyDoi WHERE [MaHang] = @MaHang AND IDHangQuyDoi = -1";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDHangQuyDoi", IDHangQuyDoi);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

        public object insertHangHoa(string IDNhomHang, string MaHang, string TenHangHoa, string IDDonViTinh, string HeSo, string IDHangSanXuat, string IDThue, string IDHangQuyDoi, string IDNhomDatHang, string GiaMuaTruocThue, string GiaBanTruocThue, string GiaMuaSauThue, string GiaBanSauThue, string GiaBan1, string GiaBan2, string GiaBan3, string GiaBan4, string GiaBan5, string TrongLuong, string HanSuDung, string IDTrangThaiHang, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object IDHH = -1;
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangHoa] ([IDNhomHang], [MaHang], [TenHangHoa], [IDDonViTinh],[HeSo], [IDHangSanXuat], [IDThue],[IDHangQuyDoi],[IDNhomDatHang],[GiaMuaTruocThue],[GiaBanTruocThue],[GiaMuaSauThue],[GiaBanSauThue], [GiaBan1], [GiaBan2], [GiaBan3], [GiaBan4], [GiaBan5], [TrongLuong], [HanSuDung], [IDTrangThaiHang], [GhiChu])" +
                                     " OUTPUT INSERTED.ID" +
                                     " VALUES (@IDNhomHang,@MaHang,@TenHangHoa,@IDDonViTinh,@HeSo,@IDHangSanXuat,@IDThue,@IDHangQuyDoi,@IDNhomDatHang,@GiaMuaTruocThue,@GiaBanTruocThue,@GiaMuaSauThue,@GiaBanSauThue, @GiaBan1,@GiaBan2, @GiaBan3,@GiaBan4,@GiaBan5,@TrongLuong,@HanSuDung,@IDTrangThaiHang,@GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDNhomHang", IDNhomHang);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@TenHangHoa", TenHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@HeSo", HeSo);
                        myCommand.Parameters.AddWithValue("@IDHangSanXuat", IDHangSanXuat);
                        myCommand.Parameters.AddWithValue("@IDThue", IDThue);
                        myCommand.Parameters.AddWithValue("@IDHangQuyDoi", IDHangQuyDoi);
                        myCommand.Parameters.AddWithValue("@IDNhomDatHang", IDNhomDatHang);
                        myCommand.Parameters.AddWithValue("@GiaMuaTruocThue", GiaMuaTruocThue);
                        myCommand.Parameters.AddWithValue("@GiaBanTruocThue", GiaBanTruocThue);
                        myCommand.Parameters.AddWithValue("@GiaMuaSauThue", GiaMuaSauThue);
                        myCommand.Parameters.AddWithValue("@GiaBanSauThue", GiaBanSauThue);
                        myCommand.Parameters.AddWithValue("@GiaBan1", GiaBan1);
                        myCommand.Parameters.AddWithValue("@GiaBan2", GiaBan2);
                        myCommand.Parameters.AddWithValue("@GiaBan3", GiaBan3);
                        myCommand.Parameters.AddWithValue("@GiaBan4", GiaBan4);
                        myCommand.Parameters.AddWithValue("@GiaBan5", GiaBan5);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@HanSuDung", HanSuDung);
                        myCommand.Parameters.AddWithValue("@IDTrangThaiHang", IDTrangThaiHang);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        IDHH = myCommand.ExecuteScalar();
                    }
                    return IDHH;
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }

        public DataTable GetListBarCode(object ID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_HangHoa_Barcode] WHERE [IDHangHoa] = @IDHangHoa";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    command.Parameters.AddWithValue("@IDHangHoa", ID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            return dt;
        }


        public void CapNhatBarCode(int ID, object IDHangHoa, string IDTrangThaiBarcode, string BarCode)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_HangHoa_Barcode] SET [IDHangHoa] = @IDHangHoa,[IDTrangThaiBarcode] = @IDTrangThaiBarcode, [BarCode] = @BarCode,[NgayCapNhat] = getdate() WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@BarCode", BarCode);
                        myCommand.Parameters.AddWithValue("@IDTrangThaiBarcode", IDTrangThaiBarcode);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

        public void ThemBarCode(object IDHangHoa, string IDTrangThaiBarcode, string BarCode)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangHoa_Barcode] ([IDHangHoa],[IDTrangThaiBarcode],[Barcode],[NgayCapNhat])" +
                             " VALUES(@IDHangHoa,@IDTrangThaiBarcode, @BarCode, getdate())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@IDTrangThaiBarcode", IDTrangThaiBarcode);
                        myCommand.Parameters.AddWithValue("@BarCode", BarCode);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

        public static string LayIDHangHoa(string TenHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_HANGHOA] WHERE [TenHangHoa] = N'" + TenHangHoa + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return tb.Rows[0]["ID"].ToString();
                    }
                    else return -1 + "";
                }
            }
        }

        public static string LayIDHangHoa_MaHang(string MaHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_HANGHOA] WHERE [MaHang] = N'" + MaHang + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return tb.Rows[0]["ID"].ToString();
                    }
                    else return -1 + "";
                }
            }
        }

        public void XoaBarCode(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "DELETE FROM [GPM_HangHoa_Barcode] WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
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

        public static int LayID_Max()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT Max(MaHang) as IDMax FROM [GPM_HANGHOA] ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["IDMax"].ToString());
                    }
                    else return -1;
                }
            }
        }



        public void ThemHangVaoTonKho(string IDKho, string IDHangHoa, string SoLuongCon, string GiaBan, string GiaBan1, string GiaBan2, string GiaBan3, string GiaBan4, string GiaBan5)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangHoaTonKho] ([IDKho],[IDHangHoa],[SoLuongCon],[GiaBan],[GiaBan1],[GiaBan2],[GiaBan3],[GiaBan4],[GiaBan5],[NgayCapNhat]) VALUES (@IDKho,@IDHangHoa,@SoLuongCon,@GiaBan,@GiaBan1,@GiaBan2,@GiaBan3,@GiaBan4,@GiaBan5,getDATE())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuongCon", SoLuongCon);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        myCommand.Parameters.AddWithValue("@GiaBan1", GiaBan1);
                        myCommand.Parameters.AddWithValue("@GiaBan2", GiaBan2);
                        myCommand.Parameters.AddWithValue("@GiaBan3", GiaBan3);
                        myCommand.Parameters.AddWithValue("@GiaBan4", GiaBan4);
                        myCommand.Parameters.AddWithValue("@GiaBan5", GiaBan5);
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