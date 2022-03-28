using System;
using System.Collections.Generic;

#nullable disable

namespace Prueba.Backend.Models
{
    public partial class TblProducto
    {
        public TblProducto()
        {
            TblFacturaProductos = new HashSet<TblFacturaProducto>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public decimal PrecioVenta { get; set; }
        public int ExistenciaActual { get; set; }

        public virtual ICollection<TblFacturaProducto> TblFacturaProductos { get; set; }
    }
}
