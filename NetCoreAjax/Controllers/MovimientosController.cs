using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCoreAjax.Entities;
using NetCoreAjax.Models;

namespace NetCoreAjax.Controllers
{
    public class MovimientosController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MovimientosController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Movimientos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movimientos.ToListAsync());
        }


        // GET: Movimientos/AddOrEdit
        // GET: Movimientos/AddOrEdit/1
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Movimiento());
            }
            else
            {
                var movimiento = await _context.Movimientos.FindAsync(id);
                if (movimiento == null)
                {
                    return NotFound();
                }
                return View(movimiento);
            }            
        }

        // POST: Movimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,NumeroCuenta,Referencia,Cantidad,Date")] Movimiento movimiento)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    _context.Add(movimiento);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _context.Update(movimiento);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MovimientoExists(movimiento.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = Utils.Helper.RenderRazorViewToString(this, "_ListAll", _context.Movimientos.ToList()) });
            }
            return Json(new { isValid = false, html = Utils.Helper.RenderRazorViewToString(this, "AddOrEdit", movimiento) });
        }

        // GET: Movimientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimiento = await _context.Movimientos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimiento == null)
            {
                return NotFound();
            }

            return View(movimiento);
        }

        // POST: Movimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);
            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimientoExists(int id)
        {
            return _context.Movimientos.Any(e => e.Id == id);
        }
    }
}
