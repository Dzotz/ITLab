using ITLab.Classes.Database;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITLabAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        // GET: api/<ColumnController>
        /// <summary>
        /// Returns Column from table by name
        /// </summary>
        /// <response code="200">_Return Column_</response>
        /// <response code="404">_No database created yet or no table with such name or no column with such name_</response>
        [HttpGet]
        [Route("{tableName}/{columnName}")]
        [ProducesResponseType(typeof(Response<Column>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Get(string tableName, string columnName)
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
            if (table.Columns.Find(x => x.Name == columnName) == null)
            {
                return NotFound(new { error = "There is no Column with such name" });
            }
            Column column = table.Columns.Find(x => x.Name == columnName);

            Response<Column> response = new Response<Column>
            {
                Value = column,
                Links = new Dictionary<string, string>{
                    { "addColumn", $"api/Column/{tableName}" },
                    { "addSetColumn", $"api/Column/Set/{tableName}" },
                    { "deleteColumn",  $"api/Column/{tableName}/{columnName}"}

                }
            };

            return Ok(response);

        }

        // POST api/<ColumnController>
        /// <summary>
        /// Creates Column in table
        /// </summary>
        /// <response code="200">_Create Column_</response>
        /// <response code="400">_There is Column with such name_</response>
        /// <response code="404">_No database created yet or no table with such name_</response>
        [HttpPost]
        [Route("{tableName}/{columnName}")]
        [ProducesResponseType(typeof(Response<Column>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Post(string tableName, string columnName, [FromBody] string type)
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
            if (table.Columns.Find(x => x.Name == columnName) != null)
            {
                return BadRequest(new { error = "There is Column with such name" });
            }

            DatabaseManager.Instance.AddColumn(tableName, columnName, type);

            Column column = table.Columns.Find(x => x.Name == columnName);

            Response<Column> response = new Response<Column>
            {
                Value = column,
                Links = new Dictionary<string, string>
                {
                    { "getColumn", $"api/Column/{tableName}/{columnName}" },
                    { "deleteColumn",  $"api/Column/{tableName}/{columnName}" }
                }
            };

            return Ok(response);
        }

        /// <summary>
        /// Creates Set Column in table
        /// </summary>
        /// <response code="200">_Create Column_</response>
        /// <response code="400">_There is Column with such name_</response>
        /// <response code="404">_No database created yet or no table with such name_</response>
        [HttpPost]
        [Route("Set/{tableName}/{columnName}")]
        [ProducesResponseType(typeof(Response<Column>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult PostSetColumn(string tableName, string columnName, [FromBody] string values)
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
            if (table.Columns.Find(x => x.Name == columnName) != null)
            {
                return BadRequest(new { error = "There is Column with such name" });
            }

            DatabaseManager.Instance.AddSetColumn(tableName, columnName, values);

            Column column = table.Columns.Find(x => x.Name == columnName);

            Response<Column> response = new Response<Column>
            {
                Value = column,
                Links = new Dictionary<string, string>
                {
                    { "getColumn", $"api/Column/{tableName}/{columnName}" },
                    { "deleteColumn",  $"api/Column/{tableName}/{columnName}" }
                }
            };

            return Ok(response);
        }

        // DELETE api/<ColumnController>/5
        /// <summary>
        /// Delete Column from table
        /// </summary>
        /// <response code="200">_Create Column_</response>
        /// <response code="404">_No database created yet or no table with such name or no column with such name_</response>
        [HttpDelete]
        [Route("{tableName}/{columnName}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Delete(string tableName, string columnName)
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
            if (table.Columns.Find(x => x.Name == columnName) == null)
            {
                return NotFound(new { error = "There is no Column with such name" });
            }
            DatabaseManager.Instance.DeleteColumn(tableName, table.Columns.FindIndex(x => x.Name == columnName));

            Response<string> response = new Response<string>
            {
                Value = "Deleted successful",
                Links = new Dictionary<string, string>
                {
                    { "getColumn", $"api/Column/{tableName}/{columnName}" },
                    { "addColumn", $"api/Column/{tableName}" },
                    { "addSetColumn", $"api/Column/Set/{tableName}" }
                }
            };

            return Ok(response);
        }
    }
}
