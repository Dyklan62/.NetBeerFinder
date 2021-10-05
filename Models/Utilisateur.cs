namespace Models
{
    public class Utilisateur
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string MotDePasse { get; set; }

        public bool IsAdmin { get; set; }
    }
}