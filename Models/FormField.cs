using System.ComponentModel.DataAnnotations;

namespace FormularioDinamico.Models
{
    public class FormField
    {
        [Display(Name = "Título do Campo")]
        public string Label { get; set; } = string.Empty;     // Texto visível do campo

        [Display(Name = "Tipo do campo")]
        public string Type { get; set; } = string.Empty;      // Tipo do campo (text, number, etc.)
        
        [Display(Name = "Nome interno do campo")]
        public string Name { get; set; } = string.Empty;      // Nome técnico usado no JSON
        public bool Required { get; set; }                    // Se é obrigatório
        public bool Negativos { get; set; } = false;          // Para número: permitir negativos
        public List<string>? Options { get; set; }            // Para select e radio
    }


}
