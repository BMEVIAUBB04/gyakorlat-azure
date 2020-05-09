using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcmeShop.Data
{
    public class Termek
    {
        [Display(Name = "Azonosító")]
        public int Id { get; set; }
        [Display(Name = "Név")]
        public string Nev { get; set; }
        [Display(Name = "Nettó ár (Ft)")]
        public double? NettoAr { get; set; }
        [Display(Name = "Raktárkészlet (db)")]
        public int? Raktarkeszlet { get; set; }
        [Display(Name = "ÁFA-kulcs")]
        public int? AfaId { get; set; }
        [Display(Name = "Kategória")]
        public int? KategoriaId { get; set; }
        [Display(Name = "Leírás (XML)")]
        public string Leiras { get; set; }
        [Display(Name = "Kép")]
        public byte[] Kep { get; set; }

        [Display(Name = "ÁFA-kulcs")]
        public Afa Afa { get; set; }
        [Display(Name = "Kategória")]
        public Kategoria Kategoria { get; set; }
        public ICollection<MegrendelesTetel> MegrendelesTetelek { get; set; }
    }
}
