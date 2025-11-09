using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyectofinal.Data;
using Proyectofinal.Models;

namespace Proyectofinal.Controllers
{
    public class HospedajeController : Controller
    {
        HospedajeData _data = new HospedajeData();
        TuristaData turistaData = new TuristaData();
        HotelData _dataHotel = new HotelData();

        public IActionResult Listar()
        {
            //LA VISTA MOSTRARÁ UNA LISTA DE CONTACTOS
            var oLista = _data.Listar();

            return View(oLista);
        }

        public IActionResult Guardar()
        {
            //METODO SOLO DEVUELVE LA VISTA
            CargarDropdowns();
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(Hospedaje obj)
        {
            //METODO RECIBE EL OBJETO PARA GUARDARLO EN BD
            if (!ModelState.IsValid)
            {
                CargarDropdowns();
                return View();
            }
                


            var respuesta = _data.Guardar(obj);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                CargarDropdowns();
                return View();
            }
        }

        public IActionResult Editar(string CodigoHospedaje)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var obj = _data.Obtener(CodigoHospedaje);
            CargarDropdowns();
            return View(obj);
        }

        [HttpPost]
        public IActionResult Editar(Hospedaje obj)
        {
            if (!ModelState.IsValid)
            {
                CargarDropdowns();
                return View();
            }



            var respuesta = _data.Editar(obj);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
                
            else
            {
                CargarDropdowns();
                return View();
            }

        }


        public IActionResult Eliminar(string CodigoHospedaje)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var obj = _data.Obtener(CodigoHospedaje);
            return View(obj);
        }

        [HttpPost]
        public IActionResult Eliminar(Hospedaje obj)
        {

            var respuesta = _data.Eliminar(obj.CodigoHospedaje);

            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        // Método privado para cargar los dropdowns
        private void CargarDropdowns()
        {
            // Obtener listas de países y sucursales
            var hotel = _dataHotel.Listar();
            var turist = turistaData.Listar();

            // Convertir a SelectList y asignar a ViewBag
            ViewBag.Hotel = new SelectList(hotel, "COD_HOTEL", "NOMBRE");
            ViewBag.Turista = new SelectList(turist, "COD_TURISTA", "NombreCompleto");
        }
    }
}
