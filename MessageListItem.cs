class MessageListItem
{
public string Text { get; set; }
public string Sender { get; set; }
public MessageListItem(string text, string sender)
{
Text = text;
Sender = sender;
}
}
public async Task GetMessagesAsync(ObservableCollection<MessageListItem> collection)
{
string watermark = null;
//Loop retrieval
while(true)
{Debug.WriteLine("Reading message every 3 seconds");
//Get activities (messages) after the watermark
var activitySet = await Client.Conversations.GetActivitiesAsync(MainConversation.ConversationId,
watermark);
//Set new watermark
watermark = activitySet?.Watermark;
//Loop through the activities and add them to the list
foreach(Activity activity in activitySet.Activities)
{
if (activity.From.Name == "MarsBot")
{
collection.Add(new MessageListItem(activity.Text, activity.From.Name));
}
} /
/Wait 3 seconds
await Task.Delay(3000);
}
}
