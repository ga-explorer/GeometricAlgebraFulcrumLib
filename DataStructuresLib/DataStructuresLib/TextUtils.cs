using System.Collections.Generic;
using System.Text;

namespace DataStructuresLib
{
    public static class TextUtils
    {
        public static string ConcatenateText(this IEnumerable<string> items)
        {
            var s = new StringBuilder();

            foreach (var item in items)
                s.Append(item);

            return s.ToString();
        }

        public static string ConcatenateText(this IEnumerable<string> items, string separator)
        {
            var s = new StringBuilder();

            var itemSeparator = separator ?? string.Empty;

            var flag = false;
            foreach (var item in items)
            {
                if (flag)
                    s.Append(itemSeparator);
                else
                    flag = true;

                s.Append(item);
            }

            return s.ToString();
        }

        public static string ConcatenateText(this IEnumerable<string> items, string separator, string finalPrefix, string finalSuffix)
        {
            var s = new StringBuilder();

            if (string.IsNullOrEmpty(finalPrefix) == false)
                s.Append(finalPrefix);

            var itemSeparator = separator ?? string.Empty;

            var flag = false;
            foreach (var item in items)
            {
                if (flag)
                    s.Append(itemSeparator);
                else
                    flag = true;

                s.Append(item);
            }

            if (string.IsNullOrEmpty(finalSuffix) == false)
                s.Append(finalSuffix);

            return s.ToString();
        }

        public static string ConcatenateText(this IEnumerable<string> items, string separator, string finalPrefix, string finalSuffix, string itemPrefix, string itemSuffix)
        {
            var s = new StringBuilder();

            if (string.IsNullOrEmpty(finalPrefix) == false)
                s.Append(finalPrefix);

            var itemSeparator = separator ?? string.Empty;

            var flag = false;
            foreach (var item in items)
            {
                if (flag)
                    s.Append(itemSeparator);
                else
                    flag = true;

                s.Append(itemPrefix).Append(item).Append(itemSuffix);
            }

            if (string.IsNullOrEmpty(finalSuffix) == false)
                s.Append(finalSuffix);

            return s.ToString();
        }
    }
}