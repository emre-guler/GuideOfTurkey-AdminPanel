using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuideOfTurkey_AdminPanel.Models
{
    public class City 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int CountryID { get; set; }
        public string Name { get; set; }
        public string Explain { get; set; }
        public string photoUrl { get; set; }
        public bool homepageState { get; set; }
        public bool deleteState { get; set; }
    }
}