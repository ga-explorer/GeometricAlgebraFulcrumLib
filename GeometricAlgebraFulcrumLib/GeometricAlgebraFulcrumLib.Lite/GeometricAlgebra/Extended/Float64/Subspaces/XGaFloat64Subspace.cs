using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Subspaces
{
    public sealed class XGaFloat64Subspace 
        : IXGaFloat64Subspace
    {
        private readonly XGaFloat64KVector _blade;
        private readonly XGaFloat64KVector _bladeInverse;
        private readonly XGaFloat64KVector _bladePseudoInverse;

        
        public XGaFloat64Processor Processor 
            => _blade.Processor;

        public XGaMetric Metric 
            => _blade.Metric;

        public int SubspaceDimension 
            => _blade.Grade;

        //public bool IsDirect { get; }

        //public bool IsDual 
        //    => !IsDirect;

        public double BladeSignature { get; }

        public bool IsValid
            => true;

        public bool IsInvalid 
            => false;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64Subspace(XGaFloat64KVector blade)
        {
            _blade = blade;
            BladeSignature = blade.SpSquared().Scalar();
            _bladeInverse = blade / BladeSignature;
            _bladePseudoInverse = blade.PseudoInverse();
        }
        

        bool IGeometricElement.IsValid()
        {
            throw new NotImplementedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector GetBlade()
        {
            return _blade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector GetBladeInverse()
        {
            return _bladeInverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector GetBladePseudoInverse()
        {
            return _bladePseudoInverse;
        }

        public IXGaFloat64Subspace Project(IXGaFloat64Subspace subspace)
        {
            var a = GetBlade();
            var aInv = GetBladePseudoInverse();
            var x = subspace.GetBlade();

            var blade =
                x.Lcp(aInv).Lcp(a);
            
            return new XGaFloat64Subspace(blade);
        }

        public IXGaFloat64Subspace Reflect(IXGaFloat64Subspace subspace)
        {
            var a = GetBlade();
            var aInv = GetBladeInverse();
            var x = subspace.GetBlade();

            // This assumes this subspace and the reflected subspace are represented using
            // direct blades
            //TODO: Implement all cases in table 7.1 page 201 in "Geometric Algebra for Computer Science"
            var isNegative = (x.Grade * (a.Grade + 1)).IsOdd();

            var axa = a.Gp(x).Gp(aInv).GetKVectorPart(subspace.SubspaceDimension);

            var blade = 
               isNegative ? -axa : axa;

            return new XGaFloat64Subspace(blade);
        }

        //public IGeoSubspace Rotate(IGeoSubspace subspace)
        //{
        //    if (Blade.Grade.IsOdd())
        //        throw new InvalidOperationException();

        //    //Debug.Assert(ScalarProcessor.IsOne(BladeSignature));

        //    var rotatedMv =
        //        GeometricProcessor.Gp(
        //            Blade,
        //            subspace.Blade, 
        //            BladeInverse
        //        ).GetKVectorPart(subspace.Blade.Grade);

        //    var blade = Blade.Grade.GradeHasNegativeReverse()
        //        ? GeometricProcessor.Negative(rotatedMv)
        //        : rotatedMv;

        //    return new GeoSubspace(GeometricProcessor, blade, subspace.IsDirect);
        //}

        public IXGaFloat64Subspace VersorProduct(IXGaFloat64Subspace subspace)
        {
            var a = GetBlade();
            var aInv = GetBladeInverse();
            var x = subspace.GetBlade();

            var subspaceBlade = subspace.GetBlade();

            var blade = 
                a.Gp(x).Gp(aInv).GetKVectorPart(subspaceBlade.Grade);

            return new XGaFloat64Subspace(blade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaFloat64Subspace Complement(IXGaFloat64Subspace subspace)
        {
            if (subspace.SubspaceDimension > SubspaceDimension)
                throw new InvalidOperationException();

            var blade = 
                subspace.GetBlade().Lcp(GetBladeInverse());

            return new XGaFloat64Subspace(blade);
        }
    }
}