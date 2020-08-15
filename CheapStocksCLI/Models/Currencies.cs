using System;
using System.Collections.Generic;
using System.Text;

namespace CheapStocksCLI.Models
{
    /// <summary>
    /// This class represents the data stored in the csv file.
    /// </summary>
    public class Currencies
    {
        public string Country { get; set; }
        public string Currency { get; set; }
        public string ISO { get; set; }
    }
}
