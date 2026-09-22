using System;
using System.Collections.Generic;

namespace AcademiaFlowAPI.Models;

public partial class Usuario
{
    public long Id { get; set; }

    public long? IdInstitucion { get; set; }

    public long? IdUnidadAcademica { get; set; }

    public string? TipoDocumento { get; set; }

    public string? NumeroDocumento { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? EmailConfirmado { get; set; }

    public string? Telefono { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? Cargo { get; set; }

    public bool? Activo { get; set; }

    public DateTimeOffset? UltimoAcceso { get; set; }

    public DateTimeOffset? FechaCreacion { get; set; }

    public DateTimeOffset? FechaActualizacion { get; set; }

    public virtual ICollection<Actividad> Actividads { get; set; } = new List<Actividad>();

    public virtual ICollection<ApiLog> ApiLogs { get; set; } = new List<ApiLog>();

    public virtual ICollection<Archivo> Archivos { get; set; } = new List<Archivo>();

    public virtual ICollection<AsignacionTarea> AsignacionTareaAsignadoPorNavigations { get; set; } = new List<AsignacionTarea>();

    public virtual ICollection<AsignacionTarea> AsignacionTareaIdUsuarioNavigations { get; set; } = new List<AsignacionTarea>();

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<Cronograma> Cronogramas { get; set; } = new List<Cronograma>();

    public virtual Institucion? IdInstitucionNavigation { get; set; }

    public virtual UnidadAcademica? IdUnidadAcademicaNavigation { get; set; }

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();

    public virtual ICollection<Proyecto> ProyectoActualizadoPorNavigations { get; set; } = new List<Proyecto>();

    public virtual ICollection<Proyecto> ProyectoCreadoPorNavigations { get; set; } = new List<Proyecto>();

    public virtual ICollection<ProyectoEstadoHistorial> ProyectoEstadoHistorialCambiadoPorNavigations { get; set; } = new List<ProyectoEstadoHistorial>();

    public virtual ICollection<ProyectoEstadoHistorial> ProyectoEstadoHistorialIdResponsableVerificacionNavigations { get; set; } = new List<ProyectoEstadoHistorial>();

    public virtual ICollection<Proyecto> ProyectoIdLiderNavigations { get; set; } = new List<Proyecto>();

    public virtual ICollection<ProyectoIntegrante> ProyectoIntegrantes { get; set; } = new List<ProyectoIntegrante>();

    public virtual ICollection<RegistroAvance> RegistroAvances { get; set; } = new List<RegistroAvance>();

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();

    public virtual ICollection<UsuarioRol> UsuarioRolAsignadoPorNavigations { get; set; } = new List<UsuarioRol>();

    public virtual ICollection<UsuarioRol> UsuarioRolIdUsuarioNavigations { get; set; } = new List<UsuarioRol>();

    public virtual ICollection<Verificacion> Verificacions { get; set; } = new List<Verificacion>();
}
