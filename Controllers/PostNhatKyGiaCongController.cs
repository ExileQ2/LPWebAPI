using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using LPWebAPI.Models;

namespace LPWebAPI.Controllers
{
    [Route("api/postNhatKyGiaCong")]
    [ApiController]
    public class PostNhatKyGiaCongController : ControllerBase
    {
        private readonly string _conn;

        public PostNhatKyGiaCongController(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        [HttpPost]
        public IActionResult Post([FromBody] NhatKyGiaCongDto dto)
        {
            try
            {
                using var conn = new SqlConnection(_conn);
                conn.Open();
                using var cmd = new SqlCommand("web.nhapNhatKyGiaCong", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProcessNo", dto.ProcessNo);
                cmd.Parameters.AddWithValue("@JobControlNo", dto.JobControlNo);
                cmd.Parameters.AddWithValue("@StaffNo", dto.StaffNo);
                cmd.Parameters.AddWithValue("@McName", dto.McName);
                cmd.Parameters.AddWithValue("@Note", dto.Note);
                cmd.Parameters.AddWithValue("@ProOrdNo", dto.ProOrdNo);
                cmd.Parameters.AddWithValue("@Serial", dto.Serial);
                cmd.Parameters.AddWithValue("@setup", dto.setup);
                cmd.Parameters.AddWithValue("@rework", dto.rework);
                cmd.Parameters.AddWithValue("@QtyGood", dto.QtyGood);
                cmd.Parameters.AddWithValue("@QtyReject", dto.QtyReject);
                cmd.Parameters.AddWithValue("@QtyRework", dto.QtyRework);
                cmd.ExecuteNonQuery();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
