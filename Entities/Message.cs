using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SendingMessageToUsers.Data
{
    public class Message
    {
        public Message(string senderUser,string sendMessage)
        {
            SenderUser = senderUser;
            SendMessage = sendMessage;
        }

        public int MessageId { get; set; }
        public string SenderUser { get; set; }
        public string SendMessage { get; set; }
    }
}
