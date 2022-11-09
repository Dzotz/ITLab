using ITLab.Classes.Database;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITLabAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RowsController : ControllerBase
    {
        // GET: api/<RowsController>
        /// <summary>
        /// Returns Row from table by id
        /// </summary>
        /// <response code="200">_Return Row_</response>
        /// <response code="400">_No row with such id_</response>
        /// <response code="404">_No database created yet or no table with such name_</response>
        [HttpGet]
        [Route("{tableName}/{rowId}")]
        [ProducesResponseType(typeof(Response<Row>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Get(string tableName, int rowId)
        {
            if (DatabaseManager.Instance.Database == null)
            {
                return NotFound(new { error = "There is no database" });
            }
            if (DatabaseManager.Instance.Database.GetTable(tableName) == null)
            {
                return NotFound(new { error = "There is no table with such name" });
            }
            Table table = DatabaseManager.Instance.Database.GetTable(tableName);
            if (table.Rows.Count<=rowId)
            {
                return BadRequest(new { error = "There is no row with such id" });
            }

            Response <Row> response = new Response<Row>
            {
                Value = table.Rows[rowId],
                Links = new Dictionary<string, string>
                {
                    { "getCell", $"api/Rows/{tableName}/{rowId}/<cellId>" },
                    { "editRow",  $"api/Rows/{tableName}/{rowId}"},
                    { "editCell", $"api/Rows/{tableName}/{rowId}/<cellId>" },
                    { "createRow", $"api/Rows/{tableName}" },
                    { "deleteRow", $"api/Rows/{tableName}/{rowId}" }
                }
            };

            return Ok(response);
        }

        // GET api/<RowsController>/5
        /// <summary>
        /// Returns Cell from table by row id and cell id
        /// </summary>
        /// <response code="200">_Return Cell value_</response>
        /// <response code="400">_No row with such id or no cell with such id_</response>
        /// <response code="404">_No database created yet or no table with such name_</response>
        [HttpGet]
        [Route("{tableName}/{rowId}/{cellId}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Get(string tableName, int rowId, int cellId)
        {
            if (DatabaseManager.Instance.Database == null)
            {
                return NotFound(new { error = "There is no database" });
            }
            if (DatabaseManager.Instance.Database.GetTable(tableName) == null)
            {
                return NotFound(new { error = "There is no table with such name" });
            }
            Table table = DatabaseManager.Instance.Database.GetTable(tableName);
            if (table.Rows.Count <= rowId)
            {
                return BadRequest(new { error = "There is no Row with such id" });
            }
            if (table.Columns.Count <= cellId)
            {
                return BadRequest(new { error = "There is no Cell with such id" });
            }

            Response<string> response = new Response<string>
            {
                Value = table.Rows[rowId].Values[cellId],
                Links = new Dictionary<string, string>
                {
                    { "getRow", $"api/Rows/{tableName}/{rowId}" },
                    { "editRow",  $"api/Rows/{tableName}/{rowId}"},
                    { "editCell", $"api/Rows/{tableName}/{rowId}/<cellId>" },
                    { "createRow", $"api/Rows/{tableName}" },
                    { "deleteRow", $"api/Rows/{tableName}/{rowId}" }

                }
            };

            return Ok(response);
        }

        // POST api/<RowsController>
        /// <summary>
        /// Create Row in table 
        /// </summary>
        /// <response code="200">_Create Row_</response>
        /// <response code="400">_Can't create Row in table without Columns or bad Row data_</response>
        /// <response code="404">_No database created yet or no table with such name_</response>
        [HttpPost]
        [Route("{tableName}")]
        [ProducesResponseType(typeof(Response<Row>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Post(string tableName, [FromBody] string value)
        {
            if (DatabaseManager.Instance.Database == null)
            {
                return NotFound(new { error = "There is no database" });
            }
            if (DatabaseManager.Instance.Database.GetTable(tableName) == null)
            {
                return NotFound(new { error = "There is no table with such name" });
            }
            Table table = DatabaseManager.Instance.Database.GetTable(tableName);
            if (table.Columns.Count ==0)
            {
                return BadRequest(new { error = "Can't create Row in table without Columns" });
            }
            try
            {
                DatabaseManager.Instance.AddRow(tableName, value);
                Response<Row> response = new Response<Row>
                {
                    Value = table.Rows[table.Rows.Count-1],
                    Links = new Dictionary<string, string>
                {
                    { "getCell", $"api/Rows/{tableName}/{table.Rows[table.Rows.Count-1]}/<cellId>" },
                    { "getRow", $"api/Rows/{tableName}/{table.Rows[table.Rows.Count-1]}" },
                    { "editRow",  $"api/Rows/{tableName}/{table.Rows[table.Rows.Count-1]}"},
                    { "editCell", $"api/Rows/{tableName}/{table.Rows[table.Rows.Count-1]}/<cellId>" },
                    { "deleteRow", $"api/Rows/{tableName}/{table.Rows[table.Rows.Count-1]}" }

                }
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // PUT api/<RowsController>/5/// <summary>
        /// Edit Row in table 
        /// </summary>
        /// <response code="200">_Edit Row_</response>
        /// <response code="400">_No row with such id or bad Row data_</response>
        /// <response code="404">_No database created yet or no table with such name_</response>
        [HttpPut]
        [Route("{tableName}/{rowId}")]
        [ProducesResponseType(typeof(Response<Row>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Put(string tableName, int rowId, [FromBody] string value)
        {
            if (DatabaseManager.Instance.Database == null)
            {
                return NotFound(new { error = "There is no database" });
            }
            if (DatabaseManager.Instance.Database.GetTable(tableName) == null)
            {
                return NotFound(new { error = "There is no table with such name" });
            }
            Table table = DatabaseManager.Instance.Database.GetTable(tableName);
            if (table.Rows.Count <= rowId)
            {
                return BadRequest(new { error = "There is no row with such id" });
            }
            try
            {
                DatabaseManager.Instance.EditRow(tableName, value, rowId);
                Response<Row> response = new Response<Row>
                {
                    Value = table.Rows[rowId],
                    Links = new Dictionary<string, string>
                {
                    { "getCell", $"api/Rows/{tableName}/{rowId}/<cellId>" },
                    { "getRow", $"api/Rows/{tableName}/{rowId}" },
                    { "editCell", $"api/Rows/{tableName}/{rowId}/<cellId>" },
                    { "createRow", $"api/Rows/{tableName}" },
                    { "deleteRow", $"api/Rows/{tableName}/{rowId}" }

                }
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Edit Cell from table by row id and cell id
        /// </summary>
        /// <response code="200">_Edit Cell value_</response>
        /// <response code="400">_No row with such id or no cell with such id or bad value_</response>
        /// <response code="404">_No database created yet or no table with such name_</response>
        [HttpPut]
        [Route("{tableName}/{rowId}/{cellId}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult PutCell(string tableName, int rowId, int cellId, [FromBody] string value)
        {
            if (DatabaseManager.Instance.Database == null)
            {
                return NotFound(new { error = "There is no database" });
            }
            if (DatabaseManager.Instance.Database.GetTable(tableName) == null)
            {
                return NotFound(new { error = "There is no table with such name" });
            }
            Table table = DatabaseManager.Instance.Database.GetTable(tableName);
            if (table.Rows.Count <= rowId)
            {
                return BadRequest(new { error = "There is no row with such id" });
            }
            if (table.Columns.Count <= cellId)
            {
                return BadRequest(new { error = "There is no Cell with such id" });
            }
            try
            {
                DatabaseManager.Instance.EditCell(tableName, cellId, rowId, value );
                Response<string> response = new Response<string>
                    {
                        Value = table.Rows[rowId].Values[cellId],
                        Links = new Dictionary<string, string>
                        {       
                            { "getCell", $"api/Rows/{tableName}/{rowId}/{cellId}" },
                            { "getRow", $"api/Rows/{tableName}/{rowId}" },
                            { "editRow",  $"api/Rows/{tableName}/{rowId}"},
                            { "createRow", $"api/Rows/{tableName}" },
                            { "deleteRow", $"api/Rows/{tableName}/{rowId}" }
                        }
                    };
                    return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE api/<RowsController>/5
        /// Delete Row from table 
        /// </summary>
        /// <response code="200">_Delete Row_</response>
        /// <response code="400">_No row with such id_</response>
        /// <response code="404">_No database created yet or no table with such name_</response>
        [HttpDelete]
        [Route("{tableName}/{rowId}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Delete(string tableName, int rowId)
        {
            if (DatabaseManager.Instance.Database == null)
            {
                return NotFound(new { error = "There is no database" });
            }
            if (DatabaseManager.Instance.Database.GetTable(tableName) == null)
            {
                return NotFound(new { error = "There is no table with such name" });
            }
            Table table = DatabaseManager.Instance.Database.GetTable(tableName);
            if (table.Rows.Count <= rowId)
            {
                return BadRequest(new { error = "There is no row with such id" });
            }
            DatabaseManager.Instance.DeleteRow(tableName, rowId);

            Response<string> response = new Response<string>
            {
                Value = "Deleted successfully",
                Links = new Dictionary<string, string>
                        {
                            { "getCell", $"api/Rows/{tableName}/{rowId}/<cellId>" },
                            { "getRow", $"api/Rows/{tableName}/{rowId}" },
                            { "editRow",  $"api/Rows/{tableName}/{rowId}"},
                            { "createRow", $"api/Rows/{tableName}" },
                            { "editCell", $"api/Rows/{tableName}/{rowId}/<cellId>" }
                        }
            };

            return Ok(response);
        }
    }
}
