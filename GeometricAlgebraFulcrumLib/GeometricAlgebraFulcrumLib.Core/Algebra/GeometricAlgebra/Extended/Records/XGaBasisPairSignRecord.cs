using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Records;

public sealed record XGaBasisPairSignRecord(XGaBasisBlade BasisBlade1, XGaBasisBlade BasisBlade2, IntegerSign Sign) : 
    IXGaBasisPairSignRecord;