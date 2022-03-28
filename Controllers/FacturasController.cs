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
    public class FacturasController : ControllerBase
    {
        private readonly OFELIAContext db;

        public FacturasController(OFELIAContext context)
        {
            db = context;
        }

        [HttpGet]
        public IEnumerable<TblFactura> Get()
        {
            return db.TblFacturas.ToList();
        }

        [HttpGet("{id}")]
        public TblFactura GetFactura(long id)
        {
            TblFactura retorno = db.TblFacturas.Find(id);
            return retorno;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Post(TblFactura factura)
        {
            db.Add(factura);
            db.SaveChanges();

            db.Add(factura.TblFacturaProductos);
            db.SaveChanges();

            return factura.Id.ToString();
        }
    }
}
