using Lisa.Breakpoint.Models;
using Lisa.Breakpoint.WebApi.Data;
using System.Web.Http;

namespace Lisa.Breakpoint.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        public UsersController()
        {
            _db = new Context();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_db.Users);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                // An Id that is smaller then 1 will never return an item.
                return BadRequest("Id must be greater the 0");
            }

            var user = _db.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]User value)
        {
            if(!Lisa.Breakpoint.Models.User.IsInitialized(value))
            {
                // The user was not properly initalized.
                return BadRequest("The item was not initialized.");
            }

            _db.Users.Add(value);
            _db.SaveChanges();

            return Created(string.Format("/Users/{0}", value.Id), value);
        }

        private Context _db;
    }
}
