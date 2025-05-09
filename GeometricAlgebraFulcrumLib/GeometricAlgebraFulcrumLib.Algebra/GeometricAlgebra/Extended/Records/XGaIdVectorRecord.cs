using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Records;

public sealed record XGaIdVectorRecord(IndexSet Id, XGaFloat64Vector Vector) :
    IXGaIdVectorRecord;
