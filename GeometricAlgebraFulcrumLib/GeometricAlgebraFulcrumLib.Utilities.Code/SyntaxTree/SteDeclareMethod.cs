namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public class SteDeclareMethod : SteSyntaxElement
{
    public string MethodName { get; set; }

    public string ReturnType { get; set; }

    public List<SteDeclareDataStore> Parameters { get; private set; }

    ///// <summary>
    ///// The kind of the method; for example: function, etc.
    ///// </summary>
    //public string MethodKind { get; set; }

    /// <summary>
    /// The declaration modifiers; for example: local, global, volatile, public, etc.
    /// </summary>
    public List<string> ModifiersList { get; private set; }

    public ISyntaxTreeElement MethodCode { get; set; }


    public SteDeclareMethod()
    {
        Parameters = new List<SteDeclareDataStore>();
        ModifiersList = new List<string>();
    }
}