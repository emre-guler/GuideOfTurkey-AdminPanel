using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuideOfTurkey_AdminPanel.Models
{
    public class UserAccount
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string photoUrl { get; set; }
        public bool deleteState { get; set; }
        public bool userRank { get; set; }
        public DateTime createdAt { get; set; }
    }
}