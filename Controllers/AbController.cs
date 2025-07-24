using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LPWebAPI.Models;

namespace LPWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbController : ControllerBase
    {
        private readonly string connString =
            "Server=192.168.1.68;Database=Test123;User Id=quan;Password=QUAN;TrustServerCertificate=True;";

        // POST /api/ab  – insert via dbo.nhap_du_lieu_AB, return NewID
        [HttpPost]
        public IActionResult PostAb([FromBody] AbInsertDto dto)
        {
            try
            {
                using var conn = new SqlConnection(connString);
                conn.Open();
                using var cmd = new SqlCommand("dbo.nhap_du_lieu_AB", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@A", dto.A);
                cmd.Parameters.AddWithValue("@B", dto.B);
                int newId = (int)cmd.ExecuteScalar();
                return Created($"/api/ab/{newId}", new { id = newId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // GET /api/ab  – list all rows in TEST
        [HttpGet]
        public IActionResult GetAll()
        {
            var list = new List<object>();
            try
            {
                using var conn = new SqlConnection(connString);
                conn.Open();
                string sql = "SELECT ID, A, B FROM dbo.TEST ORDER BY ID";
                using var cmd = new SqlCommand(sql, conn);
                using var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    list.Add(new
                    {
                        ID = rdr.GetInt32(0),
                        A = rdr.GetString(1),
                        B = rdr.GetString(2)
                    });
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
