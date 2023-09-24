using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record GaLinMatrixGradedStorageScalarRecord<T>(ILinMatrixGradedStorage<T> Storage, T Scalar) : 
    IGaScalarRecord<T>;