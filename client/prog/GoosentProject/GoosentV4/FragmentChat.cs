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
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace Goosent
{
    public class FragmentChat : BaseTabFragment
    {

        private List<UserMessage> chatMessages = new List<UserMessage>();
        private Adapters.ChatMessagesArrayAdapter chatAdapter;
        private ListView chatListView;
        private int _temp_messageCounter = 0;

        //Temp
        private List<string> fakeNames = new List<string>();
        private List<string> fakeMessages = new List<string>();
        Random random = new Random();

        public static FragmentChat getInstance(Context context)
        {
            Bundle args = new Bundle();
            FragmentChat cFragment = new FragmentChat();
            cFragment.Arguments = args;
            cFragment.SetContext(context);
            cFragment.SetTitle(context.GetString(Resource.String.tab_item_chat));

            return cFragment;
        }

        private void TempInitFake()
        {
            fakeNames.Add("Ramzan");
            fakeNames.Add("Valera");
            fakeNames.Add("Micheal");
            fakeNames.Add("Denis");
            fakeNames.Add("Max");
            fakeNames.Add("Rose");
            fakeNames.Add("Gleb");
            fakeNames.Add("Artyom");
            fakeMessages.Add("Всем привет!");
            fakeMessages.Add("MLG!");
            fakeMessages.Add("So pro! Such skill! Wow!");
            fakeMessages.Add(")))))))))))))000)000)hrtklhrtklhjmklergmnrklgergjrkgjergkljrglk;ejgrjgkl");
            fakeMessages.Add("KappaPride");
            fakeMessages.Add("SimpleText");
            fakeMessages.Add("Hello everyone!");
            fakeMessages.Add("General Kenobi!");
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            view = inflater.Inflate(Resource.Layout.FragmentChatLayout, container, false);
            TempInitFake();

            chatListView = (ListView)view.FindViewById(Resource.Id.chat_list_view);


            chatAdapter = new Adapters.ChatMessagesArrayAdapter(context, chatMessages);

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
                //string url = "http://artkholl.pythonanywhere.com/get_message?channel=egorka";
                //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                //request.ContentType = "application/json";
                //request.Method = "GET";


                //using (WebResponse response = await request.GetResponseAsync())
                //{
                //    using (Stream stream = response.GetResponseStream())
                //    {
                //        var serializer = new JsonSerializer();

                //        using (var sr = new StreamReader(stream))
                //        {
                //            using (var jsonTextReader = new JsonTextReader(sr))
                //            {
                //                // Получили Json объект
                //                var responseData = serializer.Deserialize<Dictionary<string, string>>(jsonTextReader);

                //            }
                //        }
                //    }
                //}

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
            chatMessages.Add(new UserMessage(fakeNames[random.Next(0, fakeNames.Count-1)], fakeMessages[random.Next(0, fakeMessages.Count - 1)], new TimeSpan(14, 10, 5), String.Format("#{0:X6}", random.Next(0x1000000))));

            
            

           




                chatAdapter.NotifyDataSetChanged();

            if (chatListView.LastVisiblePosition == chatAdapter.Count - 2)
            {
                AutoScrollChatToTheBottom();
            }
        }
    }
}