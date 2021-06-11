namespace CodeComposerLib.Irony.Semantic.Type
{
    /// <summary>
    /// This interface is the main interface for all language types
    /// A language type can act as an operator (to be used for cast operations for example)
    /// </summary>
    public interface ILanguageType : IIronyAstObject
    {
        /// <summary>
        /// The full signature for the language type
        /// </summary>
        string TypeSignature { get; }

        /// <summary>
        /// True if the given type is the same as this type
        /// </summary>
        /// <param name="languageType"></param>
        /// <returns></returns>
        bool IsSameType(ILanguageType languageType);

        /// <summary>
        /// True if the given type is compatible with this type (i.e. its value can be read into a variable of this type) 
        /// </summary>
        /// <param name="languageType"></param>
        /// <returns></returns>
        bool IsCompatibleType(ILanguageType languageType);
    }
}
