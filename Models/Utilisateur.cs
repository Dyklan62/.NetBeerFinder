using System.Text.Json.Serialization;

namespace Models
{
    using System.ComponentModel.DataAnnotations;

    public class Utilisateur
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string MotDePasse { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
    }
}