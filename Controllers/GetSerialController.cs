using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LPWebAPI.Models;

namespace LPWebAPI.Controllers
{
    [Route("api/[controller]")]   // GET /api/getserial/{ProOrdNo}
    [ApiController]
    public class GetSerialController : ControllerBase
    {
        private readonly string _conn;

        public GetSerialController(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        [HttpGet("{ProOrdNo}")]
        //[ProducesResponseType(typeof(List<SerialDto>), 200)]
        public ActionResult<List<SerialDto>> GetSerial(string ProOrdNo)
        {
            try
            {
                using var conn = new SqlConnection(_conn);
                conn.Open();

                using var cmd = new SqlCommand("web.laySerial", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProOrdNo", ProOrdNo);

                using var rdr = cmd.ExecuteReader();
                var list = new List<SerialDto>();
                while (rdr.Read())
                {
                    list.Add(new SerialDto
                    {
                        Serial = rdr.IsDBNull(0) ? "" : rdr.GetString(0)
                    });
                }

                if (list.Count == 0)
                    return BadRequest("khong hop le");

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
