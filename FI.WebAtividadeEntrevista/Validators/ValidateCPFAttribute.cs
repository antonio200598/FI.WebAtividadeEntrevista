using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FI.WebAtividadeEntrevista.Validators
{
    public class ValidateCPFAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string cpf = value as string;

            if (string.IsNullOrWhiteSpace(cpf))
                return new ValidationResult("CPF inválido");

            if (cpf.ToCharArray().Contains('.') || cpf.ToCharArray().Contains('-'))
                cpf = cpf.Replace(".", "").Replace("-", "").Trim();

            if (cpf.Length != 11)
                return new ValidationResult("O CPF deve ter 11 dígitos");

            if (IsAllDigitsSame(cpf))
                return new ValidationResult("CPF inválido");

            if (!IsValidCPF(cpf))
                return new ValidationResult("CPF inválido");

            return ValidationResult.Success;
        }

        private bool IsValidCPF(string cpf)
        {
            var multiplicadores1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicadores2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string cpfBase = cpf.Substring(0, 9);
            string digitos = cpf.Substring(9, 2);

            int digito1 = CalcularDigitoVerificador(cpfBase, multiplicadores1);
            int digito2 = CalcularDigitoVerificador(cpfBase + digito1.ToString(), multiplicadores2);

            string cpfCalculado = digito1.ToString() + digito2.ToString();

            return cpfCalculado == digitos;
        }

        private int CalcularDigitoVerificador(string cpf, int[] multiplicadores)
        {
            int soma = 0;

            for (int i = 0; i < multiplicadores.Length; i++)
                soma += int.Parse(cpf[i].ToString()) * multiplicadores[i];

            int resto = soma % 11;

            return (resto < 2) ? 0 : 11 - resto;
        }

        private bool IsAllDigitsSame(string cpf)
        {
            char primeiroDigito = cpf[0];

            for (int i = 1; i < cpf.Length; i++)
            {
                if (cpf[i] != primeiroDigito)
                    return false;
            }

            return true;
        }
    }
}
