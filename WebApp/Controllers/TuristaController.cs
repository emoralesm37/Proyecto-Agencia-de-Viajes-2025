using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyectofinal.Data;
using Proyectofinal.Models;

namespace Proyectofinal.Controllers
{
    public class TuristaController : Controller
    {
        TuristaData turistaData = new TuristaData();
        PaisData paisData = new PaisData();
        SurcursalData sucursalData = new SurcursalData();

        public IActionResult Listar()
        {
            //LA VISTA MOSTRARÁ UNA LISTA DE CONTACTOS
            var oLista = turistaData.Listar();
            return View(oLista);
        }

        public IActionResult Guardar()
        {
            // Cargar los dropdowns en ViewBag
            CargarDropdowns();
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(TURISTA turista)
        {
            //METODO RECIBE EL OBJETO PARA GUARDARLO EN BD
            if (!ModelState.IsValid)
            {
                CargarDropdowns(); // Recargar dropdowns si hay error de validación
                return View();
            }

            var respuesta = turistaData.Guardar(turista);

            if (respuesta)
                return RedirectToAction("Listar");
            else
            {
                CargarDropdowns(); // Recargar dropdowns si hay error al guardar
                return View();
            }
        }

        public IActionResult Editar(string COD_TURISTA)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var obj = turistaData.Obtener(COD_TURISTA);
            CargarDropdowns(); // Cargar dropdowns para edición
            return View(obj);
        }

        [HttpPost]
        public IActionResult Editar(TURISTA turista)
        {
            if (!ModelState.IsValid)
            {
                CargarDropdowns(); // Recargar dropdowns si hay error de validación
                return View();
            }

            var respuesta = turistaData.Editar(turista);

            if (respuesta)
                return RedirectToAction("Listar");
            else
            {
                CargarDropdowns(); // Recargar dropdowns si hay error al editar
                return View();
            }
        }

        public IActionResult Eliminar(string COD_TURISTA)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var obj = turistaData.Obtener(COD_TURISTA);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Eliminar(TURISTA turista)
        {
            var respuesta = turistaData.Eliminar(turista.COD_TURISTA);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        // Método privado para cargar los dropdowns
        private void CargarDropdowns()
        {
            // Obtener listas de países y sucursales
            var paises = paisData.Listar();
            var sucursales = sucursalData.Listar();

            // Convertir a SelectList y asignar a ViewBag
            ViewBag.Paises = new SelectList(paises, "CodigoPais", "Nombre");
            ViewBag.Sucursales = new SelectList(sucursales, "CodigoSucursal", "Nombre");
        }
    }
}
