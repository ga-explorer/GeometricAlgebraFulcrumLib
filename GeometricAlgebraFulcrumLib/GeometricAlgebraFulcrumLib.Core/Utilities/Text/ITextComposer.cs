using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.Utilities.Text;

public interface ITextComposer<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }

    string GetBasisVectorText(int index);

    string GetBasisBladeText(uint grade, ulong index);

    string GetBasisBladeText(IndexSet id);

    string GetBasisBladeText(IEnumerable<int> indexList);

    string GetBasisBladeText(XGaBasisBlade basisBlade);

    string GetAngleText(LinFloat64Angle angle);

    string GetAngleText(LinAngle<T> angle);

    string GetScalarText(Scalar<T> scalar);

    string GetScalarText(IScalar<T> scalar);

    string GetScalarText(double scalar);

    string GetScalarText(T scalar);

    string GetTermText(uint grade, int index, double scalar);

    string GetTermText(uint grade, int index, T scalar);

    string GetTermText(IndexSet id, double scalar);

    string GetTermText(IndexSet id, T scalar);

    string GetTermText(XGaBasisBlade basisBlade, double scalar);

    string GetTermText(XGaBasisBlade basisBlade, T scalar);

    string GetArrayText(IReadOnlyList<double> array);

    string GetArrayText(IReadOnlyList<T> array);

    string GetArrayText(double[,] array);

    string GetArrayText(T[,] array);

    string GetVectorText(LinFloat64Vector v);

    string GetVectorText(LinVector<T> v);

    string GetMultivectorText(XGaFloat64Multivector mv);

    string GetMultivectorText(XGaMultivector<T> mv);
}