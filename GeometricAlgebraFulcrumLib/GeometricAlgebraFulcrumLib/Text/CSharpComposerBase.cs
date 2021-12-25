using System;
using System.Collections.Generic;
using DataStructuresLib.Dictionary;
using NumericalGeometryLib.BasicMath;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Text
{
    public abstract class CSharpComposerBase<T>
        : ITextComposer<T>
    {
        protected TextLookupDictionary SymbolicToCodeScalarDictionary { get; }
            = new TextLookupDictionary();

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public Func<string, ulong, string> GetMultivectorScalarSymbolicNameFunc { get; set; }

        public Func<string, ulong, string> GetMultivectorScalarCodeNameFunc { get; set; }


        protected CSharpComposerBase([System.Diagnostics.CodeAnalysis.NotNull] IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            if (!geometricProcessor.IsSymbolic)
                throw new ArgumentException("The geometric processor must be symbolic");

            GeometricProcessor = geometricProcessor;
        }


        public Vector<T> DefineVector(string symbolicName, string codeName)
        {
            var scalarCount = (int) GeometricProcessor.VSpaceDimension;

            var scalarList = 
                LinVectorStorageFactory.CreateLinVectorArrayStorage<T>(scalarCount);

            for (var index = 0; index < scalarCount; index++)
            {
                var id = index.BasisVectorIndexToId();

                var scalarSymbolicName = GetMultivectorScalarSymbolicNameFunc(symbolicName, id);
                var scalarCodeName = GetMultivectorScalarCodeNameFunc(codeName, id);

                scalarList[index] = GeometricProcessor.GetScalarFromText(scalarSymbolicName);

                SymbolicToCodeScalarDictionary[scalarSymbolicName] = scalarCodeName;
            }

            return new Vector<T>(
                GeometricProcessor,
                VectorStorage<T>.CreateVector(scalarList)
            );
        }


        public string GetBasisVectorText(ulong index)
        {
            throw new System.NotImplementedException();
        }

        public string GetBasisBladeText(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public string GetBasisBladeText(uint grade, ulong index)
        {
            throw new System.NotImplementedException();
        }

        public string GetBasisBladeText(BasisBlade basisBlade)
        {
            throw new System.NotImplementedException();
        }

        public string GetBasisBladeText(IEnumerable<ulong> indexList)
        {
            throw new System.NotImplementedException();
        }

        public string GetAngleText(PlanarAngle angle)
        {
            throw new System.NotImplementedException();
        }

        public abstract string GetScalarText(T scalar);

        public string GetTermText(ulong id, T scalar)
        {
            throw new System.NotImplementedException();
        }

        public string GetTermText(uint grade, int index, T scalar)
        {
            throw new System.NotImplementedException();
        }

        public string GetTermText(uint grade, ulong index, T scalar)
        {
            throw new System.NotImplementedException();
        }

        public string GetTermText(IndexScalarRecord<T> idScalarPair)
        {
            throw new System.NotImplementedException();
        }

        public string GetTermText(GradeIndexScalarRecord<T> idScalarPair)
        {
            throw new System.NotImplementedException();
        }

        public string GetTermText(BasisBlade basisBlade, T scalar)
        {
            throw new System.NotImplementedException();
        }

        public string GetTermText(BasisTerm<T> term)
        {
            throw new System.NotImplementedException();
        }

        public string GetTermsText(IEnumerable<IndexScalarRecord<T>> idScalarTuples)
        {
            throw new System.NotImplementedException();
        }

        public string GetTermsText(IEnumerable<GradeIndexScalarRecord<T>> idScalarTuples)
        {
            throw new System.NotImplementedException();
        }

        public string GetTermsText(uint grade, IEnumerable<IndexScalarRecord<T>> indexScalarTuples)
        {
            throw new System.NotImplementedException();
        }

        public string GetTermsText(IEnumerable<BasisTerm<T>> terms)
        {
            throw new System.NotImplementedException();
        }

        public string GetArrayText(IReadOnlyList<T> array)
        {
            throw new System.NotImplementedException();
        }

        public string GetArrayText(T[,] array)
        {
            throw new System.NotImplementedException();
        }

        public string GetMultivectorText(IMultivectorStorage<T> storage)
        {
            throw new System.NotImplementedException();
        }
    }
}