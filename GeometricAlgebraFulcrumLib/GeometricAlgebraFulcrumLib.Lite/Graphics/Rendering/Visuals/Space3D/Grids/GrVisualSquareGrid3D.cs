using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Images;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Grids;

public sealed class GrVisualSquareGrid3D :
    GrVisualLineGridImage3D
{
    public GrVisualSquareGridPlane3D GridPlane { get; }

    public double DistanceToOrigin { get; set; }
        = 0d;

    public double Offset1 { get; set; }
        = 0d;
    
    public double Offset2 { get; set; }
        = 0d;

    public double UnitSize { get; set; } 
        = 1;

    public int UnitCount1 { get; set; } 
        = 24;

    public int UnitCount2 { get; set; } 
        = 24;

    public double Size1 
        => UnitCount1 * UnitSize;

    public double Size2 
        => UnitCount2 * UnitSize;

    public double Opacity { get; set; } 
        = 0.2;

    public Float64Vector3D MidPoint
        => GridPlane switch
        {
            GrVisualSquareGridPlane3D.XyPlane => 
                Float64Vector3D.Create(
                    Offset1, 
                    Offset2,
                    DistanceToOrigin
                ),
            
            GrVisualSquareGridPlane3D.YzPlane => 
                Float64Vector3D.Create(
                    DistanceToOrigin, 
                    Offset1, 
                    Offset2
                ),
            
            GrVisualSquareGridPlane3D.ZxPlane => 
                Float64Vector3D.Create(
                    Offset2,
                    DistanceToOrigin, 
                    Offset1
                ),

            _ => throw new NotSupportedException()
        };

    public Float64Vector3D Origin
        => GridPlane switch
        {
            GrVisualSquareGridPlane3D.XyPlane => 
                Float64Vector3D.Create(
                    Offset1 - 0.5 * Size1, 
                    Offset2 - 0.5 * Size2,
                    DistanceToOrigin
                ),
            
            GrVisualSquareGridPlane3D.YzPlane => 
                Float64Vector3D.Create(
                    DistanceToOrigin, 
                    Offset1 - 0.5 * Size1, 
                    Offset2 - 0.5 * Size2
                ),
            
            GrVisualSquareGridPlane3D.ZxPlane => 
                Float64Vector3D.Create(
                    Offset2 - 0.5 * Size2,
                    DistanceToOrigin, 
                    Offset1 - 0.5 * Size1
                ),

            _ => throw new NotSupportedException()
        };


    public GrVisualSquareGrid3D(string name, GrVisualSquareGridPlane3D gridPlane) 
        : base(name)
    {
        GridPlane = gridPlane;
    }
}