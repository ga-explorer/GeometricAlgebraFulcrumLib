namespace GeometricAlgebraFulcrumLib.Utilities.Code.Languages;

/// <summary>
/// This class contains information about a specific target language
/// </summary>
public class CclLanguageInfo
{
    /// <summary>
    /// The name of the target language
    /// </summary>
    public string LanguageName { get; protected set; }

    /// <summary>
    /// The version of the target language
    /// </summary>
    public string LanguageVersion { get; protected set; }

    /// <summary>
    /// A short name for the target language
    /// </summary>
    public string LanguageSymbol { get; protected set; }


    public CclLanguageInfo(string name, string version, string symbol)
    {
        LanguageName = name;
        LanguageVersion = version;
        LanguageSymbol = symbol;
    }
}