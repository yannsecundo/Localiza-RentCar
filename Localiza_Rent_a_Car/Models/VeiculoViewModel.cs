using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Localiza_Rent_a_Car.Models
{
    Route["api/v1/ViewMode"]
    public class VeiculoViewModel
    {

        [Required(ErrorMessage = "{0} é requerido")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "{0} é requerido")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "{0} é requerido")]
        public int  Quilometragem { get; set; }

        [Required(ErrorMessage = "{0} é requerido")]
        public DateTime AnoDoVeiculo { get; set; }
    }
}
