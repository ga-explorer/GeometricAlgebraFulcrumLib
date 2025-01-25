using System.Text;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Objects.ISP;

/// <summary>
/// https://en.wikipedia.org/wiki/Quadric
/// </summary>
public class GrPovRayQuadric : 
    GrPovRayObject, 
    IGrPovRayInfiniteSolidObject
{
    internal static GrPovRayQuadric Ellipsoid(double a, double b, double c)
    {
        return new GrPovRayQuadric
        {
            CoefAxx = 1 / (a * a),
            CoefByy = 1 / (b * b),
            CoefCzz = 1 / (c * c),
            CoefJ = -1
        };
    }
    
    internal static GrPovRayQuadric EllipticParaboloid(double a, double b)
    {
        return new GrPovRayQuadric
        {
            CoefAxx = 1 / (a * a),
            CoefByy = 1 / (b * b),
            CoefIz = -1
        };
    }
    
    internal static GrPovRayQuadric HyperbolicParaboloid(double a, double b)
    {
        return new GrPovRayQuadric
        {
            CoefAxx = 1 / (a * a),
            CoefByy = -1 / (b * b),
            CoefIz = -1
        };
    }

    internal static GrPovRayQuadric HyperbolicHyperboloid(double a, double b, double c)
    {
        return new GrPovRayQuadric
        {
            CoefAxx = 1 / (a * a),
            CoefByy = 1 / (b * b),
            CoefCzz = -1 / (c * c),
            CoefJ = -1
        };
    }
    
    internal static GrPovRayQuadric EllipticHyperboloid(double a, double b, double c)
    {
        return new GrPovRayQuadric
        {
            CoefAxx = 1 / (a * a),
            CoefByy = 1 / (b * b),
            CoefCzz = -1 / (c * c),
            CoefJ = 1
        };
    }
    
    internal static GrPovRayQuadric EllipticCone(double a, double b, double c)
    {
        return new GrPovRayQuadric
        {
            CoefAxx = 1 / (a * a),
            CoefByy = 1 / (b * b),
            CoefCzz = -1 / (c * c)
        };
    }
    
    internal static GrPovRayQuadric EllipticCylinder(double a, double b)
    {
        return new GrPovRayQuadric
        {
            CoefAxx = 1 / (a * a),
            CoefByy = 1 / (b * b),
            CoefJ = -1
        };
    }
    
    internal static GrPovRayQuadric HyperbolicCylinder(double a, double b)
    {
        return new GrPovRayQuadric
        {
            CoefAxx = 1 / (a * a),
            CoefByy = -1 / (b * b),
            CoefJ = -1
        };
    }
    
    internal static GrPovRayQuadric ParabolicCylinder(double a)
    {
        return new GrPovRayQuadric
        {
            CoefAxx = 1,
            CoefHy = 2 * a
        };
    }
    

    public GrPovRayFloat32Value CoefAxx { get; init; } = GrPovRayFloat32Value.Zero;

    public GrPovRayFloat32Value CoefByy { get; init; } = GrPovRayFloat32Value.Zero;

    public GrPovRayFloat32Value CoefCzz { get; init; } = GrPovRayFloat32Value.Zero;

    public GrPovRayFloat32Value CoefDxy { get; init; } = GrPovRayFloat32Value.Zero;

    public GrPovRayFloat32Value CoefExz { get; init; } = GrPovRayFloat32Value.Zero;

    public GrPovRayFloat32Value CoefFyz { get; init; } = GrPovRayFloat32Value.Zero;

    public GrPovRayFloat32Value CoefGx { get; init; } = GrPovRayFloat32Value.Zero;

    public GrPovRayFloat32Value CoefHy { get; init; } = GrPovRayFloat32Value.Zero;

    public GrPovRayFloat32Value CoefIz { get; init; } = GrPovRayFloat32Value.Zero;

    public GrPovRayFloat32Value CoefJ { get; init; } = GrPovRayFloat32Value.Zero;
    

    private GrPovRayQuadric()
    {
    }


    public GrPovRayQuadric SetProperties(GrPovRayObjectProperties properties)
    {
        Properties = new GrPovRayObjectProperties(properties);

        return this;
    }

    private string GetCoefsString()
    {
        return new StringBuilder()
            .Append("<")
            .Append(CoefAxx.GetPovRayCode())
            .Append(", ")
            .Append(CoefByy.GetPovRayCode())
            .Append(", ")
            .Append(CoefCzz.GetPovRayCode())
            .Append(">, <")
            .Append(CoefDxy.GetPovRayCode())
            .Append(", ")
            .Append(CoefExz.GetPovRayCode())
            .Append(", ")
            .Append(CoefFyz.GetPovRayCode())
            .Append(">, <")
            .Append(CoefGx.GetPovRayCode())
            .Append(", ")
            .Append(CoefHy.GetPovRayCode())
            .Append(", ")
            .Append(CoefIz.GetPovRayCode())
            .Append(">, ")
            .Append(CoefJ.GetPovRayCode())
            .ToString();
    }

    public override string GetPovRayCode()
    {
        return new LinearTextComposer()
            .AppendLine("quadric {")
            .IncreaseIndentation()
            .AppendAtNewLine(GetCoefsString())
            .AppendAtNewLine(GetModifiersCode())
            .DecreaseIndentation()
            .AppendAtNewLine("}")
            .ToString();
    }
}