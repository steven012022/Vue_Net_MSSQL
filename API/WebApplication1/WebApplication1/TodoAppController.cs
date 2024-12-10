using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoAppController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TodoAppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetNotes")]
        public async Task<IActionResult> GetNotes()
        {
            try
            {
                // Read DB_PASSWORD from environment variables
                string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

                // Read the connection string and concatenate DB_PASSWORD.
                string connectionString = _configuration.GetConnectionString("todoAppDBCon") + dbPassword;

                // Perform database operations using the SQL connection string.
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var query = "SELECT * FROM dbo.Notes";
                    var command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();

                    var notes = new List<object>();
                    while (await reader.ReadAsync())
                    {
                        notes.Add(new
                        {
                            id = reader["id"],
                            description = reader["description"].ToString()
                        });
                    }

                    return Ok(notes);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddNotes")]
        public async Task<IActionResult> AddNotes([FromBody] Note note)
        {
            if (note == null || string.IsNullOrEmpty(note.Description))
            {
                return BadRequest("Invalid note data.");
            }

            try
            {
                string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
                string connectionString = _configuration.GetConnectionString("todoAppDBCon") + dbPassword;

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var query = "INSERT INTO dbo.Notes (description) VALUES (@description)";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@description", note.Description);

                    var rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return Ok("Note added successfully.");
                    }
                    else
                    {
                        return StatusCode(500, "Failed to add note.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteNotes/{id}")]
        public async Task<IActionResult> DeleteNotes(int id)
        {
            try
            {
                string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
                string connectionString = _configuration.GetConnectionString("todoAppDBCon") + dbPassword;

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var query = "DELETE FROM dbo.Notes WHERE id = @id";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    var rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return Ok("Note deleted successfully.");
                    }
                    else
                    {
                        return NotFound("Note not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class Note
    {
        public string Description { get; set; }
    }
}
