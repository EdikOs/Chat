using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Chat.Models
{
    public class User
    {
        [NotMapped]
        public string ConnectionId { get; set; }
        public string UserId { get; set; }
    }
}
