namespace TextComposerLib.Text.Mapped
{
    /// <summary>
    /// The marking method for a marked mapping composer segment
    /// </summary>
    public enum SegmentMarkingMethod
    {
        /// <summary>
        /// A marked segment is marked using a pair of delimiters, for example [|any text|]
        /// </summary>
        Delimited, 

        /// <summary>
        /// A merked segment is marked using a left delimiter followed by an identifier name 
        /// for example #var_123
        /// </summary>
        Identified
    }
}