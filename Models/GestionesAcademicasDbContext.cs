using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFlowAPI.Models;

public partial class GestionesAcademicasDbContext : DbContext
{
    public GestionesAcademicasDbContext()
    {
    }

    public GestionesAcademicasDbContext(DbContextOptions<GestionesAcademicasDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actividad> Actividads { get; set; }

    public virtual DbSet<ApiCliente> ApiClientes { get; set; }

    public virtual DbSet<ApiLog> ApiLogs { get; set; }

    public virtual DbSet<ApiWebhook> ApiWebhooks { get; set; }

    public virtual DbSet<Archivo> Archivos { get; set; }

    public virtual DbSet<AsignacionRecurso> AsignacionRecursos { get; set; }

    public virtual DbSet<AsignacionTarea> AsignacionTareas { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<Cronograma> Cronogramas { get; set; }

    public virtual DbSet<Disciplina> Disciplinas { get; set; }

    public virtual DbSet<EntidadFinanciadora> EntidadFinanciadoras { get; set; }

    public virtual DbSet<Fase> Fases { get; set; }

    public virtual DbSet<Institucion> Institucions { get; set; }

    public virtual DbSet<Notificacion> Notificacions { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<ProgramaAcademico> ProgramaAcademicos { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<ProyectoDisciplina> ProyectoDisciplinas { get; set; }

    public virtual DbSet<ProyectoEstadoHistorial> ProyectoEstadoHistorials { get; set; }

    public virtual DbSet<ProyectoFinanciamiento> ProyectoFinanciamientos { get; set; }

    public virtual DbSet<ProyectoIntegrante> ProyectoIntegrantes { get; set; }

    public virtual DbSet<Recurso> Recursos { get; set; }

    public virtual DbSet<RegistroAvance> RegistroAvances { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    public virtual DbSet<TareaDependencium> TareaDependencia { get; set; }

    public virtual DbSet<UnidadAcademica> UnidadAcademicas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioRol> UsuarioRols { get; set; }

    public virtual DbSet<Verificacion> Verificacions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actividad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activida__3214EC07061EBCD0");

            entity.ToTable("Actividad");

            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .HasDefaultValue("Pendiente");
            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.HorasEstimadas)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(7, 2)");
            entity.Property(e => e.HorasReales)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(7, 2)");
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Orden).HasDefaultValue((short)1);
            entity.Property(e => e.PorcentajeAvance)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(5, 2)");

            entity.HasOne(d => d.IdResponsableNavigation).WithMany(p => p.Actividads)
                .HasForeignKey(d => d.IdResponsable)
                .HasConstraintName("FK__Actividad__IdRes__3D2915A8");

            entity.HasOne(d => d.IdTareaNavigation).WithMany(p => p.Actividads)
                .HasForeignKey(d => d.IdTarea)
                .HasConstraintName("FK__Actividad__IdTar__3E1D39E1");
        });

        modelBuilder.Entity<ApiCliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ApiClien__3214EC0704B3BA03");

            entity.ToTable("ApiCliente");

            entity.HasIndex(e => e.ClientId, "UQ__ApiClien__E67E1A25A5C1BE12").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.ClientId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.RateLimitHora).HasDefaultValue(1000);
            entity.Property(e => e.SecretHash).HasMaxLength(255);
        });

        modelBuilder.Entity<ApiLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ApiLog__3214EC077AD9193B");

            entity.ToTable("ApiLog");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.Ip)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Metodo).HasMaxLength(10);
            entity.Property(e => e.Ruta).HasMaxLength(500);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ApiLogs)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__ApiLog__IdClient__3F115E1A");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.ApiLogs)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__ApiLog__IdUsuari__40058253");
        });

        modelBuilder.Entity<ApiWebhook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ApiWebho__3214EC075BEF10BF");

            entity.ToTable("ApiWebhook");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Evento).HasMaxLength(80);
            entity.Property(e => e.Secreto).HasMaxLength(255);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ApiWebhooks)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__ApiWebhoo__IdCli__40F9A68C");
        });

        modelBuilder.Entity<Archivo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Archivo__3214EC073A18AB63");

            entity.ToTable("Archivo");

            entity.Property(e => e.EsVigente).HasDefaultValue(true);
            entity.Property(e => e.Extension).HasMaxLength(15);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.HashSha256)
                .HasMaxLength(64)
                .IsFixedLength();
            entity.Property(e => e.NombreOriginal).HasMaxLength(255);
            entity.Property(e => e.StorageKey).HasMaxLength(500);
            entity.Property(e => e.Tipo).HasMaxLength(50);
            entity.Property(e => e.Version).HasDefaultValue((short)1);

            entity.HasOne(d => d.IdArchivoPadreNavigation).WithMany(p => p.InverseIdArchivoPadreNavigation)
                .HasForeignKey(d => d.IdArchivoPadre)
                .HasConstraintName("FK__Archivo__IdArchi__41EDCAC5");

            entity.HasOne(d => d.IdFaseNavigation).WithMany(p => p.Archivos)
                .HasForeignKey(d => d.IdFase)
                .HasConstraintName("FK__Archivo__IdFase__42E1EEFE");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Archivos)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__Archivo__IdProye__43D61337");

            entity.HasOne(d => d.IdTareaNavigation).WithMany(p => p.Archivos)
                .HasForeignKey(d => d.IdTarea)
                .HasConstraintName("FK__Archivo__IdTarea__44CA3770");

            entity.HasOne(d => d.SubidoPorNavigation).WithMany(p => p.Archivos)
                .HasForeignKey(d => d.SubidoPor)
                .HasConstraintName("FK__Archivo__SubidoP__45BE5BA9");
        });

        modelBuilder.Entity<AsignacionRecurso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Asignaci__3214EC072B14E68A");

            entity.ToTable("AsignacionRecurso");

            entity.Property(e => e.Cantidad).HasColumnType("numeric(12, 2)");

            entity.HasOne(d => d.IdFaseNavigation).WithMany(p => p.AsignacionRecursos)
                .HasForeignKey(d => d.IdFase)
                .HasConstraintName("FK__Asignacio__IdFas__46B27FE2");

            entity.HasOne(d => d.IdRecursoNavigation).WithMany(p => p.AsignacionRecursos)
                .HasForeignKey(d => d.IdRecurso)
                .HasConstraintName("FK__Asignacio__IdRec__47A6A41B");

            entity.HasOne(d => d.IdTareaNavigation).WithMany(p => p.AsignacionRecursos)
                .HasForeignKey(d => d.IdTarea)
                .HasConstraintName("FK__Asignacio__IdTar__489AC854");
        });

        modelBuilder.Entity<AsignacionTarea>(entity =>
        {
            entity.HasKey(e => new { e.IdTarea, e.IdUsuario }).HasName("PK__Asignaci__8F68CB6148BF58D2");

            entity.ToTable("AsignacionTarea");

            entity.Property(e => e.AsignadoEn).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.HorasAsignadas)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(7, 2)");

            entity.HasOne(d => d.AsignadoPorNavigation).WithMany(p => p.AsignacionTareaAsignadoPorNavigations)
                .HasForeignKey(d => d.AsignadoPor)
                .HasConstraintName("FK__Asignacio__Asign__498EEC8D");

            entity.HasOne(d => d.IdTareaNavigation).WithMany(p => p.AsignacionTareas)
                .HasForeignKey(d => d.IdTarea)
                .HasConstraintName("FK__Asignacio__IdTar__4A8310C6");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.AsignacionTareaIdUsuarioNavigations)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Asignacio__IdUsu__4B7734FF");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comentar__3214EC0731CDB53D");

            entity.ToTable("Comentario");

            entity.Property(e => e.Eliminado).HasDefaultValue(false);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetimeoffset())");

            entity.HasOne(d => d.IdActividadNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdActividad)
                .HasConstraintName("FK__Comentari__IdAct__4C6B5938");

            entity.HasOne(d => d.IdComentarioPadreNavigation).WithMany(p => p.InverseIdComentarioPadreNavigation)
                .HasForeignKey(d => d.IdComentarioPadre)
                .HasConstraintName("FK__Comentari__IdCom__4D5F7D71");

            entity.HasOne(d => d.IdFaseNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdFase)
                .HasConstraintName("FK__Comentari__IdFas__4E53A1AA");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__Comentari__IdPro__4F47C5E3");

            entity.HasOne(d => d.IdTareaNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdTarea)
                .HasConstraintName("FK__Comentari__IdTar__503BEA1C");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comentari__IdUsu__51300E55");
        });

        modelBuilder.Entity<Cronograma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cronogra__3214EC07C8E8C075");

            entity.ToTable("Cronograma");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.Formato).HasMaxLength(20);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Version).HasDefaultValue((short)1);

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Cronogramas)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__Cronogram__IdPro__5224328E");

            entity.HasOne(d => d.SubidoPorNavigation).WithMany(p => p.Cronogramas)
                .HasForeignKey(d => d.SubidoPor)
                .HasConstraintName("FK__Cronogram__Subid__531856C7");
        });

        modelBuilder.Entity<Disciplina>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Discipli__3214EC070CA4CE41");

            entity.ToTable("Disciplina");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Codigo).HasMaxLength(20);
            entity.Property(e => e.Nombre).HasMaxLength(160);

            entity.HasOne(d => d.IdPadreNavigation).WithMany(p => p.InverseIdPadreNavigation)
                .HasForeignKey(d => d.IdPadre)
                .HasConstraintName("FK__Disciplin__IdPad__540C7B00");
        });

        modelBuilder.Entity<EntidadFinanciadora>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EntidadF__3214EC073089AF3F");

            entity.ToTable("EntidadFinanciadora");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Contacto).HasMaxLength(200);
            entity.Property(e => e.Nit).HasMaxLength(30);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Pais).HasMaxLength(80);
            entity.Property(e => e.Tipo).HasMaxLength(30);
        });

        modelBuilder.Entity<Fase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Fase__3214EC07C2674D05");

            entity.ToTable("Fase");

            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Orden).HasDefaultValue((short)1);
            entity.Property(e => e.Peso)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(5, 2)");
            entity.Property(e => e.PorcentajeAvance)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(5, 2)");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Fases)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__Fase__IdProyecto__55009F39");
        });

        modelBuilder.Entity<Institucion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Instituc__3214EC0794D34A16");

            entity.ToTable("Institucion");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Ciudad).HasMaxLength(120);
            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.Naturaleza).HasMaxLength(30);
            entity.Property(e => e.Nit).HasMaxLength(30);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Pais)
                .HasMaxLength(80)
                .HasDefaultValue("Colombia");
            entity.Property(e => e.Siglas).HasMaxLength(20);
            entity.Property(e => e.SitioWeb).HasMaxLength(250);
        });

        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC07E5E0F86F");

            entity.ToTable("Notificacion");

            entity.Property(e => e.Enlace).HasMaxLength(500);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.Leida).HasDefaultValue(false);
            entity.Property(e => e.Tipo).HasMaxLength(60);
            entity.Property(e => e.Titulo).HasMaxLength(200);

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Notificacions)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__Notificac__IdPro__55F4C372");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Notificacions)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Notificac__IdUsu__56E8E7AB");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permiso__3214EC077439FDFB");

            entity.ToTable("Permiso");

            entity.HasIndex(e => e.Clave, "UQ__Permiso__E8181E11FE2D55B5").IsUnique();

            entity.Property(e => e.Clave).HasMaxLength(80);
        });

        modelBuilder.Entity<ProgramaAcademico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Programa__3214EC07A8A4E11C");

            entity.ToTable("ProgramaAcademico");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.CodigoSnies).HasMaxLength(30);
            entity.Property(e => e.Nivel).HasMaxLength(30);
            entity.Property(e => e.Nombre).HasMaxLength(200);

            entity.HasOne(d => d.IdDisciplinaNavigation).WithMany(p => p.ProgramaAcademicos)
                .HasForeignKey(d => d.IdDisciplina)
                .HasConstraintName("FK__ProgramaA__IdDis__57DD0BE4");

            entity.HasOne(d => d.IdUnidadAcademicaNavigation).WithMany(p => p.ProgramaAcademicos)
                .HasForeignKey(d => d.IdUnidadAcademica)
                .HasConstraintName("FK__ProgramaA__IdUni__58D1301D");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proyecto__3214EC07744C6E8C");

            entity.ToTable("Proyecto");

            entity.HasIndex(e => e.Codigo, "UQ__Proyecto__06370DAC1631D42D").IsUnique();

            entity.Property(e => e.Codigo).HasMaxLength(40);
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .HasDefaultValue("Iniciado");
            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.Moneda)
                .HasMaxLength(3)
                .HasDefaultValue("COP")
                .IsFixedLength();
            entity.Property(e => e.Nombre).HasMaxLength(250);
            entity.Property(e => e.OrigenFinanciamiento)
                .HasMaxLength(30)
                .HasDefaultValue("Propio");
            entity.Property(e => e.PorcentajeAvance)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(5, 2)");
            entity.Property(e => e.PresupuestoTotal)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(16, 2)");

            entity.HasOne(d => d.ActualizadoPorNavigation).WithMany(p => p.ProyectoActualizadoPorNavigations)
                .HasForeignKey(d => d.ActualizadoPor)
                .HasConstraintName("FK__Proyecto__Actual__59C55456");

            entity.HasOne(d => d.CreadoPorNavigation).WithMany(p => p.ProyectoCreadoPorNavigations)
                .HasForeignKey(d => d.CreadoPor)
                .HasConstraintName("FK__Proyecto__Creado__5AB9788F");

            entity.HasOne(d => d.IdInstitucionNavigation).WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.IdInstitucion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Proyecto__IdInst__5BAD9CC8");

            entity.HasOne(d => d.IdLiderNavigation).WithMany(p => p.ProyectoIdLiderNavigations)
                .HasForeignKey(d => d.IdLider)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Proyecto__IdLide__5CA1C101");

            entity.HasOne(d => d.IdUnidadAcademicaNavigation).WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.IdUnidadAcademica)
                .HasConstraintName("FK__Proyecto__IdUnid__5D95E53A");

            entity.HasMany(d => d.IdProgramas).WithMany(p => p.IdProyectos)
                .UsingEntity<Dictionary<string, object>>(
                    "ProyectoPrograma",
                    r => r.HasOne<ProgramaAcademico>().WithMany()
                        .HasForeignKey("IdPrograma")
                        .HasConstraintName("FK__ProyectoP__IdPro__690797E6"),
                    l => l.HasOne<Proyecto>().WithMany()
                        .HasForeignKey("IdProyecto")
                        .HasConstraintName("FK__ProyectoP__IdPro__681373AD"),
                    j =>
                    {
                        j.HasKey("IdProyecto", "IdPrograma").HasName("PK__Proyecto__BE71C8B9A5BC7469");
                        j.ToTable("ProyectoPrograma");
                    });
        });

        modelBuilder.Entity<ProyectoDisciplina>(entity =>
        {
            entity.HasKey(e => new { e.IdProyecto, e.IdDisciplina }).HasName("PK__Proyecto__9DF50C3563CC5D60");

            entity.ToTable("ProyectoDisciplina");

            entity.Property(e => e.Principal).HasDefaultValue(false);

            entity.HasOne(d => d.IdDisciplinaNavigation).WithMany(p => p.ProyectoDisciplinas)
                .HasForeignKey(d => d.IdDisciplina)
                .HasConstraintName("FK__ProyectoD__IdDis__5E8A0973");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.ProyectoDisciplinas)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__ProyectoD__IdPro__5F7E2DAC");
        });

        modelBuilder.Entity<ProyectoEstadoHistorial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proyecto__3214EC07299E3FC8");

            entity.ToTable("ProyectoEstadoHistorial");

            entity.Property(e => e.EstadoAnterior).HasMaxLength(30);
            entity.Property(e => e.EstadoNuevo).HasMaxLength(30);
            entity.Property(e => e.FechaCambio).HasDefaultValueSql("(sysdatetimeoffset())");

            entity.HasOne(d => d.CambiadoPorNavigation).WithMany(p => p.ProyectoEstadoHistorialCambiadoPorNavigations)
                .HasForeignKey(d => d.CambiadoPor)
                .HasConstraintName("FK__ProyectoE__Cambi__607251E5");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.ProyectoEstadoHistorials)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__ProyectoE__IdPro__6166761E");

            entity.HasOne(d => d.IdResponsableVerificacionNavigation).WithMany(p => p.ProyectoEstadoHistorialIdResponsableVerificacionNavigations)
                .HasForeignKey(d => d.IdResponsableVerificacion)
                .HasConstraintName("FK__ProyectoE__IdRes__625A9A57");
        });

        modelBuilder.Entity<ProyectoFinanciamiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proyecto__3214EC07F2AC4494");

            entity.ToTable("ProyectoFinanciamiento");

            entity.Property(e => e.Moneda)
                .HasMaxLength(3)
                .HasDefaultValue("COP")
                .IsFixedLength();
            entity.Property(e => e.Monto).HasColumnType("numeric(16, 2)");
            entity.Property(e => e.NumeroConvenio).HasMaxLength(60);

            entity.HasOne(d => d.IdEntidadNavigation).WithMany(p => p.ProyectoFinanciamientos)
                .HasForeignKey(d => d.IdEntidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProyectoF__IdEnt__634EBE90");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.ProyectoFinanciamientos)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__ProyectoF__IdPro__6442E2C9");
        });

        modelBuilder.Entity<ProyectoIntegrante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proyecto__3214EC07727866F3");

            entity.ToTable("ProyectoIntegrante");

            entity.HasIndex(e => new { e.IdProyecto, e.IdUsuario }, "UQ_Proyecto_Usuario").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaIngreso).HasDefaultValueSql("(CONVERT([date],getdate()))");
            entity.Property(e => e.Rol).HasMaxLength(50);

            entity.HasOne(d => d.IdDisciplinaNavigation).WithMany(p => p.ProyectoIntegrantes)
                .HasForeignKey(d => d.IdDisciplina)
                .HasConstraintName("FK__ProyectoI__IdDis__65370702");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.ProyectoIntegrantes)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__ProyectoI__IdPro__662B2B3B");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.ProyectoIntegrantes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__ProyectoI__IdUsu__671F4F74");
        });

        modelBuilder.Entity<Recurso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recurso__3214EC07A4C38F37");

            entity.ToTable("Recurso");

            entity.Property(e => e.Cantidad)
                .HasDefaultValue(100m)
                .HasColumnType("numeric(12, 2)");
            entity.Property(e => e.CostoTotal)
                .HasComputedColumnSql("([Cantidad]*[CostoUnitario])", true)
                .HasColumnType("numeric(29, 4)");
            entity.Property(e => e.CostoUnitario)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(16, 2)");
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Tipo).HasMaxLength(30);
            entity.Property(e => e.UnidadMedida).HasMaxLength(40);

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Recursos)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__Recurso__IdProye__69FBBC1F");
        });

        modelBuilder.Entity<RegistroAvance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Registro__3214EC078E8079B0");

            entity.ToTable("RegistroAvance");

            entity.Property(e => e.FechaReporte).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.HorasDedicadas).HasColumnType("numeric(7, 2)");
            entity.Property(e => e.Porcentaje).HasColumnType("numeric(5, 2)");

            entity.HasOne(d => d.IdActividadNavigation).WithMany(p => p.RegistroAvances)
                .HasForeignKey(d => d.IdActividad)
                .HasConstraintName("FK__RegistroA__IdAct__6AEFE058");

            entity.HasOne(d => d.IdTareaNavigation).WithMany(p => p.RegistroAvances)
                .HasForeignKey(d => d.IdTarea)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__RegistroA__IdTar__6BE40491");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.RegistroAvances)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RegistroA__IdUsu__6CD828CA");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rol__3214EC074C839FE5");

            entity.ToTable("Rol");

            entity.HasIndex(e => e.Nombre, "UQ__Rol__75E3EFCF9F264104").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(60);
            entity.Property(e => e.Sistema).HasDefaultValue(false);

            entity.HasMany(d => d.IdPermisos).WithMany(p => p.IdRols)
                .UsingEntity<Dictionary<string, object>>(
                    "RolPermiso",
                    r => r.HasOne<Permiso>().WithMany()
                        .HasForeignKey("IdPermiso")
                        .HasConstraintName("FK__RolPermis__IdPer__6DCC4D03"),
                    l => l.HasOne<Rol>().WithMany()
                        .HasForeignKey("IdRol")
                        .HasConstraintName("FK__RolPermis__IdRol__6EC0713C"),
                    j =>
                    {
                        j.HasKey("IdRol", "IdPermiso").HasName("PK__RolPermi__BA9F7EA04176CE50");
                        j.ToTable("RolPermiso");
                    });
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tarea__3214EC07D10898D8");

            entity.ToTable("Tarea");

            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .HasDefaultValue("Pendiente");
            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.HorasEstimadas)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(7, 2)");
            entity.Property(e => e.Orden).HasDefaultValue((short)1);
            entity.Property(e => e.PorcentajeAvance)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(5, 2)");
            entity.Property(e => e.Prioridad)
                .HasMaxLength(30)
                .HasDefaultValue("Medio");

            entity.HasOne(d => d.IdFaseNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdFase)
                .HasConstraintName("FK__Tarea__IdFase__6FB49575");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tarea__IdProyect__70A8B9AE");

            entity.HasOne(d => d.IdResponsableNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdResponsable)
                .HasConstraintName("FK__Tarea__IdRespons__719CDDE7");
        });

        modelBuilder.Entity<TareaDependencium>(entity =>
        {
            entity.HasKey(e => new { e.IdTarea, e.IdDependeDe }).HasName("PK__TareaDep__2E80B770C953C0D4");

            entity.Property(e => e.DesfaseDias).HasDefaultValue((short)0);
            entity.Property(e => e.Tipo)
                .HasMaxLength(30)
                .HasDefaultValue("Fin a Inicio");

            entity.HasOne(d => d.IdDependeDeNavigation).WithMany(p => p.TareaDependenciumIdDependeDeNavigations)
                .HasForeignKey(d => d.IdDependeDe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TareaDepe__IdDep__72910220");

            entity.HasOne(d => d.IdTareaNavigation).WithMany(p => p.TareaDependenciumIdTareaNavigations)
                .HasForeignKey(d => d.IdTarea)
                .HasConstraintName("FK__TareaDepe__IdTar__73852659");
        });

        modelBuilder.Entity<UnidadAcademica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UnidadAc__3214EC07F9A93B0D");

            entity.ToTable("UnidadAcademica");

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Tipo).HasMaxLength(60);

            entity.HasOne(d => d.IdInstitucionNavigation).WithMany(p => p.UnidadAcademicas)
                .HasForeignKey(d => d.IdInstitucion)
                .HasConstraintName("FK__UnidadAca__IdIns__74794A92");

            entity.HasOne(d => d.IdPadreNavigation).WithMany(p => p.InverseIdPadreNavigation)
                .HasForeignKey(d => d.IdPadre)
                .HasConstraintName("FK__UnidadAca__IdPad__756D6ECB");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC07DA999ADF");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.NumeroDocumento, "UQ__Usuario__A4202588432A9B0B").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534B3A0B2E8").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Apellidos).HasMaxLength(120);
            entity.Property(e => e.Cargo).HasMaxLength(120);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.EmailConfirmado).HasDefaultValue(false);
            entity.Property(e => e.FechaActualizacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(241)
                .HasComputedColumnSql("(([Nombres]+' ')+[Apellidos])", true);
            entity.Property(e => e.Nombres).HasMaxLength(120);
            entity.Property(e => e.NumeroDocumento).HasMaxLength(30);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Telefono).HasMaxLength(30);
            entity.Property(e => e.TipoDocumento).HasMaxLength(10);

            entity.HasOne(d => d.IdInstitucionNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdInstitucion)
                .HasConstraintName("FK__Usuario__IdInsti__76619304");

            entity.HasOne(d => d.IdUnidadAcademicaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdUnidadAcademica)
                .HasConstraintName("FK__Usuario__IdUnida__7755B73D");
        });

        modelBuilder.Entity<UsuarioRol>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdRol }).HasName("PK__UsuarioR__89C12A13A903E39D");

            entity.ToTable("UsuarioRol");

            entity.Property(e => e.AsignadoEn).HasDefaultValueSql("(sysdatetimeoffset())");

            entity.HasOne(d => d.AsignadoPorNavigation).WithMany(p => p.UsuarioRolAsignadoPorNavigations)
                .HasForeignKey(d => d.AsignadoPor)
                .HasConstraintName("FK__UsuarioRo__Asign__7849DB76");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__UsuarioRo__IdRol__793DFFAF");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioRolIdUsuarioNavigations)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__UsuarioRo__IdUsu__7A3223E8");
        });

        modelBuilder.Entity<Verificacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Verifica__3214EC0769303CFC");

            entity.ToTable("Verificacion");

            entity.Property(e => e.FechaAsignacion).HasDefaultValueSql("(sysdatetimeoffset())");
            entity.Property(e => e.Resultado)
                .HasMaxLength(30)
                .HasDefaultValue("Pendiente");
            entity.Property(e => e.Tipo).HasMaxLength(80);

            entity.HasOne(d => d.AsignadoPorNavigation).WithMany(p => p.Verificacions)
                .HasForeignKey(d => d.AsignadoPor)
                .HasConstraintName("FK__Verificac__Asign__7B264821");

            entity.HasOne(d => d.IdFaseNavigation).WithMany(p => p.Verificacions)
                .HasForeignKey(d => d.IdFase)
                .HasConstraintName("FK__Verificac__IdFas__7C1A6C5A");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Verificacions)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__Verificac__IdPro__7D0E9093");

            entity.HasOne(d => d.IdTareaNavigation).WithMany(p => p.Verificacions)
                .HasForeignKey(d => d.IdTarea)
                .HasConstraintName("FK__Verificac__IdTar__7E02B4CC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
