using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Reflectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Subspaces;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors
{
    public abstract partial class XGaMultivector<T>
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> ReflectOn(XGaKVector<T> subspace)
        {
            Debug.Assert(subspace.IsNearBlade());

            return subspace
                .Gp(this)
                .Gp(subspace.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> ReflectDirectOnDirect(XGaKVector<T> subspace)
        {
            return GetKVectorParts()
                .Select(kv => kv.ReflectDirectOnDirect(subspace))
                .Aggregate(
                    (XGaMultivector<T>)Processor.ScalarZero,
                    (a, b) => a.Add(b)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> ReflectDirectOnDual(XGaKVector<T> subspace)
        {
            return GetKVectorParts()
                .Select(kv => kv.ReflectDirectOnDual(subspace))
                .Aggregate(
                    (XGaMultivector<T>)Processor.ScalarZero,
                    (a, b) => a.Add(b)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> ReflectDualOnDirect(XGaKVector<T> subspace, int vSpaceDimensions)
        {
            return GetKVectorParts()
                .Select(kv => kv.ReflectDualOnDirect(subspace, vSpaceDimensions))
                .Aggregate(
                    (XGaMultivector<T>)Processor.ScalarZero,
                    (a, b) => a.Add(b)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> ReflectDualOnDual(XGaKVector<T> subspace)
        {
            return GetKVectorParts()
                .Select(kv => kv.ReflectDualOnDual(subspace))
                .Aggregate(
                    (XGaMultivector<T>)Processor.ScalarZero,
                    (a, b) => a.Add(b)
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> ProjectOn(XGaKVector<T> subspace, bool useSubspaceInverse)
        {
            Debug.Assert(subspace.IsNearBlade());

            var subspaceInverse =
                useSubspaceInverse
                    ? subspace.PseudoInverse()
                    : subspace;

            return Fdp(subspaceInverse).Gp(subspace);
        }

    }

    public abstract partial class XGaKVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ReflectOn(XGaKVector<T> subspace)
        {
            Debug.Assert(subspace.IsNearBlade());

            return subspace
                .Gp(this)
                .Gp(subspace.Inverse())
                .GetKVectorPart(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ReflectDirectOnDirect(XGaKVector<T> subspace)
        {
            var mv1 = ReflectOn(subspace);

            var n = Grade * (subspace.Grade + 1);

            return n.IsOdd() ? -mv1 : mv1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ReflectDirectOnDual(XGaKVector<T> subspace)
        {
            var mv1 = ReflectOn(subspace);

            var n = Grade * subspace.Grade;

            return n.IsOdd() ? -mv1 : mv1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ReflectDualOnDirect(XGaKVector<T> subspace, int vSpaceDimensions)
        {
            var mv1 = ReflectOn(subspace);

            var n = (Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

            return n.IsOdd() ? -mv1 : mv1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ReflectDualOnDual(XGaKVector<T> subspace)
        {
            var mv1 = ReflectOn(subspace);

            var n = (Grade + 1) * subspace.Grade;

            return n.IsOdd() ? -mv1 : mv1;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ProjectOn(XGaKVector<T> subspace, bool useSubspaceInverse)
        {
            Debug.Assert(subspace.IsNearBlade());

            var subspaceInverse =
                useSubspaceInverse
                    ? subspace.PseudoInverse()
                    : subspace;

            return Fdp(subspaceInverse).Gp(subspace).GetKVectorPart(Grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSubspace<T> ToSubspace()
        {
            return new XGaSubspace<T>(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSubspace<T> DualToSubspace(int vSpaceDimensions)
        {
            return new XGaSubspace<T>(
                Dual(vSpaceDimensions)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSubspace<T> UnDualToSubspace(int vSpaceDimensions)
        {
            return new XGaSubspace<T>(
                UnDual(vSpaceDimensions)
            );
        }
    }

    public sealed partial class XGaScalar<T>
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ReflectOn(XGaKVector<T> subspace)
        {
            Debug.Assert(subspace.IsNearBlade());

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ReflectDirectOnDirect(XGaKVector<T> subspace)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ReflectDirectOnDual(XGaKVector<T> subspace)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ReflectDualOnDirect(XGaKVector<T> subspace, int vSpaceDimensions)
        {
            var n = subspace.Grade + vSpaceDimensions;

            return n.IsOdd() ? -this : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ReflectDualOnDual(XGaKVector<T> subspace)
        {
            return subspace.IsOdd() ? -this : this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ProjectOn(XGaKVector<T> subspace, bool useSubspaceInverse)
        {
            Debug.Assert(subspace.IsNearBlade());

            var subspaceInverse =
                useSubspaceInverse
                    ? subspace.PseudoInverse()
                    : subspace;

            return Fdp(subspaceInverse).Gp(subspace).GetScalarPart();
        }

    }

    public sealed partial class XGaVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaPureReflector<T> ToPureReflector()
        {
            return XGaPureReflector<T>.Create(this);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> ReflectOn(XGaKVector<T> subspace)
        {
            Debug.Assert(subspace.IsNearBlade());

            return subspace
                .Gp(this)
                .Gp(subspace.Inverse())
                .GetVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> ReflectDirectOnDirect(XGaKVector<T> subspace)
        {
            var mv1 = ReflectOn(subspace);

            return subspace.IsEven() ? -mv1 : mv1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> ReflectDirectOnDual(XGaKVector<T> subspace)
        {
            var mv1 = ReflectOn(subspace);

            return subspace.IsOdd() ? -mv1 : mv1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> ReflectDualOnDirect(XGaKVector<T> subspace, int vSpaceDimensions)
        {
            var mv1 = ReflectOn(subspace);

            var n = vSpaceDimensions - 1;

            return n.IsOdd() ? -mv1 : mv1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> ReflectDualOnDual(XGaKVector<T> subspace)
        {
            return ReflectOn(subspace);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> ProjectOn(XGaKVector<T> subspace, bool useSubspaceInverse)
        {
            Debug.Assert(subspace.IsNearBlade());

            var subspaceInverse =
                useSubspaceInverse
                    ? subspace.PseudoInverse()
                    : subspace;

            return Fdp(subspaceInverse).Gp(subspace).GetVectorPart();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> ProjectOnVector(XGaVector<T> subspace)
        {
            return ProjectOn(subspace.ToSubspace());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> ProjectOnBivector(XGaBivector<T> subspace)
        {
            return ProjectOn(subspace.ToSubspace());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> ProjectOnKVector(XGaKVector<T> subspace)
        {
            return ProjectOn(subspace.ToSubspace());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> ProjectOn(XGaSubspace<T> subspace)
        {
            return subspace.Project(this);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> RejectOnVector(XGaVector<T> subspace)
        {
            return this - ProjectOn(subspace.ToSubspace());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> RejectOnBivector(XGaBivector<T> subspace)
        {
            return this - ProjectOn(subspace.ToSubspace());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> RejectOnKVector(XGaKVector<T> subspace)
        {
            return this - ProjectOn(subspace.ToSubspace());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> RejectOn(XGaSubspace<T> subspace)
        {
            return this - subspace.Project(this);
        }

    }

    public sealed partial class XGaBivector<T>
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> ReflectOn(XGaKVector<T> subspace)
        {
            Debug.Assert(subspace.IsNearBlade());

            return subspace
                .Gp(this)
                .Gp(subspace.Inverse())
                .GetBivectorPart();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> ReflectDirectOnDirect(XGaKVector<T> subspace)
        {
            return ReflectOn(subspace);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> ReflectDirectOnDual(XGaKVector<T> subspace)
        {
            return ReflectOn(subspace);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> ReflectDualOnDirect(XGaKVector<T> subspace, int vSpaceDimensions)
        {
            var mv1 = ReflectOn(subspace);

            var n = (Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

            return n.IsOdd() ? -mv1 : mv1;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> ReflectDualOnDual(XGaKVector<T> subspace)
        {
            var mv1 = ReflectOn(subspace);
        
            return subspace.IsOdd() ? -mv1 : mv1;
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> ProjectOn(XGaKVector<T> subspace, bool useSubspaceInverse)
        {
            Debug.Assert(subspace.IsNearBlade());
        
            var subspaceInverse = 
                useSubspaceInverse 
                    ? subspace.PseudoInverse() 
                    : subspace;

            return Fdp(subspaceInverse).Gp(subspace).GetBivectorPart();
        }

    }

    public sealed partial class XGaHigherKVector<T>
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> ReflectOn(XGaKVector<T> subspace)
        {
            Debug.Assert(subspace.IsNearBlade());

            return subspace
                .Gp(this)
                .Gp(subspace.Inverse())
                .GetHigherKVectorPart(Grade);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> ReflectDirectOnDirect(XGaKVector<T> subspace)
        {
            var mv1 = ReflectOn(subspace);

            var n = Grade * (subspace.Grade + 1);

            return n.IsOdd() ? -mv1 : mv1;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> ReflectDirectOnDual(XGaKVector<T> subspace)
        {
            var mv1 = ReflectOn(subspace);

            var n = Grade * subspace.Grade;

            return n.IsOdd() ? -mv1 : mv1;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> ReflectDualOnDirect(XGaKVector<T> subspace, int vSpaceDimensions)
        {
            var mv1 = ReflectOn(subspace);

            var n = (Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

            return n.IsOdd() ? -mv1 : mv1;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> ReflectDualOnDual(XGaKVector<T> subspace)
        {
            var mv1 = ReflectOn(subspace);

            var n = (Grade + 1) * subspace.Grade;

            return n.IsOdd() ? -mv1 : mv1;
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> ProjectOn(XGaKVector<T> subspace, bool useSubspaceInverse)
        {
            Debug.Assert(subspace.IsNearBlade());
        
            var subspaceInverse = 
                useSubspaceInverse 
                    ? subspace.PseudoInverse() 
                    : subspace;

            return Fdp(subspaceInverse).Gp(subspace).GetHigherKVectorPart(Grade);
        }

    }
}
