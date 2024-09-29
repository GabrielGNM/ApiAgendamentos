using System.Text.Json.Serialization;

namespace Domain.Models.Controller.Requests
{
    public sealed record AgendamentoDto
    {

        [JsonPropertyName("nomePaciente")]
        public string? NomePaciente { get; set; }

        [JsonPropertyName("dataAtendimento")]
        public string? DataAtendimento { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("emailMedicoResponsavel")]
        public string? EmailMedicoResponsavel { get; set; }
    }
}
