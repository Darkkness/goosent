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
using Android.Graphics;

using Android.Support.V4;
using System.Threading.Tasks;

namespace Goosent
{
    public class FragmentChat : BaseTabFragment
    {

        private List<UserMessage> chatMessages = new List<UserMessage>();
        private Adapters.ChatArrayAdapter chatAdapter;
        private ListView chatListView;
        private int _temp_messageCounter = 0;

        public static FragmentChat getInstance(Context context)
        {
            Bundle args = new Bundle();
            FragmentChat cFragment = new FragmentChat();
            cFragment.Arguments = args;
            cFragment.SetContext(context);
            cFragment.SetTitle(context.GetString(Resource.String.tab_item_chat));

            return cFragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            view = inflater.Inflate(Resource.Layout.FragmentChatLayout, container, false);

            chatListView = (ListView)view.FindViewById(Resource.Id.chat_list_view);

            chatAdapter = new Adapters.ChatArrayAdapter(context, chatMessages);

            chatListView.Adapter = chatAdapter;
            StartConstantChatUpdating();

            return view;
        }

        public void SetContext(Context context)
        {
            this.context = context;
        }

        private async Task StartConstantChatUpdating()
        {
            while (true)
            {            
                Activity.RunOnUiThread(() =>
                {
                    UpdateChatListView();
                });
                
                _temp_messageCounter += 1;
                await Task.Delay(TimeSpan.FromSeconds(0.3));
            }
        }

        private void AutoScrollChatToTheBottom()
        {
            chatListView.SetSelection(chatAdapter.Count - 1);
        }

        private void UpdateChatListView()
        {
            chatMessages.Add(new UserMessage("Ramрекркерzan", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.  " + _temp_messageCounter.ToString(), new TimeSpan(14, 10, 5), Color.Green));
            chatAdapter.NotifyDataSetChanged();

            Console.WriteLine("Vis pos: " + chatListView.LastVisiblePosition.ToString() + " " + (chatAdapter.Count - 1).ToString());
            if (chatListView.LastVisiblePosition == chatAdapter.Count - 2)
            {
                Console.WriteLine("Autoscroll");
                AutoScrollChatToTheBottom();
            }
        }
    }
}