namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves;

public interface IGraphicsC1ArcLengthCurve3D :
    IGraphicsC1ParametricFiniteCurve3D
{
    double GetLength();

    double ParameterToLength(double parameterValue);

    double LengthToParameter(double length);
}