using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace SystemProwizji
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Tworzymy listy które przechowują stworzonych użytkowników oraz przelewy.
            LinkedList<Uczestnik> Uczestnicy = new LinkedList<Uczestnik>();
            List<Przelew> Przelewy = new List<Przelew>();

            //Część odpowiedzialana za wczytanie pliku xml oraz znalezenie odpowiednich nodeów w plikach XML.
            XmlDocument xmlStruktura = new XmlDocument();
            string strukturaPath = Directory.GetCurrentDirectory() + @"\struktura.xml";
            xmlStruktura.Load(strukturaPath);
            XmlNodeList nodeListStruktura = xmlStruktura.GetElementsByTagName("uczestnik");

            XmlDocument xmlPrzelewy = new XmlDocument();
            string przelewyPath = Directory.GetCurrentDirectory() + @"\przelewy.xml";
            xmlPrzelewy.Load(przelewyPath);
            XmlNodeList nodeListPrzelewy = xmlPrzelewy.GetElementsByTagName("przelew");

            //Odczytywanie wartości obietku Uzytkownika z kazdego z nodeów stworzonych za pomocą pliku struktura.xml
            //jeżeli id użytkownika = 1, nie ma on przełozonego stad muisałem pokryć ten wyjątek w pętli.
            foreach (XmlNode node in nodeListStruktura)
            {
                int id = int.Parse(node.Attributes[0].Value);
                int subordinates = node.ChildNodes.Count;

                if (id == 1)
                {
                    Uczestnik nowyUczestnik = new Uczestnik(id, 0, subordinates, 0);
                    Uczestnicy.AddLast(nowyUczestnik);
                }
                else
                {
                    Uczestnik nowyUczestnik = new Uczestnik(id, 0, subordinates, int.Parse(node.ParentNode.Attributes[0].Value));
                    Uczestnicy.AddLast(nowyUczestnik);
                }
            }

            //Nadawanie wartośći poziomu zajmowanego przez uczestnika w systemie.
            foreach (Uczestnik uczestnik in Uczestnicy)
            {
                var szefUczestnikaTemp = uczestnik;

                while (szefUczestnikaTemp.BossId != 0)
                {
                    uczestnik.Group = uczestnik.Group + 1;
                    szefUczestnikaTemp = Uczestnik.FindUczestnik(szefUczestnikaTemp.BossId, Uczestnicy);
                }
            }

            //Odczytywanie wartości obietku Przelew z kazdego z nodeów stworzonych za pomocą pliku przelewy.xml
            foreach (XmlNode node in nodeListPrzelewy)
            {
                int adresatPrzelewu = int.Parse(node.Attributes[0].Value);
                double kwotaPrzelewu = double.Parse(node.Attributes[1].Value);

                Przelew nowyPrzelew = new Przelew(adresatPrzelewu, kwotaPrzelewu);

                Przelewy.Add(nowyPrzelew);
            }

            //Wyszukanie uzytkownika odpowiedzialnego za dany przelew
            foreach (Przelew przelew in Przelewy)
            {
                double kwotaPrzelewu = przelew.KwotaPrzelewu;
                int adresatPrzelewu = przelew.AdresatPrzelewu;
                var wlascicielPrzelewu = Uczestnik.FindUczestnik(adresatPrzelewu, Uczestnicy);
                int[] idPrzełozonych = new int[wlascicielPrzelewu.Group];

                //Tworzenie listy przełozonych danego użytkownika
                var testowySzef = Uczestnik.FindUczestnik(wlascicielPrzelewu.BossId, Uczestnicy);
                for (int i = 0; i < wlascicielPrzelewu.Group; i++)
                {
                    idPrzełozonych[i] = testowySzef.Id;
                    testowySzef = Uczestnik.FindUczestnik(testowySzef.BossId, Uczestnicy);
                }

                int iloscPrzelozonych = idPrzełozonych.Length;

                //Wykorzystanie listy przełozonych danego użytkownika aby dodac pieniadze do ich konta.
                for (int najwyzszyPrzelozony = iloscPrzelozonych - 1; najwyzszyPrzelozony >= 0; najwyzszyPrzelozony--)
                {
                    kwotaPrzelewu = kwotaPrzelewu * 0.5;
                    testowySzef = Uczestnik.FindUczestnik(idPrzełozonych[najwyzszyPrzelozony], Uczestnicy);
                    testowySzef.Money = testowySzef.Money + kwotaPrzelewu;
                }

                testowySzef.Money = testowySzef.Money + kwotaPrzelewu;
            }

            //Wypisanie danych
            foreach (Uczestnik uczestnik in Uczestnicy)
            {
                Console.WriteLine($"{uczestnik.Id} {uczestnik.Group} {uczestnik.Subordinates} {uczestnik.Money}");
            }
        }
    }
}
