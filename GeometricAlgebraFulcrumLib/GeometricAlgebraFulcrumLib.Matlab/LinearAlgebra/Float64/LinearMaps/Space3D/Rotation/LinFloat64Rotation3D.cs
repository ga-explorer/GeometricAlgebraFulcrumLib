using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Composers;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Rotation;

public abstract class LinFloat64Rotation3D :
    LinFloat64ReflectionBase3D
{
    public override bool SwapsHandedness
        => false;

    public abstract LinFloat64Quaternion GetQuaternion();

    public abstract LinFloat64Rotation3D GetInverseRotation();


    
    public override LinFloat64ReflectionBase3D GetReflectionLinearMapInverse()
    {
        return GetInverseRotation();
    }

    
    public LinFloat64RotationComposer3D ToRotationComposer()
    {
        return LinFloat64RotationComposer3D.CreateFromRotation(this);
    }
}