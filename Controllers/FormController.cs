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



        [HttpPost]
        public async Task<IActionResult> CreateDadosFisico(DadosPsFisica fisica)
        {
            try
            {
                var pessoaDados = fisica.txtAgencia;
                //var ENVIOU = EnviarEmail();
                var teste = TesteEnvioEmail("gota852@gmail.com", "teste", "teste");

                return View(fisica); 
            }
            catch (Exception ex)
            {
                // Lógica para lidar com exceções, se necessário
                return View(ex);
            }
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

                mail.Subject = "Ouvidoria Sesc " + subject;
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
                    mail.Subject = "Fale conosco SESC - " + subject;
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
