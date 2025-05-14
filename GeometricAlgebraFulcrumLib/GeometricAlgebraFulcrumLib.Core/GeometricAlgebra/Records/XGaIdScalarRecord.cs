using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Records;

public sealed record XGaIdScalarRecord(IndexSet Id, double Scalar) :
    IXGaIdScalarRecord<double>;