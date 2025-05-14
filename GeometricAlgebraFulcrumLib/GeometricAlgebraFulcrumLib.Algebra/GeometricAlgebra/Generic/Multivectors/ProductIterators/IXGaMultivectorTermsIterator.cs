using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.ProductIterators;

public interface IXGaMultivectorTermsIterator<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }

    XGaProcessor<T> Processor { get; }

    XGaMultivector<T> Multivector1 { get; }

    XGaMultivector<T> Multivector2 { get; }


    IEnumerable<KeyValuePair<IndexSet, T>> GetOpIdScalarRecords();

    IEnumerable<KeyValuePair<IndexSet, T>> GetEGpIdScalarRecords();

    IEnumerable<KeyValuePair<IndexSet, T>> GetESpIdScalarRecords();

    IEnumerable<T> GetESpScalars();

    IEnumerable<KeyValuePair<IndexSet, T>> GetELcpIdScalarRecords();

    IEnumerable<KeyValuePair<IndexSet, T>> GetERcpIdScalarRecords();

    IEnumerable<KeyValuePair<IndexSet, T>> GetEHipIdScalarRecords();

    IEnumerable<KeyValuePair<IndexSet, T>> GetEFdpIdScalarRecords();

    IEnumerable<KeyValuePair<IndexSet, T>> GetECpIdScalarRecords();

    IEnumerable<KeyValuePair<IndexSet, T>> GetEAcpIdScalarRecords();


    IEnumerable<KeyValuePair<IndexSet, T>> GetGpIdScalarRecords();

    IEnumerable<KeyValuePair<IndexSet, T>> GetSpIdScalarRecords();

    IEnumerable<T> GetSpScalars();

    IEnumerable<KeyValuePair<IndexSet, T>> GetLcpIdScalarRecords();

    IEnumerable<KeyValuePair<IndexSet, T>> GetRcpIdScalarRecords();

    IEnumerable<KeyValuePair<IndexSet, T>> GetHipIdScalarRecords();

    IEnumerable<KeyValuePair<IndexSet, T>> GetFdpIdScalarRecords();

    IEnumerable<KeyValuePair<IndexSet, T>> GetCpIdScalarRecords();

    IEnumerable<KeyValuePair<IndexSet, T>> GetAcpIdScalarRecords();
}