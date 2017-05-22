using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web2ThumbnailBot.Helpers;

namespace Web2ThumbnailBot.Dialogs
{
    [Serializable]
    public class Thumbnails2 : IDialog<object>
    {

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

            Activity replyActivity = activity.CreateReply($"Procesing: {activity.Text}");
            await context.PostAsync(replyActivity);


            string imgUrl = GetThumbnail(activity.Text);
            replyActivity = CreateResponseCard(activity, imgUrl);
            await context.PostAsync(replyActivity);
        }

        public static string GetThumbnail(string url)
        {
            string apiUrl = UrlUtilities.CStrApiParms + url;
            var rc = new RestClient(UrlUtilities.CStrThumbApi);
            var rq = new RestRequest(apiUrl, Method.GET);
            rc.Execute(rq);
            return UrlUtilities.CStrThumbApi + apiUrl;
        }

        public static Activity CreateResponseCard(Activity msg, string imgUrl)
        {
            Activity reply = msg.CreateReply(imgUrl);
            reply.Recipient = msg.From;
            reply.Type = ActivityTypes.Message;
            reply.Attachments = new List<Attachment>();

            var cardImages = new List<CardImage> { new CardImage(imgUrl) };

            var thumbnailCard = new ThumbnailCard { Subtitle = msg.Text, Images = cardImages };

            reply.Attachments.Add(thumbnailCard.ToAttachment());
            return reply;
        }
    }
}