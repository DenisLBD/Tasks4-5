using System.ComponentModel.DataAnnotations;
using System;

namespace Users.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        [Required]
        [Display(Name = "Recipient")]
        public string RecipientName { get; set; }

        [Required]
        [Display(Name = "Topic")]
        public string Topic { get; set; }

        [Required]
        [Display(Name = "Message:")]
        public string Message { get; set; }
        public DateTime SendTime { get; set; }
    }
}
