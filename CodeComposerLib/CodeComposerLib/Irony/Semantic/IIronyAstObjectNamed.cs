using CodeComposerLib.Irony.Semantic.Scope;

namespace CodeComposerLib.Irony.Semantic;

/// <summary>
/// The main interface for any object that can be stored in a parent Irony DSL
/// </summary>
public interface IIronyAstObjectNamed : IIronyAstObject
{
    /// <summary>
    /// The name of the object
    /// </summary>
    string ObjectName { get; }

    /// <summary>
    /// The ID of the object
    /// </summary>
    int ObjectId { get; }

    /// <summary>
    /// The parent scope of the object
    /// </summary>
    LanguageScope ParentScope { get; }

}