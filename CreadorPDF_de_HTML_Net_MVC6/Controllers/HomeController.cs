using CreadorPDF_de_HTML_Net_MVC6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
//min 10:10
using DinkToPdf; //Referencia al paquete de pdf
using DinkToPdf.Contracts; //Contratos
using Microsoft.AspNetCore.Http.Extensions;//Acceder a las extensiones 

namespace CreadorPDF_de_HTML_Net_MVC6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConverter converter;

        //Inyectamos el servicio ded DinkToPdf min 10:20
        public HomeController(ILogger<HomeController> logger, IConverter converter)
        {
            _logger = logger;
            this.converter = converter;
        }

        public IActionResult Index()
        {
            return View();
        }

        //min 10:45
        public IActionResult VistaPdf()
        {
            return View();
        }

        //min 12:10
        //Va a servir para mostrar la vista
        //UNICAMENTE OBTENEMOS LA VISTA DE LA URL PARA CONVERTIRLO EN PDF
        public IActionResult MostrarPdfenPagina()
        {
            //almacena la url de la página actual min 13:15
            string pagina_actual = HttpContext.Request.Path;
            //Obtene la ruta especifica de nuestro proyecto
            string url_pagina = HttpContext.Request.GetEncodedUrl();
            //reemplasamos dentro la url de la pag min 13:55
            //se va a reemplazar por la pagina actual y se va a borrar
            url_pagina = url_pagina.Replace(pagina_actual, "");
            url_pagina = $"{url_pagina}/Home/VistaPdf";
            


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}