using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterAvailability.Dto
{
    public class IssueDetail
    {

        public int? Id { get; set; }
        public DateTime Date { get; set; }

        public string Description { get; set; }
        public string ContentType { get; set; }
       
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string InternalId { get; set; }
    }
}
