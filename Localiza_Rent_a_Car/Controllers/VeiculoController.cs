using Dominio.Entidades;
using Dominio.IRepositories;
using Historia.Veiculos;
using Localiza_Rent_a_Car.Factories;
using Localiza_Rent_a_Car.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Localiza_Rent_a_Car.Controllers
{
    
    Route["api/v1/veiculos"]
        public class VeiculoController : ControllerBase
    {
        private readonly IncluirVeiculo _incluirVeiculo;
        private readonly ConsultarVeiculo _consultarVeiculo;
        private readonly AtualizarVeiculo _atualizarVeiculo;
        private readonly RemoverVeiculo _removerVeiculo;

        public VeiculoController(IVeiculoRepository veiculoRepository)
        {
            _incluirVeiculo = new IncluirVeiculo(veiculoRepository);
            _consultarVeiculo = new ConsultarVeiculo(veiculoRepository);
            _atualizarVeiculo = new AtualizarVeiculo(veiculoRepository);
            _removerVeiculo = new RemoverVeiculo(veiculoRepository);
        }

        [HttpPost("incluir-veiculo")]
        public async Task<IActionResult> Criar([FromBody] VeiculoViewModel veiculoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensagem = "O campo Marca é obrigatório" });
            }

            var veiculo = VeiculoFactory.MapearVeiculo(veiculoViewModel);

            await _incluirVeiculo.Executar(veiculo);

            return Ok(new { mensagem = "Veiculo Incluido com sucesso!" });
        }

        [HttpPut("atualizar-veiculo")]
        public async Task<IActionResult> Alterar([FromBody] VeiculoViewModel veiculoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensagem = "O campo Modelo é obrigatório" });
            }

            try
            {
                var veiculo = VeiculoFactory.MapearVeiculo(veiculoViewModel);

                await _atualizarVeiculo.Executar(veiculoViewModel.Marca, veiculo);

                return Ok(new { mensagem = "Veiculo atualizado com sucesso" });
            }
            catch (System.Exception)
            {

                return BadRequest(new { erro = "Erro ao atualizar o veiculo" });
            }

        }

        [HttpDelete("remover-veiculo/{modelo}")]
        public async Task<IActionResult> Excluir(string modelo)
        {
            try
            {
                await _removerVeiculo.Executar(modelo);
                return Ok(new { mensagem = "Veiculo removido com sucesso" });
            }
            catch (System.Exception)
            {

                return BadRequest(new { erro = "Erro ao remover o veiculo" });
            }

        }

        [HttpGet("listar-veiculos-marca")]
        public async Task<IEnumerable<VeiculoViewModel>> ListarMarca()
        {
            var listaDeVeiculosMarca = await _consultarVeiculo.ListarVeiculosMarca();

            if (listaDeVeiculosMarca == null)
            {
                return null;
            }

            var listaVeiculoVm = VeiculoFactory.MapearListaVeiculoViewModel(listaDeVeiculosMarca);
            return listaVeiculoVm;
        }

        [HttpGet("listar-veiculos-modelo")]
        public async Task<IEnumerable<VeiculoViewModel>> ListarModelo()
        {
            var listaDeVeiculosModelo = await _consultarVeiculo.ListarVeiculosModelo();

            if (listaDeVeiculosModelo == null)
            {
                return null;
            }

            var listaVeiculoVm = VeiculoFactory.MapearListaVeiculoViewModel(listaDeVeiculosModelo);
            return listaVeiculoVm;
        }

        [HttpGet("consultar-veiculo-marca/{marca}")]
        public async Task<VeiculoViewModel> ConsultarPorMarca(string marca)
        {
            var veiculoViewModel = await _consultarVeiculo.ConsultarPorMarca(ToString());

            if (veiculoViewModel == null)
            {
                return null;
            }

            var veiculoVM = VeiculoFactory.MapearVeiculoViewModel(veiculoViewModel);
            return veiculoVM;
        }

        [HttpGet("consultar-veiculo-modelo/{modelo}")]
        public async Task<VeiculoViewModel> ConsultarPorModelo(string modelo)
        {
            var veiculoViewModel = await _consultarVeiculo.ConsultarPorModelo(ToString());

            if (veiculoViewModel == null)
            {
                return null;
            }

            var veiculoVM = VeiculoFactory.MapearVeiculoViewModel(veiculoViewModel);
            return veiculoVM;
        }
    }

    
}
