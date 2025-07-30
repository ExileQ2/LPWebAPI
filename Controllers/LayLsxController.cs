using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LPWebAPI.Models;

namespace LPWebAPI.Controllers
{
    [Route("api/[controller]")]      // GET /api/laylsx/{ProOrdNo}
    [ApiController]
    public class LaylsxController : ControllerBase
    {
        private readonly string _conn;

        public LaylsxController(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        [HttpGet("{ProOrdNo}")]
        //[ProducesResponseType(typeof(List<JobNoDto>), 200)] // Uncomment if you want explicit Swagger hint
        public ActionResult<List<JobNoDto>> Get(string ProOrdNo)
        {
            try
            {
                using var cn = new SqlConnection(_conn);
                cn.Open();

                using var cmd = new SqlCommand("web.layLSX", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProOrdNo", ProOrdNo);

                using var rdr = cmd.ExecuteReader();
                var list = new List<JobNoDto>();
                while (rdr.Read())
                {
                    list.Add(new JobNoDto
                    {
                        ProOrdNo = rdr.IsDBNull(0) ? "" : rdr.GetString(0)
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