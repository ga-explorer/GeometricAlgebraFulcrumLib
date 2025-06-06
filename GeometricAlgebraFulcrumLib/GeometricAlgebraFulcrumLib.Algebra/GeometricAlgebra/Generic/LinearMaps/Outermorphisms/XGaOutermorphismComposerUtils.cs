﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;

public static class XGaOutermorphismComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaLinearMapOutermorphism<T> CreateOutermorphism<T>(this XGaProcessor<T> processor, LinUnilinearMap<T> linearMap)
    {
        return new XGaLinearMapOutermorphism<T>(processor, linearMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaLinearMapOutermorphism<T> ToOutermorphism<T>(this LinUnilinearMap<T> linearMap, XGaProcessor<T> processor)
    {
        return new XGaLinearMapOutermorphism<T>(processor, linearMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaDiagonalOutermorphism<T> CreateDiagonalAutomorphism<T>(this XGaVector<T> diagonalVector)
    {
        return new XGaDiagonalOutermorphism<T>(diagonalVector);
    }
        
        
    public static XGaLinearMapOutermorphism<T> CreateClarkeRotationMap<T>(this XGaProcessor<T> processor, int vectorsCount)
    {
        var scalarProcessor = processor.ScalarProcessor;

        var clarkeMapArray =
            scalarProcessor.CreateClarkeRotationArray(vectorsCount);

        var basisVectorImagesDictionary = 
            new Dictionary<int, LinVector<T>>();

        for (var i = 0; i < vectorsCount; i++)
            basisVectorImagesDictionary.Add(
                i, 
                clarkeMapArray.ColumnToLinVector(scalarProcessor, i)
            );

        return scalarProcessor.CreateLinUnilinearMap(
            basisVectorImagesDictionary
        ).ToOutermorphism(processor);
    }

}