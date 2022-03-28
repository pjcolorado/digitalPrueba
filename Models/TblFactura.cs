using System;
using System.Collections.Generic;

#nullable disable

namespace Prueba.Backend.Models
{
    public partial class TblFactura
    {
        public TblFactura()
        {
            TblFacturaProductos = new HashSet<TblFacturaProducto>();
        }

        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public decimal TotalVenta { get; set; }

        public virtual TblCliente IdClienteNavigation { get; set; }
        public virtual ICollection<TblFacturaProducto> TblFacturaProductos { get; set; }
    }
}
