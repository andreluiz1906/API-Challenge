using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_Challenge.Models
{
    public class Conta
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage ="O número da conta é obrigatório")]
        [Range(1, 5000, ErrorMessage ="O número da conta precisa estar entre 1 e 5000")]        
        public int Numero { get; set; }
        [Range(0, 9999999999.99, ErrorMessage = "O saldo precisa estar entre R$ 0,00 e R$ 9.999.999.999,99")]
        public decimal Saldo { get; set; }
    }
}
