using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using CMAP.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CMAP.Controllers.api
{
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return new List<Event> { new Event() { Id = 1 } };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Event Get(int id)
        {
            return new Event() { Id = 1 };
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
