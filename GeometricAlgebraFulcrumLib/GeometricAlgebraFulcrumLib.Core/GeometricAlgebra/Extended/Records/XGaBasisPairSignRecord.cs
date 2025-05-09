using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;

public sealed record XGaBasisPairSignRecord(XGaBasisBlade BasisBlade1, XGaBasisBlade BasisBlade2, IntegerSign Sign) : 
    IXGaBasisPairSignRecord;