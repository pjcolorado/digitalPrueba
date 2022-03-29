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
        public IActionResult Get()
        {
            Respuesta resp = new Respuesta();
            try
            {
                var lista = db.TblProductos.ToList();
                resp.Exito = 1;
                resp.Data = lista;
            }
            catch (Exception ex)
            {
                resp.Mensaje = ex.Message;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet("{id}")]
        public IActionResult GetFactura(long id)
        {
            Respuesta resp = new Respuesta();
            try
            {
                var lista = db.TblProductos.Find(id);
                resp.Exito = 1;
                resp.Data = lista;
            }
            catch (Exception ex)
            {
                resp.Mensaje = ex.Message;
                throw;
            }
            return Ok(resp);
        }

        [HttpPost]
        public IActionResult Post(FacturaDto factura)
        {
            Respuesta resp = new Respuesta();
            try
            {
                TblFactura nuevo = new TblFactura();
                nuevo.IdCliente = factura.IdCliente;
                nuevo.Fecha = System.DateTime.Now;
                nuevo.TotalVenta = factura.TotalVenta;

                db.Add(nuevo);
                db.SaveChanges();

                TblFacturaProducto detalle;
                foreach (var item in factura.detalles)
                {
                    detalle = new TblFacturaProducto();
                    detalle.IdProducto = item.IdProducto;
                    detalle.Cantidad = item.Cantidad;
                    detalle.ValorUnitario = item.ValorUnitario;
                    detalle.ValorTotal = item.ValorTotal;
                    detalle.IdFactura = nuevo.Id;
                    db.Add(detalle);
                }
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                resp.Mensaje = ex.Message;
            }

            return Ok(resp);
        }

    }
}
