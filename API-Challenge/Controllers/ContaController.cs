using API_Challenge.Models;
using API_Challenge.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IContaRepository _service;
        public ContaController(IContaRepository service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult PostConta(Conta conta)
        {
            try
            {
                string retorno = _service.PostConta(conta);

                if (retorno == "Ok")
                    return CreatedAtRoute("GetConta", new { numeroConta = conta.Numero }, conta);
                else
                    return BadRequest(retorno);
                
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Conta>> Get()
        {
            var retorno = _service.GetAllContas();
            return Ok(retorno);
        }

        [HttpGet("{numeroConta}", Name = "GetConta")]
        public ActionResult GetContaPorNumero(int numeroConta)
        {
            try
            {
                if (numeroConta <= 0)
                    return BadRequest("Dados inválidos");

                var retorno = _service.GetContasPorNumero(numeroConta);
                if (retorno == null)
                    return NotFound("Conta não encontrada");
                else
                    return Ok(retorno);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("saldo/{numeroConta}",  Name = "GetSaldo")]
        public IActionResult GetSaldoConta(int numeroConta)
        {
            try
            {
                if (numeroConta <= 0)
                    return BadRequest("Dados inválidos");

                var retorno = _service.GetContasPorNumero(numeroConta);
                if (retorno == null)
                    return NotFound("Conta não encontrada");

                List<decimal> saldo = new List<decimal> { retorno.Saldo };
                return Ok(JsonConvert.SerializeObject(saldo, Formatting.Indented));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("sacar/{conta}/{valor}")]
        public IActionResult PutSacar(int conta, decimal valor)
        {
            try
            {
                string retorno = _service.PutDados(conta, valor, "sacar");

                if (retorno.Contains("Saldo Atualizado"))
                {
                    return Ok(retorno);
                }
                else
                {
                    return BadRequest(retorno);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("depositar/{conta}/{valor}")]
        public IActionResult PutDepositar(int conta, decimal valor)
        {
            try
            {
                string retorno = _service.PutDados(conta, valor, "depositar");

                if (retorno.Contains("Saldo Atualizado"))
                {
                    return Ok(retorno);
                }
                else
                {
                    return BadRequest(retorno);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
