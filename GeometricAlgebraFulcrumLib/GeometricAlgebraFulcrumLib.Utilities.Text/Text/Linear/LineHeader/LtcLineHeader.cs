namespace GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear.LineHeader;

public abstract class LtcLineHeader
{
    /// <summary>
    /// Reset all data related to this line header
    /// </summary>
    public abstract void Reset();

    /// <summary>
    /// Get current text of this line header
    /// </summary>
    /// <returns></returns>
    public abstract string GetHeaderText();


    public override string ToString()
    {
        return GetHeaderText();
    }
}