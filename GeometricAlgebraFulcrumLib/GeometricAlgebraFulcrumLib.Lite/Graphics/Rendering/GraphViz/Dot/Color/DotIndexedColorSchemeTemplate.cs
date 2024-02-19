namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Color;

/// <summary>
/// This class acts as a generator for a color scheme value
/// See http://www.graphviz.org/content/color-names for more details
/// </summary>
public sealed class DotIndexedColorSchemeTemplate : DotStoredValue
{
    private static readonly Dictionary<string, DotColorScheme> SchemeCache =
        new Dictionary<string, DotColorScheme>();


    public int MinColors => 3;

    public int MaxColors { get; }


    public DotColorScheme this[int colorsNum]
    {
        get
        {
            if (colorsNum < MinColors || colorsNum > MaxColors)
                throw new ArgumentOutOfRangeException(nameof(colorsNum));

            var schemeName = Value + colorsNum;

            if (SchemeCache.TryGetValue(schemeName, out var scheme))
                return scheme;

            scheme = new DotColorScheme(schemeName, colorsNum);

            SchemeCache.Add(schemeName, scheme);

            return scheme;
        }
    }


    internal DotIndexedColorSchemeTemplate(string value, int maxColors)
        : base(value)
    {
        MaxColors = maxColors;
    }
}