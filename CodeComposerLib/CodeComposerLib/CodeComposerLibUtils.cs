using System;
using System.IO;
using System.Reflection;
using TextComposerLib.Settings;

namespace CodeComposerLib
{
    public static class CodeComposerLibUtils
    {
        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private static SettingsComposer SettingsDefaults { get; }
            = new SettingsComposer();

        public static SettingsComposer Settings { get; }
            = new SettingsComposer(SettingsDefaults);


        static CodeComposerLibUtils()
        {
            //Set default settings
            SettingsDefaults["graphvizFolder"] = @"C:\Program Files (x86)\Graphviz2.38\bin";

            //Define settings file for TextComposerLib
            Settings.FilePath =
                Path.Combine(
                    Path.GetDirectoryName(AssemblyDirectory) ?? string.Empty,
                    "CodeComposerLib.xml");

            //Try reading settings file
            var result = Settings.UpdateFromFile(false);

            if (string.IsNullOrEmpty(result) == false)
                //Error wilr reading settings file, tru saving a default settings file
                Settings.ChainToFile();
        }
    }
}
