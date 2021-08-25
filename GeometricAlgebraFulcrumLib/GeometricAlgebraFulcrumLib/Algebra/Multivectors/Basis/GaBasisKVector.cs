using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis
{
    public sealed record GaBasisKVector 
        : GaBasisBlade
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GaBasisKVector Create(ulong id)
        {
            id.BasisBladeIdToGradeIndex(out var grade, out var index);

            return new GaBasisKVector(id, grade, index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GaBasisKVector Create(uint grade, ulong index)
        {
            var id = index.BasisBladeIndexToId(grade);

            return new GaBasisKVector(id, grade, index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static GaBasisKVector Create(ulong id, uint grade, ulong index)
        {
            return new GaBasisKVector(id, grade, index);
        }


        public override ulong Id { get; }

        public override uint Grade { get; }

        public override ulong Index { get; }
        
        public override bool IsScalar => false;
        
        public override bool IsVector => false;
        
        public override bool IsBivector => false;


        private GaBasisKVector(ulong id, uint grade, ulong index)
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