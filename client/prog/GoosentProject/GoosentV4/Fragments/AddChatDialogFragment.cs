using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Goosent.Fragments
{
    public class AddChatDialogFragment : DialogFragment
    {
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(Activity);
            builder.SetTitle("Добавить чат");
            LayoutInflater inflater = Activity.LayoutInflater;

            builder.SetView(inflater.Inflate(Resource.Layout.AddChatDialogLayout, null));


            return builder.Create();
        }
    }
}