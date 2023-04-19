using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public sealed record GaGradeLinMatrixStorageRecord<T>(uint Grade, ILinMatrixStorage<T> Storage) : 
    IGaGradeRecord;