using Microsoft.AspNetCore.Mvc;
using Proyectofinal.Data;
using Proyectofinal.Models;

namespace Proyectofinal.Controllers
{
    public class HotelController : Controller
    {
        HotelData _data = new HotelData();

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
        public IActionResult Guardar(HOTEL obj)
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

        public IActionResult Editar(string COD_HOTEL)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var obj = _data.Obtener(COD_HOTEL);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Editar(HOTEL obj)
        {
            if (!ModelState.IsValid)
                return View();


            var respuesta = _data.Editar(obj);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }


        public IActionResult Eliminar(string COD_HOTEL)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var obj = _data.Obtener(COD_HOTEL);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Eliminar(HOTEL obj)
        {

            var respuesta = _data.Eliminar(obj.COD_HOTEL);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}
