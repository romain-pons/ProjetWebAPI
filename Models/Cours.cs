namespace ProjetWebAPI.Models
{
    public class Cours
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public int ProfId { get; set; }
        public Prof Prof { get; set; }
    }
}
