using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba.Backend.Models;

namespace Prueba.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly OFELIAContext db;

        public ClientesController(OFELIAContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Respuesta resp = new Respuesta();
            try
            {
                var lista = db.TblClientes.ToList();
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
                var lista = db.TblClientes.Find(id);
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
        public IActionResult Add(ClienteDto cliente)
        {
            Respuesta resp = new Respuesta();
            try
            {
                db.Add(cliente);
                db.SaveChanges();
                resp.Exito = 1;
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

    /*
    [ApiController]
    [Route("[controller]")]
    public class TblClientesController : Controller
    {
        private readonly OFELIAContext _context;

        public TblClientesController(OFELIAContext context)
        {
            _context = context;
        }

        // GET: TblClientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblClientes.ToListAsync());
        }

        // GET: TblClientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCliente = await _context.TblClientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCliente == null)
            {
                return NotFound();
            }

            return View(tblCliente);
        }

        // GET: TblClientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblClientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoDocumento,NumeroDocumento,Nombre,FechaNacimiento,Direccion,Telefono")] TblCliente tblCliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblCliente);
        }

        // GET: TblClientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCliente = await _context.TblClientes.FindAsync(id);
            if (tblCliente == null)
            {
                return NotFound();
            }
            return View(tblCliente);
        }

        // POST: TblClientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoDocumento,NumeroDocumento,Nombre,FechaNacimiento,Direccion,Telefono")] TblCliente tblCliente)
        {
            if (id != tblCliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblClienteExists(tblCliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblCliente);
        }

        // GET: TblClientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCliente = await _context.TblClientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCliente == null)
            {
                return NotFound();
            }

            return View(tblCliente);
        }

        // POST: TblClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblCliente = await _context.TblClientes.FindAsync(id);
            _context.TblClientes.Remove(tblCliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblClienteExists(int id)
        {
            return _context.TblClientes.Any(e => e.Id == id);
        }
    }
}
*/