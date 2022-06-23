using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektTAI
{
    //public class Emplo
    //{
    //    [DisplayName("Identyfikator")]
    //    public int Id { get; set; }
    //    [DisplayName("Imię")]
    //    public string Imie { get; set; }
    //    [DisplayName("Nazwisko")]
    //    public string Nazwisko { get; set; }
    //    [DisplayName("Numer telefonu")]
    //    public string NumerTelefonu { get; set; }
    //    public List<object> SpecjalizacjePracownikas { get; set; }
    //    public List<object> Zlecenies { get; set; }

        
    //}


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
        public List<CzescNaMagazyny> czescNaMagazynies { get; set; }
    }

    public class IdproducentaNavigation
    {
        public int id { get; set; }
        public string nazwa { get; set; }
        public List<CzescNaMagazyny> czescNaMagazynies { get; set; }
    }

    public class IdtypuNavigation
    {
        public int id { get; set; }
        public string typ { get; set; }
        public List<CzescNaMagazyny> czescNaMagazynies { get; set; }
    }

    public class CzescNaMagazyny
    {
        public int id { get; set; }
        [DisplayName("Kod segmentu")]
        public string kodSegmentu { get; set; }
        public int idproducenta { get; set; }
        public int idtypu { get; set; }
        public int idmodelu { get; set; }
        [DisplayName("Zarchiwizowane")]
        public bool archiwum { get; set; }
        [DisplayName("Model części")]
        public Models? idmodeluNavigation { get; set; }
        [DisplayName("Producent części")]
        public Producent? idproducentaNavigation { get; set; }
        [DisplayName("Typ części")]
        public Type? idtypuNavigation { get; set; }
        public List<CzescUzytaDoZlecenium>? czescUzytaDoZlecenia { get; set; }
    }

    public class CustomCzescNaMagazyny
    {
        [DisplayName("Kod segmentu")]
        public string kodSegmentu { get; set; }
        [DisplayName("Zarchiwizowane")]
        public bool archiwum { get; set; }
        [DisplayName("Model części")]
        public Models idmodeluNavigation { get; set; }
        [DisplayName("Producent części")]
        public Producent idproducentaNavigation { get; set; }
        [DisplayName("Typ części")]
        public Type idtypuNavigation { get; set; }
    }

    public class Emplo
    {
        [DisplayName("Identyfikator")]
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NumerTelefonu { get; set; }
        public List<SpecjalizacjePracownika> SpecjalizacjePracownikas { get; set; }
        public List<Zleceny> Zlecenies { get; set; }

        public override string ToString()
        {
            return $"{Imie} {Nazwisko} {NumerTelefonu}";
        }
    }
    public class CustomSpecjalizacjePracownika
    {
        
        [DisplayName("Diagnostyka problemu\n(1-5)")]
        public int Diagnostyka { get; set; }
        [DisplayName("Naprawa części\n(1-5)")]
        public int NaprawaCzesci { get; set; }
        [DisplayName("Naprawa oprogramowania\n(1-5)")]
        public int NaprawaSoftu { get; set; }
        [DisplayName("Budowa urządzeń\n(1-5)")]
        public int Budowanie { get; set; }
        
    }

    public class SpecjalizacjePracownika
    {
        [DisplayName("Identyfikator")]
        public int Id { get; set; }
        public int Diagnostyka { get; set; }
        public int NaprawaCzesci { get; set; }
        public int NaprawaSoftu { get; set; }
        public int Budowanie { get; set; }
        public int Idpracownika { get; set; }
        public object IdpracownikaNavigation { get; set; }
    }


    public class Zleceny
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public bool KontaktTelefoniczny { get; set; }
        public DateTime DataPrzyjecia { get; set; }
        public double Koszt { get; set; }
        public object DataWydania { get; set; }
        public object NumerTelefonu { get; set; }
        public string Status { get; set; }
        public bool SzybkieZlecenie { get; set; }
        public string OpisZlecenia { get; set; }
        public int Idpracownika { get; set; }
        public object IdpracownikaNavigation { get; set; }
        public List<object> CzescUzytaDoZlecenia { get; set; }
    }
}
