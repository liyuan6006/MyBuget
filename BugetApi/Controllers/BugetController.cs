using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BugetApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BugetApi.Controllers
{
    [Route("api/[controller]")]
    public class BugetController : Controller
    {
        private readonly BugetContext _context;
        public BugetController(BugetContext context) {
            _context = context;

            if (_context.BugetItems.Count()==0)
            {
                _context.BugetItems.Add(new BugetItem { Name = "Item1", Description = "Item1 description" });
                _context.SaveChanges();
            }
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<BugetItem> Get()
        {
            return _context.BugetItems.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetBuget")]
        public IActionResult Get(int id)
        {
            //Expression<Func<BugetItem, bool>> lambda = buget => buget.Id == id;
            var item = _context.BugetItems.FirstOrDefault(s=>s.Id==id);
            if (item==null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]BugetItem item)
        {
            if (item==null)
            {
                return BadRequest();
            }
            _context.BugetItems.Add(item);
            _context.SaveChanges();
            return CreatedAtRoute("GetBuget", new { id = item.Id }, item);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]BugetItem item)
        {
            if (item==null||item.Id!=id)
            {
                return BadRequest();
            }

            var bugetItem =_context.BugetItems.FirstOrDefault(s => s.Id == id);
            if (bugetItem==null)
            {
                return NotFound();
            }
            bugetItem.Name = item.Name;
            bugetItem.Description = item.Description;
            _context.BugetItems.Update(bugetItem);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bugetItem = _context.BugetItems.FirstOrDefault(s => s.Id == id);
            if (bugetItem==null)
            {
                return NotFound();
            }
            _context.BugetItems.Remove(bugetItem);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
