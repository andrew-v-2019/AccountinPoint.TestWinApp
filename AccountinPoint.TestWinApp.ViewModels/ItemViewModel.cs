
using System.Collections.Generic;
using System.Linq;

namespace AccountinPoint.TestWinApp.ViewModels
{
    public class ItemViewModel
    {
        public string Msgctxt { get; set; }
        public string Name { get; set; }

        public List<KeyValuePair<string, string>> Values { get; set; } = new List<KeyValuePair<string, string>>();

        public List<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();


        public void Push(string msgCtxt, string msgId, string msgString)
        {
            var split = msgCtxt.Split(".");
            var name = split[0];
            var remainMsgCtxt = string.Join(".", split.Skip(1));

            var child = Items.FirstOrDefault(i => i.Name.Equals(name));

            if (child == null)
            {
                child = InitItem(msgCtxt, name);
                Items.Add(child);
            }

            if (!string.IsNullOrWhiteSpace(remainMsgCtxt))
            {
                child.Push(remainMsgCtxt, msgId, msgString);
            }
            else
            {
                var keyValPair = new KeyValuePair<string, string>(msgId, msgString);
                child.Values.Add(keyValPair);
            }
        }

        private static ItemViewModel InitItem(string msgCtxt, string name)
        {
            return new ItemViewModel()
            {
                Name = name,
                Msgctxt = msgCtxt
            };
        }
    }
}
