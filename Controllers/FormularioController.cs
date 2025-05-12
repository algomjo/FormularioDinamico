using Microsoft.AspNetCore.Mvc;
using FormularioDinamico.Models;
using Newtonsoft.Json;

public class FormularioController : Controller
{
    public IActionResult Index()
    {
        var campos = CarregarJsonFormulario();
        ViewBag.Formulario = campos;
        return View(new FormData());
    }

    [HttpPost]
    public IActionResult Index(FormData dados)
    {
        var campos = CarregarJsonFormulario();
        ViewBag.Formulario = campos;

        // Mostra o resultado serializado na tela
        ViewBag.Resultado = JsonConvert.SerializeObject(dados.Values, Formatting.Indented);
        return View(dados);
    }

    private List<FormField> CarregarJsonFormulario()
    {
        var caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "formulario.json");
        var json = System.IO.File.ReadAllText(caminho);
        return JsonConvert.DeserializeObject<List<FormField>>(json);
    }
}
