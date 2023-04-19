using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record GaLinMatrixStorageScalarRecord<T>(ILinMatrixStorage<T> Storage, T Scalar) : 
    IGaScalarRecord<T>;