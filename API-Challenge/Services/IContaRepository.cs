using API_Challenge.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Challenge.Services
{
    public interface IContaRepository
    {
        Conta GetContasPorNumero(int numeroConta);
        IEnumerable<Conta> GetAllContas();
        String PostConta(Conta conta);
        String PutDados(int numeroConta, decimal valor, string operacao);
    }
}
