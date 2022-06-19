using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTAI
{
    public class Emplo
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NumerTelefonu { get; set; }
        public List<object> SpecjalizacjePracownikas { get; set; }
        public List<object> Zlecenies { get; set; }
    }

    public class CzescNaMagazyny
    {
        public int Id { get; set; }
        public string KodSegmentu { get; set; }
        public int Idproducenta { get; set; }
        public int Idtypu { get; set; }
        public int Idmodelu { get; set; }
        public bool Archiwum { get; set; }
        public object IdmodeluNavigation { get; set; }
        public object IdproducentaNavigation { get; set; }
        public object IdtypuNavigation { get; set; }
        public List<object> CzescUzytaDoZlecenia { get; set; }
    }

    public interface IDictionaries 
    {
        public int Id { get; set; }
        public List<CzescNaMagazyny> CzescNaMagazynies { get; set; }
    }

    public class Producent : IDictionaries
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }

        public List<CzescNaMagazyny> CzescNaMagazynies { get; set; }

        public override string ToString()
        {
            return $"{Nazwa}";
        }
    }

    public class Type : IDictionaries
    {
        public int Id { get; set; }
        public string Typ { get; set; }
        public List<CzescNaMagazyny> CzescNaMagazynies { get; set; }

        public override string ToString()
        {
            return $"{Typ}";
        }
    }

    public class Models : IDictionaries
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public List<CzescNaMagazyny> CzescNaMagazynies { get; set; }

        public override string ToString()
        {
            return $"{Model}";
        }
    }


}
