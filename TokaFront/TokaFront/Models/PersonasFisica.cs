using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokaFront.Models
{
    public class PersonasFisica
    {
        public int IdPersonaFisica { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string RFC { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? UsuarioAgrega { get; set; }
        public bool? Activo { get; set; }
    }

    public class PeronaFisicaValidator: AbstractValidator<PersonasFisica>
    {
        public PeronaFisicaValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(255);
            RuleFor(x => x.ApellidoPaterno).NotEmpty().MaximumLength(255);
            RuleFor(x => x.RFC).NotEmpty().Matches("^([A-Z,Ñ,&]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[A-Z|\\d]{3})$");
        }

    }
}
