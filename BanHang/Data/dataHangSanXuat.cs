using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dataHangSanXuat
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

        public DataTable getDanhSachHangSX()
        {
            string cmd = "SELECT * FROM [GPM_HangSanXuat] WHERE [DAXOA] = 0 AND ID != 1";
            return getData(cmd);
        }

        public void insertHangSX(string TenHangSanXuat, string SDT, string DiaChi, string Email, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangSanXuat] ([TenHangSanXuat], [SDT],[DiaChi],[Email],[GhiChu],[NgayCapNhat]) VALUES (@TenHangSanXuat, @SDT,@DiaChi,@Email,@GhiChu,getDATE())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@TenHangSanXuat", TenHangSanXuat);
                        myCommand.Parameters.AddWithValue("@SDT", SDT);
                        myCommand.Parameters.AddWithValue("@DiaChi", DiaChi);
                        myCommand.Parameters.AddWithValue("@Email", Email);
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

        public void updateHangSX(string ID, string TenHangSanXuat, string SDT, string DiaChi, string Email, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HangSanXuat] SET [TenHangSanXuat] = @TenHangSanXuat,[SDT] = @SDT,[DiaChi] = @DiaChi,[Email] = @Email,[GhiChu] = @GhiChu, [NgayCapNhat] = getDATE() WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@TenHangSanXuat", TenHangSanXuat);
                        myCommand.Parameters.AddWithValue("@SDT", SDT);
                        myCommand.Parameters.AddWithValue("@DiaChi", DiaChi);
                        myCommand.Parameters.AddWithValue("@Email", Email);
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

        public void deleteHangSX(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HangSanXuat] SET [DaXoa] = 1 WHERE [ID] = @ID";
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
        public static string LayIDHangSanXuat(string TenHangSanXuat)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_HANGSANXUAT] WHERE [TenHangSanXuat] = N'" + TenHangSanXuat + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return tb.Rows[0]["ID"].ToString();
                    }
                    else return 1 + "";
                }
            }
        }
    }
}