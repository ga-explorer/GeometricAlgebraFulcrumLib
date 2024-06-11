using GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry.Operations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry.Primitives;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry.Transforms;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry;

public static class SdfUtils
{
        
    public static SdfPlaneXy3D PlaneXy { get; }
        = new SdfPlaneXy3D();

    public static SdfPlaneXz3D PlaneXz { get; }
        = new SdfPlaneXz3D();

    public static SdfPlaneYz3D PlaneYz { get; }
        = new SdfPlaneYz3D();
        
    public static SdfSphere3D UnitSphere { get; }
        = new SdfSphere3D();
        
    public static SdfBox3D UnitCube { get; }
        = new SdfBox3D();

        
    public static SdfBinaryOr3D Or(this ISdfGeometry3D surface1, ISdfGeometry3D surface2)
    {
        return new SdfBinaryOr3D()
        {
            Surface1 = surface1,
            Surface2 = surface2
        };
    }
        
    public static SdfBinaryOrNot3D OrNot(this ISdfGeometry3D surface1, ISdfGeometry3D surface2)
    {
        return new SdfBinaryOrNot3D()
        {
            Surface1 = surface1,
            Surface2 = surface2
        };
    }

    public static SdfBinaryAnd3D And(this ISdfGeometry3D surface1, ISdfGeometry3D surface2)
    {
        return new SdfBinaryAnd3D()
        {
            Surface1 = surface1,
            Surface2 = surface2
        };
    }
        
    public static SdfBinaryAndNot3D AndNot(this ISdfGeometry3D surface1, ISdfGeometry3D surface2)
    {
        return new SdfBinaryAndNot3D()
        {
            Surface1 = surface1,
            Surface2 = surface2
        };
    }

    public static SdfRounding3D Rounding(this ISdfGeometry3D surface, double radius)
    {
        return new SdfRounding3D()
        {
            Surface = surface,
            Radius = radius
        };
    }

    public static SdfRotateX3D RotateX(this ISdfGeometry3D surface, double angle)
    {
        return new SdfRotateX3D()
        {
            Surface = surface,
            Angle = angle
        };
    }

    public static SdfRotateY3D RotateY(this ISdfGeometry3D surface, double angle)
    {
        return new SdfRotateY3D()
        {
            Surface = surface,
            Angle = angle
        };
    }

    public static SdfRotateZ3D RotateZ(this ISdfGeometry3D surface, double angle)
    {
        return new SdfRotateZ3D()
        {
            Surface = surface,
            Angle = angle
        };
    }
}