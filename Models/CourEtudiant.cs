namespace PAS.Models
{
    public class CourEtudiant
    {
        public int CourId { get; set; }
        public Cour Cour { get; set; }

        public int EtudiantId { get; set; }
        public Etudiant Etudiant { get; set; }
    }
}
