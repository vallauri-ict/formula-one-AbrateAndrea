using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOneDLL
{
    public class DriverListDTO
    {
        public DriverListDTO(int number, string name, byte[] image, string teamName, string countryCode)
        {
            Number = number;
            Name = name;
            Image = image;
            TeamName = teamName;
            CountryCode = countryCode;
        }

        public int Number { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string TeamName { get; set; }
        public string CountryCode { get; set; }

        public string CountryFlag
        {
            get
            {
                return String.Format("https://www.countryflags.io/{0}/flat/64.png", CountryCode.ToLower());
            }
        }
    }
}
