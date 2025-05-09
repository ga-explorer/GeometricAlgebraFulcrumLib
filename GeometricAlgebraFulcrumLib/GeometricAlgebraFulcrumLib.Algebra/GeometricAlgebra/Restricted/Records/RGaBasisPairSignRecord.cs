using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Records;

public sealed record RGaBasisPairSignRecord(RGaBasisBlade BasisBlade1, RGaBasisBlade BasisBlade2, IntegerSign Sign) : 
    IRGaBasisPairSignRecord;