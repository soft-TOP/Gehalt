using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gehalt
{
    /// <summary>
    /// Beispiel für die Verwendung einer Aufzählung
    /// </summary>
    public class CGehaltCollection : IEnumerable
    {
        private readonly CGehalt[] _gehaltcollection;

        public CGehaltCollection(CGehalt[] gehaltarray)
        {
            // die Anzahl der Einträge einmal ermitteln
            // - spart Rechenzeit
            // - verhindert fehler bei den Arraygrenzen
            int count = gehaltarray.Length;

            // das klasseninterne Array initialisieren
            _gehaltcollection = new CGehalt[count];

            // das interne Array mit den Werten des übergebenen befüllen
            for (int i = 0; i < count; i++)
                _gehaltcollection[i] = gehaltarray[i];
        }

        /// <summary>
        /// liefert die Objekte der Aufzählung einzeln zurück
        /// </summary>
        /// <returns>jeweils ein Objekt der Aufzählung</returns>
        public IEnumerator GetEnumerator()
        {
            // die einfache Variante: den bereits bestehenden Enumerator nutzen
            //return _gehaltcollection.GetEnumerator();

            // die Variante "zu Fuß": einen neuen Enumerator bauen, der alle Einträge in der Aufzählung einzeln zurückgibt
            for (int i = 0; i < _gehaltcollection.Length; i++)
                yield return _gehaltcollection[i];
        }

    }
    public class CGehalt
    {
        public CGehalt(decimal b) { Betrag = b; }

        public decimal Betrag { get; set; }
    }



    class Program
    {
        static void Main(string[] args)
        {
            CGehalt Gehalt;
            CGehalt[] GehaltArray = new CGehalt[7];

            // das gesamte Array befüllen
            for (int i = 0; i < GehaltArray.Length; i++)
            {
                // für das Gehalt einen einigermaßen sinnvollen Wert berechnen
                Gehalt = new CGehalt(77.8M * (i+1));
                GehaltArray[i] = Gehalt;
            }

            // das Array in die Collection überführen
            CGehaltCollection GehaltCollection = new CGehaltCollection(GehaltArray);

            // mal sehen, was da so drin steht...
            foreach (var item in GehaltCollection)
            {
                Console.WriteLine(((CGehalt)item).Betrag.ToString("#,#.00#").PadLeft(10));
            }
            Console.ReadLine();
        }
    }
}
