using GUI.Model;

namespace GUI.Message
{
    public class SendDefaultDataMessage
    {
        public Config content;

        public SendDefaultDataMessage(Config c)
        {
            this.content = c;
        }
    }
}
