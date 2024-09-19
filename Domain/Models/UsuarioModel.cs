using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models;

[Table("Usuarios")]
public sealed record UsuarioModel
{

    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("nomeUsuario")]
    public string? NomePaciente { get; set; }

    [JsonPropertyName("dataAtendimento")]
    public DateTime DataAtendimento { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("emailMedicoResponsavel")]
    public string? EmailMedicoResponsavel { get; set; }

}
