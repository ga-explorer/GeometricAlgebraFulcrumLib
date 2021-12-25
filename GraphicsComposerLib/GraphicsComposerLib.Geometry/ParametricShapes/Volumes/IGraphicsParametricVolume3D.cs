using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Volumes
{
    public interface IGraphicsParametricVolume3D : 
        IGeometricElement
    {
        Tuple3D GetPoint(ITuple3D parameterValue);

        Tuple3D GetPoint(double parameterValue1, double parameterValue2, double parameterValue3);

        double GetScalarDistance(ITuple3D parameterValue);

        double GetScalarDistance(double parameterValue1, double parameterValue2, double parameterValue3);
        
        GrParametricVolumeLocalFrame3D GetFrame(ITuple3D parameterValue);

        GrParametricVolumeLocalFrame3D GetFrame(double parameterValue1, double parameterValue2, double parameterValue3);
    }
}