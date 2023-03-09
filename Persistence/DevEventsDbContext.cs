using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP._NET_Core_Web_API.Entities;

namespace ASP._NET_Core_Web_API.Persistence
{
    public class DevEventsDbContext
    {
        public List<DevEvent> Events { get; set; }

        public DevEventsDbContext()
        {
            Events = new List<DevEvent>();
        }
    }
}