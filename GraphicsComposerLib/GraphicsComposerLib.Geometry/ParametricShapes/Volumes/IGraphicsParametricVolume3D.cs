using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Volumes
{
    public interface IGraphicsParametricVolume3D : 
        IGeometricElement
    {
        Float64Tuple3D GetPoint(IFloat64Tuple3D parameterValue);

        Float64Tuple3D GetPoint(double parameterValue1, double parameterValue2, double parameterValue3);

        double GetScalarDistance(IFloat64Tuple3D parameterValue);

        double GetScalarDistance(double parameterValue1, double parameterValue2, double parameterValue3);
        
        GrParametricVolumeLocalFrame3D GetFrame(IFloat64Tuple3D parameterValue);

        GrParametricVolumeLocalFrame3D GetFrame(double parameterValue1, double parameterValue2, double parameterValue3);
    }
}