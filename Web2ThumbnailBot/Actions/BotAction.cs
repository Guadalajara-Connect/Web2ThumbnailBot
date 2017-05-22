using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace Web2ThumbnailBot.Actions
{
    [Serializable]
    public abstract class BotAction : IDialog<object>, IBotAction
    {
        public abstract Task<Activity> CreateResponseCard(Activity activity);

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            // get the activity
            var activity = await result as Activity;
            if (activity == null)
            {
                await context.PostAsync("The message is empty");
                return;
            }

            Activity replyActivity = activity.CreateReply($"Processing: {activity.Text}");
            await context.PostAsync(replyActivity);

            replyActivity = await CreateResponseCard(activity);
            await context.PostAsync(replyActivity);
        }
    }
}