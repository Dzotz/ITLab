using ITLab.Classes.Database;
using Microsoft.AspNetCore.Mvc;
using ITLabAPI;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITLabAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        // GET: api/DatabaseController
        /// <summary>
        /// Returns created Database from Database Manager
        /// </summary>
        /// <response code="200">_Return Database_</response>
        /// <response code="404">_No database created yet_</response>
        [HttpGet]
        [ProducesResponseType(typeof(Response<Database>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Get()
        {
            if(DatabaseManager.Instance.Database == null)
            {
                return NotFound(new { error = "There is no database" });

            }
            Response<Database> response = new Response<Database>
            {
                Value = DatabaseManager.Instance.Database,
                Links = new Dictionary<string, string>{
                    { "createDatabase", "api/DatabaseController" },
                    { "deleteDatabase", "api/DatabaseController" },
                    { "updateDatabase", "api/DatabaseController" }
                }
            };
            return Ok(response);
        }

        // POST api/DatabaseController
        /// <summary>
        /// Creates Database with specified Name
        /// </summary>
        /// <response code="200">_Return Database created_</response>
        /// <response code="400">_Name is NULL or Database was already created_</response>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Database>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Post([FromBody] string Name)
        {
            if(Name == null)
            {
                return BadRequest(new { error = "You haven't entered name of Database" });
            }
            
            if(DatabaseManager.Instance.Database!=null&& DatabaseManager.Instance.Database.Name==Name)
            {
                return BadRequest(new { error = "You already working with this Database" });
            }
            DatabaseManager.Instance.CreateDatabase(Name);
            Response<Database> response = new Response<Database>
            {
                Value = DatabaseManager.Instance.Database,
                Links = new Dictionary<string, string>{
                    { "getDatabase", "api/DatabaseController" },
                    { "deleteDatabase", "api/DatabaseController" },
                    { "updateDatabase", "api/DatabaseController" }
                }
            };
            return Ok(response);
        }

        // PUT api/DatabaseController
        /// <summary>
        /// Updates Database with specified Name
        /// </summary>
        /// <response code="200">_Return Database updated</response>
        /// <response code="400">_Name is NULL or Database was already created_</response>
        /// <response code="404">_Database isn`t created yets_</response>
        [HttpPut]
        [ProducesResponseType(typeof(Response<Database>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Put([FromBody] string Name)
        {
           
            if (DatabaseManager.Instance.Database == null)
            {
                return NotFound(new { error = "There is no database" });
            }
            if (Name == null)
            {
                return BadRequest(new { error = "You haven't entered new name of Database" });
            }
            DatabaseManager.Instance.Database.Name = Name;
            Response<Database> response = new Response<Database>
            {
                Value = DatabaseManager.Instance.Database,
                Links = new Dictionary<string, string>{
                    { "getDatabase", "api/DatabaseController" },
                    { "deleteDatabase", "api/DatabaseController" },
                    { "createDatabase", "api/DatabaseController" }
                }
            };
            return Ok(response);
        }

        // DELETE api/DatabaseController/5
        /// <summary>
        /// Updates Database with specified Name
        /// </summary>
        /// <response code="200">_Database deleted successful</response>
        /// <response code="404">_Database isn`t created yets_</response>
        [HttpDelete]
        [ProducesResponseType(typeof(Response<string>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Delete()
        {
            if (DatabaseManager.Instance.Database == null)
            {
                return NotFound(new { error = "There is no database" });
            }
            DatabaseManager.Instance.Database = null;
            Response<string> response = new Response<string>
            {
                Value = "Deleted successful",
                Links = new Dictionary<string, string>{
                    { "getDatabase", "api/DatabaseController" },
                    { "updateDatabase", "api/DatabaseController" },
                    { "createDatabase", "api/DatabaseController" }
                }
            };
            return Ok(response);
        }
    }
}
