/* Mathilda Eriksson, DT191G, VT24 */
namespace EquiMarketApp.ViewModels;

public class ConversationCreateViewModel
    {
        public int AdId { get; set; }
        public string AdTitle { get; set; }
        public string ReceiverUserId { get; set; }
        public string MessageText { get; set; }
    }