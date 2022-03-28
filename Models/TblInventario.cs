using System;
using System.Collections.Generic;

#nullable disable

namespace Prueba.Backend.Models
{
    public partial class TblInventario
    {
        public long Id { get; set; }
        public int IdProducto { get; set; }
        public int CantidadActual { get; set; }
        public string CantidadMinima { get; set; }
    }
}
