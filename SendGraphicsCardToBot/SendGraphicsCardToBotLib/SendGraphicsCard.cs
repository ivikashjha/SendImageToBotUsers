//SendGraphicsCard.cs
namespace SendGraphicsCardToBotLib
{
    #region Namespace

    using Microsoft.Bot.Connector;
    using Microsoft.Bot.Connector.Authentication;
    using Microsoft.Bot.Schema;
    using System;
    using System.Collections.Generic;
    using System.IO;

    #endregion

    /// <summary>
    /// SendGraphicsCard
    /// </summary>
    public class SendGraphicsCard
    {
        public string SendMessage()
        {
            string exceptionMessage = "Message Card Sent Successfully";
            List<string> lstConversationId = new List<string>();
            //Vikash
            lstConversationId.Add("a:1ConversationID");
            
            ConnectorClient connector1 = new ConnectorClient(new Uri("https://smba.trafficmanager.net/amer/"),"APPID", "SecretKey");
            
            foreach (var convId in lstConversationId)
            {
                for (int i = 1; i <= 4; i++)
                {
                    var attachments = new List<Attachment>();
                    attachments.Add(GetInlineAttachment());                    

                    var activity = new Activity
                    {
                        Type = ActivityTypes.Message,
                        Recipient = new ChannelAccount("vikash.ranjan.jha@xyz.com"),
                        Conversation = new ConversationAccount(false, "personal", convId),
                        Text = "Test",
                        Attachments = attachments
                    };

                    try
                    {
                        MicrosoftAppCredentials.TrustServiceUrl("https://smba.trafficmanager.net/amer/");
                        ResourceResponse ttt = connector1.Conversations.SendToConversationAsync(convId, activity).Result;
                    }
                    catch (System.Exception ex)
                    {
                        exceptionMessage = ex.Message;
                    }
                }
            }
            return exceptionMessage;
        }

        private static Attachment GetInlineAttachment()
        {
            var imagePath = Path.Combine(Environment.CurrentDirectory, @"Resources\1.png");
            var imageData = Convert.ToBase64String(File.ReadAllBytes(imagePath));

            return new Attachment
            {
                Name = @"Resources\1.png",
                ContentType = "image/png",
                ContentUrl = $"data:image/png;base64,{imageData}",
                Content="Testing"
            };
        }        
    }
}