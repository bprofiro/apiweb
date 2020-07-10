using System;
using System.Collections.Generic;
using apiweb.Models;
using apiweb.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace apiweb.Controllers
{
  public class FundoCapitalController: Controller
  {
    private readonly IFundoCapitalRepository _repositorio;

    public FundoCapitalController(IFundoCapitalRepository repositorio)
    {
      _repositorio = repositorio;
    }

    [HttpGet("fundoscapital")]
    public IActionResult ListarFundos()
    {
      return Ok(_repositorio.ListarFundos());
    }

    [HttpPost("fundoscapital")]
    public IActionResult AdicionarFundo([FromBody]FundoCapital fundo)
    {
      _repositorio.Adicionar(fundo);
      return Ok();
    }

    [HttpPut("fundoscapital/{id}")]
    public IActionResult AlterarFundo(Guid id, [FromBody]FundoCapital fundo)
    {
      var fundoAntigo = _repositorio.ObterPorId(id);

      if (fundoAntigo == null)
      {
        return NotFound();
      }

      fundoAntigo.Nome = fundo.Nome;
      fundoAntigo.ValorAtual = fundo.ValorAtual;
      fundoAntigo.ValorNecessario = fundo.ValorNecessario;
      fundoAntigo.DataResgate = fundo.DataResgate;

      _repositorio.Alterar(fundoAntigo);

      return Ok();
    }

    [HttpGet("fundoscapital/{id}")]
    public IActionResult ObterFundo(Guid id)
    {
      var fundo = _repositorio.ObterPorId(id);

      if (fundo == null)
      {
        return NotFound();
      }

      return Ok(fundo);
    }

    [HttpDelete("fundoscapital/{id}")]
    public IActionResult RemoverFundo(Guid id)
    {
      var fundo = _repositorio.ObterPorId(id);

      if (fundo == null)
      {
        return NotFound();
      }

      _repositorio.RemoverFundo(fundo);

      return Ok();
    }
  }
}