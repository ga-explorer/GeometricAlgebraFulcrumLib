using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

public sealed record RGaBasisPairSignRecord(RGaBasisBlade BasisBlade1, RGaBasisBlade BasisBlade2, IntegerSign Sign) : 
    IRGaBasisPairSignRecord;