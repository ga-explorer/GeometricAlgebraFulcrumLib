using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Records;

public sealed record RGaBasisPairSignRecord(RGaBasisBlade BasisBlade1, RGaBasisBlade BasisBlade2, IntegerSign Sign) : 
    IRGaBasisPairSignRecord;