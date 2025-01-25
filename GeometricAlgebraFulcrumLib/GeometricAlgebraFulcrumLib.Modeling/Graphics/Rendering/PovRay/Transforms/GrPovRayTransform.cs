using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Transforms;

public abstract class GrPovRayTransform : 
    IGrPovRayTransform
{
    public static GrPovRayTranslateTransform TranslateX(GrPovRayFloat32Value value)
    {
        return new GrPovRayTranslateTransform(
            value, 
            GrPovRayFloat32Value.Zero, 
            GrPovRayFloat32Value.Zero
        );
    }

    public static GrPovRayTranslateTransform TranslateY(GrPovRayFloat32Value value)
    {
        return new GrPovRayTranslateTransform(
            GrPovRayFloat32Value.Zero, 
            value, 
            GrPovRayFloat32Value.Zero
        );
    }

    public static GrPovRayTranslateTransform TranslateZ(GrPovRayFloat32Value value)
    {
        return new GrPovRayTranslateTransform(
            GrPovRayFloat32Value.Zero, 
            GrPovRayFloat32Value.Zero,
            value
        );
    }

    public static GrPovRayTranslateTransform Translate(GrPovRayVector3Value direction)
    {
        return new GrPovRayTranslateTransform(direction);
    }

    public static GrPovRayTranslateTransform Translate(GrPovRayFloat32Value directionX, GrPovRayFloat32Value directionY, GrPovRayFloat32Value directionZ)
    {
        return new GrPovRayTranslateTransform(directionX, directionY, directionZ);
    }


    public static GrPovRayScaleTransform ScaleX(GrPovRayFloat32Value factor)
    {
        return new GrPovRayScaleTransform(
            factor, 
            GrPovRayFloat32Value.One, 
            GrPovRayFloat32Value.One
        );
    }
    
    public static GrPovRayScaleTransform ScaleY(GrPovRayFloat32Value factor)
    {
        return new GrPovRayScaleTransform(
            GrPovRayFloat32Value.One, 
            factor, 
            GrPovRayFloat32Value.One
        );
    }

    public static GrPovRayScaleTransform ScaleZ(GrPovRayFloat32Value factor)
    {
        return new GrPovRayScaleTransform(
            GrPovRayFloat32Value.One, 
            GrPovRayFloat32Value.One,
            factor
        );
    }
    
    public static GrPovRayScaleTransform Scale(GrPovRayVector3Value factor)
    {
        return new GrPovRayScaleTransform(factor);
    }

    public static GrPovRayScaleTransform Scale(GrPovRayFloat32Value factor)
    {
        return new GrPovRayScaleTransform(factor, factor, factor);
    }

    public static GrPovRayScaleTransform Scale(GrPovRayFloat32Value factorX, GrPovRayFloat32Value factorY, GrPovRayFloat32Value factorZ)
    {
        return new GrPovRayScaleTransform(factorX, factorY, factorZ);
    }
    

    public static GrPovRayRotateTransform RotateX(GrPovRayAngleValue angle)
    {
        return new GrPovRayRotateTransform(
            angle, 
            GrPovRayAngleValue.Angle0, 
            GrPovRayAngleValue.Angle0
        );
    }
    
    public static GrPovRayRotateTransform RotateY(GrPovRayAngleValue angle)
    {
        return new GrPovRayRotateTransform(
            GrPovRayAngleValue.Angle0, 
            angle, 
            GrPovRayAngleValue.Angle0
        );
    }

    public static GrPovRayRotateTransform RotateZ(GrPovRayAngleValue angle)
    {
        return new GrPovRayRotateTransform(
            GrPovRayAngleValue.Angle0, 
            GrPovRayAngleValue.Angle0,
            angle
        );
    }
    
    public static GrPovRayRotateTransform Rotate(GrPovRayAngleValue angleX, GrPovRayAngleValue angleY, GrPovRayAngleValue angleZ)
    {
        return new GrPovRayRotateTransform(angleX, angleY, angleZ);
    }

    
    public static GrPovRayAffineTransform Affine(IFloat64AffineMap3D affineMap)
    {
        return new GrPovRayAffineTransform(affineMap);
    }


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