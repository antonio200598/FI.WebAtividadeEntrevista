using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoBeneficiario boBeneficiario = new BoBeneficiario();
            BoCliente boCliente = new BoCliente();

            try
            {
                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    throw new Exception(string.Join("<br/>", erros));
                }

                if (boCliente.VerificarExistencia(model.CPF))
                    throw new Exception(string.Format("CPF já atribuído para um cliente: {0}", model.CPF));

                List<string> cpfsDuplicados = model.Beneficiarios.GroupBy(x => x.CPF).Where(y => y.Count() > 1).Select(z => z.Key).ToList();

                if (cpfsDuplicados.Any())
                    throw new Exception($"Existem beneficiarios com o mesmo CPF: {string.Join(", ", cpfsDuplicados)}");

                Cliente cliente = new Cliente()
                {
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF
                };

                model.Id = boCliente.Incluir(cliente);

                if (model.Beneficiarios.Count > 0)
                {
                    foreach (BeneficiarioModel beneficiarioModel in model.Beneficiarios)
                    {
                        if (beneficiarioModel == null)
                            continue;

                        Beneficiario beneficiario = new Beneficiario()
                        {
                            Nome = beneficiarioModel.Nome,
                            CPF = beneficiarioModel.CPF,
                            ClienteId = model.Id
                        };

                        beneficiario.Id = boBeneficiario.Incluir(beneficiario);
                    }
                }

                return Json("O Cliente foi incluído com sucesso!");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;

                return Json(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoBeneficiario boBeneficiario = new BoBeneficiario();
            BoCliente boCliente = new BoCliente();

            try
            {
                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    throw new Exception(string.Join("<br/>", erros));
                }

                List<string> cpfsDuplicados = model.Beneficiarios.GroupBy(x => x.CPF).Where(y => y.Count() > 1).Select(z => z.Key).ToList();

                if (cpfsDuplicados.Any())
                    throw new Exception($"Existem beneficiarios com o mesmo CPF: {string.Join(", ", cpfsDuplicados)}");

                List<Beneficiario> beneficiarioList = boBeneficiario.ListarPorCliente(model.Id);

                foreach (Beneficiario beneficiario in beneficiarioList)
                {
                    if (model.Beneficiarios.Find(x => x.CPF == beneficiario.CPF && x.Id != beneficiario.Id) != null)
                        throw new Exception(string.Format("Beneficiario de CPF: {0} já está cadastrado para este cliente!", beneficiario.CPF));
                }

                Cliente cliente = new Cliente()
                {
                    Id = model.Id,
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF
                };

                boCliente.Alterar(cliente);

                if (model.Beneficiarios.Count > 0)
                {
                    foreach (BeneficiarioModel beneficiarioModel in model.Beneficiarios)
                    {
                        if (beneficiarioModel == null)
                            continue;

                        if (beneficiarioModel.Id > 0)
                        {
                            Beneficiario beneficiario = new Beneficiario()
                            {
                                Id = beneficiarioModel.Id.Value,
                                ClienteId = model.Id,
                                Nome = beneficiarioModel.Nome,
                                CPF = beneficiarioModel.CPF
                            };

                            boBeneficiario.Alterar(beneficiario);
                        }
                        else
                        {
                            Beneficiario beneficiario = new Beneficiario()
                            {
                                ClienteId = model.Id,
                                Nome = beneficiarioModel.Nome,
                                CPF = beneficiarioModel.CPF
                            };

                            beneficiarioModel.Id = boBeneficiario.Incluir(beneficiario);
                        }
                    }
                }

                return Json("Cadastro alterado com sucesso!");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;

                return Json(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            ClienteModel model = null;

            BoCliente boCliente = new BoCliente();

            Cliente cliente = boCliente.Consultar(id);

            if (cliente != null)
            {
                List<Beneficiario> beneficiarioList = new BoBeneficiario().ListarPorCliente(id);

                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    CPF = cliente.CPF,
                    Beneficiarios = beneficiarioList.Select(beneficiario => new BeneficiarioModel
                    {
                        Id = beneficiario.Id,
                        CPF = beneficiario.CPF,
                        Nome = beneficiario.Nome
                    }).ToList()
                };
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Deletar(long id)
        {
            try
            {
                List<Beneficiario> beneficiarioList = new BoBeneficiario().ListarPorCliente(id);

                if (beneficiarioList != null && beneficiarioList.Count > 0)
                    throw new Exception(string.Format("Existem beneficiários vinculados ao cliente, não é possivel realizar a exclusão"));

                new BoCliente().Excluir(id);

                return Json("Cliente excluído com sucesso!");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;

                return Json(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult DeletarBeneficiario(long id)
        {
            try
            {
                new BoBeneficiario().Excluir(id);

                return Json("Beneficiario excluído com sucesso!");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;

                return Json(string.Format("Não é possivel realizar a exclusão do beneficiário: {0}", ex.Message));
            }
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}