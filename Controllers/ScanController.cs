using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using LPWebAPI.Models;

namespace LPWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanController : ControllerBase
    {
        // Define connection string here
        private readonly string connString = "Data Source=QUAN\\SQLEXPRESS01;Initial Catalog=TestQR;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

        [HttpPost]
        public IActionResult PostScan([FromBody] ScanData data)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO DummyQRScan (
                            Jobdetail, PerID, Name, Mno, Partno, JobPhase, SetM, Pass, Fail, Rework, 
                            CheckQuant, Seriesno, StartTime, EndTime, TimeCountM, CycleTimeM, 
                            Efficiency, Workno, ProductOrder, Note, Company, Groupname, 
                            PhaseName, ReworkBit, MachineCode
                        ) VALUES (
                            @Jobdetail, @PerID, @Name, @Mno, @Partno, @JobPhase, @SetM, @Pass, @Fail, @Rework, 
                            @CheckQuant, @Seriesno, @StartTime, @EndTime, @TimeCountM, @CycleTimeM, 
                            @Efficiency, @Workno, @ProductOrder, @Note, @Company, @Groupname, 
                            @PhaseName, @ReworkBit, @MachineCode
                        )";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Jobdetail", data.Jobdetail);
                        cmd.Parameters.AddWithValue("@PerID", data.PerID);
                        cmd.Parameters.AddWithValue("@Name", data.Name);
                        cmd.Parameters.AddWithValue("@Mno", data.Mno);
                        cmd.Parameters.AddWithValue("@Partno", data.Partno);
                        cmd.Parameters.AddWithValue("@JobPhase", data.JobPhase);
                        cmd.Parameters.AddWithValue("@SetM", data.SetM);
                        cmd.Parameters.AddWithValue("@Pass", data.Pass);
                        cmd.Parameters.AddWithValue("@Fail", data.Fail);
                        cmd.Parameters.AddWithValue("@Rework", data.Rework);
                        cmd.Parameters.AddWithValue("@CheckQuant", (object?)data.CheckQuant ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Seriesno", data.Seriesno);
                        cmd.Parameters.AddWithValue("@StartTime", data.StartTime);
                        cmd.Parameters.AddWithValue("@EndTime", data.EndTime);
                        cmd.Parameters.AddWithValue("@TimeCountM", data.TimeCountM);
                        cmd.Parameters.AddWithValue("@CycleTimeM", data.CycleTimeM);
                        cmd.Parameters.AddWithValue("@Efficiency", (object?)data.Efficiency ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Workno", data.Workno);
                        cmd.Parameters.AddWithValue("@ProductOrder", data.ProductOrder);
                        cmd.Parameters.AddWithValue("@Note", data.Note);
                        cmd.Parameters.AddWithValue("@Company", data.Company);
                        cmd.Parameters.AddWithValue("@Groupname", data.Groupname);
                        cmd.Parameters.AddWithValue("@PhaseName", data.PhaseName);
                        cmd.Parameters.AddWithValue("@ReworkBit", data.ReworkBit);
                        cmd.Parameters.AddWithValue("@MachineCode", data.MachineCode);

                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok(new { message = "Inserted successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
