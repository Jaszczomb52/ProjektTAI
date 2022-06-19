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

    public class CzescUzytaDoZlecenium
    {
        public int id { get; set; }
        public DateTime dataWpisu { get; set; }
        public int idzlecenia { get; set; }
        public int idczesci { get; set; }
        public object idczesciNavigation { get; set; }
        public object idzleceniaNavigation { get; set; }
    }

    public class IdmodeluNavigation
    {
        public int id { get; set; }
        public string model { get; set; }
        public List<object> czescNaMagazynies { get; set; }
    }

    public class IdproducentaNavigation
    {
        public int id { get; set; }
        public string nazwa { get; set; }
        public List<object> czescNaMagazynies { get; set; }
    }

    public class IdtypuNavigation
    {
        public int id { get; set; }
        public string typ { get; set; }
        public List<object> czescNaMagazynies { get; set; }
    }

    public class CzescNaMagazyny
    {
        public int id { get; set; }
        public string kodSegmentu { get; set; }
        public int idproducenta { get; set; }
        public int idtypu { get; set; }
        public int idmodelu { get; set; }
        public bool archiwum { get; set; }
        public Models idmodeluNavigation { get; set; }
        public Producent idproducentaNavigation { get; set; }
        public Type idtypuNavigation { get; set; }
        public List<CzescUzytaDoZlecenium> czescUzytaDoZlecenia { get; set; }
    }


}
