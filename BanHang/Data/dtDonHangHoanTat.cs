﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtDonHangHoanTat
    {
        public DataTable LayDanhSachDonHangDuyet(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TOP 1000 * FROM [GPM_DuyetHangChiNhanh] WHERE ( '" + IDKho + "' = 1 OR [IDKhoLap] = '" + IDKho + "') AND [GPM_DuyetHangChiNhanh].TrangThaiDuyet = 1  AND IDTrangThaiXuLy !=2 ORDER BY [ID] DESC";
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