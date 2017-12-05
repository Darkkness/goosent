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
    public class ChannelsSetsList : IEnumerable<ChannelsSet>
    {
        private List<ChannelsSet> _setsList = new List<ChannelsSet>();
        
        public bool AddSet(ChannelsSet set)
        {
            // false если уже есть сет с таким именем
            foreach (ChannelsSet fSet in _setsList)
            {
                if (set.Name == fSet.Name)
                {
                    return false;
                }
            }
            _setsList.Add(set);

            return true;
        }

        public IEnumerator<ChannelsSet> GetEnumerator()
        {
            return _setsList.GetEnumerator();
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<ChannelsSet> SetsList
        {
            get { return _setsList; }
        }
    }
}