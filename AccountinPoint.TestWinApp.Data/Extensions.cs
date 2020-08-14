using AccountinPoint.TestWinApp.ViewModels;

namespace AccountinPoint.TestWinApp.Data
{
    internal static class Extensions
    {
        public static bool IsMessageCtxt(this string s)
        {
            return s.StartsWith(nameof(ItemViewModel.Msgctxt).ToLower());
        }

        public static string ReadFiled(this string s, string name)
        {
            return s.Replace(name, "").Replace("\"", "").Trim();
        }
    }
}
