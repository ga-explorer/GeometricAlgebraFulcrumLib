using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Statistics;

/// <summary>
/// A factory for generating randomly 2D\3D vectors using polar\spherical coordinates with 
/// parametric angles uniformly distributed
/// </summary>
public class RandomEuclideanVectorsComposer
{
    public System.Random RandomGenerator { get; }


    //TODO: Create a BoundingSphericalBox class to hold this information
    public double RMin { get; set; } 
        = 0.0d;

    public double RMax { get; set; } 
        = 1.0d;

    public double RRange
    {
        get { return RMax - RMin; }
    }

    public double ThetaMin { get; set; } 
        = 0.0d;

    public double ThetaMax { get; set; } 
        = MathNet.Numerics.Constants.Pi;

    public double ThetaRange
    {
        get { return ThetaMax - ThetaMin; }
    }

    public double PhiMin { get; set; } 
        = 0.0d;

    public double PhiMax { get; set; } 
        = MathNet.Numerics.Constants.Pi2;

    public double PhiRange
    {
        get { return PhiMax - PhiMin; }
    }


    public RandomEuclideanVectorsComposer()
    {
        RandomGenerator = new System.Random();
    }

    public RandomEuclideanVectorsComposer(int seed) 
    {
        RandomGenerator = new System.Random(seed);
    }

    public RandomEuclideanVectorsComposer(System.Random randomGenerator)
    {
        RandomGenerator = randomGenerator;
    }


    public Float64Vector2D GetRandomUnitVector2Dp()
    {
        var phi = PhiMin + RandomGenerator.GetNumber() * PhiRange;
        var cosPhi = Math.Cos(phi);
        var sinPhi = Math.Sin(phi);

        return Float64Vector2D.Create((Float64Scalar)cosPhi,
            (Float64Scalar)sinPhi);
    }

    public Float64Vector2D GetRandomVector2Dp()
    {
        var r = RMin + RandomGenerator.GetNumber() * RRange;
        var phi = PhiMin + RandomGenerator.GetNumber() * PhiRange;
        var cosPhi = Math.Cos(phi);
        var sinPhi = Math.Sin(phi);

        return Float64Vector2D.Create((Float64Scalar)(r * cosPhi),
            (Float64Scalar)(r * sinPhi));
    }

    public Float64Vector3D GetRandomUnitVector3Dp()
    {
        var theta = ThetaMin + RandomGenerator.GetNumber() * ThetaRange;
        var phi = PhiMin + RandomGenerator.GetNumber() * PhiRange;
        var cosTheta = Math.Cos(theta);
        var sinTheta = Math.Sin(theta);
        var cosPhi = Math.Cos(phi);
        var sinPhi = Math.Sin(phi);

        return Float64Vector3D.Create(sinTheta * cosPhi,
            sinTheta * sinPhi,
            cosTheta);
    }

    public Float64Vector3D GetRandomVector3Dp()
    {
        var r = RMin + RandomGenerator.GetNumber() * RRange;
        var theta = ThetaMin + RandomGenerator.GetNumber() * ThetaRange;
        var phi = PhiMin + RandomGenerator.GetNumber() * PhiRange;
        var cosTheta = Math.Cos(theta);
        var sinTheta = Math.Sin(theta);
        var cosPhi = Math.Cos(phi);
        var sinPhi = Math.Sin(phi);
            
        return Float64Vector3D.Create(r * sinTheta * cosPhi,
            r * sinTheta * sinPhi,
            r * cosTheta);
    }


    //public VectorFrame3Dp GetRandomFrame3Dp()
    //{
    //    var rotationVector = GetRandomUnitVector3Dp();
    //    var rotationAngle = GetNumber() * FloatDpUtils.TwoPi;
    //    var quaternion = rotationVector.ToRotationQuaternion(rotationAngle);

    //}


    public Float64Vector2D[] GetRandomUnitVectors2Dp(int count)
    {
        var vectorsList = new Float64Vector2D[count];

        for (var i = 0; i < count; i++)
            vectorsList[i] = GetRandomUnitVector2Dp();

        return vectorsList;
    }

    public Float64Vector2D[] GetRandomVectors2Dp(int count)
    {
        var vectorsList = new Float64Vector2D[count];

        for (var i = 0; i < count; i++)
            vectorsList[i] = GetRandomVector2Dp();

        return vectorsList;
    }

    public Float64Vector3D[] GetRandomUnitVectors3Dp(int count)
    {
        var vectorsList = new Float64Vector3D[count];

        for (var i = 0; i < count; i++)
            vectorsList[i] = GetRandomUnitVector3Dp();

        return vectorsList;
    }

    public Float64Vector3D[] GetRandomVectors3Dp(int count)
    {
        var vectorsList = new Float64Vector3D[count];

        for (var i = 0; i < count; i++)
            vectorsList[i] = GetRandomVector3Dp();

        return vectorsList;
    }
}