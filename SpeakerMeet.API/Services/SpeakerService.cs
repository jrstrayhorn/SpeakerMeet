using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpeakerMeet.API.Models;

namespace SpeakerMeet.API.Services
{
    public class SpeakerService : ISpeakerService
    {
        private readonly List<Speaker> _speakers;
        public SpeakerService()
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
                    },
                new Speaker {
                    Name = "Joseph"
                    }
                };
        }

        public IEnumerable<Speaker> Search(string searchString)
        {
            return _speakers.Where(s => 
                s.Name.StartsWith(searchString, StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}
