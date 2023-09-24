using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Records;

public sealed record XGaBasisPairSignRecord(XGaBasisBlade BasisBlade1, XGaBasisBlade BasisBlade2, IntegerSign Sign) : 
    IXGaBasisPairSignRecord;