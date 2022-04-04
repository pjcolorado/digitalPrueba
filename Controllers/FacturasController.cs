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
                var lista = db.TblFacturas.ToList();
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
                FacturaDto fact = new FacturaDto();
                FacturaProductoDto item = new FacturaProductoDto();
                var obj = db.TblFacturas.Find(id);
                if (obj != null)
                {
                    fact.Id = Convert.ToInt32(obj.Id);
                    fact.IdCliente = obj.IdCliente;
                    fact.TotalVenta = obj.TotalVenta;
                    fact.Fecha = obj.Fecha;

                    foreach (var prod in db.TblFacturaProductos.Where(x => x.IdFactura.Equals(obj.Id)).ToList())
                    {
                        item = new FacturaProductoDto();
                        item.Id = prod.Id;
                        item.IdFactura = prod.IdFactura;
                        item.IdProducto = prod.IdProducto;
                        item.Cantidad = prod.Cantidad;
                        item.ValorUnitario = prod.ValorUnitario;
                        item.ValorTotal = prod.ValorTotal;
                        fact.detalles.Add(item);
                    }
                }

                resp.Exito = 1;
                resp.Data = fact;
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
                if (factura.detalles == null || factura.detalles.Count == 0)
                    throw new Exception("No hay detalles de productos");

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
                resp.Exito = 1;
                resp.Mensaje = string.Format("Se generó la factura {0}", nuevo.Id.ToString());
            }
            catch (Exception ex)
            {
                resp.Mensaje = ex.Message;
            }

            return Ok(resp);
        }

    }
}
