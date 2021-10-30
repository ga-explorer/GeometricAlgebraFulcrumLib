namespace GraphicsComposerLib.GraphViz.Dot
{
    /// <summary>
    /// This is the main interface for representing GraphViz attribute values. All values
    /// are essentially strings in the final GraphViz code but to provide more structure
    /// several classes implementing this interface are used to assign values to the
    /// attributes in the constructed GraphViz AST.
    /// See http://www.graphviz.org/content/attrs for more details
    /// </summary>
    public interface IDotValue
    {
        /// <summary>
        /// The plane-text value without any quotes
        /// </summary>
        string Value { get; }

        /// <summary>
        /// The text value enclosed in double qoute
        /// </summary>
        string QuotedValue { get; }

        /// <summary>
        /// The text value enclosed between tag delimiters
        /// </summary>
        string TaggedValue { get; }

        /// <summary>
        /// The text value converted into a C# string literal enclosed in double qoute
        /// </summary>
        string LiteralValue { get; }
    }
}
