using WebApiColegios;

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Inicializa una nueva instancia de la clase <see cref="Startup"/> con la configuración del constructor.
/// </summary>
var starTup = new Startup(builder.Configuration);

/// <summary>
/// Configura los servicios de la aplicación.
/// </summary>
starTup.ConfigureServices(builder.Services);

var app = builder.Build();

/// <summary>
/// Obtiene el servicio de logging para la clase <see cref="Startup"/>.
/// </summary>
var servicesLogger = (ILogger<Startup>)app.Services.GetService(typeof(ILogger<Startup>));

/// <summary>
/// Configura el pipeline de solicitudes HTTP para la aplicación.
/// </summary>
starTup.Configure(app, app.Environment, servicesLogger);

/// <summary>
/// Ejecuta la aplicación.
/// </summary>
app.Run();
