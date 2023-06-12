namespace PAS.Models
{
    public class Jour
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public int ProfesseurId { get; set; }
        public Professeur Professeur { get; set; }
    }
}
