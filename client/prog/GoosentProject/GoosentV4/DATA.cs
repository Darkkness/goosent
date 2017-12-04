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

namespace Goosent
{
    static class DATA
    {
        public static Dictionary<string, int> platformsImagesResourceIds = new Dictionary<string, int>
        {
            {"twitch", Resource.Drawable.twitch }
        };
        
    }
}