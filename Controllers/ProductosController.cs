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
        public IEnumerable<TblProducto> GetProductos()
        {
            return db.TblProductos.ToList();
        }

        [HttpGet("{id}")]
        public TblProducto GetProducto(int id)
        {
            TblProducto retorno = db.TblProductos.Find(id);
            return retorno;
        }
    }
}
