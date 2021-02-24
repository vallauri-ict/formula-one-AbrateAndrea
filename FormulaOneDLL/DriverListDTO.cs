using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOneDLL
{
    public class DriverListDTO
    {
        public DriverListDTO(int number, string name, byte[] image, string teamName, string countryName)
        {
            this.number = number;
            this.name = name;
            this.image = image;
            this.teamName = teamName;
            this.countryName = countryName;
        }

        public int number { get; set; }
        public string name { get; set; }
        public byte[] image { get; set; }
        public string teamName { get; set; }
        public string countryName { get; set; }
    }
}