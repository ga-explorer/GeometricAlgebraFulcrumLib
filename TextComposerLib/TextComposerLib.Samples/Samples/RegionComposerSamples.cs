using System;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Region;

namespace TextComposerLib.Samples.Samples
{
    public static class RegionComposerSamples
    {
        internal static string Task1()
        {
            var composer = new RcTemplate();
            composer.RegionBeginMarker.MarkerText = @"// begin gmac";
            composer.RegionEndMarker.MarkerText = @"// end gmac";
            composer.FixedTagMarker.MarkerText = @"//$";
            composer.SlotTagMarker.MarkerText = @"//#";
            composer.JoinSlotTagsBeginMarker.MarkerText = @"//#+";

            composer.AddFixedRegion("Fixed Line 1");

            composer.AddFixedRegion("Fixed Line 2" + Environment.NewLine + "Fixed Line 3", "    ");

            var slotRegion = composer.AddSlotRegion("Add[a, b, -3]", "    ");

            slotRegion.AddFixedText("Fixed Line 1");
            slotRegion.AddFixedText("Fixed Line 2");

            slotRegion.AddSlotTag("SlotTag1[]");
            slotRegion.AddSlotTag("for (int i = 0; i < 4; i++)" + Environment.NewLine + "    methodCall();");

            slotRegion.AddFixedText("Fixed Line 3");
            slotRegion.AddFixedText("Fixed Line 4");

            return composer.GetText();
        }

        internal static string Task2()
        {
            var composer = new RcTemplate();
            composer.RegionBeginMarker.MarkerText = @"#region gmac";
            composer.RegionEndMarker.MarkerText = @"#endregion";
            composer.FixedTagMarker.MarkerText = @"//$";
            composer.SlotTagMarker.MarkerText = @"//#";
            composer.JoinSlotTagsBeginMarker.MarkerText = @"//<";
            composer.JoinSlotTagsEndMarker.MarkerText = @"//>";

            composer.ParseTemplate(
@"Fixed Line 1
    Fixed Line 2
    Fixed Line 3

#region gmac Add[a, b, -3]

Fixed Line 1
Fixed Line 2

//# SlotTag1[]
generated text
generated text

//<
//# for (int i = 0; i < 4; i++)
//#     methodCall();
//>
generated text
generated text
//$
Fixed Line 3
Fixed Line 4

#endregion"
                );

            //composer.RegionBeginMarker.MarkerText = @"// begin slot";
            //composer.RegionEndMarker.MarkerText = @"// end slot";
            //composer.FixedTagMarker.MarkerText = @"//.";
            //composer.SlotTagMarker.MarkerText = @"//^";
            //composer.JoinSlotTagsBeginMarker.MarkerText = @"//{";
            //composer.JoinSlotTagsEndMarker.MarkerText = @"//}";

            var linearComposer = new LinearTextComposer();

            linearComposer
                .AppendLine("Template Text:")
                .AppendLine(composer.GetTemplateText())
                .AppendLine()
                .AppendLine("Generated Text:")
                .AppendLine(composer.GetGeneratedText())
                .AppendLine()
                .AppendLine("Full Text:")
                .AppendLine(composer.GetText());

            return linearComposer.ToString();
        }
    }
}
