using System.Collections.Generic;

namespace GUI.Model
{
    public class Settings
    {
        public string ApiKey { get; set; }
        public int SessionId { get; set; }
        public List<User> Users { get; set; }
    }
}
