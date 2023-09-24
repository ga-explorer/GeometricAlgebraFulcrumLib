using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record GaGradeLinMatrixStorageScalarRecord<T>(uint Grade, ILinMatrixStorage<T> Storage, T Scalar) : 
    IGaGradeScalarRecord<T>;