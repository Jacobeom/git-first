using System;

namespace TwitterAvailability.Dto
{
    public class Product
    {
        public int? Id { get; set; }
        public string ProductName { get; set; }
        public string ProductKey { get; set; }
        public bool Hidden { get; set; }
    }
}
