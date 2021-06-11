using System.Diagnostics;
using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Frames
{
    public readonly struct GaGeometricProductOfTermsResult<T>
    {
        public int Signature { get; }

        public ulong Id { get; }

        public int Grade => Id.BasisBladeGrade();

        public ulong Index => Id.BasisBladeIndex();

        public bool IsZero => Signature == 0;

        public T Scalar1 { get; }

        public T Scalar2 { get; }


        internal GaGeometricProductOfTermsResult(int signature, ulong id, T scalar1, T scalar2)
        {
            Debug.Assert(signature >= -1 && signature <= 1);

            Signature = signature;
            Id = id;
            Scalar1 = scalar1;
            Scalar2 = scalar2;
        }


        public T GetScalar(IGaScalarProcessor<T> scalarProcessor)
        {
            if (Signature == 0)
                return scalarProcessor.ZeroScalar;

            return Signature < 0 
                ? scalarProcessor.NegativeTimes(Scalar1, Scalar2)
                : scalarProcessor.Times(Scalar1, Scalar2);
        }
    }
}