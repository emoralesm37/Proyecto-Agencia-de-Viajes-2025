using Microsoft.AspNetCore.Mvc;
using Proyectofinal.Data;
using Proyectofinal.Models;

namespace Proyectofinal.Controllers
{
    public class VueloController : Controller
    {
        VueloData turistaData = new VueloData();

        public IActionResult Listar()
        {
            //LA VISTA MOSTRARÁ UNA LISTA DE CONTACTOS
            var oLista = turistaData.Listar();

            return View(oLista);
        }

        public IActionResult Guardar()
        {
            //METODO SOLO DEVUELVE LA VISTA
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(Vuelo turista)
        {
            //METODO RECIBE EL OBJETO PARA GUARDARLO EN BD
            if (!ModelState.IsValid)
                return View();


            var respuesta = turistaData.Guardar(turista);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Editar(string Num_vuelo)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var obj = turistaData.Obtener(Num_vuelo);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Editar(Vuelo turista)
        {
            if (!ModelState.IsValid)
                return View();


            var respuesta = turistaData.Editar(turista);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }


        public IActionResult Eliminar(string Num_vuelo)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var obj = turistaData.Obtener(Num_vuelo);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Eliminar(Vuelo turista)
        {

            var respuesta = turistaData.Eliminar(turista.Num_vuelo);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }
    }
}
