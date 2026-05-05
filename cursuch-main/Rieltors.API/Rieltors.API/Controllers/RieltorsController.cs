using Microsoft.AspNetCore.Mvc;
using Rieltors.API.Models;

namespace Rieltors.API.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class RieltorController : ControllerBase
        {
            [HttpGet]
            public ActionResult<List<Realtor>> GetAll()
            {
            var Rieltors = new List<Realtor> {
                new Realtor
                {
                    Id = 1,
                    Name = "Bob",
                    Experience = 12,
                    Price = 10000,
                    Number = 676689945,
                }
                };
                return Ok(Rieltors);
            }
        }

}

