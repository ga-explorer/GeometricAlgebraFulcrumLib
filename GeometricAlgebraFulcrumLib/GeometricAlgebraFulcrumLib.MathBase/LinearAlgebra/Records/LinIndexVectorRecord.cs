using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Records
{
    public sealed record LinIndexVectorRecord(int Index, LinFloat64Vector Vector);

    public sealed record LinIndexVectorRecord<T>(int Index, LinVector<T> Vector);
}