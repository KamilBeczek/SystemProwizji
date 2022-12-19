using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemProwizji
{
    public class Uczestnik
    {
        /// <summary>
        /// Id Uzytkownika.
        /// </summary>
        public int Id;

        /// <summary>
        /// Grupa którą zajmuję uzytkownik.
        /// </summary>
        public int Group;

        /// <summary>
        /// Ilość podwładnych użytkownika.
        /// </summary>
        public int Subordinates;

        /// <summary>
        /// Id przełozonego użytkownika.
        /// </summary>
        public int BossId;

        /// <summary>
        /// Ilość pieniędzy posiadanej przez użytkownika.
        /// </summary>
        public double Money;

        public Uczestnik(int id, int group, int subordinates, int bossid)
        {
            this.Id = id;
            this.Group = group;
            this.Subordinates = subordinates;
            this.BossId = bossid;
            this.Money = 0;
        }

        /// <summary>
        /// Metoda uzywana do znalezienia odpowiedniego uzytkownika w litscie uzytkowników.
        /// </summary>
        /// <param name="numer"></param>
        /// <param name="lista"></param>
        /// <returns></returns>
        public static Uczestnik FindUczestnik(int idUzytkownika, LinkedList<Uczestnik> listaUzytkownikow)
        {
            foreach (Uczestnik szukanyUzytkownik in listaUzytkownikow)
            {
                if (szukanyUzytkownik.Id == idUzytkownika)
                {
                    return szukanyUzytkownik;
                }
            }
            return null;
        }
    }
}
