using System;
using TextComposerLib.Text.Columns;

namespace TextComposerLib.Samples.Samples
{
    public static class TextColumnsComposerSamples
    {
        internal static string Task1()
        {
            var composer = new TextColumnsComposer(3)
            {
                ColumnSeparator = ": "
            };

            composer[0, 0] = "Customers";
            composer[0, 1] = "Ahmad H. Eid" + Environment.NewLine + "Nagwa Monshed Mansour";
            composer[0, 2] = "40" + Environment.NewLine + "36";

            composer[1, 0] = "Servers" + Environment.NewLine + "in" + Environment.NewLine + "Use";
            composer[1, 1] = "128.0.0.0";

            composer[2, 0] = "Manual";
            //composer[2, 1] = "Yes";
            composer[2, 2] = "No";

            composer.DefaultColumnAlignment = TextColumnAlignment.Right;
            composer.SetColumnAlignment(TextColumnAlignment.Left, 0);

            composer.DefaultRowAlignment = TextRowAlignment.Bottom;
            composer.SetRowAlignment(TextRowAlignment.Top, 1);

            return composer.GenerateText();
        }

    }
}
