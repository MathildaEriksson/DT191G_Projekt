using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EquiMarketApp.Models;

public class Message
{
    public int MessageId { get; set; }

    [Required]
    public int ConversationId { get; set; }
    public  Conversation Conversation { get; set; }

    [Required]
    public string SenderId { get; set; }
    public  ApplicationUser Sender { get; set; }

    [Required]
    [StringLength(1000)] 
    public string MessageText { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
}
