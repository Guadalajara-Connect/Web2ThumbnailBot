using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Web2ThumbnailBot.Dialogs;

namespace Web2ThumbnailBot.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                // ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                await Conversation.SendAsync(activity, () => new Thumbnails2());
                // await ProcessResponse(connector, activity);
            }
            else
            {
                HandleSystemMessage(activity);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //private async Task ProcessResponse(ConnectorClient connector, Activity input)
        //{
        //    if (IsValidUri(input.Text, out string exMsg))
        //    {
        //        await Thumbnails.ProcessScreenshot(connector, input);
        //    }
        //    else
        //    {
        //        var reply = input.CreateReply("Hi, what is the URL you want a thumbnail for?");
        //        await connector.Conversations.ReplyToActivityAsync(reply);
        //    }
        //}

        

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}