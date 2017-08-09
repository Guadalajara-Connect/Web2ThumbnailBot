using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web2ThumbnailBot.Helpers;

namespace Web2ThumbnailBot.Dialogs
{
    [Serializable]
    public class ThumbnailBotDialog : BotBaseDialog
    {
        public override Task<Activity> CreateResponseCard(Activity activity)
        {
            return Task.FromResult(GetResponseCard(activity));
        }

        private static string GetServiceUrl(string url)
        {
            return UrlUtilities.CStrThumbApi + UrlUtilities.CStrApiParms + url;
        }

        private static Activity GetResponseCard(Activity msg)
        {
            string thumbnailServiceUrl = GetServiceUrl(msg.Text);

            Activity reply = msg.CreateReply($"Thumbnail link: {thumbnailServiceUrl}");
            reply.Recipient = msg.From;
            reply.Type = ActivityTypes.Message;
            reply.Attachments = new List<Attachment>();

            var thumbnailCard = new ThumbnailCard
            {
                Subtitle = msg.Text,
                Images = new List<CardImage> {new CardImage(thumbnailServiceUrl, msg.Text)}
            };

            reply.Attachments.Add(thumbnailCard.ToAttachment());
            return reply;
        }
    }
}