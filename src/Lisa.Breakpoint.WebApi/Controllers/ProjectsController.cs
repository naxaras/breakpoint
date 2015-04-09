using Lisa.Breakpoint.Models;
using Lisa.Breakpoint.WebApi.Data;
using System.Linq;
using System.Web.Http;

namespace Lisa.Breakpoint.WebApi.Controllers
{
    public class ProjectsController : ApiController
    {
        public ProjectsController()
        {
            _db = new Context();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_db.Projects);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                // An Id that is smaller then 1 will never return an item.
                return BadRequest("Id must be greater that 0.");
            }

            var project = _db.Projects.FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]Project value)
        {
            if(value == null || Project.IsInitialized(value))
            {
                // The project was not properly initalized.
                return BadRequest("The item was not initialized.");
            }

            _db.Projects.Add(value);
            _db.SaveChanges();

            return Created(string.Format("/Projects/{0}", value.Id), value);
        }

        private Context _db;
    }
}