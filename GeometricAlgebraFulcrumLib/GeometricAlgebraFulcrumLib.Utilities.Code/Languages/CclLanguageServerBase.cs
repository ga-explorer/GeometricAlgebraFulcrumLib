namespace GeometricAlgebraFulcrumLib.Utilities.Code.Languages;

public abstract class CclLanguageServerBase
{
    public abstract string DefaultFileExtension { get; }

    /// <summary>
    /// The code generator for this language
    /// </summary>
    public CclLanguageCodeGeneratorBase CodeGenerator { get; }

    /// <summary>
    /// The syntax factory for this language
    /// </summary>
    public CclLanguageSyntaxFactory SyntaxFactory { get; private set; }

    /// <summary>
    /// The main information of the target language
    /// </summary>
    public CclLanguageInfo LanguageInfo 
        => CodeGenerator.LanguageInfo;

    /// <summary>
    /// The scalar (i.e. real) type name of this language
    /// </summary>
    public string ScalarTypeName
    {
        get => CodeGenerator.ScalarTypeName;
        set => CodeGenerator.ScalarTypeName = value;
    }

    /// <summary>
    /// The value of the zero scalar
    /// </summary>
    public string ScalarZero
    {
        get => CodeGenerator.ScalarZero;
        set => CodeGenerator.ScalarZero = value;
    }


    protected CclLanguageServerBase(CclLanguageCodeGeneratorBase codeComposer, CclLanguageSyntaxFactory syntaxFactory)
    {
        CodeGenerator = codeComposer;
        SyntaxFactory = syntaxFactory;
    }
}