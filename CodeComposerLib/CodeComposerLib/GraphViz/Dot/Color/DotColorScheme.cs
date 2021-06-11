namespace CodeComposerLib.GraphViz.Dot.Color
{
    /// <summary>
    /// This class represents a color scheme
    /// See http://www.graphviz.org/content/color-names for more details
    /// </summary>
    public sealed class DotColorScheme : DotStoredValue
    {
        public static readonly DotColorScheme X11 = new DotColorScheme("X11", 0);

        public static readonly DotIndexedColorSchemeTemplate Accent = new DotIndexedColorSchemeTemplate("accent", 8);
        public static readonly DotIndexedColorSchemeTemplate Blues = new DotIndexedColorSchemeTemplate("blues", 9);
        public static readonly DotIndexedColorSchemeTemplate Brbg = new DotIndexedColorSchemeTemplate("brbg", 12);
        public static readonly DotIndexedColorSchemeTemplate Bugn = new DotIndexedColorSchemeTemplate("bugn", 9);
        public static readonly DotIndexedColorSchemeTemplate Bupu = new DotIndexedColorSchemeTemplate("bupu", 9);
        public static readonly DotIndexedColorSchemeTemplate Dark2 = new DotIndexedColorSchemeTemplate("dark2", 8);
        public static readonly DotIndexedColorSchemeTemplate Gnbu = new DotIndexedColorSchemeTemplate("gnbu", 9);
        public static readonly DotIndexedColorSchemeTemplate Greens = new DotIndexedColorSchemeTemplate("greens", 9);
        public static readonly DotIndexedColorSchemeTemplate Greys = new DotIndexedColorSchemeTemplate("greys", 9);
        public static readonly DotIndexedColorSchemeTemplate Oranges = new DotIndexedColorSchemeTemplate("oranges", 9);
        public static readonly DotIndexedColorSchemeTemplate Orrd = new DotIndexedColorSchemeTemplate("orrd", 9);
        public static readonly DotIndexedColorSchemeTemplate Paired = new DotIndexedColorSchemeTemplate("paired", 12);
        public static readonly DotIndexedColorSchemeTemplate Pastel1 = new DotIndexedColorSchemeTemplate("pastel1", 9);
        public static readonly DotIndexedColorSchemeTemplate Pastel2 = new DotIndexedColorSchemeTemplate("pastel2", 8);
        public static readonly DotIndexedColorSchemeTemplate Piyg = new DotIndexedColorSchemeTemplate("piyg", 11);
        public static readonly DotIndexedColorSchemeTemplate Prgn = new DotIndexedColorSchemeTemplate("prgn", 11);
        public static readonly DotIndexedColorSchemeTemplate Pubu = new DotIndexedColorSchemeTemplate("pubu", 9);
        public static readonly DotIndexedColorSchemeTemplate Pubugn = new DotIndexedColorSchemeTemplate("pubugn", 9);
        public static readonly DotIndexedColorSchemeTemplate Puor = new DotIndexedColorSchemeTemplate("puor", 11);
        public static readonly DotIndexedColorSchemeTemplate Purd = new DotIndexedColorSchemeTemplate("purd", 9);
        public static readonly DotIndexedColorSchemeTemplate Purples = new DotIndexedColorSchemeTemplate("purples", 9);
        public static readonly DotIndexedColorSchemeTemplate Rdbu = new DotIndexedColorSchemeTemplate("rdbu", 11);
        public static readonly DotIndexedColorSchemeTemplate Rdgy = new DotIndexedColorSchemeTemplate("rdgy", 11);
        public static readonly DotIndexedColorSchemeTemplate Rdpu = new DotIndexedColorSchemeTemplate("rdpu", 9);
        public static readonly DotIndexedColorSchemeTemplate Rdylbu = new DotIndexedColorSchemeTemplate("rdylbu", 11);
        public static readonly DotIndexedColorSchemeTemplate Rdylgn = new DotIndexedColorSchemeTemplate("rdylgn", 11);
        public static readonly DotIndexedColorSchemeTemplate Reds = new DotIndexedColorSchemeTemplate("reds", 9);
        public static readonly DotIndexedColorSchemeTemplate Set1 = new DotIndexedColorSchemeTemplate("set1", 9);
        public static readonly DotIndexedColorSchemeTemplate Set2 = new DotIndexedColorSchemeTemplate("set2", 8);
        public static readonly DotIndexedColorSchemeTemplate Set3 = new DotIndexedColorSchemeTemplate("set3", 12);
        public static readonly DotIndexedColorSchemeTemplate Spectral = new DotIndexedColorSchemeTemplate("spectral", 11);
        public static readonly DotIndexedColorSchemeTemplate Ylgn = new DotIndexedColorSchemeTemplate("ylgn", 9);
        public static readonly DotIndexedColorSchemeTemplate Ylgnbu = new DotIndexedColorSchemeTemplate("ylgnbu", 9);
        public static readonly DotIndexedColorSchemeTemplate Ylorbr = new DotIndexedColorSchemeTemplate("ylorbr", 9);
        public static readonly DotIndexedColorSchemeTemplate Ylorrd = new DotIndexedColorSchemeTemplate("ylorrd", 9);

        /// <summary>
        /// The maximum number of colors in this scheme
        /// </summary>
        public int MaxColors { get; }

        /// <summary>
        /// True if this is an indexed color scheme
        /// </summary>
        public bool IsIndexed => MaxColors > 0;

        /// <summary>
        /// Retrieve an indixed color from this scheme
        /// </summary>
        /// <param name="colorIndex"></param>
        /// <returns></returns>
        public DotIndexedColor this[int colorIndex] => new DotIndexedColor(this, colorIndex % MaxColors);


        internal DotColorScheme(string name, int maxColors)
            : base(name)
        {
            MaxColors = maxColors;
        }
    }
}
