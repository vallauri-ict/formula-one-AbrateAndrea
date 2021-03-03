using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOneDLL
{
    public class TeamListDTO
    {
            public TeamListDTO(string TeamName, byte[] TeamLogo, string[] Name, List<byte[]> Image, byte[] CarImage)
            {
                this.TeamName = TeamName;
                this.TeamLogo = TeamLogo;
                this.Name = Name;
                this.Image = Image;
                this.CarImage = CarImage;
            }

            public string TeamName { get; set; }
            public byte[] TeamLogo { get; set; }
            public string[] Name { get; set; }
            public List<byte[]> Image { get; set; }
            public byte[] CarImage { get; set; }
    }
}