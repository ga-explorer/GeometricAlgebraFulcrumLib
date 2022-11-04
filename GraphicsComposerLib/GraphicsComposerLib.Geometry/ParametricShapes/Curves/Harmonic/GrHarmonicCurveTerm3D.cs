using System;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Harmonic;

public sealed class GrHarmonicCurveTerm3D
{
    public int HarmonicFactor { get; }

    public Tuple3D MagnitudeVector { get; }


    internal GrHarmonicCurveTerm3D(int harmonicFactor, Tuple3D magnitudeVector)
    {
        if (harmonicFactor < 1)
            throw new ArgumentOutOfRangeException(nameof(harmonicFactor));

        if (!magnitudeVector.IsValid())
            throw new ArgumentException(nameof(magnitudeVector));

        HarmonicFactor = harmonicFactor;
        MagnitudeVector = magnitudeVector;
    }


    internal Tuple3D GetPoint(double parameterValue)
    {
        var w = 2d * Math.PI * HarmonicFactor;

        return new Tuple3D(
            MagnitudeVector.X * (w * parameterValue).Cos(),
            MagnitudeVector.Y * (w * (parameterValue + 1d / 3d)).Cos(),
            MagnitudeVector.Z * (w * (parameterValue - 1d / 3d)).Cos()
        );
    }

    internal Tuple3D GetTangent(double parameterValue)
    {
        var w = 2d * Math.PI * HarmonicFactor;

        return new Tuple3D(
            -MagnitudeVector.X * w * (w * parameterValue).Sin(),
            -MagnitudeVector.Y * w * (w * (parameterValue + 1d / 3d)).Sin(),
            -MagnitudeVector.Z * w * (w * (parameterValue - 1d / 3d)).Sin()
        );
    }
    
    internal Tuple3D GetSecondDerivative(double parameterValue)
    {
        var w = 2d * Math.PI * HarmonicFactor;
        var w2 = w * w;

        return new Tuple3D(
            -MagnitudeVector.X * w2 * (w * parameterValue).Cos(),
            -MagnitudeVector.Y * w2 * (w * (parameterValue + 1d / 3d)).Cos(),
            -MagnitudeVector.Z * w2 * (w * (parameterValue - 1d / 3d)).Cos()
        );
    }
}