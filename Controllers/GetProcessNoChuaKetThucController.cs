using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace LPWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetProcessNoChuaKetThucController : ControllerBase
    {
        private readonly string _conn;

        public GetProcessNoChuaKetThucController(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        [HttpGet("{StaffNo}/{McName}")]
        public IActionResult Get(string StaffNo, string McName)
        {
            try
            {
                using var conn = new SqlConnection(_conn);
                conn.Open();
                using var cmd = new SqlCommand("web.layProcessNoChuaKetThuc", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffNo", StaffNo);
                cmd.Parameters.AddWithValue("@McName", McName);
                using var rdr = cmd.ExecuteReader();
                string processNo = " ";
                if (rdr.Read() && !rdr.IsDBNull(0))
                {
                    processNo = rdr.GetString(0);
                }
                return Ok(new { processNo });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
