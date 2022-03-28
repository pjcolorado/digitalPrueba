using System;
using System.Collections.Generic;

#nullable disable

namespace Prueba.Backend.Models
{
    public partial class TblCliente
    {
        public TblCliente()
        {
            TblFacturas = new HashSet<TblFactura>();
        }

        public int Id { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public virtual ICollection<TblFactura> TblFacturas { get; set; }
    }
}
