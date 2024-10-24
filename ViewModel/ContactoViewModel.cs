using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajes.Models;

namespace Viajes.ViewModel
{
    public class ContactoViewModel
    {
        public Contacto? FormContacto  {get; set;}
        public IEnumerable<Contacto>? ListContacto  {get; set;}
    }
}