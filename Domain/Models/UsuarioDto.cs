using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models;


public sealed record UsuarioDto
{
    public long Id { get; set; }

    [Required]   
    public string? NomeUsuario { get; set; }

   
    public string? Password { get; set; }

    
    public string? Email { get; set; }

    public string?Telefone { get; set; }

    [Required]
    public Tipo Tipo { get; set; }

    [Required]
    public Perfil Perfil { get; set; }


}
