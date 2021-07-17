using API_Challenge.Data;
using API_Challenge.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Challenge.Controllers
{
    [ApiController]
    [Route("contaBancaria/")]
    public class BancoController : ControllerBase
    {
        private ContaBancariaContext _context;
        public BancoController(ContaBancariaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult PostConta([FromBody]Conta conta)
        {
            try
            {
                if (conta == null)
                {
                    return BadRequest("Dados inválidos");
                }

                Conta valida = _context.Contas.FirstOrDefault(x => x.Numero == conta.Numero);
                if (valida == null)
                {
                    _context.Contas.Add(conta);
                    _context.SaveChanges();
                    return CreatedAtAction(nameof(GetContaPorNumero), new { numConta = conta.Numero }, conta);
                }

                return BadRequest("O número informado já está em uso");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IEnumerable<Conta> GetContas()
        {
            return _context.Contas;
        }

        [HttpGet("{numConta}")]
        public IActionResult GetContaPorNumero(int numConta)
        {
            try
            {
                if(numConta == null || numConta <= 0)
                {
                    return BadRequest("Dados inválidos");
                }

                Conta conta = _context.Contas.FirstOrDefault(c => c.Numero == numConta);
                if (conta != null)
                {
                    return Ok(conta);
                }

                return NotFound("Conta não encontrada");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("saldo/{numConta}")]
        public IActionResult GetSaldoConta(int numConta)
        {
            try
            {
                if (numConta == null || numConta <= 0)
                {
                    return BadRequest("Dados inválidos");
                }

                Conta conta = _context.Contas.FirstOrDefault(c => c.Numero == numConta);
                if (conta != null)
                {
                    List<decimal> saldo = new List<decimal> { conta.Saldo };
                    return Ok(JsonConvert.SerializeObject(saldo, Formatting.Indented));
                }

                return NotFound("Conta não encontrada");
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
                if(conta == null || valor == null)
                {
                    return BadRequest("Dados inválidos");
                }

                if(conta <= 0)
                {
                    return BadRequest("O número da conta informado é inválido");
                }

                if(valor <= 0)
                {
                    return BadRequest("O valor informado é inválido");
                }

                Conta contaBancaria = _context.Contas.FirstOrDefault(c => c.Numero == conta);
                if(contaBancaria == null)
                {
                    return BadRequest("Dados inválidos");
                }

                decimal saldoAtual = contaBancaria.Saldo;
                decimal saldoNovo = saldoAtual + valor;

                contaBancaria.Saldo = saldoNovo;

                _context.Contas.Update(contaBancaria);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetSaldoConta), new { numConta = contaBancaria.Numero }, contaBancaria);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("sacar/{conta}/{valor}")]
        public IActionResult PutSacar(int conta, decimal valor)
        {
            try
            {
                if (conta == null || valor == null)
                {
                    return BadRequest("Dados inválidos");
                }

                if (conta <= 0)
                {
                    return BadRequest("O número da conta informado é inválido");
                }

                if (valor <= 0)
                {
                    return BadRequest("O valor informado é inválido");
                }

                Conta contaBancaria = _context.Contas.FirstOrDefault(c => c.Numero == conta);
                if (contaBancaria == null)
                {
                    return BadRequest("Dados inválidos");
                }

                decimal saldoAtual = contaBancaria.Saldo;
                decimal saldoNovo = saldoAtual - valor;

                if(saldoNovo < 0)
                {
                    return BadRequest("Saldo insuficiente");
                }

                contaBancaria.Saldo = saldoNovo;
                _context.Contas.Update(contaBancaria);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetSaldoConta), new { numConta = contaBancaria.Numero }, contaBancaria);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
