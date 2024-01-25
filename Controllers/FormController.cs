using Empreendimento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Net.Mail;
using System.Net;

namespace Empreendimento.Controllers
{
    public class FormController : Controller
    {
        private readonly EmailSettings _emailSettings;

        public FormController(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public IActionResult EnviarEmail()
        {
            // Configuração do cliente SMTP
            SmtpClient client = new SmtpClient("smtp.office365.com")
            {
                Port = int.Parse("smtp.office365.com"),
                Credentials = new NetworkCredential("INVEST-FACTORING@outlook.com", "TESTE@123a"),
                EnableSsl = true,
            };

            // Construir o e-mail
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("INVEST-FACTORING@outlook.com"),
                Subject = "Assunto do E-mail",
                Body = "Corpo do E-mail",
                IsBodyHtml = true,
            };

            // Adicionar destinatários
            mailMessage.To.Add("INVEST-FACTORING@outlook.com");
            if (!string.IsNullOrEmpty(_emailSettings.CcEmail))
            {
                mailMessage.CC.Add(_emailSettings.CcEmail);
            }

            try
            {
                // Enviar o e-mail
                client.Send(mailMessage);
                ViewBag.Message = "E-mail enviado com sucesso!";
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Erro ao enviar o e-mail: {ex.Message}";
            }

            return View();
        }

        public IActionResult PessoaFisica()
        {
            return View();
        }
        public IActionResult PessoaJuridica()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PessoaJuridicaEnvio(FichaCadastroPessoaJuridica fichaCadastroPessoaJuridica)
        {

            string dadosAsString = $"Razão Social: {fichaCadastroPessoaJuridica.txtRazaoSocial}<br>" +
                         $"Nome Fantasia: {fichaCadastroPessoaJuridica.txtNomeFantasia}<br>" +
                         $"CNPJ: {fichaCadastroPessoaJuridica.txtCNPJ}<br>" +
                         $"Inscrição Municipal: {fichaCadastroPessoaJuridica.txtInscricaoMunicipal}<br>" +
                         $"Data de Abertura: {fichaCadastroPessoaJuridica.txtDataAbertura}<br>" +
                         $"Endereço Comercial: {fichaCadastroPessoaJuridica.txtEnderecoComercial}<br>" +
                         $"Número: {fichaCadastroPessoaJuridica.txtNumero}<br>" +
                         $"CEP: {fichaCadastroPessoaJuridica.txtCep}<br>" +
                         $"Cidade: {fichaCadastroPessoaJuridica.txtCidade}<br>" +
                         $"UF: {fichaCadastroPessoaJuridica.txtUf}<br>" +
                         $"Telefone Comercial: {fichaCadastroPessoaJuridica.txtTelefoneComercial}<br>" +
                         $"Telefone Celular: {fichaCadastroPessoaJuridica.txtTelefoneCelular}<br>" +
                         $"Telefone Recado: {fichaCadastroPessoaJuridica.txtTelefoneRecado}<br>" +
                         $"Faturamento Mensal: {fichaCadastroPessoaJuridica.txtFaturamentoMensal}<br>" +
                         $"E-mail: {fichaCadastroPessoaJuridica.txtEmail}<br><br>" + // Duas quebras de linha
                         $"Sócio Majoritário:<br>" +
                         $"Nome: {fichaCadastroPessoaJuridica.txtSocioMajoritario}<br>" +
                         $"RG: {fichaCadastroPessoaJuridica.txtRG}<br>" +
                         $"CPF: {fichaCadastroPessoaJuridica.txtCpf}<br>" +
                         $"Data de Nascimento: {fichaCadastroPessoaJuridica.txtDataNascimento}<br>" +
                         $"Endereço Residencial: {fichaCadastroPessoaJuridica.txtEnderecoResidencial}<br>" +
                         $"Bairro: {fichaCadastroPessoaJuridica.txtBairro}<br>" +
                         $"Número: {fichaCadastroPessoaJuridica.NumeroSocio}<br>" +
                         $"CEP: {fichaCadastroPessoaJuridica.CEPSocio}<br>" +
                         $"Renda Mensal: {fichaCadastroPessoaJuridica.txtRendaMensal}<br><br>" + // Duas quebras de linha
                         $"Referências Bancárias:<br>" +
                         $"Banco 1: {fichaCadastroPessoaJuridica.txtBanco1}<br>" +
                         $"Operação 1: {fichaCadastroPessoaJuridica.txtOperacao1}<br>" +
                         $"Agência 1: {fichaCadastroPessoaJuridica.txtAgencia1}<br>" +
                         $"Conta 1: {fichaCadastroPessoaJuridica.txtConta1}<br>" +
                         $"Banco 2: {fichaCadastroPessoaJuridica.txtBanco2}<br>" +
                         $"Operação 2: {fichaCadastroPessoaJuridica.txtOperacao2}<br>" +
                         $"Agência 2: {fichaCadastroPessoaJuridica.txtAgencia2}<br>" +
                         $"Conta 2: {fichaCadastroPessoaJuridica.txtConta2}<br><br>" + // Duas quebras de linha
                         $"Dados do Crédito:<br>" +
                         $"Valor do Crédito: {fichaCadastroPessoaJuridica.txtValorCredito}<br>" +
                         $"Vendedor: {fichaCadastroPessoaJuridica.txtVendedor}.";

            var teste = TesteEnvioEmail("INVEST-FACTORING@outlook.com", "Emprestimo", dadosAsString);


            return RedirectToAction("EnviadoSucesso");
        }



        [HttpPost]
        public async Task<IActionResult> CreateDadosFisico(DadosPsFisica fisica)
        {
            try
            {
                var pessoaDados = fisica.txtAgencia;
                string dadosAsString = $"Nome Completo: {fisica.txtNomeCompleto}<br>" +
                        $"CPF: {fisica.txtCPF}<br>" +
                        $"RG: {fisica.txtRG}<br>" +
                        $"Orgão Expedidor: {fisica.txtOrgaoExp}<br>" +
                        $"Data de Nascimento: {fisica.txtDataNascimento}<br>" +
                        $"Estado Civil: {fisica.txtEstadoCivil}<br>" +
                        $"Nome do Cônjuge: {fisica.txtNomeConjuge}<br>" +
                        $"Data nascimento Conjuge: {fisica.txtDataNascimentoConjuge}<br>" +
                        $"Email: {fisica.txtEmail}<br>" +
                        $"Telefone Comercial: {fisica.txtTelefoneComercial}<br>" +
                        $"Telefone Celular: {fisica.txtTelefoneCelular}<br>" +
                        $"Endereço Residencial: {fisica.txtEnderecoResidencial}<br>" +
                        $"Numero: {fisica.txtNumero}<br>" +
                        $"Cidade: {fisica.txtCidade}<br>" +
                        $"UF: {fisica.txtUF}<br>" +
                        $"CEP: {fisica.txtCEP}<br>" +
                        $"Empresa: {fisica.txtEmpresa}<br>" +
                        $"Profissão: {fisica.txtProfissao}<br>" +
                        $"Data Admissão: {fisica.txtDataAdmissao}<br>" +
                        $"Renda Líquida: {fisica.txtRendaLiquida}<br>" +
                        $"Banco: {fisica.txtBanco}<br>" +
                        $"Operação: {fisica.txtOperacao}<br>" +
                        $"Agência: {fisica.txtAgencia}<br>" +
                        $"Conta: {fisica.txtConta}<br>" +
                        $"Valor Crédito: {fisica.txtValorCredito}<br>" +
                        $"Vendedor: {fisica.txtVendedor}.";


                var teste = TesteEnvioEmail("INVEST-FACTORING@outlook.com", "Emprestimo", dadosAsString);

                return RedirectToAction("EnviadoSucesso");
            }
            catch (Exception ex)
            {
                // Lógica para lidar com exceções, se necessário
                return RedirectToAction("PessoaFisica");
            }
        }

        public IActionResult EnviadoSucesso()
        {
            return View();
        }

        public async Task<string> TesteEnvioEmail(string email, string assunto, string mensagem)
        {
            try
            {
                //email destino, assunto do email, mensagem a enviar
                await SendEmailAsync(email, assunto, mensagem);
               
                return "Ok";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "Erro";
            }
        }


        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var teste =  Execute(email, subject, message);
                return Task.FromResult(0);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task ExecuteOuvidoria(string email, string subject, string message, ArrayList anexos)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email) ? "INVEST-FACTORING@outlook.com" : email;

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress("INVEST-FACTORING@outlook.com", "E-mail")
                };

                mail.To.Add(new MailAddress(toEmail));
                mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                mail.Subject = "EMPRESTIMO " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                //outras opções
                //mail.Attachments.Add(new Attachment(arquivo));
                //

                using (SmtpClient smtp = new SmtpClient("smtp.office365.com", _emailSettings.PrimaryPort))
                {
                    //smtp.Credentials = new NetworkCredential("INVEST-FACTORING@outlook.com", "TESTE@123a");
                    smtp.UseDefaultCredentials = false; // Use local IP, no credentials needed
                    smtp.EnableSsl = false; // Ativar o TLS
                    smtp.Timeout = 10000;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        public async Task Execute(string email, string subject, string message)
        {

            using (SmtpClient smtpCliente = new SmtpClient())
            {
                try
                {
                    string nomeArquivo = "";
                    if (email == null || email == "")
                    {
                        email = "INVEST-FACTORING@outlook.com";
                    }

                    MailMessage mail = new MailMessage();
                    // Obtem os anexos contidos em um arquivo arraylist e inclui na mensagem
                   

                    mail.From = new MailAddress("INVEST-FACTORING@outlook.com");
                    mail.To.Add(new MailAddress("INVEST-FACTORING@outlook.com"));
                    mail.CC.Add(new MailAddress(email));
                    mail.Subject = "EMPRESTIMO - " + subject;
                    mail.Body = message;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                    //outras opções
                    //mail.Attachments.Add(new Attachment(arquivo));
                    //

                    using (SmtpClient smtp = new SmtpClient("smtp.office365.com", _emailSettings.PrimaryPort))
                    {
                        smtp.Credentials = new NetworkCredential("INVEST-FACTORING@outlook.com", "TESTE@123a");
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


    }
}
