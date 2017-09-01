﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtHangCombo
    {
        public static string Dem_Max()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                int STTV = 0;
                string So;
                string GPM = "00000";
                string cmdText = "SELECT * FROM [GPM_HangHoa] WHERE [TenHangHoa] is not null";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    STTV = tb.Rows.Count + 1;
                    int DoDaiHT = STTV.ToString().Length;
                    string DoDaiGPM = GPM.Substring(0, 5 - DoDaiHT);
                    So = DoDaiGPM + STTV;
                    return So;
                }
            }
        }
        public void XoaHangCombo(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_HangHoa] SET [DAXOA] = 1 WHERE [ID] = @ID";
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
        public DataTable LayDanhSachKho()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_Kho] WHERE [DAXOA] = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void ThemBarCode(object IDHangHoa, string BarCode)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangHoa_Barcode] ([IDHangHoa],[Barcode],[NgayCapNhat])  VALUES(@IDHangHoa, @BarCode, getdate())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
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
        public void ThemHangVaoTonKho(object IDKho, int IDHangHoa, string SoLuongCon, string GiaBan, string GiaBan1, string GiaBan2, string GiaBan3, string GiaBan4, string GiaBan5)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangHoaTonKho] ([IDKho],[IDHangHoa],[SoLuongCon],[NgayCapNhat],[GiaBan],[GiaBan1],[GiaBan2],[GiaBan3],[GiaBan4],[GiaBan5]) VALUES (@IDKho,@IDHangHoa,@SoLuongCon,getdate(),@GiaBan,@GiaBan1,@GiaBan2,@GiaBan3,@GiaBan4,@GiaBan5)";
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
        public DataTable LayDanhSachHangHoa_BanHangCombo()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT [GPM_HANGHOA].ID,[GPM_HANGHOA].MaHang,[GPM_HANGHOA].TenHangHoa,[GPM_HANGHOA].GiaBanSauThue,[GPM_DonViTinh].TenDonViTinh FROM [GPM_HANGHOA],[GPM_DonViTinh] WHERE [GPM_HANGHOA].[DAXOA] = 0 AND ([GPM_HANGHOA].[IDTrangThaiHang] =1 OR [GPM_HANGHOA].[IDTrangThaiHang] = 3) AND [GPM_HANGHOA].IDDonViTinh = [GPM_DonViTinh].ID";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachHangHoaCombo(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT [GPM_HANGHOA].*,[GPM_NHOMHANG].TenNhomHang,[GPM_HangHoaTonKho].SoLuongCon FROM [GPM_HangHoaTonKho],[GPM_HANGHOA],[GPM_NHOMHANG] WHERE [GPM_HangHoaTonKho].IDKho = '" + IDKho + "'  AND  [GPM_HangHoaTonKho].IDHangHoa = [GPM_HANGHOA].ID   AND [GPM_NHOMHANG].ID = [GPM_HANGHOA].IDNhomHang  AND  GPM_HANGHOA.[DAXOA] = 0 AND IDTrangThaiHang > 4 AND TenHangHoa is not null";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public object ThemIDHangHoa_Temp()
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    object IDPhieuNhapSi = null;
                    string cmdText = "INSERT INTO [GPM_HangHoa] ([IDTrangThaiHang],[NgayCapNhat]) OUTPUT INSERTED.ID VALUES (5,getdate())";
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
        public DataTable DanhSachHangHoaCombo_Temp(int IDHangHoaComBo)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "  SELECT [GPM_DonViTinh].TenDonViTinh,[GPM_HangHoa_Combo_Temp].*,[GPM_HangHoa].TenHangHoa  FROM [GPM_HangHoa_Combo_Temp],[GPM_HangHoa],[GPM_DonViTinh] WHERE  [GPM_DonViTinh].ID = [GPM_HangHoa_Combo_Temp].IDDonViTinh  AND [GPM_HangHoa].ID = [GPM_HangHoa_Combo_Temp].IDHangHoa  AND [GPM_HangHoa_Combo_Temp].[IDHangHoaCombo] =  '" + IDHangHoaComBo + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public void ThemHangHoa_Temp(int IDHangHoaCombo, string IDHangHoa, int SoLuong, float GiaBanTruocThue, float ThanhTien, string MaHang, string IDDonViTinh, string TrongLuong, float GiaBanSauThue, float GiaMuaTruocThue, float GiaMuaSauThue, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();

                    string cmdText = "INSERT INTO [GPM_HangHoa_Combo_Temp] ([MaHang],[IDDonViTinh],[IDHangHoaCombo],[IDHangHoa],[SoLuong],[GiaBanTruocThue],[ThanhTien],[TrongLuong],[GiaBanSauThue],[GiaMuaTruocThue],[GiaMuaSauThue],[GhiChu]) VALUES (@MaHang,@IDDonViTinh,@IDHangHoaCombo,@IDHangHoa,@SoLuong,@GiaBanTruocThue,@ThanhTien,@TrongLuong,@GiaBanSauThue,@GiaMuaTruocThue,@GiaMuaSauThue,@GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@GiaMuaTruocThue", GiaMuaTruocThue);
                        myCommand.Parameters.AddWithValue("@GiaMuaSauThue", GiaMuaSauThue);
                        myCommand.Parameters.AddWithValue("@GiaBanSauThue", GiaBanSauThue);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@IDHangHoaCombo", IDHangHoaCombo);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@GiaBanTruocThue", GiaBanTruocThue);
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

        public void ThemHangHoa(int IDHangHoaCombo, string IDHangHoa, string SoLuong, string GiaBanTruocThue, string ThanhTien, string IDDonViTinh, string MaHang, string TrongLuong, string GiaBanSauThue, string GiaMuaTruocThue, string GiaMuaSauThue, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();

                    string cmdText = "INSERT INTO [GPM_HangHoa_Combo] ([IDHangHoaCombo],[IDHangHoa],[SoLuong],[GiaBanTruocThue],[ThanhTien],[IDDonViTinh],[MaHang],[TrongLuong],[GiaBanSauThue],[GiaMuaTruocThue],[GiaMuaSauThue],[GhiChu]) VALUES (@IDHangHoaCombo,@IDHangHoa,@SoLuong,@GiaBanTruocThue,@ThanhTien,@IDDonViTinh,@MaHang,@TrongLuong,@GiaBanSauThue,@GiaMuaTruocThue,@GiaMuaSauThue,@GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoaCombo", IDHangHoaCombo);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@GiaBanTruocThue", GiaBanTruocThue);
                        myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@GiaBanSauThue", GiaBanSauThue);
                        myCommand.Parameters.AddWithValue("@GiaMuaTruocThue", GiaMuaTruocThue);
                        myCommand.Parameters.AddWithValue("@GiaMuaSauThue", GiaMuaSauThue);
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
        public void XoaHangHoa_Temp_ID(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_HangHoa_Combo_Temp] WHERE [ID] = '" + ID + "'";
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

        public void XoaHangHoa_Temp_IDHangCombo(int IDHangHoaComBo)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_HangHoa_Combo_Temp] WHERE [IDHangHoaComBo] = '" + IDHangHoaComBo + "'";
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

       

        public DataTable KTHangHoa_Temp(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_HangHoa_Combo_Temp] WHERE [IDHangHoa]= '" + IDHangHoa + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void UpdateHangHoa_temp(int IDHangHoaCombo, string IDHangHoa, int SoLuong, float GiaBanTruocThue, float ThanhTien, string MaHang, string IDDonViTinh, string TrongLuong, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_HangHoa_Combo_Temp] SET [GhiChu] = @GhiChu,[TrongLuong] =@TrongLuong,[IDDonViTinh] = @IDDonViTinh,[MaHang] = @MaHang,[IDHangHoaCombo] = @IDHangHoaCombo,[SoLuong] = @SoLuong,[GiaBanTruocThue] = @GiaBanTruocThue ,[ThanhTien] = @ThanhTien  WHERE [IDHangHoa] = @IDHangHoa";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@IDHangHoaCombo", IDHangHoaCombo);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@GiaBanTruocThue", GiaBanTruocThue);
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

        public void CapNhatHangHoa(int ID, string MaHang, string TenHangHoa, string IDNhomHang, string IDDonViTinh, string GiaMuaTruocThue, string GiaBanTruocThue, string GiaMuaSauThue, string GiaBanSauThue, string TrongLuong, string GhiChu, string HanSuDung, string GiaBan1, string GiaBan2, string GiaBan3, string GiaBan4, string GiaBan5)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_HangHoa] SET [GiaBan2] = @GiaBan1,[GiaBan3] = @GiaBan1,[GiaBan4] = @GiaBan1,[GiaBan5] = @GiaBan1,[GiaBan1] = @GiaBan1,[HanSuDung] = @HanSuDung,[GhiChu] = @GhiChu,[TrongLuong] = @TrongLuong,[GiaBanSauThue] = @GiaBanSauThue,[GiaMuaTruocThue] =@GiaMuaTruocThue,[GiaBanTruocThue] = @GiaBanTruocThue,[GiaMuaSauThue] = @GiaMuaSauThue,[IDDonViTinh] = @IDDonViTinh, [IDNhomHang]= @IDNhomHang,[MaHang] = @MaHang,[TenHangHoa] = @TenHangHoa  WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@TenHangHoa", TenHangHoa);
                        myCommand.Parameters.AddWithValue("@IDNhomHang", IDNhomHang);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@GiaMuaTruocThue", GiaMuaTruocThue);
                        myCommand.Parameters.AddWithValue("@GiaBanTruocThue", GiaBanTruocThue);
                        myCommand.Parameters.AddWithValue("@GiaMuaSauThue", GiaMuaSauThue);
                        myCommand.Parameters.AddWithValue("@GiaBanSauThue", GiaBanSauThue);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@HanSuDung", HanSuDung);
                        myCommand.Parameters.AddWithValue("@GiaBan1", GiaBan1);
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



        public DataTable DanhSachHangHoaCombo_IDHangHoaComBo(string IDHangHoaComBo)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select * from [GPM_HangHoa_Combo] where [IDHangHoaCombo] =  '" + IDHangHoaComBo + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachHangHoaCombo_ID(int ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_HangHoa_Combo] WHERE [ID] = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public void CapNhatHangHoa_Combo(int ID, string TrangThai, string TrongLuong, string TongTien, string HanSuDung, string TenHangHoa)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_HangHoa] SET [TenHangHoa] = @TenHangHoa,[HanSuDung] = @HanSuDung,[GiaBanSauThue] = @TongTien,[IDTrangThaiHang] = @TrangThai,[TrongLuong] = @TrongLuong  WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@TenHangHoa", TenHangHoa);
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
                        myCommand.Parameters.AddWithValue("@TrongLuong", TrongLuong);
                        myCommand.Parameters.AddWithValue("@TongTien", TongTien);
                        myCommand.Parameters.AddWithValue("@HanSuDung", HanSuDung);
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
    }
}