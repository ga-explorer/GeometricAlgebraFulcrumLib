namespace TextComposerLib.Text.Region;

/// <summary>
/// The main interface for both fixed and slot text tags inside slot text regions
/// </summary>
public interface IRcTag : IRcTemplatePart
{
    /// <summary>
    /// True if the tag is fixed
    /// </summary>
    bool IsFixed { get; }

    /// <summary>
    /// True if the tag is a slot
    /// </summary>
    bool IsSlot { get; }
}