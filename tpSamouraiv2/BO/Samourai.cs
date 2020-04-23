using System.Collections.Generic;

namespace BO
{
    public class Samourai : Dojo
    {

        public int Force { get; set; }
        public string Nom { get; set; }
        public virtual Arme Arme { get; set; }
        public int Potentiel { get; set; }
        public virtual List<ArtMartial> ArtsMartiaux { get; set; } = new List<ArtMartial>();
    }
}
