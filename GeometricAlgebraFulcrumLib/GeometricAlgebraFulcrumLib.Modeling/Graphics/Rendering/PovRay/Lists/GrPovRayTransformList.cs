using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Transforms;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;

public sealed class GrPovRayTransformList :
    GrPovRayCodeElementList<IGrPovRayTransform>
{
    public override string Separator 
        => Environment.NewLine;


    public GrPovRayTransformList Clear()
    {
        CodeElementList.Clear();

        return this;
    }

    public GrPovRayTransformList Remove(int index)
    {
        CodeElementList.RemoveAt(index);

        return this;
    }

    
    public GrPovRayTransformList TranslateX(GrPovRayFloat32Value value)
    {
        CodeElementList.Add(
            GrPovRayTransform.TranslateX(value)
        );

        return this;
    }
    
    public GrPovRayTransformList TranslateY(GrPovRayFloat32Value value)
    {
        CodeElementList.Add(
            GrPovRayTransform.TranslateY(value)
        );

        return this;
    }

    public GrPovRayTransformList TranslateZ(GrPovRayFloat32Value value)
    {
        CodeElementList.Add(
            GrPovRayTransform.TranslateZ(value)
        );

        return this;
    }
    
    public GrPovRayTransformList Translate(ITriplet<Float64Scalar> value)
    {
        CodeElementList.Add(
            GrPovRayTransform.Translate(
                GrPovRayVector3Value.Create(value)
            )
        );

        return this;
    }
    
    public GrPovRayTransformList Translate(LinFloat64Vector3D value)
    {
        CodeElementList.Add(
            GrPovRayTransform.Translate(value)
        );

        return this;
    }

    public GrPovRayTransformList Translate(GrPovRayVector3Value value)
    {
        CodeElementList.Add(
            GrPovRayTransform.Translate(value)
        );

        return this;
    }
    
    public GrPovRayTransformList Translate(GrPovRayFloat32Value valueX, GrPovRayFloat32Value valueY, GrPovRayFloat32Value valueZ)
    {
        CodeElementList.Add(
            GrPovRayTransform.Translate(valueX, valueY, valueZ)
        );

        return this;
    }

    
    public GrPovRayTransformList ScaleX(GrPovRayFloat32Value value)
    {
        CodeElementList.Add(
            GrPovRayTransform.ScaleX(value)
        );

        return this;
    }

    public GrPovRayTransformList ScaleY(GrPovRayFloat32Value value)
    {
        CodeElementList.Add(
            GrPovRayTransform.ScaleY(value)
        );

        return this;
    }

    public GrPovRayTransformList ScaleZ(GrPovRayFloat32Value value)
    {
        CodeElementList.Add(
            GrPovRayTransform.ScaleZ(value)
        );

        return this;
    }

    public GrPovRayTransformList Scale(GrPovRayFloat32Value value)
    {
        CodeElementList.Add(
            GrPovRayTransform.Scale(value)
        );

        return this;
    }
    
    public GrPovRayTransformList Scale(GrPovRayVector3Value value)
    {
        CodeElementList.Add(
            GrPovRayTransform.Scale(value)
        );

        return this;
    }
    
    public GrPovRayTransformList Scale(GrPovRayFloat32Value valueX, GrPovRayFloat32Value valueY, GrPovRayFloat32Value valueZ)
    {
        CodeElementList.Add(
            GrPovRayTransform.Scale(valueX, valueY, valueZ)
        );

        return this;
    }

    
    public GrPovRayTransformList RotateX(GrPovRayAngleValue angle)
    {
        CodeElementList.Add(
            GrPovRayTransform.RotateX(angle)
        );

        return this;
    }
    
    public GrPovRayTransformList RotateY(GrPovRayAngleValue angle)
    {
        CodeElementList.Add(
            GrPovRayTransform.RotateY(angle)
        );

        return this;
    }

    public GrPovRayTransformList RotateZ(GrPovRayAngleValue angle)
    {
        CodeElementList.Add(
            GrPovRayTransform.RotateZ(angle)
        );

        return this;
    }

    public GrPovRayTransformList Rotate(GrPovRayAngleValue angleX, GrPovRayAngleValue angleY, GrPovRayAngleValue angleZ)
    {
        CodeElementList.Add(
            GrPovRayTransform.Rotate(angleX, angleY, angleZ)
        );

        return this;
    }
    

    public GrPovRayTransformList Affine(IFloat64AffineMap3D value)
    {
        CodeElementList.Add(
            new GrPovRayAffineTransform(value)
        );

        return this;
    }


    public GrPovRayTransformList Named(string identifier)
    {
        CodeElementList.Add(
            new GrPovRayNamedTransform(identifier)
        );

        return this;
    }

    public GrPovRayTransformList Transform(IGrPovRayTransform transform)
    {
        CodeElementList.Add(transform);

        return this;
    }
}