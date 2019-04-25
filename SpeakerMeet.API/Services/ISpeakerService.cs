using SpeakerMeet.API.Controllers;
using SpeakerMeet.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeakerMeet.API.Services
{
    public interface ISpeakerService
    {
        IEnumerable<Speaker> Search(string searchString);
    }
}
