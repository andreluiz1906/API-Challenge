using API_Challenge.Data;
using API_Challenge.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace API_Challenge.Services
{
    public class ContaRepository : IContaRepository
    {
        private ContaBancariaContext _context;
        public ContaRepository(ContaBancariaContext context)
        {
            _context = context;
        }

        IEnumerable<Conta> IContaRepository.GetAllContas()
        {
            return _context.Contas;
        }

        public Conta GetContasPorNumero(int numeroConta)
        {
            return _context.Contas.FirstOrDefault(c => c.Numero == numeroConta);
        }

        public string PostConta(Conta conta)
        {
            try
            {
                if (conta == null)
                    return "Dados inválidos";

                Conta valida = _context.Contas.FirstOrDefault(x => x.Numero == conta.Numero);
                if (valida == null)
                {
                    _context.Contas.Add(conta);
                    _context.SaveChanges();
                     return "Ok";
                }

                return "O número informado já está em uso";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string PutDados(int numeroConta, decimal valor, string operacao)
        {
            try
            {
                Conta conta = GetContasPorNumero(numeroConta);
                if (conta == null || valor <= 0)
                    return "Dados inválidos";

                decimal saldoAtual = conta.Saldo;
                decimal saldoAtualizado = 0;
                if(operacao == "sacar")
                {
                    saldoAtualizado = saldoAtual - valor;
                    if (saldoAtualizado < 0)
                        return "Saldo insuficiente";
                }
                else
                {
                    saldoAtualizado = saldoAtual + valor;
                }
                conta.Saldo = saldoAtualizado;

                _context.Contas.Update(conta);
                _context.SaveChanges();
                return "Saldo Atualizado: "+ String.Format(new CultureInfo("pt-BR"), "{0:c}", conta.Saldo);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
