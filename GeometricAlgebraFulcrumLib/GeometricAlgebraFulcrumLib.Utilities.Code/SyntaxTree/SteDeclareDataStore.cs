using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public class SteDeclareDataStore : SteSyntaxElement
{
    public bool LocalDataStore { get; set; }

    public string DataStoreName { get; set; }

    public string DataStoreType { get; set; }

    public string DataStoreScope { get; set; }

    /// <summary>
    /// The kind of the data store; for example: variable, array, constant, parameter, etc.
    /// </summary>
    public string DataStoreKind { get; set; }

    /// <summary>
    /// The declaration modifiers; for example: local, global, volatile, public, etc.
    /// </summary>
    public List<string> ModifiersList { get; private set; }

    public ISyntaxTreeElement InitialValue { get; set; }


    public SteDeclareDataStore()
    {
        ModifiersList = new List<string>();
    }
}