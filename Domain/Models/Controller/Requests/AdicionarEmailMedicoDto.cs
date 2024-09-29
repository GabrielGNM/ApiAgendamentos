namespace Domain.Models.Controller.Requests
{
    public sealed record AdicionarEmailMedicoDto
    {
        public required string Email { get; set; }

        public long Id { get; set; }
    }
    
}
