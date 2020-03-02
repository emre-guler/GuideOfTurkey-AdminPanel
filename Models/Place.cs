using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuideOfTurkey_AdminPanel.Models
{
    public class Place 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int DistrictID { get; set; }
        public int TypeID { get; set; }
        public string Name { get; set; }
        public string Explain { get; set; }
        public float Rating  { get; set; }
        public string photoUrl { get; set; }
        public bool deleteState { get; set; }
    }
}