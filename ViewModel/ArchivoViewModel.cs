using System;
using AcademiaFlowAPI.Models;

namespace AcademiaFlowAPI.ViewModel
{
    public class ArchivoViewModel
    {
        public long Id { get; set; }

        public long IdProyecto { get; set; }

        public string NombreProyecto { get; set; } = "";

        public long? IdFase { get; set; }

        public string? NombreFase { get; set; }

        public long? IdTarea { get; set; }

        public string? NombreTarea { get; set; }

        public string? Tipo { get; set; }

        public string NombreOriginal { get; set; } = null!;

        public string StorageKey { get; set; } = null!;

        public string? Extension { get; set; }

        public long? TamanoBytes { get; set; }

        public string? HashSha256 { get; set; }

        public short? Version { get; set; }

        public long? IdArchivoPadre { get; set; }

        public bool? EsVigente { get; set; }

        public string? Descripcion { get; set; }

        public long? SubidoPor { get; set; }

        public string? NombreUsuarioSubio { get; set; }

        public DateTimeOffset? FechaCreacion { get; set; }

        public static ArchivoViewModel ToViewModel(
            Archivo ob,
            string nombreProyecto = "",
            string? nombreFase = null,
            string? nombreTarea = null,
            string? nombreUsuarioSubio = null)
        {
            return new ArchivoViewModel()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                NombreProyecto = nombreProyecto,
                IdFase = ob.IdFase,
                NombreFase = nombreFase,
                IdTarea = ob.IdTarea,
                NombreTarea = nombreTarea,
                Tipo = ob.Tipo,
                NombreOriginal = ob.NombreOriginal,
                StorageKey = ob.StorageKey,
                Extension = ob.Extension,
                TamanoBytes = ob.TamanoBytes,
                HashSha256 = ob.HashSha256,
                Version = ob.Version,
                IdArchivoPadre = ob.IdArchivoPadre,
                EsVigente = ob.EsVigente,
                Descripcion = ob.Descripcion,
                SubidoPor = ob.SubidoPor,
                NombreUsuarioSubio = nombreUsuarioSubio,
                FechaCreacion = ob.FechaCreacion
            };
        }

        public static Archivo ToArchivo(ArchivoViewModel ob)
        {
            return new Archivo()
            {
                Id = ob.Id,
                IdProyecto = ob.IdProyecto,
                IdFase = ob.IdFase,
                IdTarea = ob.IdTarea,
                Tipo = ob.Tipo,
                NombreOriginal = ob.NombreOriginal,
                StorageKey = ob.StorageKey,
                Extension = ob.Extension,
                TamanoBytes = ob.TamanoBytes,
                HashSha256 = ob.HashSha256,
                Version = ob.Version,
                IdArchivoPadre = ob.IdArchivoPadre,
                EsVigente = ob.EsVigente,
                Descripcion = ob.Descripcion,
                SubidoPor = ob.SubidoPor,
                FechaCreacion = ob.FechaCreacion
            };
        }
    }
}