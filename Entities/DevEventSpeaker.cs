using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP._NET_Core_Web_API.Entities
{
    public class DevEventSpeaker
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TalkTitle { get; set; }
        public string TalkDescription { get; set; }
        public string LinkedInProfile { get; set; }
    }
}