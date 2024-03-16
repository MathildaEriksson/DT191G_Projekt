/* Mathilda Eriksson, DT191G, VT24 */
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EquiMarketApp.Models;

public class Conversation
{
    public int ConversationId { get; set; }

    [Required]
    public int AdId { get; set; }
    public Ad Ad { get; set; }

    [Required]
    public string InitiatorUserId { get; set; }
    public ApplicationUser InitiatorUser { get; set; }

    [Required]
    public string ReceiverUserId { get; set; }
    public ApplicationUser ReceiverUser { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public ICollection<Message> Messages { get; set; }
}
