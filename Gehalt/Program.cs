using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GehaltIEnumerable
{
    /// <summary>
    /// Beispiel für die Verwendung einer Aufzählung
    /// Durch die generische Schnittstelle ist Rückgabetyp in "foreach" definiert
    /// </summary>
    public class CGehaltCollection : IEnumerable<CGehalt>
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
        /// der generische GetEnumerator liefert die typisierten Objekte der Aufzählung einzeln zurück
        /// </summary>
        /// <returns>jeweils ein Objekt der Aufzählung</returns>
        public IEnumerator<CGehalt> GetEnumerator()
        {
            // die Variante "zu Fuß": einen neuen Enumerator bauen, der alle Einträge in der Aufzählung einzeln zurückgibt
            for (int i = 0; i < _gehaltcollection.Length; i++)
                yield return _gehaltcollection[i];

            // die einfache Variante: den bereits bestehenden Enumerator nutzen
            //return ((IEnumerable<CGehalt>)_gehaltcollection).GetEnumerator();
        }

        /// <summary>
        /// Der nichtgenerische GetEnumerator wird von der Schnittstelle erzwungen
        /// </summary>
        /// <returns>jeweils ein Objekt der Aufzählung</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
                Console.WriteLine(item.Betrag.ToString("#,#.00#").PadLeft(10));
            }
            Console.ReadLine();
        }
    }
}
