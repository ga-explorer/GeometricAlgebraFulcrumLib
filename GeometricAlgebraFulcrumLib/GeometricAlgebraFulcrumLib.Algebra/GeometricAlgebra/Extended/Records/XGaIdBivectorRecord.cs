using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Records;

public sealed record XGaIdBivectorRecord(IndexSet Id, XGaFloat64Bivector Bivector) :
    IXGaIdBivectorRecord;
