using FI.WebAtividadeEntrevista.Validators;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FI.WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Beneficiario
    /// </summary>
    public class BeneficiarioModel
    {
        public long? Id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }

        private string _cpf;

        /// <summary>
        /// CPF
        /// </summary>
        [Required]
        [ValidateCPF(ErrorMessage = "CPF do beneficiário inválido")]
        public string CPF
        {
            get => _cpf;
            set => _cpf = Regex.Replace(value, @"\D", "");
        }
    }
}
