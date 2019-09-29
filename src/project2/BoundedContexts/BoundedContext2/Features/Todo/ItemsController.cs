using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace project2.BoundedContexts.BoundedContext2.Features.Todo
{
    [Route("api/todo")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        // GET: api/todo
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/todo/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return id.ToString();
        }

        // POST: api/todo
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/todo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/todo/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
