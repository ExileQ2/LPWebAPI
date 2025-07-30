using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using LPWebAPI.Models;

namespace LPWebAPI.Controllers
{
    [Route("api/[controller]")]   // GET /api/getstaff/{staffNo}
    [ApiController]
    public class GetStaffController : ControllerBase
    {
        private readonly string _conn;

        public GetStaffController(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        [HttpGet("{staffNo:int}")]
        public IActionResult GetStaff(int staffNo)
        {
            try
            {
                using var conn = new SqlConnection(_conn);
                conn.Open();

                using var cmd = new SqlCommand("web.layInfoNhanVien", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffNo", staffNo);

                using var rdr = cmd.ExecuteReader();
                if (!rdr.Read())
                    return BadRequest("khong hop le");

                var dto = new StaffsDto
                {
                    FullName  = rdr.IsDBNull(0) ? "" : rdr.GetString(0),
                    WorkJob   = rdr.IsDBNull(1) ? "" : rdr.GetString(1),
                    WorkPlace = rdr.IsDBNull(2) ? "" : rdr.GetString(2)
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
