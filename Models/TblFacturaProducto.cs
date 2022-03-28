using System;
using System.Collections.Generic;

#nullable disable

namespace Prueba.Backend.Models
{
    public partial class TblFacturaProducto
    {
        public long Id { get; set; }
        public long IdFactura { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }

        public virtual TblFactura IdFacturaNavigation { get; set; }
        public virtual TblProducto IdProductoNavigation { get; set; }
    }
}
