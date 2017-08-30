using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dataNganhHang
    {
        public static bool KiemTraMaNganhHang_ID(string MaNganh,string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT MaNganh FROM [GPM_NGANHHANG] WHERE [MaNganh] = '" + MaNganh + "' AND ID =  " + ID;
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
        public static bool KiemTraMaNganhHang(string MaNganh)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT MaNganh FROM [GPM_NGANHHANG] WHERE [MaNganh] = " + MaNganh;
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
        public static int LayID_Max()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT Max(MaNganh) as IDMax FROM [GPM_NGANHHANG] ";
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

        public DataTable getDanhSachNganhHang()
        {
            string cmd = "SELECT * FROM [GPM_NGANHHANG] WHERE [DAXOA] = 0 AND ID != 1";
            return getData(cmd);
        }

        public void XoaNganhHang(int ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_NGANHHANG] SET [DAXOA] = 1 WHERE [ID] = @ID";
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

        public void updateNganhHang(int ID, string MaNganh, string TenNganhHang, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_NGANHHANG] SET [MaNganh] = @MaNganh,[TenNganhHang] = @TenNganhHang,[GhiChu] = @GhiChu, [NgayCapNhat] = getDATE() WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@MaNganh", MaNganh);
                        myCommand.Parameters.AddWithValue("@TenNganhHang", TenNganhHang);
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

        public void insertNganhHang(string MaNganh, string TenNganhHang, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_NGANHHANG] ([MaNganh], [TenNganhHang],[GhiChu],[NgayCapNhat]) VALUES (@MaNganh, @TenNganhHang,@GhiChu,getDATE())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@MaNganh", MaNganh);
                        myCommand.Parameters.AddWithValue("@TenNganhHang", TenNganhHang);
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
    }
}