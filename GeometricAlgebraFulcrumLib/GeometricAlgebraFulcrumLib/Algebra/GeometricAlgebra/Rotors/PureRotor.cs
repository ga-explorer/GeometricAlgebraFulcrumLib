using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors
{
    /// <summary>
    /// A pure rotor is the exponential of a 2-blade. The geometric product of
    /// the rotor with its reverse is one. The squared norm of the 2-blade could either
    /// be positive, zero, or negative. Each case has its own formulation for the exponential
    /// See Section 7.4 of "Geometric Algebra for Computer Science"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class PureRotor<T>
        : Rotor<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PureRotor<T> Create(IGeometricAlgebraProcessor<T> processor, T scalarPart, BivectorStorage<T> bivectorPart)
        {
            return new PureRotor<T>(
                processor,
                processor.Add(scalarPart, bivectorPart),
                processor.Subtract(scalarPart, bivectorPart)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new static PureRotor<T> Create(IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> multivector)
        {
            return new PureRotor<T>(
                processor,
                multivector,
                processor.Reverse(multivector)
            );
        }


        internal PureRotor([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] T scalarPart, [NotNull] BivectorStorage<T> bivectorPart)
            : base(processor, processor.Add(scalarPart, bivectorPart), processor.Subtract(scalarPart, bivectorPart))
        {
        }

        private PureRotor([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] IMultivectorStorage<T> multivector, [NotNull] IMultivectorStorage<T> multivectorReverse)
            : base(processor, multivector, multivectorReverse)
        {
        }


        public override bool IsValid()
        {
            // Make sure the storage and its reverse are correct
            if (!GeometricProcessor.IsNearZero(GeometricProcessor.Subtract(GeometricProcessor.Reverse(Multivector), MultivectorReverse)))
                return false;

            // Make sure storage contains only terms of grades 0,2
            if ((Multivector.GetStoredGradesBitPattern() | 5UL) != 5UL)
                return false;

            // Make sure storage gp reverse(storage) == 1
            var gp = 
                GeometricProcessor.EGp(Multivector, MultivectorReverse);

            if (!gp.IsScalar())
                return false;

            var diff =
                GeometricProcessor.Subtract(
                    GeometricProcessor.GetTermScalar(gp, 0),
                    GeometricProcessor.ScalarOne
                );

            return GeometricProcessor.IsNearZero(diff);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureRotor<T> GetPureRotorReverse()
        {
            return new PureRotor<T>(
                GeometricProcessor, 
                MultivectorReverse, 
                Multivector
            );
        }
    }
}