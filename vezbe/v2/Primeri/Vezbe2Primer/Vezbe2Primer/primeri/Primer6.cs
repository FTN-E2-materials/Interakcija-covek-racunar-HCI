using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vezbe2Primer.util;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Vezbe2Primer.primeri
{
    [Serializable] //ovo oznacava da se klasa moze serijalizovati, nista drugo ne treba, posto joj se atributi mogu serijalizovati
    class Osoba
    {
        public Guid ID
        {
            get;
            set;
        }
        public string Ime
        {
            get;
            set;
        }

        public string Prezime
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Ime + " " + Prezime;
        }
    }

    class Repozitorijum
    {
        private Dictionary<Guid, Osoba> _r = new Dictionary<Guid, Osoba>();
        private readonly string _datoteka;

        public Repozitorijum()
        {
            _datoteka = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "repozitorijum.podaci");
            UcitajDatoteku();
        }

        public void Dodaj(Osoba o)
        {
            if (o.ID == Guid.Empty)
                o.ID = Guid.NewGuid();
            if (!_r.ContainsKey(o.ID))
                _r.Add(o.ID, o);
            MemorisiDatoteku();
        }

        public void Obrisi(Osoba o)
        {
            _r.Remove(o.ID);
            MemorisiDatoteku();
        }

        public Osoba this[Guid g]
        {
            get
            {
                return _r[g];
            }
            set
            {
                _r[g] = value;
            }
        }

        private void MemorisiDatoteku()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            try
            {
                stream = File.Open(_datoteka, FileMode.OpenOrCreate);
                formatter.Serialize(stream, _r);
            }
            catch
            {
                // 
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }

        private void UcitajDatoteku()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            if (File.Exists(_datoteka))
            {
                try
                {
                    stream = File.Open(_datoteka, FileMode.Open);
                    _r = (Dictionary<Guid, Osoba>)formatter.Deserialize(stream);
                }
                catch
                {
                    // 
                }
                finally
                {
                    if (stream != null)
                        stream.Dispose();
                }

            }
            else
                _r = new Dictionary<Guid, Osoba>();
        }

        public Dictionary<Guid, Osoba> getAll()
        {
            return _r;
        }
    }

    public class Primer6 : AbstractPrimer
    {
        public Primer6(frmPrimer2 f): base(f) {}

        public override void izvrsi()
        {
            ispisi("Primer 8 - serijalizacija.\r\n");
            ispisi("#############\r\n");

            Repozitorijum r = new Repozitorijum();
            ispisi("U fajlu je:");
            ispisi("\r\n");
            foreach(Osoba o in r.getAll().Values)
            {
                ispisi(o.ToString());
                ispisi("\r\n");
            }
            Osoba o1 = new Osoba();
            Osoba o2 = new Osoba();
            Osoba o3 = new Osoba();

            o1.Ime = "Petar";
            o1.Prezime = "Petrovic";

            o2.Ime = "Nikola";
            o2.Prezime = "Nikolic";

            o3.Ime = "Jovan";
            o3.Prezime = "Jovanovic";

            r.Dodaj(o1);
            r.Dodaj(o2);
            r.Dodaj(o3);

            ispisi("Posle dodavanja u fajlu je:");
            ispisi("\r\n");
            foreach (Osoba o in r.getAll().Values)
            {
                ispisi(o.ToString());
                ispisi("\r\n");
            }
        }

    }
}