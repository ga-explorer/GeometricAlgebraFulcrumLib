using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

public interface ITextComposer<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }

    string GetBasisVectorText(int index);

    string GetBasisBladeText(ulong id);

    string GetBasisBladeText(uint grade, ulong index);

    string GetBasisBladeText(IndexSet id);

    string GetBasisBladeText(IEnumerable<int> indexList);

    string GetBasisBladeText(RGaBasisBlade basisBlade);

    string GetBasisBladeText(XGaBasisBlade basisBlade);

    string GetAngleText(LinFloat64Angle angle);

    string GetAngleText(LinAngle<T> angle);

    string GetScalarText(Scalar<T> scalar);

    string GetScalarText(IScalar<T> scalar);

    string GetScalarText(double scalar);

    string GetScalarText(T scalar);

    string GetTermText(ulong id, double scalar);

    string GetTermText(ulong id, T scalar);

    string GetTermText(uint grade, int index, double scalar);

    string GetTermText(uint grade, int index, T scalar);

    string GetTermText(IndexSet id, double scalar);

    string GetTermText(IndexSet id, T scalar);

    string GetTermText(RGaBasisBlade basisBlade, double scalar);

    string GetTermText(RGaBasisBlade basisBlade, T scalar);

    string GetTermText(XGaBasisBlade basisBlade, double scalar);

    string GetTermText(XGaBasisBlade basisBlade, T scalar);

    string GetArrayText(IReadOnlyList<double> array);

    string GetArrayText(IReadOnlyList<T> array);

    string GetArrayText(double[,] array);

    string GetArrayText(T[,] array);

    string GetVectorText(LinFloat64Vector v);

    string GetVectorText(LinVector<T> v);

    string GetMultivectorText(RGaFloat64Multivector mv);

    string GetMultivectorText(RGaMultivector<T> mv);

    string GetMultivectorText(XGaFloat64Multivector mv);

    string GetMultivectorText(XGaMultivector<T> mv);
}