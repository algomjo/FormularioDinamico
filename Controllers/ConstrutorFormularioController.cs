using FormularioDinamico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace FormularioDinamico.Controllers
{
    public class ConstrutorFormularioController : Controller
    {
        private const string SessionKey = "CamposFormulario";

        private List<FormField> ObterCampos()
        {
            var sessionData = HttpContext.Session.GetString(SessionKey);
            return string.IsNullOrEmpty(sessionData)
                ? new List<FormField>()
                : JsonConvert.DeserializeObject<List<FormField>>(sessionData);
        }

        private void SalvarCampos(List<FormField> campos)
        {
            var json = JsonConvert.SerializeObject(campos);
            HttpContext.Session.SetString(SessionKey, json);
        }

        public IActionResult RemoverCampo(int index)
        {
            var campos = ObterCampos();
            if (index >= 0 && index < campos.Count)
            {
                campos.RemoveAt(index);
                SalvarCampos(campos);
            }

            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {
            var tipos = new Dictionary<string, string>
                {
                    { "text", "Texto" },
                    { "email", "E-mail" },
                    { "number", "Número" },
                    { "select", "Lista de opções" },
                    { "textarea", "Área de texto" },
                    { "date", "Data" },
                    { "range", "Faixa Numérica (Range)" },
                    { "checkbox", "Caixa de Checagem (Checkbox)" },
                    { "radio", "Botão de Opção (Radio)" }
                };

            ViewBag.Tipos = tipos.Select(t => new SelectListItem { Value = t.Key, Text = t.Value }).ToList();
            ViewBag.TiposDescricao = tipos; // <-- Novo ViewBag com o dicionário

            ViewBag.Campos = ObterCampos();
            return View(new FormField());
        }

        public IActionResult EditarCampo(int index)
        {
            var campos = ObterCampos();
            if (index < 0 || index >= campos.Count)
                return RedirectToAction("Index");

            ViewBag.Tipos = new Dictionary<string, string>
    {
                { "text", "Texto" },
                { "email", "E-mail" },
                { "number", "Número" },
                { "select", "Lista de opções" },
                { "textarea", "Área de texto" },
                { "date", "Data" },
                { "range", "Faixa Numérica (Range)" },
                { "checkbox", "Caixa de Checagem (Checkbox)" },
                { "radio", "Botão de Opção (Radio)" }
    }
            .Select(t => new SelectListItem { Value = t.Key, Text = t.Value }).ToList();

            ViewBag.Index = index;
            return View("EditarCampo", campos[index]); // Reutiliza uma nova view
        }

        [HttpPost]
        public IActionResult EditarCampo(int index, FormField campo, string[]? optionsInput)
        {
            var campos = ObterCampos();

            if (index < 0 || index >= campos.Count)
                return RedirectToAction("Index");

            if (optionsInput != null && campo.Type is "select" or "radio")
            {
                campo.Options = optionsInput[0]
                    .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(campo.Label))
            {
                campo.Name = campo.Label
                    .ToLower()
                    .Replace(" ", "_")
                    .Replace("-", "_")
                    .Replace("ç", "c")
                    .Replace("á", "a").Replace("ã", "a").Replace("â", "a")
                    .Replace("é", "e").Replace("ê", "e")
                    .Replace("í", "i")
                    .Replace("ó", "o").Replace("õ", "o").Replace("ô", "o")
                    .Replace("ú", "u")
                    .Replace("ñ", "n");
            }

            campos[index] = campo;
            SalvarCampos(campos);

            return RedirectToAction("Index");
        }



        [HttpPost]
        public IActionResult AdicionarCampo(FormField campo, string[]? optionsInput)
        {
            if (optionsInput != null && campo.Type is "select" or "radio")
            {
                campo.Options = optionsInput[0]
                    .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
            }


            // Geração automática do "Name"
            if (!string.IsNullOrWhiteSpace(campo.Label))
            {
                campo.Name = campo.Label
                    .ToLower()
                    .Replace(" ", "_")
                    .Replace("-", "_")
                    .Replace("ç", "c")
                    .Replace("á", "a").Replace("ã", "a").Replace("â", "a")
                    .Replace("é", "e").Replace("ê", "e")
                    .Replace("í", "i")
                    .Replace("ó", "o").Replace("õ", "o").Replace("ô", "o")
                    .Replace("ú", "u")
                    .Replace("ñ", "n");
            }


            var campos = ObterCampos();
            campos.Add(campo);
            SalvarCampos(campos);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SalvarJson()
        {
            var campos = ObterCampos();
            var caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "formulario.json");
            System.IO.File.WriteAllText(caminho, JsonConvert.SerializeObject(campos, Formatting.Indented));
            HttpContext.Session.Remove(SessionKey);
            return RedirectToAction("Index", "Formulario");
        }
    }
}
