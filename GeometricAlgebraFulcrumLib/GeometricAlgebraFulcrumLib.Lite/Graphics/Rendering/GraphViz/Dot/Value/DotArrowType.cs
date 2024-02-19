using System.Text;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Value;

/// <summary>
/// This class represents an arrow head type value and contains an enumeration
/// of GraphViz arrow types.
/// See http://www.graphviz.org/content/arrow-shapes for more details
/// </summary>
public sealed class DotArrowType : DotStoredValue
{
    public static readonly DotArrowType Box = new DotArrowType("box");
    public static readonly DotArrowType LeftBox = new DotArrowType("lbox");
    public static readonly DotArrowType RightBox = new DotArrowType("rbox");
    public static readonly DotArrowType OpenBox = new DotArrowType("obox");
    public static readonly DotArrowType OpenLeftBox = new DotArrowType("olbox");
    public static readonly DotArrowType OpenRightBox = new DotArrowType("orbox");
    public static readonly DotArrowType Crow = new DotArrowType("crow");
    public static readonly DotArrowType LeftCrow = new DotArrowType("lcrow");
    public static readonly DotArrowType RightCrow = new DotArrowType("rcrow");
    public static readonly DotArrowType Diamond = new DotArrowType("diamond");
    public static readonly DotArrowType LeftDiamond = new DotArrowType("ldiamond");
    public static readonly DotArrowType RightDiamond = new DotArrowType("rdiamond");
    public static readonly DotArrowType OpenDiamond = new DotArrowType("odiamond");
    public static readonly DotArrowType OpenLeftDiamond = new DotArrowType("oldiamond");
    public static readonly DotArrowType OpenRightDiamond = new DotArrowType("ordiamond");
    public static readonly DotArrowType Dot = new DotArrowType("dot");
    public static readonly DotArrowType OpenDot = new DotArrowType("odot");
    public static readonly DotArrowType Inv = new DotArrowType("inv");
    public static readonly DotArrowType LeftInv = new DotArrowType("linv");
    public static readonly DotArrowType RightInv = new DotArrowType("rinv");
    public static readonly DotArrowType OpenInv = new DotArrowType("oinv");
    public static readonly DotArrowType OpenLeftInv = new DotArrowType("olinv");
    public static readonly DotArrowType OpenRightInv = new DotArrowType("orinv");
    public static readonly DotArrowType None = new DotArrowType("none");
    public static readonly DotArrowType Normal = new DotArrowType("normal");
    public static readonly DotArrowType LeftNormal = new DotArrowType("lnormal");
    public static readonly DotArrowType RightNormal = new DotArrowType("rnormal");
    public static readonly DotArrowType OpenNormal = new DotArrowType("onormal");
    public static readonly DotArrowType OpenLeftNormal = new DotArrowType("olnormal");
    public static readonly DotArrowType OpenRightNormal = new DotArrowType("ornormal");
    public static readonly DotArrowType Tee = new DotArrowType("tee");
    public static readonly DotArrowType LeftTee = new DotArrowType("ltee");
    public static readonly DotArrowType RightTee = new DotArrowType("rtee");
    public static readonly DotArrowType Vee = new DotArrowType("vee");
    public static readonly DotArrowType LeftVee = new DotArrowType("lvee");
    public static readonly DotArrowType RightVee = new DotArrowType("rvee");
    public static readonly DotArrowType Curve = new DotArrowType("curve");
    public static readonly DotArrowType LeftCurve = new DotArrowType("lcurve");
    public static readonly DotArrowType RightCurve = new DotArrowType("rcurve");
    public static readonly DotArrowType InvCurve = new DotArrowType("icurve");
    public static readonly DotArrowType LeftInvCurve = new DotArrowType("licurve");
    public static readonly DotArrowType RightInvCurve = new DotArrowType("ricurve");


    internal static string ConcatArrows(IEnumerable<DotArrowType> values)
    {
        var s = new StringBuilder();

        foreach (var value in values)
            s.Append(value.Value);

        return s.ToString();
    }

    private DotArrowType(string value) 
        : base(value)
    {
    }
}