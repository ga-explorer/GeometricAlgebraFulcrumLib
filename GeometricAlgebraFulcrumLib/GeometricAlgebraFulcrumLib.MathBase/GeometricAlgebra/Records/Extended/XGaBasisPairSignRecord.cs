using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Basis;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Extended;

public sealed record XGaBasisPairSignRecord(XGaBasisBlade BasisBlade1, XGaBasisBlade BasisBlade2, IntegerSign Sign) : 
    IXGaBasisPairSignRecord;