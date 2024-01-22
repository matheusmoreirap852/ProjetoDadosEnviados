using Empreendimento.Models;
using Microsoft.AspNetCore.Mvc;

namespace Empreendimento.Controllers
{
    public class FormController : Controller
    {
        public IActionResult PessoaFisica()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDadosFisico(DadosPsFisica fisica)
        {
            try
            {
                var pessoaDados = fisica.Agencia;
                // Lógica para processar os dados e salvar no banco de dados, se necessário
                // Você pode adicionar lógica adicional aqui

                return View(fisica); // Ou redirecionar para outra página se necessário
            }
            catch (Exception ex)
            {
                // Lógica para lidar com exceções, se necessário
                return View(ex);
            }
        }
        
    }
}
