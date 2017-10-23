using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtCauHinhHanMuc
    {
        public static double DonGiaTheoIDNhom(string IDNhomNguoiDung)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_CauHinhHanMuc]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        double DonGia = 0;
                        DataRow dr = tb.Rows[0];
                        if (IDNhomNguoiDung == "6")
                        {
                            DonGia = double.Parse(dr["GiaGiamDoc"].ToString());
                        }
                        if (IDNhomNguoiDung == "5")
                        {
                            DonGia = double.Parse(dr["GiaKho"].ToString());
                        }
                        if (IDNhomNguoiDung == "4")
                        {
                            DonGia = double.Parse(dr["GiaGiamSat"].ToString());
                        }
                        return DonGia;
                    }
                    else return 0;
                }
            }
        }
        public DataTable DanhSach()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT  * FROM [GPM_CauHinhHanMuc]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void CapNhat(string GiaGiamDoc, string GiaGiamSat, string GiaKho)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_CauHinhHanMuc] SET [GiaGiamDoc] = @GiaGiamDoc,[GiaGiamSat] = @GiaGiamSat,[GiaKho] = @GiaKho, [NgayCapNhat] = getdate()";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@GiaGiamDoc", GiaGiamDoc);
                        myCommand.Parameters.AddWithValue("@GiaGiamSat", GiaGiamSat);
                        myCommand.Parameters.AddWithValue("@GiaKho", GiaKho);
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