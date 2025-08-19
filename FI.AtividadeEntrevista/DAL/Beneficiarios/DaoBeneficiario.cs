using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FI.AtividadeEntrevista.DAL.Beneficiarios
{
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal long Incluir(Beneficiario beneficiario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("IdCliente", beneficiario.ClienteId),
                new SqlParameter("Nome", beneficiario.Nome),
                new SqlParameter("CPF", beneficiario.CPF)
            };

            DataSet dataSet = base.Consultar("FI_SP_IncBeneficiario", parametros);

            long result = 0;

            if (dataSet.Tables[0].Rows.Count > 0)
                long.TryParse(dataSet.Tables[0].Rows[0][0].ToString(), out result);

            return result;
        }

        /// <summary>
        /// Lista todos os beneficiários
        /// </summary>
        internal List<Beneficiario> ListarPorCliente(long clienteId)
        {
            List<SqlParameter> parametros = new List<SqlParameter> { new SqlParameter("IdCliente", clienteId) };

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
            List<Beneficiario> beneficiarios = Converter(ds);

            return beneficiarios;
        }

        /// <summary>
        /// Altera um beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal void Alterar(Beneficiario beneficiario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("Nome", beneficiario.Nome),
                new SqlParameter("CPF", beneficiario.CPF),
                new SqlParameter("Id", beneficiario.Id)
            };

            base.Executar("FI_SP_AltBeneficiario", parametros);
        }

        /// <summary>
        /// Exclui o Beneficiário
        /// </summary>
        /// <param name="cliente">Objeto de beneficiario</param>
        internal void Excluir(long Id)
        {
            List<SqlParameter> parametros = new List<SqlParameter> { new SqlParameter("Id", Id) };

            base.Executar("FI_SP_DelBeneficiario", parametros);
        }

        private List<Beneficiario> Converter(DataSet ds)
        {
            List<Beneficiario> lista = new List<Beneficiario>();

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Beneficiario beneficiario = new Beneficiario
                    {
                        Id = row.Field<long>("Id"),
                        ClienteId = row.Field<long>("IdCliente"),
                        Nome = row.Field<string>("Nome"),
                        CPF = row.Field<string>("CPF")
                    };

                    lista.Add(beneficiario);
                }
            }

            return lista;
        }
    }
}