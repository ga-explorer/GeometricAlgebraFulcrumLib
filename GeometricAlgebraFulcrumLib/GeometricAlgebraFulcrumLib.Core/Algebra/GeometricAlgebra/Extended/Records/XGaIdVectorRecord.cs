using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Records;

public sealed record XGaIdVectorRecord(IIndexSet Id, XGaFloat64Vector Vector) :
    IXGaIdVectorRecord;
