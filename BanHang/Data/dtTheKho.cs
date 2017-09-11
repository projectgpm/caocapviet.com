using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtTheKho
    {
        public static string LayTenKho_ID(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TenCuaHang FROM [GPM_Kho] WHERE [ID] = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb.Rows[0]["TenCuaHang"].ToString();
                }
            }
        }
        public static object ThemTheKho(string MaDonHang, string DienGiai, string NhapTrongKy, string XuatTrongKy, string TonCuoiKy, string IDNhanVien, string IDKho, string IDHangHoa, string LoaiPhieu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object ID = null;
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_TheKho] ([MaDonHang], [NgayLap], [DienGiai], [NhapTrongKy],[XuatTrongKy],[TonCuoiKy], [IDNhanVien],[IDKho],[IDHangHoa],[LoaiPhieu])  OUTPUT INSERTED.ID  VALUES (@MaDonHang,getdate(),@DienGiai, @NhapTrongKy,@XuatTrongKy,@TonCuoiKy,@IDNhanVien,@IDKho,@IDHangHoa,@LoaiPhieu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@LoaiPhieu", LoaiPhieu);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@MaDonHang", MaDonHang);
                        myCommand.Parameters.AddWithValue("@DienGiai", DienGiai);
                        myCommand.Parameters.AddWithValue("@NhapTrongKy", NhapTrongKy);
                        myCommand.Parameters.AddWithValue("@XuatTrongKy", XuatTrongKy);
                        myCommand.Parameters.AddWithValue("@TonCuoiKy", TonCuoiKy);
                        myCommand.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        ID = myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                    return ID;
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
    }
}