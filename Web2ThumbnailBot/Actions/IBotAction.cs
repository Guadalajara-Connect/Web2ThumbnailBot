using Microsoft.Bot.Connector;
using System.Threading.Tasks;

namespace Web2ThumbnailBot.Actions
{
    public interface IBotAction
    {
        Task<Activity> CreateResponseCard(Activity activity);
    }
}
