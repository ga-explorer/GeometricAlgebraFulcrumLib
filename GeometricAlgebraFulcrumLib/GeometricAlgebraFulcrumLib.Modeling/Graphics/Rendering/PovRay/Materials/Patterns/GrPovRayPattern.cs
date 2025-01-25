using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Materials.Patterns;

/// <summary>
/// https://www.povray.org/documentation/3.7.0/r3_4.html#r3_4_7
/// </summary>
public abstract class GrPovRayPattern :
    IGrPovRayPattern
{
    public static GrPovRayBuiltInPattern Boxed { get; }
        = new GrPovRayBuiltInPattern("boxed");
    
    public static GrPovRayBuiltInPattern Bumps { get; }
        = new GrPovRayBuiltInPattern("bumps");
    
    public static GrPovRayBuiltInPattern Cells { get; }
        = new GrPovRayBuiltInPattern("cells");
    
    public static GrPovRayBuiltInPattern Dents { get; }
        = new GrPovRayBuiltInPattern("dents");
    
    public static GrPovRayBuiltInPattern Granite { get; }
        = new GrPovRayBuiltInPattern("granite");
    
    public static GrPovRayBuiltInPattern Leopard { get; }
        = new GrPovRayBuiltInPattern("leopard");
    
    public static GrPovRayBuiltInPattern Onion { get; }
        = new GrPovRayBuiltInPattern("onion");
    
    public static GrPovRayBuiltInPattern Planar { get; }
        = new GrPovRayBuiltInPattern("planar");
    
    public static GrPovRayBuiltInPattern Radial { get; }
        = new GrPovRayBuiltInPattern("radial");
    
    public static GrPovRayBuiltInPattern Ripples { get; }
        = new GrPovRayBuiltInPattern("ripples");
    
    public static GrPovRayBuiltInPattern Spherical { get; }
        = new GrPovRayBuiltInPattern("spherical");
    
    public static GrPovRayBuiltInPattern Tiling { get; }
        = new GrPovRayBuiltInPattern("tiling");

    public static GrPovRayBuiltInPattern Waves { get; }
        = new GrPovRayBuiltInPattern("waves");
    
    public static GrPovRayBuiltInPattern Wood { get; }
        = new GrPovRayBuiltInPattern("wood");
    
    public static GrPovRayBuiltInPattern Wrinkles { get; }
        = new GrPovRayBuiltInPattern("wrinkles");


    public static GrPovRayBrickPattern Brick() 
        => GrPovRayBrickPattern.Default;
    
    public static GrPovRayBrickPattern Brick(GrPovRayColorValue color1)
        => GrPovRayBrickPattern.Create(color1);

    public static GrPovRayBrickPattern Brick(GrPovRayColorValue color1, GrPovRayColorValue color2)
        => GrPovRayBrickPattern.Create(color1, color2);

    public static GrPovRayCheckerPattern Checker() 
        => GrPovRayCheckerPattern.Default;

    public static GrPovRayCheckerPattern Checker(GrPovRayColorValue color1)
        => GrPovRayCheckerPattern.Create(color1);

    public static GrPovRayCheckerPattern Checker(GrPovRayColorValue color1, GrPovRayColorValue color2)
        => GrPovRayCheckerPattern.Create(color1, color2);
    
    public static GrPovRayGradientPattern GradientX()
        => GrPovRayGradientPattern.DefaultX;

    public static GrPovRayGradientPattern GradientY()
        => GrPovRayGradientPattern.DefaultY;

    public static GrPovRayGradientPattern GradientZ()
        => GrPovRayGradientPattern.DefaultZ;

    public static GrPovRayGradientPattern Gradient(GrPovRayVector3Value orientation)
        => GrPovRayGradientPattern.Create(orientation);
    
    public static GrPovRayHexagonPattern Hexagon() 
        => GrPovRayHexagonPattern.Default;

    public static GrPovRayHexagonPattern Hexagon(GrPovRayColorValue color1)
        => GrPovRayHexagonPattern.Create(color1);

    public static GrPovRayHexagonPattern Hexagon(GrPovRayColorValue color1, GrPovRayColorValue color2)
        => GrPovRayHexagonPattern.Create(color1, color2);
    
    public static GrPovRayHexagonPattern Hexagon(GrPovRayColorValue color1, GrPovRayColorValue color2, GrPovRayColorValue color3)
        => GrPovRayHexagonPattern.Create(color1, color2, color3);
    
    public static GrPovRaySquarePattern Square() 
        => GrPovRaySquarePattern.Default;

    public static GrPovRaySquarePattern Square(GrPovRayColorValue color1)
        => GrPovRaySquarePattern.Create(color1);

    public static GrPovRaySquarePattern Square(GrPovRayColorValue color1, GrPovRayColorValue color2)
        => GrPovRaySquarePattern.Create(color1, color2);
    
    public static GrPovRaySquarePattern Square(GrPovRayColorValue color1, GrPovRayColorValue color2, GrPovRayColorValue color3)
        => GrPovRaySquarePattern.Create(color1, color2, color3);
    
    public static GrPovRayTriangularPattern Triangular() 
        => GrPovRayTriangularPattern.Default;

    public static GrPovRayTriangularPattern Triangular(GrPovRayColorValue color1)
        => GrPovRayTriangularPattern.Create(color1);

    public static GrPovRayTriangularPattern Triangular(GrPovRayColorValue color1, GrPovRayColorValue color2)
        => GrPovRayTriangularPattern.Create(color1, color2);
    
    public static GrPovRayTriangularPattern Triangular(GrPovRayColorValue color1, GrPovRayColorValue color2, GrPovRayColorValue color3)
        => GrPovRayTriangularPattern.Create(color1, color2, color3);
    
    public static GrPovRayTriangularPattern Triangular(GrPovRayColorValue color1, GrPovRayColorValue color2, GrPovRayColorValue color3, GrPovRayColorValue color4)
        => GrPovRayTriangularPattern.Create(color1, color2, color3, color4);
    
    public static GrPovRayTriangularPattern Triangular(GrPovRayColorValue color1, GrPovRayColorValue color2, GrPovRayColorValue color3, GrPovRayColorValue color4, GrPovRayColorValue color5)
        => GrPovRayTriangularPattern.Create(color1, color2, color3, color4, color5);
    
    public static GrPovRayTriangularPattern Triangular(GrPovRayColorValue color1, GrPovRayColorValue color2, GrPovRayColorValue color3, GrPovRayColorValue color4, GrPovRayColorValue color5, GrPovRayColorValue color6)
        => GrPovRayTriangularPattern.Create(color1, color2, color3, color4, color5, color6);


    public virtual bool IsEmptyCodeElement()
    {
        return false;
    }

    public abstract string GetPovRayCode();

    public override string ToString()
    {
        return GetPovRayCode();
    }
}