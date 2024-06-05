using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Code.Languages;
using GeometricAlgebraFulcrumLib.Utilities.Text.Files;
using GeometricAlgebraFulcrumLib.Utilities.Text.Loggers.Progress;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Parametric;

namespace GeometricAlgebraFulcrumLib.Utilities.Code;

/// <summary>
/// A base class for structured text code generation process into multiple source code files
/// </summary>
public abstract class CclCodeFileComposerBase : 
    ICclCodeComposer
{
    /// <summary>
    /// The name of this code library composer
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// The description of this code library composer
    /// </summary>
    public abstract string Description { get; }

    /// <summary>
    /// The name of this composer used during reporting progress
    /// </summary>
    public abstract string ProgressSourceId { get; }

    /// <summary>
    /// The progress log object
    /// </summary>
    public abstract ProgressComposer Progress { get; }

    /// <summary>
    /// The language object providing code composition and generation services
    /// </summary>
    public abstract CclLanguageServerBase Language { get; }

    /// <summary>
    /// The target language code generator of this library composer
    /// </summary>
    public CclLanguageCodeGeneratorBase CodeComposer 
        => Language.CodeGenerator;

    /// <summary>
    /// The language syntax elements factory for this code library composer
    /// </summary>
    public CclLanguageSyntaxFactory SyntaxFactory 
        => Language.SyntaxFactory;

    /// <summary>
    /// The Files Composer object where the final text is written during code generation
    /// </summary>
    public TextFileComposer ActiveFileComposer { get; }

    /// <summary>
    /// A collection of parametric template composers to be used during code generation
    /// </summary>
    public ParametricTextComposerCollection Templates { get; }

    /// <summary>
    /// The active file's linear text composer object
    /// </summary>
    public LinearTextComposer ActiveFileTextComposer 
        => ActiveFileComposer.TextComposer;

    /// <summary>
    /// This property is set to true internally after initializing the parametric templates collection
    /// </summary>
    protected bool TemplatesReady { get; private set; }


    protected CclCodeFileComposerBase(string filePath)
    {
        ActiveFileComposer = new TextFileComposer(filePath);

        Templates = new ParametricTextComposerCollection();

        TemplatesReady = false;
    }


    /// <summary>
    /// Initialize parametric text templates used for code generation. This method is called every time
    /// the Initialize() method is called and TemplatesReady = false. The new value for TemplatesReady is
    /// set in the Initialize() method using the returned value from InitializeTemplates()
    /// </summary>
    /// <returns></returns>
    protected abstract bool InitializeTemplates();

    /// <summary>
    /// Initializes any other components of a generator sub-class inherited from this one.
    /// This method is called automatically by the Initialize() method
    /// </summary>
    protected abstract void InitializeSubComponents();

    /// <summary>
    /// This method verifies that the code generator is ready to start code generation process.
    /// This method is called before initializing the generator to make sure all relevant inputs \ parameters
    /// are ready.
    /// </summary>
    /// <returns></returns>
    protected abstract bool VerifyReadyToGenerate();

    /// <summary>
    /// Perform the actual text generation process after a call to InitializeGenerator() is done. 
    /// A call to FinalizeGenerator() must follow the execution of this method.
    /// </summary>
    protected abstract void ComposeTextFile();

    /// <summary>
    /// Finalizes any other components of a generator sub-class inherited from this one.
    /// This method is called automatically by the Finalize() method
    /// </summary>
    protected abstract void FinalizeSubComponents();


    /// <summary>
    /// Initializes the text file generation process. This method must be called before 
    /// any generation process
    /// </summary>
    protected void InitializeGenerator()
    {
        //Call initialize templates if needed
        if (TemplatesReady == false)
            TemplatesReady = InitializeTemplates();

        //For each template, clear all parameters bindings
        foreach (var template in Templates.Values)
            template.ClearBindings();

        //Clear the contents of the files composer
        ActiveFileComposer.Clear();

        //Initialize any other components of a generator sub-class inherited from this one
        InitializeSubComponents();
    }

    /// <summary>
    /// Finalizes the text file generation process. This method must be called after 
    /// any generation process
    /// </summary>
    protected void FinalizeGenerator()
    {
        //Finalize any other components of a generator sub-class inherited from this one
        FinalizeSubComponents();
    }

    /// <summary>
    /// Performs text files generation based on the given AST information. 
    /// This method calls InitializeGenerator(), ComposeTextFiles(), then FinalizeGenerator().
    /// </summary>
    public void Generate()
    {
        Generate(ComposeTextFile);
    }

    /// <summary>
    /// Performs text files generation based on the given AST information. 
    /// This method calls InitializeGenerator(), composeTextFilesAction(), then FinalizeGenerator().
    /// </summary>
    /// <param name="composeTextFilesAction"></param>
    public void Generate(Action composeTextFilesAction)
    {
        //if (this.SetProgressRunning() == false) return;

        if (VerifyReadyToGenerate() == false)
        {
            this.SetProgressNotRunning();
                
            return;
        }

        InitializeGenerator();

        try
        {
            composeTextFilesAction();
        }
        catch (OperationCanceledException e)
        {
            this.ReportError(e);
        }
        finally
        {
            if (!ActiveFileComposer.IsFinalized)
                ActiveFileComposer.FinalizeText();

            FinalizeGenerator();

            this.SetProgressNotRunning();
        }
    }

    public override string ToString()
    {
        var s = new StringBuilder();

        s.Append(CodeComposer.LanguageInfo.LanguageSymbol);
        s.Append(" ");
        s.Append(Name);

        return s.ToString();
    }
}