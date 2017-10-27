using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtPhanQuyen
    {
        public static bool KiemTraTonTai(string IDNhomNguoiDung , string IDMenu)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_PhanQuyen] WHERE [IDNhomNguoiDung] = '" + IDNhomNguoiDung + "' AND IDMenu = '" + IDMenu + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
        public void ThemQuyen(string IDNhomNguoiDung, string IDMenu, string TrangThai, string ChucNang)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_PhanQuyen] ([IDNhomNguoiDung],[IDMenu],[TrangThai],[ChucNang],[NgayCapNhat]) VALUES (@IDNhomNguoiDung,@IDMenu,@TrangThai,@ChucNang, getdate())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDNhomNguoiDung", IDNhomNguoiDung);
                        myCommand.Parameters.AddWithValue("@IDMenu", IDMenu);
                        myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
                        myCommand.Parameters.AddWithValue("@ChucNang", ChucNang);
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
        public DataTable LayDanhSachMenu(string IDNhomNguoiDung)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                //string cmdText = "SELECT [GPM_Menu].Link,[GPM_Menu].TenDanhMuc,[GPM_PhanQuyen].ID, [GPM_PhanQuyen].TrangThai,[GPM_PhanQuyen].ChucNang FROM [GPM_PhanQuyen],[GPM_Menu] WHERE [GPM_PhanQuyen].IDMenu = [GPM_Menu].ID AND [GPM_PhanQuyen].IDNhomNguoiDung = '" + IDNhomNguoiDung + "'";
                string cmdText = "SELECT GPM_PhanQuyen.*,[GPM_Menu].Link FROM GPM_PhanQuyen,[GPM_Menu] WHERE [GPM_Menu].ID = [GPM_PhanQuyen].IDMenu  AND GPM_PhanQuyen.IDNhomNguoiDung =  '" + IDNhomNguoiDung + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void XoaQuyen(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "DELETE [GPM_PhanQuyen] WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình Xóa dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public void CapNhatQuyen(int IDNhomNguoiDung, int ID, int TrangThai, int ChucNang)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_PhanQuyen] SET [TrangThai] = @TrangThai,[ChucNang] = @ChucNang,[NgayCapNhat] = getdate() WHERE [ID] = @ID AND [IDNhomNguoiDung] = @IDNhomNguoiDung";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ChucNang", ChucNang);
                        myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@IDNhomNguoiDung", IDNhomNguoiDung);

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

        //public void CapNhatQuyen_Full(int ID, int IDNhomNguoiDung, int IDMenu, int TrangThai, int ChucNang)
        //{
        //    using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        try
        //        {
        //            myConnection.Open();
        //            string cmdText = "UPDATE [GPM_PhanQuyen] SET [IDMenu] = @IDMenu,[TrangThai] = @TrangThai,[ChucNang] = @ChucNang,[IDNhomNguoiDung] = @IDNhomNguoiDung WHERE [ID] = @ID";
        //            using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
        //            {
        //                myCommand.Parameters.AddWithValue("@ChucNang", ChucNang);
        //                myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
        //                myCommand.Parameters.AddWithValue("@ID", ID);
        //                myCommand.Parameters.AddWithValue("@IDMenu", IDMenu);
        //                myCommand.Parameters.AddWithValue("@IDNhomNguoiDung", IDNhomNguoiDung);

        //                myCommand.ExecuteNonQuery();
        //            }
        //            myConnection.Close();
        //        }
        //        catch
        //        {
        //            throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
        //        }
        //    }
        //}

        //public void Insert_Full(int ID, int IDNhomNguoiDung, int IDMenu, int TrangThai, int ChucNang)
        //{
        //    using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
        //    {
        //        try
        //        {
        //            myConnection.Open();
        //            string cmdText = "INSERT INTO [GPM_PhanQuyen] ([IDMenu],[TrangThai],[ChucNang],[IDNhomNguoiDung],[NgayCapNhap]) VALUE (@IDMenu,@TrangThai,@ChucNang,@IDNhomNguoiDung,@NgayCapNhap)";
        //            using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
        //            {
        //                myCommand.Parameters.AddWithValue("@ChucNang", ChucNang);
        //                myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
        //                myCommand.Parameters.AddWithValue("@ID", ID);
        //                myCommand.Parameters.AddWithValue("@IDMenu", IDMenu);
        //                myCommand.Parameters.AddWithValue("@IDNhomNguoiDung", IDNhomNguoiDung);

        //                myCommand.ExecuteNonQuery();
        //            }
        //            myConnection.Close();
        //        }
        //        catch
        //        {
        //            throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
        //        }
        //    }
        //}
    }
}