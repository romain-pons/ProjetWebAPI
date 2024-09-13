namespace ProjetWebAPI.Models
{
    public class Etudiants
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Age { get; set; }
    }

    public class EtudiantsUpdate
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Age { get; set; }
    }
}
