using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Viajes.Data;
using Viajes.Models;
using Viajes.ViewModel;
using Viajes.Helper;
using MLModel1_ConsoleApp1;
using Microsoft.Extensions.ML;

namespace Viajes.Controllers
{
    public class ContactoController : Controller
    {
        private readonly ILogger<ContactoController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> _predictionEnginePool;
        public ContactoController(ILogger<ContactoController> logger,ApplicationDbContext context,PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> predictionEnginePool)
        {
            _logger = logger;
            _context = context;
            _predictionEnginePool = predictionEnginePool;

        }

        public IActionResult Index()
        {
            var miscontactos = from o in _context.DataContacto select o;
            _logger.LogDebug("contactos {miscontactos}", miscontactos);
            var viewModel = new ContactoViewModel{
                FormContacto = new Contacto(),
                ListContacto = miscontactos
            };
             _logger.LogDebug("viewModel {viewModel}", viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Enviar(ContactoViewModel viewModel)
        {
            _logger.LogDebug("Ingreso a Enviar Mensaje");
            MLModel1.ModelInput sampleData = new MLModel1.ModelInput()
            {
                Sentiment_Text = viewModel.FormContacto.Message,
            };
            MLModel1.ModelOutput prediction = _predictionEnginePool.Predict(sampleData);
            var dato=prediction.PredictedLabel;
            Console.WriteLine($"El sentimiento de modelo es: {dato}");
            


            if(prediction.PredictedLabel ==1){
                viewModel.FormContacto.Sentimiento = "Positivo";
            }
            else{
                viewModel.FormContacto.Sentimiento = "Negativo";
            }
            var contacto = new Contacto
            {
                Name = viewModel.FormContacto.Name,
                Email = viewModel.FormContacto.Email,
                Message = viewModel.FormContacto.Message,
                Contrasena = viewModel.FormContacto.Contrasena,
                Sentimiento = viewModel.FormContacto.Sentimiento
            };
            Console.WriteLine($"El sentimiento es: {contacto.Sentimiento}");

            //var emailService = new SendMail();
            //await emailService.EnviarCorreoAsync(contacto.Email, "Asunto del correo", contacto.Message,contacto.Contrasena);
            var __apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            var emailService2 = new SendMailSendGrid();
            await emailService2.EnviarCorreoAsync(contacto.Email, "Asunto del correo", contacto.Message,contacto.Contrasena);


            _context.Add(contacto);
            _context.SaveChanges();

            TempData["Message"] = $"Se registro el contacto, con un mensaje {contacto.Sentimiento} ";            
            
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}