namespace PAS.Models
{
    public class Classe
    {
        public int ClasseId { get; set; }
        public string Niveau { get; set; }
        public string? Section { get; set; }

        public int AnneeAcademiqueId { get; set; }
        public AnneeAcademique AnneeAcademique { get; set; }

        public ICollection<Cour> Cours { get; set; }
    }
}
