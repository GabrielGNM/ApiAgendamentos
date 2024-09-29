using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models.Repository;

public sealed record AgendamentoModel
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("nomePaciente")]
    public string? NomePaciente { get; set; }

    [JsonPropertyName("dataAtendimento")]
    public DateTime DataAtendimento { get; set; }

    [JsonPropertyName("email")]
    [EmailAddress]
    public string? Email { get; set; }

    [JsonPropertyName("emailMedicoResponsavel")]
    [EmailAddress]
    public string? EmailMedicoResponsavel { get; set; }

}
