using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis
{
    public sealed record BasisKVector 
        : BasisBlade
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BasisKVector Create(ulong id)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return new BasisKVector(id, grade, index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BasisKVector Create(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            return new BasisKVector(id, grade, index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BasisKVector Create(ulong id, uint grade, ulong index)
        {
            return new BasisKVector(id, grade, index);
        }


        public override ulong Id { get; }

        public override uint Grade { get; }

        public override ulong Index { get; }
        
        public override bool IsScalar => false;
        
        public override bool IsVector => false;
        
        public override bool IsBivector => false;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BasisKVector(ulong id, uint grade, ulong index)
        {
            Id = id;
            Grade = grade;
            Index = index;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<ulong> GetBasisVectorIndices()
        {
            return Id.BasisBladeIdToBasisVectorIndices();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return new StringBuilder()
                .Append('<')
                .Append(string.Join(',', GetBasisVectorIndices()))
                .Append('>')
                .ToString();
        }
    }
}