namespace PAS.Models
{
    public class AnneeAcademique
    {
        public int AnneeAcademiqueId { get; set; }
        public string AnneeScolaire { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public bool Statut { get; set; }

        public ICollection<Classe> Classes { get; set; }
    }
}
