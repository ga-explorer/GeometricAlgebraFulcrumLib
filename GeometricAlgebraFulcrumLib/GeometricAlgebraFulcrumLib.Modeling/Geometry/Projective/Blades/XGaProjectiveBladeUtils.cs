using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Elements;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Blades;

public static class XGaProjectiveBladeUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEGaVector<T>(this XGaProjectiveBlade<T> blade)
    {
        return blade.ProjectiveSpace.IsValidEGaElement(blade.InternalKVector) &&
               blade.InternalKVector.IsVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEGaBivector<T>(this XGaProjectiveBlade<T> blade)
    {
        return blade.ProjectiveSpace.IsValidEGaElement(blade.InternalKVector) &&
               blade.InternalKVector.IsBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEGaTrivector<T>(this XGaProjectiveBlade<T> blade)
    {
        return blade.ProjectiveSpace.IsValidEGaElement(blade.InternalKVector) &&
               blade.InternalKVector.Grade == 3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEGaBlade<T>(this XGaProjectiveBlade<T> blade)
    {
        return blade.ProjectiveSpace.IsValidEGaElement(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPGaBlade<T>(this XGaProjectiveBlade<T> blade)
    {
        return blade.ProjectiveSpace.IsValidElement(blade.InternalKVector);
    }


    public static XGaProjectiveElementKind GetElementKind<T>(this XGaProjectiveBlade<T> blade)
    {
        var signature = blade.InternalKVector.SpSquared();

        if (signature.IsZero()) 
            return XGaProjectiveElementKind.Ideal;

        if (signature.IsNotZero())
            return XGaProjectiveElementKind.Euclidean; 

        throw new InvalidOperationException();
    }
    
    public static XGaProjectiveElementSpecs<T> GetElementSpecs<T>(this XGaProjectiveBlade<T> blade)
    {
        var projectiveSpace = blade.ProjectiveSpace;
        
        var signature = blade.InternalKVector.SpSquared();

        if (signature.IsZero())
            return new XGaProjectiveElementSpecs<T>(
                projectiveSpace,
                XGaProjectiveElementKind.Ideal,
                XGaProjectiveElementEncoding.PGa
            );

        if (signature.IsNotZero())
            return new XGaProjectiveElementSpecs<T>(
                projectiveSpace,
                XGaProjectiveElementKind.Euclidean,
                XGaProjectiveElementEncoding.PGa
            );

        throw new InvalidOperationException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaProjectiveBlade<T>> GetBasisBladesEGa<T>(this XGaProjectiveSpace<T> projectiveSpace)
    {
        return (1UL << (projectiveSpace.VSpaceDimensions - 1))
            .GetRange()
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id =>
                projectiveSpace
                    .ProjectiveProcessor
                    .KVectorTerm((id << 1).BitPatternToIndexSet())
                    .ToProjectiveBlade(projectiveSpace)
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaProjectiveBlade<T>> GetBasisBladesPGa<T>(this XGaProjectiveSpace<T> projectiveSpace)
    {
        return projectiveSpace
            .GaSpaceDimensions
            .GetRange()
            .OrderBy(id => id.Grade())
            .ThenBy(id => id)
            .Select(id => 
                projectiveSpace
                    .ProjectiveProcessor
                    .KVectorTerm(id.BitPatternToIndexSet())
                    .ToProjectiveBlade(projectiveSpace)
            );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Gp<T>(this XGaMultivector<T> mv, XGaProjectiveBlade<T> blade)
    {
        return mv.Gp(blade.InternalKVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveBlade<T> Op<T>(this IEnumerable<XGaProjectiveBlade<T>> bladeList)
    {
        return new XGaProjectiveBlade<T>(
            bladeList.First().ProjectiveSpace,
            bladeList.Select(blade => blade.InternalKVector).Op().GetFirstKVectorPart()
        );
    }
}