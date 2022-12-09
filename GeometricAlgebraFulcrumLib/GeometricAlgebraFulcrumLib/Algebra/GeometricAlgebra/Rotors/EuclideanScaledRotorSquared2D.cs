using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors
{
    public sealed class EuclideanScaledRotorSquared2D<T>
        : ScaledRotorBase<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static EuclideanScaledRotorSquared2D<T> Create(IGeometricAlgebraProcessor<T> processor, Scalar<T> scalar0)
        {
            return new EuclideanScaledRotorSquared2D<T>(processor, scalar0, processor.CreateScalarZero());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static EuclideanScaledRotorSquared2D<T> Create(IGeometricAlgebraProcessor<T> processor, Scalar<T> scalar0, Scalar<T> scalar12)
        {
            return new EuclideanScaledRotorSquared2D<T>(processor, scalar0, scalar12);
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static implicit operator Multivector<T>(ScaledPureRotor<T> rotor)
        //{
        //    return rotor.Multivector;
        //}


        public Scalar<T> Scalar0 { get; }

        public Scalar<T> Scalar12 { get; }


        private EuclideanScaledRotorSquared2D([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] Scalar<T> scalar0, [NotNull] Scalar<T> scalar12)
            : base(processor)
        {
            Scalar0 = scalar0;
            Scalar12 = scalar12;
        }


        public override bool IsValid()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EuclideanScaledRotorSquared2D<T> GetPureScaledRotorSquared2DInverse()
        {
            var scalingFactorSquared = GetScalingFactorSquared();

            return new EuclideanScaledRotorSquared2D<T>(
                GeometricProcessor,
                Scalar0 / scalingFactorSquared,
                -Scalar12 / scalingFactorSquared
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Scalar<T> GetScalingFactor()
        {
            return GetScalingFactorSquared().Sqrt();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetScalingFactorSquared()
        {
            return Scalar0 * Scalar0 + Scalar12 * Scalar12;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EuclideanScaledRotorSquared2D<T> GetPureRotorSquared2D()
        {
            var scalingFactor = 
                GetScalingFactor();

            return Create(
                GeometricProcessor,
                Scalar0 / scalingFactor,
                Scalar12 / scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IScaledRotor<T> GetScaledRotorInverse()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> OmMap(T u1, T u2)
        {
            var v1 = Scalar0 * u1 + Scalar12 * u2;
            var v2 = Scalar0 * u2 - Scalar12 * u1;

            return GeometricProcessor.CreateVector(v1, v2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaVector<T> OmMap(GaVector<T> mv)
        {
            var u1 = mv[0];
            var u2 = mv[1];

            return OmMap(u1, u2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaBivector<T> OmMap(GaBivector<T> mv)
        {
            return GetScalingFactor() * mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaKVector<T> OmMap(GaKVector<T> mv)
        {
            return mv.Grade switch
            {
                0 or 2 => GetScalingFactor() * mv,
                1 => OmMap(mv[0], mv[1]).AsKVector(),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> OmMap(GaMultivector<T> mv)
        {
            var mv0 = mv[0];
            var mv1 = mv[1];
            var mv2 = mv[2];
            var mv12 = mv[3];
            
            var scalingFactor = GetScalingFactor();

            var v0 = scalingFactor * mv0;
            var v1 = Scalar0 * mv1 + Scalar12 * mv2;
            var v2 = Scalar0 * mv2 - Scalar12 * mv1;
            var v12 = scalingFactor * mv12;

            return GeometricProcessor.CreateMultivector2D(v0, v1, v2, v12);
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public EuclideanScaledRotorSquared2D<T> GpSquared()
        //{
        //    var s0 = ScalarProcessor.Subtract(
        //        ScalarProcessor.Times(Scalar0, Scalar0),
        //        ScalarProcessor.Times(Scalar12, Scalar12)
        //    );

        //    var s12 = ScalarProcessor.Times(
        //        ScalarProcessor.ScalarTwo,
        //        ScalarProcessor.Times(Scalar0, Scalar12)
        //    );

        //    return new EuclideanScaledRotorSquared2D<T>(GeometricProcessor, s0, s12);
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EuclideanScaledRotorSquared2D<T> Gp(EuclideanScaledRotorSquared2D<T> rotor2)
        {
            var s0 = Scalar0 * rotor2.Scalar0 - Scalar12 * rotor2.Scalar12;
            var s12 = Scalar0 * rotor2.Scalar12 + Scalar12 * rotor2.Scalar0;

            return new EuclideanScaledRotorSquared2D<T>(GeometricProcessor, s0, s12);
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public VectorStorage<T> Gp(VectorStorage<T> vector)
        //{
        //    var u1 = ScalarProcessor.GetTermScalarByIndex(vector, 0);
        //    var u2 = ScalarProcessor.GetTermScalarByIndex(vector, 1);

        //    var v1 = ScalarProcessor.Add(
        //        ScalarProcessor.Times(Scalar0, u1),
        //        ScalarProcessor.Times(Scalar12, u2)
        //    );

        //    var v2 = ScalarProcessor.Subtract(
        //        ScalarProcessor.Times(Scalar0, u2),
        //        ScalarProcessor.Times(Scalar12, u1)
        //    );

        //    return GeometricProcessor.CreateVectorStorage(v1, v2);
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> GetMultivector()
        {
            return GeometricProcessor.CreateMultivector(
                GetMultivectorStorage()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> GetMultivectorReverse()
        {
            return GeometricProcessor.CreateMultivector(
                GetMultivectorStorageReverse()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> GetMultivectorInverse()
        {
            return GeometricProcessor.CreateMultivector(
                GetMultivectorStorageInverse()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorage()
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = Scalar0,
                [3] = Scalar12
            };

            return GeometricProcessor.CreateMultivectorStorageSparse(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorageReverse()
        {
            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = Scalar0,
                [3] = -Scalar12
            };

            return GeometricProcessor.CreateMultivectorStorageSparse(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IMultivectorStorage<T> GetMultivectorStorageInverse()
        {
            var scalingFactor = 
                GetScalingFactor();

            var idScalarDictionary = new Dictionary<ulong, T>()
            {
                [0] = Scalar0 / scalingFactor,
                [3] = -Scalar12 / scalingFactor
            };

            return GeometricProcessor.CreateMultivectorStorageSparse(idScalarDictionary);
        }
    }
}