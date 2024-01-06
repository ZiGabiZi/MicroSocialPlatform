using FinalDAW2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalDAW2.Models
{
    public class Friend
    {
        [Key]
        public int Id { get; set; }

        public string SenderUsername { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public int Status { get; set; }


    }
}
