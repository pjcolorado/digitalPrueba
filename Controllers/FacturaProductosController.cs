using Microsoft.AspNetCore.Mvc;
using Prueba.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacturaProductosController : ControllerBase
    {
        private readonly OFELIAContext db;

        public FacturaProductosController(OFELIAContext context)
        {
            db = context;
        }

        [HttpGet("{idFactura}")]
        public IEnumerable<TblFacturaProducto> Get(long idFactura)
        {
            return db.TblFacturaProductos.Where(x => x.IdFactura == idFactura).ToList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Post(long idFactura, TblFacturaProducto detalles)
        {
            if (db.TblFacturas.Find(idFactura) != null)
            {
                db.Add(detalles);
                db.SaveChanges();
            }

            return "Ok";
        }
    }
}
