namespace TextComposerLib.Text.Region
{
    /// <summary>
    /// The main interface for both fixed and slot text regions
    /// </summary>
    public interface IRcRegion : IRcTemplatePart
    {
        /// <summary>
        /// True if the text region is fixed
        /// </summary>
        bool IsFixed { get; }

        /// <summary>
        /// True if the text region is a slot
        /// </summary>
        bool IsSlot { get; }
    }
}