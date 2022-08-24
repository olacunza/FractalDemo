using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FractalDemo.Datos;
using FractalDemo.Models;
using FractalDemo.ViewModel;

namespace FractalDemo.Controllers
{
    public class OrdenesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdenesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ordenes
        public IActionResult Index(string buscarOrden, int pagina = 1)
        {            
            var cantidadRegistrosPorPagina = 5;
                using (_context)
                {
                    var orders = _context.Ordenes.OrderBy(o => o.Orden_Id)
                        .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                        .Take(cantidadRegistrosPorPagina).ToList();
                    var totalDeRegistros = _context.Productos.Count();

                    var modelo = new IndexVM();
                    modelo.Ordenes = orders;
                    modelo.PaginaActual = pagina;
                    modelo.TotalDeRegistros = totalDeRegistros;
                    modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;

                    return View(modelo);
                }
            

        }

        public IActionResult IndexBuscar(string buscarOrden)
        {
            var Ordeness = from or in _context.Ordenes select or;
            if (!String.IsNullOrEmpty(buscarOrden))
            {
                Ordeness = Ordeness.Where(o => o.Cliente.Contains(buscarOrden));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            return View(Ordeness);
        }

        // GET: Ordenes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ordenes == null)
            {
                return NotFound();
            }

            var orden = await _context.Ordenes
                .FirstOrDefaultAsync(m => m.Orden_Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // GET: Ordenes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ordenes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Orden_Id,NumOrden,EstadoPedido,Cliente,Fecha,CantidadProducto,MontoInicial,IGV,MontoFinal")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orden);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orden);
        }

        // GET: Ordenes/Edit/5
        public IActionResult Edit(int id)
        {
            OrdenProductoVM ordenProductos = new OrdenProductoVM
            {
                ListaOrdenProductos = _context.OrdenProducto.Include(p => p.Producto).Include(o => o.Orden)
                .Where(o => o.Orden_Id == id),
                OrdenProducto = new OrdenProducto()
                {
                    Orden_Id = id
                },
                Orden = _context.Ordenes.FirstOrDefault(o => o.Orden_Id == id)
            };

            List<int> listaTemporalProductoOrdenes = ordenProductos.ListaOrdenProductos.Select(p => p.Producto_Id).ToList();
            var listaTemporal = _context.Productos.Where(p => !listaTemporalProductoOrdenes.Contains(p.Producto_Id)).ToList();
            ordenProductos.ListaProductos = listaTemporal.Select(i => new SelectListItem
            {
                Text = i.NombreProducto,
                Value = i.Producto_Id.ToString()
            });

            var mInicial = ordenProductos.Orden.MontoInicial;

            var cantidad = ordenProductos.Orden.CantidadProducto;

            return View(ordenProductos);
        }

        // POST: Ordenes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Orden_Id,NumOrden,EstadoPedido,Cliente,Fecha,CantidadProducto,MontoInicial,IGV,MontoFinal")] Orden orden)
        {
            if (id != orden.Orden_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenExists(orden.Orden_Id))
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
            return View(orden);
        }

        // GET: Ordenes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ordenes == null)
            {
                return NotFound();
            }

            var orden = await _context.Ordenes
                .FirstOrDefaultAsync(m => m.Orden_Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // POST: Ordenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ordenes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Ordenes'  is null.");
            }
            var orden = await _context.Ordenes.FindAsync(id);
            if (orden != null)
            {
                _context.Ordenes.Remove(orden);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult AdministrarProductos(int id)
        {
            OrdenProductoVM ordenProductos = new OrdenProductoVM
            {
                ListaOrdenProductos = _context.OrdenProducto.Include(p => p.Producto).Include(o => o.Orden)
                .Where(o => o.Orden_Id == id),
                OrdenProducto = new OrdenProducto()
                {
                    Orden_Id = id
                },
                Orden = _context.Ordenes.FirstOrDefault(o => o.Orden_Id == id)
            };
            List<int> listaTemporalProductoOrdenes = ordenProductos.ListaOrdenProductos.Select(p => p.Producto_Id).ToList();
            var listaTemporal = _context.Productos.Where(p => !listaTemporalProductoOrdenes.Contains(p.Producto_Id)).ToList();
            ordenProductos.ListaProductos = listaTemporal.Select(i => new SelectListItem
            {
                Text = i.NombreProducto,
                Value = i.Producto_Id.ToString()
            });
            return View(ordenProductos);
        }
        [HttpPost]
        public IActionResult AdministrarProductos(OrdenProductoVM ordenProductos)
        {
                if (ordenProductos.OrdenProducto.Orden_Id != 0 && ordenProductos.OrdenProducto.Producto_Id != 0)
                {
                _context.OrdenProducto.Add(ordenProductos.OrdenProducto);
                //_context.Ordenes.Add(ordenProductos.Orden);
                //_context.Add(ordenProductos.Orden);
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(AdministrarProductos), new { @id = ordenProductos.Orden.Orden_Id });

            
        }

        [HttpPost]
        public IActionResult EliminarProductos(int idProducto, OrdenProductoVM ordenProductos)
        {
            int idOrden = ordenProductos.Orden.Orden_Id;
            OrdenProducto ordenProducto = _context.OrdenProducto.FirstOrDefault(
                    u => u.Producto_Id == idProducto && u.Orden_Id == idOrden
                );
                _context.OrdenProducto.Remove(ordenProducto);
                _context.SaveChanges();
            return RedirectToAction(nameof(Edit), new { @id = idOrden });
        }

        private bool OrdenExists(int id)
        {
          return (_context.Ordenes?.Any(e => e.Orden_Id == id)).GetValueOrDefault();
        }
    }
}
