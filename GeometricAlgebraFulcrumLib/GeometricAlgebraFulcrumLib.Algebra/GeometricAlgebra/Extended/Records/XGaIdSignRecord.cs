using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Records;

public sealed record XGaIdSignRecord(IndexSet Id, IntegerSign Sign) :
    IXGaIdSignRecord;