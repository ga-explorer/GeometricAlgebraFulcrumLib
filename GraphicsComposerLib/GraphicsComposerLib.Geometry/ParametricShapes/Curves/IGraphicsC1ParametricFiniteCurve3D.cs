namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves;

public interface IGraphicsC1ParametricFiniteCurve3D :
    IGraphicsC1ParametricCurve3D
{
    double ParameterValueMin { get; }

    double ParameterValueMax { get; }
}