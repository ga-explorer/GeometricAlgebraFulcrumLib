using System;
using System.IO;
using TextComposerLib.Settings;

namespace EuclideanGeometryLib
{
    public static class EuclideanGeometryUtils
    {
        private static SettingsComposer SettingsDefaults { get; }
            = new SettingsComposer();

        internal static SettingsComposer Settings { get; }
            = new SettingsComposer(SettingsDefaults);


        static EuclideanGeometryUtils()
        {
            //Set default settings
            //SettingsDefaults["graphvizFolder"] = @"C:\Program Files (x86)\Graphviz2.38\bin";

            //Define settings file for TextComposerLib
            Settings.FilePath = 
                Path.Combine(
                    Path.GetDirectoryName(AppContext.BaseDirectory) ?? string.Empty, 
                    "GeometryComposerLibSettings.xml");

            //Try reading settings file
            var result = Settings.UpdateFromFile(false);

            if (string.IsNullOrEmpty(result) == false)
                //Error wilr reading settings file, tru saving a default settings file
                Settings.ChainToFile();
        }
    }
}
