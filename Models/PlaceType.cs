using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuideOfTurkey_AdminPanel.Models
{
    public class PlaceType 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string TypeName { get; set; }
        public string photoUrl { get; set; }
        public bool homepageState { get; set; }
        public bool deleteState { get; set; }
    }
}