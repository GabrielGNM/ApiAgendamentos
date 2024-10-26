

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models;


public sealed record AuthenticateDto
{
    [Required]
    public long Email { get; set; }

    [Required]
    public string Password { get; set; }


}
