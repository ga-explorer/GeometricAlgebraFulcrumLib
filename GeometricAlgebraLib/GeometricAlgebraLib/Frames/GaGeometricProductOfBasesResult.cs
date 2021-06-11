using System.Diagnostics;

namespace GeometricAlgebraLib.Frames
{
    public readonly struct GaGeometricProductOfBasesResult
    {
        public int Signature { get; }

        public ulong Id { get; }

        public int Grade => Id.BasisBladeGrade();

        public ulong Index => Id.BasisBladeIndex();

        public bool IsZero => Signature == 0;


        internal GaGeometricProductOfBasesResult(int signature, ulong id)
        {
            Debug.Assert(signature >= -1 && signature <= 1);

            Signature = signature;
            Id = id;
        }
    }
}