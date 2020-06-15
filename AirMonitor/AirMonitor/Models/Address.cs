using System;
using System.Collections.Generic;
using System.Text;

namespace AirMonitor.Models
{
    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string DisplayAddress1 { get; set; }
        public string DisplayAddress2 { get; set; }
        public string DisplayAddressFull { get { return this.DisplayAddress1 + ", " + DisplayAddress2; } }

        public Address()
        {

        }
    }
}
