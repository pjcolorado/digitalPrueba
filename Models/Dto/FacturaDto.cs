using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Backend.Models
{
    public class FacturaDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public decimal TotalVenta { get; set; }
        public List<FacturaProductoDto> detalles { get; set; }

        public FacturaDto()
        {
            this.detalles = new List<FacturaProductoDto>();
        }
    }
}
