using Microsoft.Bot.Connector;
using System.Threading.Tasks;

namespace Web2ThumbnailBot.Dialogs
{
    public interface ICardDialog
    {
        Task<Activity> CreateResponseCard(Activity activity);
    }
}
