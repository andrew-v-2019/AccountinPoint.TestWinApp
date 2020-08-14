using AccountinPoint.TestWinApp.ViewModels;
using System.Linq;

namespace AccountinPoint.TestWinApp.Data
{
    public class Loader
    {
        public ItemViewModel LoadStrings(string[] strings)
        {
            var filteredData = strings.Where(IsRelevantString).ToArray();

            var root = LoadData(filteredData);
            return root;
        }

        private static ItemViewModel LoadData(string[] strings)
        {
            var i = 0;
            var count = strings.Count();
            var root = new ItemViewModel();
            do
            {
                if (strings[i].IsMessageCtxt())
                {
                    var msgctxt = strings[i].ReadFiled(nameof(ItemViewModel.Msgctxt).ToLower());
                    var msgid = strings[i + 1].ReadFiled("msgid");
                    var msgstr = strings[i + 2].ReadFiled("msgstr");
                    root.Push(msgctxt, msgid, msgstr);
                    i += 2;
                }
                else
                {
                    i += 1;
                }
            } while (i < count);

            return root;
        }

        private static bool IsRelevantString(string s)
        {
            return s.IsMessageCtxt()
                || s.StartsWith("msgid")
                || s.StartsWith("msgstr");
        }
    }
}
