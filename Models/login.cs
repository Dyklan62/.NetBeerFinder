using System.Text.Json.Serialization;

namespace Models
{
    public class Login
    {
        public string MotDePasse { get; set; }
        public string Email { get; set; }
    }
}