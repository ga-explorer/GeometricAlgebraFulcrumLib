using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Records
{
    public sealed record LinIndexVectorRecord(int Index, Float64Vector Vector);

    public sealed record LinIndexVectorRecord<T>(int Index, LinVector<T> Vector);
}