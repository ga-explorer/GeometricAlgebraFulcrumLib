using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Records;

public sealed record XGaIdVectorRecord(IndexSet Id, XGaFloat64Vector Vector) :
    IXGaIdVectorRecord;
