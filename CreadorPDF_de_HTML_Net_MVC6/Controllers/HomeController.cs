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

            //min 14:45
            //Se crea un nuevo elemento del tipo
            var configuraciones_pdf = new HtmlToPdfDocument() {
                //Parámetros de configuración global settings
                GlobalSettings = new GlobalSettings()
                {
                    //PaperSize = PaperKind.Legal, //Tam de pagina
                    PaperSize = new PechkinPaperSize("170", "375"), //Tam de papel personalizado Width y height
                    Orientation = Orientation.Portrait //Orientacion de pagina
                },
                //Parámetros de configuración
                Objects = {
                    new ObjectSettings()
                    {
                        Page = url_pagina //Se le asigna la url de la pag que se desea convertir a pdf
                    }
                }
            };

            //min 16:10
            //convertir archivo a pdf
            //Accedemos al convert y le pasamos la configuracion_pdf
            var archivoAPdf = converter.Convert(configuraciones_pdf);

            //return View();
            //Como se va a retornar un archivo min 17:00
            //al final se especifica el tipo del archivo
            return File(archivoAPdf, "application/pdf");
        }

        //min 17:25
        //Va a servir para descargar el pdf(todo casi igual que crearlo)

        public IActionResult DescargarPdf()
        {
            string pagina_actual = HttpContext.Request.Path;
            string url_pagina = HttpContext.Request.GetEncodedUrl();
            url_pagina = url_pagina.Replace(pagina_actual, "");
            url_pagina = $"{url_pagina}/Home/VistaPdf";

            var configuraciones_pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    //PaperSize = PaperKind.A6, //Tam de pagina
                    PaperSize = new PechkinPaperSize("170", "375"), //Tam de papel personalizado Width y height
                    Orientation = Orientation.Portrait
                },

                Objects = {
                    new ObjectSettings()
                    {
                        Page = url_pagina
                    }
                }
            };

            var archivoAPdf = converter.Convert(configuraciones_pdf);
            //nombre que va a tener el pdf que se va a descargar. min 17:45
            string nombrePDF = "Reporte_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";

            //retorna un archivo de tipo pdf con el nombre especifico min 18:30
            return File(archivoAPdf, "application/pdf", nombrePDF);
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