using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors.ProductIterators;

public interface IRGaMultivectorTermsIterator<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }

    RGaProcessor<T> Processor { get; }

    RGaMultivector<T> Multivector1 { get; }

    RGaMultivector<T> Multivector2 { get; }


    IEnumerable<KeyValuePair<ulong, T>> GetOpIdScalarRecords();

    IEnumerable<KeyValuePair<ulong, T>> GetEGpIdScalarRecords();

    IEnumerable<KeyValuePair<ulong, T>> GetESpIdScalarRecords();

    IEnumerable<T> GetESpScalars();

    IEnumerable<KeyValuePair<ulong, T>> GetELcpIdScalarRecords();

    IEnumerable<KeyValuePair<ulong, T>> GetERcpIdScalarRecords();

    IEnumerable<KeyValuePair<ulong, T>> GetEHipIdScalarRecords();

    IEnumerable<KeyValuePair<ulong, T>> GetEFdpIdScalarRecords();

    IEnumerable<KeyValuePair<ulong, T>> GetECpIdScalarRecords();

    IEnumerable<KeyValuePair<ulong, T>> GetEAcpIdScalarRecords();


    IEnumerable<KeyValuePair<ulong, T>> GetGpIdScalarRecords();

    IEnumerable<KeyValuePair<ulong, T>> GetSpIdScalarRecords();

    IEnumerable<T> GetSpScalars();

    IEnumerable<KeyValuePair<ulong, T>> GetLcpIdScalarRecords();

    IEnumerable<KeyValuePair<ulong, T>> GetRcpIdScalarRecords();

    IEnumerable<KeyValuePair<ulong, T>> GetHipIdScalarRecords();

    IEnumerable<KeyValuePair<ulong, T>> GetFdpIdScalarRecords();

    IEnumerable<KeyValuePair<ulong, T>> GetCpIdScalarRecords();

    IEnumerable<KeyValuePair<ulong, T>> GetAcpIdScalarRecords();
}