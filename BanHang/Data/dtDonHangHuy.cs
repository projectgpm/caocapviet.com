using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtDonHangHuy
    {
        public DataTable LayDanhSachDonHangDuyet(string IDKho, string HienThi, string NgayBD, string NgayKT)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TOP " + HienThi + " * FROM [GPM_DuyetHangChiNhanh] WHERE NgayDat <'" + NgayKT + "' AND  NgayDat >= '" + NgayBD + "' AND( '" + IDKho + "' = 1 OR [IDKhoLap] = '" + IDKho + "') AND [GPM_DuyetHangChiNhanh].IDTrangThaiXuLy = 2  ORDER BY [ID] DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
    }
}