using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry.Multivectors
{
    public sealed class GaVector<T>
        : IGaGeometry<T>
    {
        public static GaVector<T> Create(IGaProcessor<T> processor, IGasVector<T> storage)
        {
            return new GaVector<T>(processor, storage);
        }


        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public IGaProcessor<T> Processor { get; }

        public IGasVector<T> Storage { get; }
        
        public bool IsValid 
            => Storage
                .GetScalars()
                .All(Storage.ScalarProcessor.IsValid);

        public bool IsInvalid 
            => Storage
                .GetScalars()
                .Any(s => !Storage.ScalarProcessor.IsValid(s));


        private GaVector([NotNull] IGaProcessor<T> processor, [NotNull] IGasVector<T> storage)
        {
            Processor = processor;
            Storage = storage;
        }


        public GaVector<T> GetRotatedVector(IGaRotor<T> rotor)
        {
            return new GaVector<T>(
                Processor,
                rotor.MapVector(Storage).GetVectorPart()
            );
        }

        public GaVector<T> GetProjectedVector(GaSubspace<T> subspace)
        {
            return new GaVector<T>(
                Processor,
                subspace.Project(Storage).GetVectorPart()
            );
        }
    }
}