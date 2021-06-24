using Dominio.Entidades;
using Localiza_Rent_a_Car.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Localiza_Rent_a_Car.Factories
{
    
    Route["api/v1/Factory"]
    public class VeiculoFactory
    {
        [HttpPost]
        public static Veiculo MapearVeiculo(VeiculoViewModel veiculoViewModel)
        {
            return new Veiculo(veiculoViewModel.Marca, veiculoViewModel.Modelo, veiculoViewModel.Quilometragem, veiculoViewModel.AnoDoVeiculo);
        }
        [HttpPost]
        public static VeiculoViewModel MapearVeiculoViewModel(Veiculo veiculo)
        {
            return new VeiculoViewModel() { Marca = veiculo.Marca, Modelo = veiculo.Modelo, Quilometragem = veiculo.Quilometragem, AnoDoVeiculo = veiculo.AnoDoVeiculo };
        }
        [HttpPost]
        public static IEnumerable<VeiculoViewModel> MapearListaVeiculoViewModel(IEnumerable<Veiculo> lista)
        {
            var listaVeiculoViewModel = new List<VeiculoViewModel>();
            VeiculoViewModel veiculovm;

            foreach (var item in lista)
            {
                veiculovm = new VeiculoViewModel
                {
                    Marca = item.Marca,
                    Modelo = item.Modelo,
                    Quilometragem = item.Quilometragem,
                    AnoDoVeiculo = item.AnoDoVeiculo
                };

                listaVeiculoViewModel.Add(veiculovm);
            }

            return listaVeiculoViewModel;
        }
    }
}
