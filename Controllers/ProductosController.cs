using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prueba.Backend.Models;

namespace Prueba.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class ProductosController : ControllerBase
    {
        private readonly OFELIAContext db;

        public ProductosController(OFELIAContext context)
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
        public IActionResult Get(int id)
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
    }
}
