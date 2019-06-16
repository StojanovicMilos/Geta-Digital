using System.Collections.Generic;
using System.Linq;
using Task3.BLL.BO;
using Task3.BLL.Interfaces;

namespace Task3.DAL
{
    public class ContactPersonDAO : IContactPersonDAO
    {
        public IQueryable<ContactPerson> GetContactPersons()
            => new List<ContactPerson>
                {
                    new ContactPerson("Sørfold", "Nordland", "Kjell.Stokbakken@Legacy.com"),
                    new ContactPerson("Gildeskål", "Nordland", "Kjell.Stokbakken@Legacy.com"),
                    new ContactPerson("Rødøy", "Nordland", "Kjell.Stokbakken@Legacy.com"),
                    new ContactPerson("Dønna", "Nordland", "Kjell.Stokbakken@Legacy.com"),
                    new ContactPerson("Herøy", "Nordland", "Kjell.Stokbakken@Legacy.com"),
                    new ContactPerson("Alstahaug", "Nordland", "Kjell.Stokbakken@Legacy.com"),
                    new ContactPerson("Brønnøy", "Nordland", "Kjell.Stokbakken@Legacy.com"),
                    new ContactPerson("Sømna", "Nordland", "Kjell.Stokbakken@Legacy.com"),
                    new ContactPerson("Leka", "Nord Trøndelag", "Kjell.Stokbakken@Legacy.com"),
                    new ContactPerson("Nærøy", "Nord Trøndelag", "Kjell.Stokbakken@Legacy.com"),
                    new ContactPerson("Meløy", "Nordland", "Kjell.Stokbakken@Legacy.com"),
                    new ContactPerson("Høylandet", "Nord Trøndelag", "Kjell.Stokbakken@Legacy.com"),
                    new ContactPerson("Bodø", "Nordland", "Kjell.Stokbakken@Legacy.com"),
                    new ContactPerson("Fosnes", "Nord Trøndelag", "knut.utheim@Legacy.com"),
                    new ContactPerson("Flatanger", "Nord Trøndelag", "knut.utheim@Legacy.com"),
                    new ContactPerson("Osen", "Sør Trøndelag", "knut.utheim@Legacy.com"),
                    new ContactPerson("Frøya", "Sør Trøndelag", "knut.utheim@Legacy.com"),
                    new ContactPerson("Hitra", "Sør Trøndelag", "knut.utheim@Legacy.com"),
                    new ContactPerson("Smøla", "Møre og Romsdal", "knut.utheim@Legacy.com"),
                    new ContactPerson("Averøy", "Møre og Romsdal", "knut.utheim@Legacy.com"),
                    new ContactPerson("Roan", "Sør Trøndelag", "knut.utheim@Legacy.com"),
                    new ContactPerson("Snillfjord", "Sør Trøndelag", "knut.utheim@Legacy.com"),
                    new ContactPerson("Aure", "Møre og Romsdal", "knut.utheim@Legacy.com"),
                    new ContactPerson("Bjugn", "Sør Trøndelag", "knut.utheim@Legacy.com"),
                    new ContactPerson("mrHeroy", "Møre og Romsdal", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Volda", "Møre og Romsdal", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Vanylven", "Møre og Romsdal", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Selje", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Vågsøy", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Bremanger", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Ørsta", "Møre og Romsdal", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Ulstein", "Møre og Romsdal", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Flora", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Leikanger", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Høyanger", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Fjaler", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Solund", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Hyllestad", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Gulen", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Ålesund", "Møre og Romsdal", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Aukra", "Møre og Romsdal", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Fræna", "Møre og Romsdal", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Haram", "Møre og Romsdal", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Giske", "Møre og Romsdal", "Per-Roar.Gjerde@Legacy.com"),
                    new ContactPerson("Askøy", "Hordaland", "astrid.sande@Legacy.com"),
                    new ContactPerson("Fjell", "Hordaland", "astrid.sande@Legacy.com"),
                    new ContactPerson("Sund", "Hordaland", "astrid.sande@Legacy.com"),
                    new ContactPerson("Etne", "Hordaland", "astrid.sande@Legacy.com"),
                    new ContactPerson("Jondal", "Hordaland", "astrid.sande@Legacy.com"),
                    new ContactPerson("Kvinnherad", "Hordaland", "astrid.sande@Legacy.com"),
                    new ContactPerson("Tysvær", "Rogaland", "astrid.sande@Legacy.com"),
                    new ContactPerson("Vindafjord", "Rogaland", "astrid.sande@Legacy.com"),
                    new ContactPerson("Finnøy", "Rogaland", "astrid.sande@Legacy.com"),
                    new ContactPerson("Hjelmeland", "Rogaland", "astrid.sande@Legacy.com"),
                    new ContactPerson("Flekkefjord", "Vest Agder", "astrid.sande@Legacy.com"),
                    new ContactPerson("Masfjorden", "Hordaland", "astrid.sande@Legacy.com"),
                    new ContactPerson("Øygarden", "Hordaland", "astrid.sande@Legacy.com")
                }
                .AsQueryable();
    }
}
