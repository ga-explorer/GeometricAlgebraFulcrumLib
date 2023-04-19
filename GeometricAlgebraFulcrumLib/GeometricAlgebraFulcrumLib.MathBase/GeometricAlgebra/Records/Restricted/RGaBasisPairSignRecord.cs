using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

public sealed record RGaBasisPairSignRecord(RGaBasisBlade BasisBlade1, RGaBasisBlade BasisBlade2, IntegerSign Sign) : 
    IRGaBasisPairSignRecord;