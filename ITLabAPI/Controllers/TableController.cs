using ITLab.Classes.Database;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITLabAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        // GET: api/<TableController>/<name>
        /// <summary>
        /// Returns Table by name from Database
        /// </summary>
        /// <response code="200">_Return Table_</response>
        /// <response code="400">_There is no Name specified for table_</response>
        /// <response code="404">_No database created yet or no table with such name_</response>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(Response<Table>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Get(string name)
        {
            if (DatabaseManager.Instance.Database == null)
            {
                return NotFound(new { error = "There is no database" });
            }
            if (name == null)
            {
                return BadRequest(new { error = "You haven't entered name of Table" });
            }
            if (DatabaseManager.Instance.Database.GetTable(name) == null)
            {
                return NotFound(new { error = "There is no table with such name" });
            }
            Table rez = DatabaseManager.Instance.Database.GetTable(name);
            Response<Table> response = new Response<Table>
            {
                Value = rez,
                Links = new Dictionary<string, string>
                {
                    { "createTable", "api/TableController" },
                    { "deleteTable", $"api/TableController/{name}" },
                    { "updateTable", $"api/TableController/{name}" }
                }
            };

            return Ok(response);
        }

        // POST api/<TableController>
        /// <summary>
        /// Creates Table
        /// </summary>
        /// <response code="200">_Return created Table_</response>
        /// <response code="400">_There is no Name specified for table or table is already existing_</response>
        /// <response code="404">_No database created yet_</response>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Table>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Post([FromBody] string name)
        {
            if (DatabaseManager.Instance.Database == null)
            {
                return NotFound(new { error = "There is no database" });
            }
            if (name == null)
            {
                return BadRequest(new { error = "You haven't entered name of Table" });
            }
            if (DatabaseManager.Instance.Database.GetTable(name) != null)
            {
                return BadRequest(new { error = "Table is already existing" });
            }

            DatabaseManager.Instance.AddTable(name);

            Response<Table> response = new Response<Table>
            {
                Value = DatabaseManager.Instance.Database.GetTable(name),
                Links = new Dictionary<string, string>
                {
                    { "getTable", $"api/TableController/{name}"},
                    { "deleteTable", $"api/TableController/{name}" },
                    { "updateTable", $"api/TableController/{name}" }

                }
            };

            return Ok(response);
        }

        // PUT api/<TableController>/name
        /// <summary>
        /// Creates Table
        /// </summary>
        /// <response code="200">_Return updated Table_</response>
        /// <response code="400">_There is no Name specified for table or table is already existing_</response>
        /// <response code="404">_No database created yet or no table with such name_</response>
        [HttpPut("{name}")]
        [ProducesResponseType(typeof(Response<Table>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Put(string name, [FromBody] string value)
        {

            if (DatabaseManager.Instance.Database == null)
            {
                return NotFound(new { error = "There is no database" });
            }
            if (name == null||value == null)
            {
                return BadRequest(new { error = "You haven't entered old or new name of Table" });
            }
            if (DatabaseManager.Instance.Database.GetTable(name) == null)
            {
                return NotFound(new { error = "There is no table with such name" });
            }

            DatabaseManager.Instance.Database.GetTable(name).Name = value;

            Response<Table> response = new Response<Table>
            {
                Value = DatabaseManager.Instance.Database.GetTable(value),
                Links = new Dictionary<string, string>
                {
                    { "getTable", $"api/TableController/{name}"},
                    { "createTable", "api/TableController" },
                    { "deleteTable", $"api/TableController/{name}" }
                }
            };

            return Ok(response);

        }

        // DELETE api/<TableController>/name
        /// <summary>
        /// Delete Table by name from Database
        /// </summary>
        /// <response code="200">_Table deleted Successfully_</response>
        /// <response code="400">_There is no Name specified for table_</response>
        /// <response code="404">_No database created yet or no table with such name_</response>
        [HttpDelete("{name}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Delete(string name)
        {
            if (DatabaseManager.Instance.Database == null)
            {
                return NotFound(new { error = "There is no database" });
            }
            if (name == null)
            {
                return BadRequest(new { error = "You haven't entered new name of Table" });
            }
            if (DatabaseManager.Instance.Database.GetTable(name) == null)
            {
                return NotFound(new { error = "There is no table with such name" });
            }

            DatabaseManager.Instance.DeleteTable(name);

            Response<string> response = new Response<string>
            {
                Value = "Deleted successful",
                Links = new Dictionary<string, string>{
                    { "getTable", $"api/TableController/{name}"},
                    { "createTable", "api/TableController" },
                    { "updateTable", $"api/TableController/{name}" }
                }
            };
            return Ok(response);
        }

        /// <summary>
        /// Returns Table Multiply
        /// </summary>
        /// <response code="200">_Table multiplied Successfully_</response>
        /// <response code="400">_There is no Name specified for table_</response>
        /// <response code="404">_No database created yet or no table with such name_</response>
        [HttpGet]
        [Route("/{tableName1}/{tableName2}")]
        [ProducesResponseType(typeof(Response<Table>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult GetMultiply(string tableName1, string tableName2)
        {
            if (DatabaseManager.Instance.Database == null)
            {
                return NotFound(new { error = "There is no database" });
            }
            if (tableName1 == null)
            {
                return BadRequest(new { error = "You haven't entered new name of Table" });
            }
            if (tableName2 == null)
            {
                return BadRequest(new { error = "You haven't entered new name of Table" });
            }
            if (DatabaseManager.Instance.Database.GetTable(tableName1) == null)
            {
                return NotFound(new { error = "There is no table with such name" });
            }
            if (DatabaseManager.Instance.Database.GetTable(tableName2) == null)
            {
                return NotFound(new { error = "There is no table with such name" });
            }

            Table result = DatabaseManager.Instance.Multiply(tableName1, tableName2);

            Response<Table> response = new Response<Table>
            {
                Value = result,
                Links = new Dictionary<string, string>{
                    
                }
            };
            return Ok(response);

        }
    }
}
