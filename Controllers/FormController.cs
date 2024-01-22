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
        private EmailSettings _emailSettings;

        public FormController(IOptions<EmailSettings> emailSettings) {
            _emailSettings = emailSettings.Value; 
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
                string toEmail = string.IsNullOrEmpty(email) ? _emailSettings.ToEmail : email;

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "E-mail")
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

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    //smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
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
                        email = _emailSettings.ToEmail;
                    }

                    MailMessage mail = new MailMessage();
                    // Obtem os anexos contidos em um arquivo arraylist e inclui na mensagem
                   

                    mail.From = new MailAddress(_emailSettings.FromEmail);
                    mail.To.Add(new MailAddress(_emailSettings.ToEmail));
                    mail.CC.Add(new MailAddress(email));
                    mail.Subject = "Fale conosco SESC - " + subject;
                    mail.Body = message;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                    //outras opções
                    //mail.Attachments.Add(new Attachment(arquivo));
                    //

                    using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                    {
                        smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
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
