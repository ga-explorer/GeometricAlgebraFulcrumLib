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


        IEnumerable<IndexScalarRecord<T>> GetGpIdScalarRecords(BasisBladeSet basisSet);

        IEnumerable<IndexScalarRecord<T>> GetSpIdScalarRecords(BasisBladeSet basisSet);

        IEnumerable<T> GetSpScalars(BasisBladeSet basisSet);

        IEnumerable<IndexScalarRecord<T>> GetLcpIdScalarRecords(BasisBladeSet basisSet);

        IEnumerable<IndexScalarRecord<T>> GetRcpIdScalarRecords(BasisBladeSet basisSet);

        IEnumerable<IndexScalarRecord<T>> GetHipIdScalarRecords(BasisBladeSet basisSet);

        IEnumerable<IndexScalarRecord<T>> GetFdpIdScalarRecords(BasisBladeSet basisSet);

        IEnumerable<IndexScalarRecord<T>> GetCpIdScalarRecords(BasisBladeSet basisSet);

        IEnumerable<IndexScalarRecord<T>> GetAcpIdScalarRecords(BasisBladeSet basisSet);
    }
}