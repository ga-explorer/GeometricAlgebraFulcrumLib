using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.ProductIterators
{
    public interface IMultivectorStorageTermsIterator<T>
    {
        IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        IMultivectorStorage<T> Storage1 { get; }

        IMultivectorStorage<T> Storage2 { get; }


        IEnumerable<IndexScalarRecord<T>> GetOpIdScalarRecords();

        IEnumerable<IndexScalarRecord<T>> GetEGpIdScalarRecords();

        IEnumerable<IndexScalarRecord<T>> GetESpIdScalarRecords();

        IEnumerable<T> GetESpScalars();

        IEnumerable<IndexScalarRecord<T>> GetELcpIdScalarRecords();

        IEnumerable<IndexScalarRecord<T>> GetERcpIdScalarRecords();

        IEnumerable<IndexScalarRecord<T>> GetEHipIdScalarRecords();

        IEnumerable<IndexScalarRecord<T>> GetEFdpIdScalarRecords();

        IEnumerable<IndexScalarRecord<T>> GetECpIdScalarRecords();

        IEnumerable<IndexScalarRecord<T>> GetEAcpIdScalarRecords();


        IEnumerable<IndexScalarRecord<T>> GetGpIdScalarRecords(GeometricAlgebraBasisSet basisSet);

        IEnumerable<IndexScalarRecord<T>> GetSpIdScalarRecords(GeometricAlgebraBasisSet basisSet);

        IEnumerable<T> GetSpScalars(GeometricAlgebraBasisSet basisSet);

        IEnumerable<IndexScalarRecord<T>> GetLcpIdScalarRecords(GeometricAlgebraBasisSet basisSet);

        IEnumerable<IndexScalarRecord<T>> GetRcpIdScalarRecords(GeometricAlgebraBasisSet basisSet);

        IEnumerable<IndexScalarRecord<T>> GetHipIdScalarRecords(GeometricAlgebraBasisSet basisSet);

        IEnumerable<IndexScalarRecord<T>> GetFdpIdScalarRecords(GeometricAlgebraBasisSet basisSet);

        IEnumerable<IndexScalarRecord<T>> GetCpIdScalarRecords(GeometricAlgebraBasisSet basisSet);

        IEnumerable<IndexScalarRecord<T>> GetAcpIdScalarRecords(GeometricAlgebraBasisSet basisSet);
    }
}