using TextComposerLib.Settings;
using TextComposerLib.Text.Linear;

namespace TextComposerLib.Samples.Samples
{
    internal static class SettingsComposerSamples
    {
        internal static string Task1()
        {
            var textComposer = new LinearTextComposer();

            var settings1 = new SettingsComposer();

            settings1["imagesPath"] = @"C:\Media\Photos";
            settings1["maxImages"] = "3";
            settings1["allowRemove"] = "yes";
            settings1["maxImages"] = "5";

            settings1.XmlConverter.StoreItemValueAsXAttribute = false;

            textComposer
                .AppendAtNewLine("Settings as XML document, item values are stored as tag values:")
                .AppendAtNewLine(settings1.ToString())
                .AppendLineAtNewLine();

            settings1.XmlConverter.StoreItemValueAsXAttribute = true;

            textComposer
                .AppendAtNewLine("Settings as XML document, item values are stored as attributes:")
                .AppendAtNewLine(settings1.ToString())
                .AppendLineAtNewLine();

            return textComposer.ToString();
        }

        internal static string Task2()
        {
            var textComposer = new LinearTextComposer();

            var defaultsDict = new SettingsComposer();
            defaultsDict["key1"] = "Default Value1";
            defaultsDict["key2"] = "Default Value2";
            defaultsDict["key3"] = "Default Value3";
            defaultsDict["key4"] = "Default Value4";

            var settingsDict = new SettingsComposer(defaultsDict);
            settingsDict["key3"] = "Value 3";
            settingsDict["key4"] = "Value 4";
            settingsDict["key5"] = "Value 5";
            settingsDict["key6"] = "Value 6";

            //Create a backup of the settings to be used for restoration after each step
            var settingsDictBackup = new SettingsComposer(defaultsDict);
            settingsDictBackup.UpdateFrom(settingsDict);

            textComposer
                .AppendAtNewLine("Parent Settings Contents:")
                .AppendAtNewLine(defaultsDict.ToXDocumentText())
                .AppendLineAtNewLine();

            textComposer
                .AppendAtNewLine("Child Settings Contents:")
                .AppendAtNewLine(settingsDict.ToXDocumentText())
                .AppendLineAtNewLine();

            textComposer
                .AppendAtNewLine("Full Settings Contents:")
                .AppendAtNewLine(settingsDict.ChainToXDocumentText())
                .AppendLineAtNewLine();

            //Clear all settings items
            settingsDict.ClearItems();

            textComposer
                .AppendAtNewLine("Child Settings Contents after ClearItems() is called:")
                .AppendAtNewLine(settingsDict.ToXDocumentText())
                .AppendLineAtNewLine();

            textComposer
                .AppendAtNewLine("Full Settings Contents after ClearItems() is called:")
                .AppendAtNewLine(settingsDict.ChainToXDocumentText())
                .AppendLineAtNewLine();

            //Restore settings from backup dictionary
            settingsDict.UpdateFrom(settingsDictBackup);

            return textComposer.ToString();
        }
    }
}
