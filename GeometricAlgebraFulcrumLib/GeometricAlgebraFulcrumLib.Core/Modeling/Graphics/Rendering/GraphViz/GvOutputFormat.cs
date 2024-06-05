namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.GraphViz;

/// <summary>
/// Output formats for GraphViz renderings.
/// See http://www.graphviz.org/content/output-formats for more details
/// </summary>
public class GvOutputFormat
{
    //TODO: Complete this list with sub-formats
    //Image formats: These can be rendered into an in-memory Image or to a file
    public static readonly GvImageOutputFormat Bmp = new GvImageOutputFormat("bmp");
    public static readonly GvImageOutputFormat Gif = new GvImageOutputFormat("gif");
    public static readonly GvImageOutputFormat Jpg = new GvImageOutputFormat("jpg");
    public static readonly GvImageOutputFormat Jpeg = new GvImageOutputFormat("jpeg");
    public static readonly GvImageOutputFormat Jpe = new GvImageOutputFormat("jpe");
    public static readonly GvImageOutputFormat Png = new GvImageOutputFormat("png");
    public static readonly GvImageOutputFormat Tif = new GvImageOutputFormat("tif");
    public static readonly GvImageOutputFormat Tiff = new GvImageOutputFormat("tiff");

    //Text formats: These can be rendered into in in-memory string or to a file
    public static readonly GvTextOutputFormat Canon = new GvTextOutputFormat("canon");
    public static readonly GvTextOutputFormat Dot = new GvTextOutputFormat("dot");
    public static readonly GvTextOutputFormat Gv = new GvTextOutputFormat("gv");
    public static readonly GvTextOutputFormat Plain = new GvTextOutputFormat("plain");
    public static readonly GvTextOutputFormat PlainExt = new GvTextOutputFormat("plain-ext");
    public static readonly GvTextOutputFormat XDot = new GvTextOutputFormat("xdot");
    public static readonly GvTextOutputFormat XDot12 = new GvTextOutputFormat("xdot1.2");
    public static readonly GvTextOutputFormat XDot14 = new GvTextOutputFormat("xdot1.4");
        
    //Other formats: These can be rendered only into a file
    public static readonly GvOutputFormat Emf = new GvOutputFormat("emf");
    public static readonly GvOutputFormat EmfPlus = new GvOutputFormat("emfplus");
    public static readonly GvOutputFormat Eps = new GvOutputFormat("eps");
    public static readonly GvOutputFormat Pic = new GvOutputFormat("pic");
    public static readonly GvOutputFormat Pdf = new GvOutputFormat("pdf");
    public static readonly GvOutputFormat Pov = new GvOutputFormat("pov");
    public static readonly GvOutputFormat Ps = new GvOutputFormat("ps");
    public static readonly GvOutputFormat Ps2 = new GvOutputFormat("ps2");
    public static readonly GvOutputFormat CMap = new GvOutputFormat("cmap");
    public static readonly GvOutputFormat CMapX = new GvOutputFormat("cmapx");
    public static readonly GvOutputFormat CMapXNp = new GvOutputFormat("cmapx_np");
    public static readonly GvOutputFormat Svg = new GvOutputFormat("svg");
    public static readonly GvOutputFormat Svgz = new GvOutputFormat("svgz");
    public static readonly GvOutputFormat Vml = new GvOutputFormat("vml");
    public static readonly GvOutputFormat Vmlz = new GvOutputFormat("vmlz");
    public static readonly GvOutputFormat Vrml = new GvOutputFormat("vrml");
    public static readonly GvOutputFormat Fig = new GvOutputFormat("fig");
    public static readonly GvOutputFormat Gd = new GvOutputFormat("gd");
    public static readonly GvOutputFormat Gd2 = new GvOutputFormat("gd2");
    public static readonly GvOutputFormat Imap = new GvOutputFormat("imap");
    public static readonly GvOutputFormat ImapNp = new GvOutputFormat("imap_np");
    public static readonly GvOutputFormat IsMap = new GvOutputFormat("ismap");
    public static readonly GvOutputFormat MetaFile = new GvOutputFormat("metafile");
    public static readonly GvOutputFormat Tk = new GvOutputFormat("tk");
    public static readonly GvOutputFormat WBmp = new GvOutputFormat("wbmp");

    public string Name { get; }


    internal GvOutputFormat(string name)
    {
        Name = name;
    }


    public override string ToString()
    {
        return Name;
    }
}