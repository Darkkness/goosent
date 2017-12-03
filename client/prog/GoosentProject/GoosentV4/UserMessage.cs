using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace Goosent
{
    public class UserMessage
    {
        public string userName;
        public string userMessageText;
        public TimeSpan messageTime;
        public Color userColor;

        public UserMessage(string userName, string userMessageText, TimeSpan messageTime, Color userColor)
        {
            this.userName = userName;
            this.userMessageText = userMessageText;
            this.messageTime = messageTime;
            this.userColor = userColor;
        }
    }
}