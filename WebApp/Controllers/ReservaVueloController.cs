using Microsoft.AspNetCore.Mvc;
using Proyectofinal.Data;
using Proyectofinal.Models;

namespace Proyectofinal.Controllers
{
    public class ReservaVueloController : Controller
    {
        ReservaVueloData _data = new ReservaVueloData();

        public IActionResult Listar()
        {
            //LA VISTA MOSTRARÁ UNA LISTA DE CONTACTOS
            var oLista = _data.Listar();

            return View(oLista);
        }

        public IActionResult Guardar()
        {
            //METODO SOLO DEVUELVE LA VISTA
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(ReservaVuelo obj)
        {
            //METODO RECIBE EL OBJETO PARA GUARDARLO EN BD
            if (!ModelState.IsValid)
                return View();


            var respuesta = _data.Guardar(obj);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Editar(string CodigoReservaVuelo)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var obj = _data.Obtener(CodigoReservaVuelo);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Editar(ReservaVuelo obj)
        {
            if (!ModelState.IsValid)
                return View();


            var respuesta = _data.Editar(obj);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }


        public IActionResult Eliminar(string CodigoReservaVuelo)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var obj = _data.Obtener(CodigoReservaVuelo);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Eliminar(ReservaVuelo obj)
        {

            var respuesta = _data.Eliminar(obj.CodigoReservaVuelo);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}
