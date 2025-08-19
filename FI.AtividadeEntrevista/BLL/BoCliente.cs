using FI.AtividadeEntrevista.DAL;
using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(Cliente cliente)
        {
            DaoCliente cli = new DaoCliente();
            return cli.Incluir(cliente);
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public void Alterar(Cliente cliente)
        {
            DaoCliente cli = new DaoCliente();
            cli.Alterar(cliente);
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public Cliente Consultar(long id)
        {
            DaoCliente cli = new DaoCliente();
            return cli.Consultar(id);
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DaoCliente cli = new DaoCliente();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DaoCliente cli = new DaoCliente();
            return cli.Pesquisa(iniciarEm,  quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// Verifica existencia de CPF no banco
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF)
        {
            DaoCliente cli = new DaoCliente();
            return cli.VerificarExistencia(CPF);
        }
    }
}
