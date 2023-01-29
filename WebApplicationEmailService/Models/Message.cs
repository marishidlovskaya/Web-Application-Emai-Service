using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationEmailService.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public DateTime DateMessage { get; set; }

        [MaxLength(50)]
        public string? TitleMessage { get; set; }
        [MaxLength(400)]
        public string? TextMessage { get; set; }

        public  User Receiver { get; set; }
        public  User Sender { get; set; }

    }
}
