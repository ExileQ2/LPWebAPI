using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using LPWebAPI.Models;

namespace LPWebAPI.Controllers
{
    [Route("api/[controller]")]   // GET /api/getmachine/{McName}
    [ApiController]
    public class GetMachineController : ControllerBase
    {
        private readonly string _conn;

        public GetMachineController(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        [HttpGet("{McName}")]
        public IActionResult GetMachine(string McName)
        {
            try
            {
                using var conn = new SqlConnection(_conn);
                conn.Open();

                using var cmd = new SqlCommand("web.layInfoMay", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GetMachine", McName);

                using var rdr = cmd.ExecuteReader();
                if (!rdr.Read())
                    return BadRequest("khong hop le");

                var dto = new MachineInfoDto
                {
                    Model = rdr.IsDBNull(0) ? "" : rdr.GetString(0),
                    Status = rdr.IsDBNull(1) ? "" : rdr.GetString(1)
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
