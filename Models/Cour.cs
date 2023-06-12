namespace PAS.Models
{
    public class Cour
    {
        public int CourId { get; set; }
        public string NomCours { get; set; }
        public string? Description { get; set; }

        public int ClasseId { get; set; }
        public Classe Classe { get; set; }

        public ICollection<Professeur> Professeurs { get; set; }
    }
}
