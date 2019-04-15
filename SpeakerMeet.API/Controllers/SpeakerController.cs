using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SpeakerMeet.API.Controllers
{
    public class Speaker
    {
        public string Name { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class SpeakerController : ControllerBase
    {
        private readonly List<Speaker> _speakers;
        public SpeakerController()
        {
            _speakers = new List<Speaker>() { 
                new Speaker { 
                    Name = "Jim Speaker"
                    }, 
                new Speaker { 
                    Name = "Joshua Smith"
                    },
                new Speaker
                {
                    Name = "Josh Martin"
                }
                };
        }

        public IActionResult Search(string term)
        {
            return new OkObjectResult(_speakers.Where(s => s.Name.Contains(term)).ToList());
        }
    }
}