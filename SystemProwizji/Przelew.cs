using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemProwizji
{
    public class Przelew
    {
        /// <summary>
        /// Kwota przelewu
        /// </summary>
        public double KwotaPrzelewu { get; set; }

        /// <summary>
        /// Id uzytkownika do ktorego kierowany jest przelew
        /// </summary>
        public int AdresatPrzelewu { get; set; }

        public Przelew(int AdresatPrzelewu, double KwotaPrzelewu)
        {
            this.KwotaPrzelewu = KwotaPrzelewu;
            this.AdresatPrzelewu = AdresatPrzelewu;
        }
    }
}
