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
    public class ChannelsSet
    {
        private List<Channel> _channels;
        private string _name;

        public ChannelsSet(string name)
        {
            _name = name;
            _channels = new List<Channel>();
        }

        public bool AddChannel(Channel channel)
        {
            try
            {
                _channels.Add(channel);

                return true;
            }
            catch (Exception)
            {
                throw new Exception("Не удалось добавить чат в набор");

                return false;
            }
        }

        public bool RemoveChannel(Channel channel)
        {
            try
            {
                _channels.Remove(channel);

                return true;
            } catch (Exception)
            {
                throw new Exception("Не удалось удалить чат из набора");

                return false;
            }
        }

        public List<Channel> Channels
        {
            get { return _channels; }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}