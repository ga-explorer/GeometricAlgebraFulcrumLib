using GeometricAlgebraFulcrumLib.Utilities.Text.Settings;

namespace GeometricAlgebraFulcrumLib.Utilities.Text;

public static class TextComposerLibUtils
{
    private static SettingsComposer SettingsDefaults { get; }
        = new SettingsComposer();

    internal static SettingsComposer Settings { get; }
        = new SettingsComposer(SettingsDefaults);


    static TextComposerLibUtils()
    {
        //Set default settings
        //SettingsDefaults["graphvizFolder"] = @"C:\Program Files (x86)\Graphviz2.38\bin";

        //Define settings file for GeometricAlgebraFulcrumLib.Utilities.Text
        Settings.FilePath =
            Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                "TextComposerLibSettings.xml");

        //Try reading settings file
        var result = Settings.UpdateFromFile(false);

        if (string.IsNullOrEmpty(result) == false)
            //Error wilr reading settings file, tru saving a default settings file
            Settings.ChainToFile();
    }
}