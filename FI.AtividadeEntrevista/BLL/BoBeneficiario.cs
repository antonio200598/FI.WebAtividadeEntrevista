using FI.AtividadeEntrevista.DAL.Beneficiarios;
using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo Beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public long Incluir(Beneficiario beneficiario)
        {
            DaoBeneficiario daoBeneficiario = new DaoBeneficiario();
            return daoBeneficiario.Incluir(beneficiario);
        }

        /// <summary>
        /// Lista os beneficiario
        /// </summary>
        public List<Beneficiario> ListarPorCliente(long clienteId)
        {
            DaoBeneficiario daoBeneficiario = new DaoBeneficiario();
            return daoBeneficiario.ListarPorCliente(clienteId);
        }

        /// <summary>
        /// Altera um beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public void Alterar(Beneficiario beneficiario)
        {
            new DaoBeneficiario().Alterar(beneficiario);
        }

        /// <summary>
        /// Excluir o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            new DaoBeneficiario().Excluir(id);
        }
    }
}
