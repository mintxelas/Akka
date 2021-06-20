using ActorCluster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuideController : ControllerBase
    {
        private readonly ILogger<GuideController> _logger;

        public GuideController(ILogger<GuideController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<GroupGuide> Get()
        {
            throw new NotImplementedException();
        }
    }
}
