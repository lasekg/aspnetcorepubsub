namespace Pubsub.ViewModels
{
    public class MessageList
    {
        public int CronCallCounter { get; set; }
        public int CronMessageCounter { get; set; }


        public bool MissingProjectId { get; set; } = false;
    }
}
