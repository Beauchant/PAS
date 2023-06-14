namespace PAS.Models
{
    public class Etudiant
    {
        public int EtudiantId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Sexe { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Adresse { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Occupation { get; set; }
        public string? StatutMatrimonial { get; set; }
        public string Maladie { get; set; }
        //public ICollection<Classe> Classes { get; set; }
        public ICollection<Cour> Cours { get; set; }
        public ICollection<Classe> Classes { get; set; }
        public ICollection<CourEtudiant> CourEtudiants { get; set; }
    }
}
