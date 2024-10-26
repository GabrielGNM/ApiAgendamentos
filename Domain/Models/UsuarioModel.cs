using Domain.Models.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models;

[Table("Usuarios")]
public sealed record UsuarioModel
{
    [Key]
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [Required]
    [JsonPropertyName("nomeUsuario")]
    public string? NomeUsuario { get; set; }

    [Required]
    [JsonIgnore]
    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [Required]
    [MaxLength(256)]
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [Required]
    [JsonPropertyName("telefone")]
    public string?Telefone { get; set; }

    [Required]
    [JsonPropertyName("tipo")]
    public Tipo Tipo { get; set; }

    [Required]
    [JsonPropertyName("perfil")]
    public Perfil Perfil { get; set; }

    public ICollection<AgendamentoModel>? AgendamentoModel { get; set; }

}


public enum Tipo
{
    Cliente,
    Profissional
}

public enum Perfil
{
    [Display(Name = "Administrador")]
    Administrador,

    [Display(Name = "Usuario")]
    Usuario,
}

