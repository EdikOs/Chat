using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat.Models
{
    public class TextMessage
    {
        public int TextMessageId { get; set; }
        public string Text { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }
    }
}
