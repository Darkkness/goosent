using System;
using System.Collections;
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
    public class ChannelsSet : IEnumerable<Channel>
    {
        private List<Channel> _channels = new List<Channel>();
        private string _name;

        public ChannelsSet(string name)
        {
            _name = name;
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

        public IEnumerator<Channel> GetEnumerator()
        {
            return _channels.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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