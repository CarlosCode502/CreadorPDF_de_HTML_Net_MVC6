//TODOS LOS RECURSOS QUE ESTAMOS UTILIZANDO min 06:00

using DinkToPdf; //Referencia al paquete de pdf
using DinkToPdf.Contracts; //Contratos
using CreadorPDF_de_HTML_Net_MVC6.Extension; //Para poder utilizar la carpeta de extensión

var builder = WebApplication.CreateBuilder(args);

//Utilizando las referencias. min 07:05
//Implementación o acctivación del servicio de esta libreria
var context = new CustomAssemblyLoadContext();
//Úbicar el directorio donde se encuentra el archivo y concatenar con la carpeta que contiene la libreria
context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "LibreriaPDF\\libwkhtmltox.dll"));

// Add services to the container.
builder.Services.AddControllersWithViews();

//Utilizamos todas las librerias en un servicio min 8:30
//IConverter le pertenece a la libreria
//Sincronizarlo con la configuración del pdf en "(new PdfTools)"
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
 
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
