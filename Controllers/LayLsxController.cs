using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using LPWebAPI.Models;

namespace LPWebAPI.Controllers
{
    [Route("api/[controller]")]      // GET /api/laylsx/{A}
    [ApiController]
    public class LaylsxController : ControllerBase
    {
        private readonly string _conn;

        public LaylsxController(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        [HttpGet("{A}")]
        public IActionResult Get(string A)
        {
            try
            {
                using var cn = new SqlConnection(_conn);
                cn.Open();

                using var cmd = new SqlCommand("dbo.layLSX", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@A", A);

                using var rdr = cmd.ExecuteReader();
                if (!rdr.Read())
                    return BadRequest("khong hop le");

                var dto = new JobNoDto
                {
                    ProOrdNo = rdr.IsDBNull(0) ? "" : rdr.GetString(0)
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