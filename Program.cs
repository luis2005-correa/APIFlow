using AcademiaFlowAPI.Data;
using AcademiaFlowAPI.Interfaces;
using AcademiaFlowAPI.Services;
using AcademiaFlowAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IActividadService, ActividadService>();
builder.Services.AddScoped<IApiClienteservice, ApiClienteservice>();
builder.Services.AddScoped<IApiLogService, ApiLogService>();
builder.Services.AddScoped<IApiWebhookService, ApiWebhookService>();
builder.Services.AddScoped<IArchivoService, ArchivoService>();
builder.Services.AddScoped<IAsignacionRecursoService, AsignacionRecursoService>();
builder.Services.AddScoped<IAsignacionTareaService, AsignacionTareaService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();
builder.Services.AddScoped<ICronogramaService, CronogramaService>();
builder.Services.AddScoped<IDisciplinaService, DisciplinaService>();
builder.Services.AddScoped<IEntidadFinanciadoraService, EntidadFinanciadoraService>();
builder.Services.AddScoped<IFaseService, FaseService>();
builder.Services.AddScoped<IInstitucionService, InstitucionService>();
builder.Services.AddScoped<INotificacionService, NotificacionService>();
builder.Services.AddScoped<IPermisoService, PermisoService>();
builder.Services.AddScoped<IProgramaAcademicoService, ProgramaAcademicoService>();
builder.Services.AddScoped<IProyectoDisciplinaService, ProyectoDisciplinaService>();
builder.Services.AddScoped<IProyectoEstadoHistorialService, ProyectoEstadoHistorialService>();
builder.Services.AddScoped<IProyectoFinanciamientoService, ProyectoFinanciamientoService>();
builder.Services.AddScoped<IProyectoIntegranteService, ProyectoIntegranteService>();
builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<IRecursoService, RecursoService>();
builder.Services.AddScoped<IRegistroAvanceService, RegistroAvanceService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<ITareaDependenciumService, TareaDependenciumService>();
builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<IUnidadAcademicaService, UnidadAcademicaService>();
builder.Services.AddScoped<IUsuarioRolService, UsuarioRolService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IVerificacionService, VerificacionService>();

builder.Services.AddDbContext<GestionesAcademicasDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("con")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Permisos", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("Permisos");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
