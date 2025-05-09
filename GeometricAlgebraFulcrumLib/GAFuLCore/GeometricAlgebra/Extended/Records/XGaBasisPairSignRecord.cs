using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Records;

public sealed record XGaBasisPairSignRecord(XGaBasisBlade BasisBlade1, XGaBasisBlade BasisBlade2, IntegerSign Sign) : 
    IXGaBasisPairSignRecord;