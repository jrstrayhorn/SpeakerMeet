using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeakerMeet.API.Models;
using SpeakerMeet.API.Services;

namespace SpeakerMeet.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SpeakerController : ControllerBase
    {
        private readonly ISpeakerService _speakerService;
        private readonly List<Speaker> _speakers;
        public SpeakerController(ISpeakerService speakerService)
        {
            _speakers = new List<Speaker>() { 
                new Speaker { 
                    Name = "Jim"
                    }, 
                new Speaker { 
                    Name = "Josh"
                    },
                new Speaker {
                    Name = "Joshua"
                    },new Speaker {
                    Name = "Joseph"
                    }
                };
            _speakerService = speakerService;
        }

        public IActionResult Search(string term)
        {
            _speakerService.Search(term);
            return new OkObjectResult(_speakers.Where(s => s.Name.StartsWith(term, StringComparison.OrdinalIgnoreCase)).ToList());
        }
    }
}