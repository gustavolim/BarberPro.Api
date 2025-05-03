using BarberPro.Domain.Interfaces;
using LinqToDB.Mapping;

namespace BarberPro.Domain.Entities
{
    [Table(Name = "Usuario")]
    public class Usuario : IEntidade
    {
        [PrimaryKey]
        [Column(Name = "Id")]
        public Guid Id { get; set; }

        [Column(Name = "Nome")]
        public string Nome { get; set; }

        [Column(Name = "Email")]
        public string Email { get; set; }

        [Column(Name = "EhBarbeiro")]
        public bool EhBarbeiro { get; set; }

        [Column(Name = "SenhaHash")]
        public string SenhaHash { get; set; }

        [Association(ThisKey = "Id", OtherKey = "ClienteId", CanBeNull = true)]
        [NotColumn] // não é mapeado diretamente na tabela
        public List<Agendamento> Agendamentos { get; set; } = new();
    }
}
