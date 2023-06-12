namespace PAS.Models
{
    public class Professeur
    {
        public int ProfesseurId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Sexe { get; set; }
        public string Adresse { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ICollection<Cour> Cours { get; set; }
        public ICollection<Heure> Heures { get; set; }
        public ICollection<Jour> Jours { get; set; }
    }
}
