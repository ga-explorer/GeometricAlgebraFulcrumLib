# Geometric Algebra Fulcrum Library (GA-FuL)

Geometric Algebra Fulcrum Library (GA-FuL for short) is a unified generic C# library for Geometric Algebra computations using any kind of scalars (floating point, symbolic, etc.). GA-FuL can be used for prototyping geometric algorithms based on the powerful mathematics of geometric algebra. The GA-FuL code base can be used to perform numeric computations, symbolic manipulations, and optimized code generation.

## Why The Name *Geometric Algebra Fulcrum*
**Geometric Algebra** is a powerful mathematical language that unifies many algebraic tools under the same framework of mathematical operations. Such tools include, for example, real vectors, complex numbers, quaternions, octanions, spinors, matrices, among others; along with their algebraic operations. The most important feature of GA is, however, that it unifies the geometric reasoning process among many seemingly diverse fields of application domains. Thus, **Geometric Algebra** acts as a **Fulcrum** for geometric reasoning across scientific and engineering domains. We can use this **Geometric Algebra Fulcrum** to seamlessly balance the ideal needs of geometric reasoning with the concrete tools of algebraic manipulation under a unifying mathematical framework.

On the other side, there is a need for software tools that act as a pivot point, i.e. a **Fulcrum**, for prototyping and implementing several kinds of computations on multivectors. Commonly required computations include numerical, symbolic, and code generation, among others. Writing and maintaining a separate code base for each kind is highly impractical. GA-FuL is intended to play the role of a **Fulcrum** for prototyping algorithms and implementing software based on **Geometric Algebra**.

## Design Intentions 
GA-FuL is intended to:
1. abstract and unify the representation and manipulation of multivectors as a layer of higher level generic computations on any kind of useful scalars (i.e. numbers).
2. represent multivectors internally as sparsely as possible using several suitable data structures to reduce memory requirements for high-dimensional geometric algebras (up to 64 dimensions).
3. provide a simple unified and generic Application Programming Interface (API) for several classes of applications, including, but not limited to, numerical computations, symbolic manipulations, and optimized code generation.
4. implement an extensible layered approach to allow users of different backgrounds to select the suitable level of coding they can handle, ranging from a very high-level of coordinate-independent prototyping using GA operations to a fully-controlled lower level of direct manipulation of scalars and coordinates.

## GA-FuL Layers

### 1. Storage Layer

The storage layer is the lowest, and largest, layer of code in GA-FuL. The main objective of the GA-FuL storage layer is to provide a set of generic classes for storing <a href="https://en.wikipedia.org/wiki/Scalar_(mathematics)" target="_blank">scalars</a>, <a href="https://en.wikipedia.org/wiki/Vector_(mathematics_and_physics)" target="_blank">vectors</a>, <a href="https://en.wikipedia.org/wiki/Matrix_(mathematics)" target="_blank">matrices</a>, <a href="https://en.wikipedia.org/wiki/Multivector" target="_blank">multivectors</a>, and <a href="https://en.wikipedia.org/wiki/Outermorphism" target="_blank">outermorphisms</a> at the coordinates level, as compactly as possible. The storage layer classes are located inside the <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/tree/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage" target="_blank">`GeometricAlgebraFulcrumLib.Storage`</a> name space.

#### 1.1 Storing Linear Algebra Vectors

The <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/LinearAlgebra/Vectors/ILinVectorStorage.cs" target="_blank">`ILinVectorStorage<T>`</a> generic interface provides a unified representation of classical dense and sparse vectors of linear algebra. In GA-FuL, a vector can be viewed as a set of **(index, scalar) tuples**, where the index implicitly represents a specific basis vector of an arbitrary <a href="https://en.wikipedia.org/wiki/Vector_space" target="_blank">linear space</a>. A GA-FuL vector as a whole implicitly represents a linear combination of basis vectors. Which basis is used depends on the usage context of the vector. In the storage layer, no information is retained regarding the specific linear space or the exact kind of scalars used.

The following is a list of important vector interfaces in the GA-FuL storage layer:

* <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/LinearAlgebra/Vectors/Dense/ILinVectorDenseStorage.cs" target="_blank">`ILinVectorDenseStorage<T>`</a> : Represents dense vectors of arbitrary length. The basis vector indices are not stored internally, only scalars are stored in 1-dimensional <a href="https://en.wikipedia.org/wiki/Array_data_type" target="_blank">array-like</a> data structures.
* <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/LinearAlgebra/Vectors/Sparse/ILinVectorSparseStorage.cs" target="_blank">`ILinVectorSparseStorage<T>`</a> : Represents sparse vectors of arbitrary length. The basis vector indices and scalars are stored internally in <a href="https://en.wikipedia.org/wiki/Associative_array" target="_blank">dictionary-like</a> data structures.
* <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/LinearAlgebra/Vectors/Graded/ILinVectorGradedStorage.cs" target="_blank">`ILinVectorGradedStorage<T>`</a> : This is mainly used to represent the graded structure of <a href="https://en.wikipedia.org/wiki/Multivector" target="_blank">Grassmann Numbers</a> in an <a href="https://en.wikipedia.org/wiki/Exterior_algebra" target="_blank">Exterior Algebra</a>. Each graded vector can be viewed as a set of **(grade, index, scalar)** tuples. Internally, the tuples are grouped by grade into (grade, <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/LinearAlgebra/Vectors/ILinVectorStorage.cs" target="_blank">`ILinVectorStorage<T>`</a>) structures.

#### 1.2 Storing Linear Algebra Matrices

Classes derived from the generic <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/LinearAlgebra/Matrices/ILinMatrixStorage.cs" target="_blank">`ILinMatrixStorage<T>`</a> interface represent dense and sparse matrices. A GA-FuL matrix is conceptually a set of **(row index, column index, scalar)** tuples. As in the case of vectors, no information is stored in this layer about the exact basis used for the matrices, only the index-scalar structure of the matrix is represented internally.

The following is a list of important GA-FuL matrix storage interfaces:

* <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/LinearAlgebra/Matrices/Dense/ILinMatrixDenseStorage.cs" target="_blank">`ILinMatrixDenseStorage<T>`</a> : Represents dense matrices of arbitrary size. The row-column indices are not stored internally, only scalars are stored in 2-dimensional <a href="https://en.wikipedia.org/wiki/Array_data_type" target="_blank">array-like</a> data structures.
* <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/LinearAlgebra/Matrices/Sparse/ILinMatrixSparseStorage.cs" target="_blank">`ILinMatrixSparseStorage<T>`</a> : Represents sparse matrices of arbitrary size. The row-column indices and scalars are stored internally in <a href="https://en.wikipedia.org/wiki/Associative_array" target="_blank">dictionary-like</a> data structures.
* <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/LinearAlgebra/Matrices/Graded/ILinMatrixGradedStorage.cs" target="_blank">`ILinMatrixGradedStorage<T>`</a> : This interface is mainly used to represent the graded structure of <a href="https://en.wikipedia.org/wiki/Outermorphism" target="_blank">Outermorphisms</a> in an <a href="https://en.wikipedia.org/wiki/Exterior_algebra" target="_blank">Exterior Algebra</a>. Each graded matrix can be viewed as a set of **(grade, row index, column index, scalar)** tuples. Internally, the tuples are grouped by grade into (grade, <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/LinearAlgebra/Matrices/ILinMatrixStorage.cs" target="_blank">`ILinMatrixStorage<T>`</a>) structures.

#### 1.3 Storing Multivectors

In <a href="https://en.wikipedia.org/wiki/Geometric_algebra" target="_blank">geometric algebra</a>, the space of multivectors is essentially a <a href="https://en.wikipedia.org/wiki/Graded_vector_space" target="_blank">graded linear space</a> with additional mathematical structure. A multivector is the sum of k-vectors, each of grade k. Each k-vector space is itself a smaller linear subspace of the larger multivectors linear space. 

In GA-FuL, the additional mathematical structure of GA multivectors is encoded in several classes which internally have <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/LinearAlgebra/Vectors/Graded/ILinVectorGradedStorage.cs" target="_blank">`ILinVectorGradedStorage<T>`</a> objects to hold the actual **(grade, index, scalar)** data:

* <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/GeometricAlgebra/VectorStorage.cs" target="_blank">`VectorStorage<T>`</a>, <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/GeometricAlgebra/BivectorStorage.cs" target="_blank">`BivectorStorage<T>`</a>, <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/GeometricAlgebra/KVectorStorage.cs" target="_blank">`KVectorStorage<T>`</a> : These classes represent GA vectors, bivectors, and general k-vectors. An object of such classes only contains internally a single-grade graded storage.
* <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/GeometricAlgebra/MultivectorGradedStorage.cs" target="_blank">`MultivectorGradedStorage<T>`</a> : This class represents more general multi-grade multivectors as a set of k-vectors.
* <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/GeometricAlgebra/MultivectorStorage.cs" target="_blank">`MultivectorStorage<T>`</a> : This class also represents general multivectors, but as a uniformly indexed set of terms.

#### 1.4 Storing Outermorphisms

In GA-FuL there are two kinds of outermorphisms: computed and stored. A stored outermorphism internally contains a graded matrix, with some additional operations. The <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Storage/GeometricAlgebra/OutermorphismStorage.cs" target="_blank">`OutermorphismStorage<T>`</a> class represents the graded matrix storage of a stored outermorphism. Computed outermorphisms are described in following sections.

### 2. Processors Layer

The second layer in GA-FuL is a collection of classes to implement processing operations on scalars, vectors, matrices, multivectors, and outermorphisms. Processing operations include creating storage objects for specific purposes, applying common algebraic operations (addition, subtraction, products, etc.) and converting between storage objects (for example converting a set of **(index, scalar)** terms into a vector, converting a graded matrix into a stored outermorphism, etc.) There are generally 4 kinds of processors in GA-FuL described in the following subsections.

#### 2.1 Scalar Algebra Processors

A scalar processor implements the generic interface <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Processors/ScalarAlgebra/IScalarAlgebraProcessor.cs" target="_blank">`IScalarAlgebraProcessor<T>`</a>. This interface implements basic operations on scalars, such as addition, subtraction, product, division, power, exponential, logarithm, and trigonometric functions. Additionally, many extension methods are defined in the GA-FuL utilities layer, described later, for more operations on scalars. The user can define a custom scalar processor by implementing the <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Processors/ScalarAlgebra/IScalarAlgebraProcessor.cs" target="_blank">`IScalarAlgebraProcessor<T>`</a> interface to any desired scalar type `T`.

There are some predefined scalar processors in GA-FuL such as:

* <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Processors/ScalarAlgebra/ScalarAlgebraFloat32Processor.cs" target="_blank">`ScalarAlgebraFloat32Processor`</a>, <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Processors/ScalarAlgebra/ScalarAlgebraFloat64Processor.cs" target="_blank">`ScalarAlgebraFloat64Processor`</a> : Scalar processors for the <a href="https://docs.oracle.com/cd/E19957-01/806-3568/ncg_goldberg.html" target="_blank">standard IEEE 32\64 bits floating point numbers</a>.
* <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Processors/ScalarAlgebra/ScalarAlgebraComplexProcessor.cs" target="_blank">`ScalarAlgebraComplexProcessor`</a> : A scalar processor for complex numbers based on the <a href="https://numerics.mathdotnet.com/" target="_blank">MathNet.Numerics</a> library.
* <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Processors/ScalarAlgebra/ScalarAlgebraSymbolicProcessor.cs" target="_blank">`ScalarAlgebraSymbolicProcessor`</a> : A scalar processor for symbolic scalar expressions based on the `Entity` class of the <a href="https://am.angouri.org/" target="_blank">AngouriMath</a> symbolic algebra library.
* <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib.Mathematica/Processors/ScalarAlgebraMathematicaProcessor.cs" target="_blank">`ScalarAlgebraMathematicaProcessor`</a> : A scalar processor for symbolic scalar expressions based on the <a href="https://reference.wolfram.com/language/NETLink/ref/net/Wolfram.NETLink.Expr.html" target="_blank">`Expr`</a> class of the <a href="https://www.wolfram.com/mathematica/" target="_blank">Wolfram Mathematica</a> computer algebra system. Here, `Expr` objects are assumed to represent symbolic scalars, i.e. not matrices, lists, Boolean values, etc.

#### 2.2 Linear Algebra Processors

In GA-FuL, a linear processor is an extended scalar processor which implements either one of the two generic interfaces <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Processors/LinearAlgebra/ILinearAlgebraProcessor.cs" target="_blank">`ILinearAlgebraProcessor<T>`</a> or <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Processors/LinearAlgebra/ILinearAlgebraProcessor.cs" target="_blank">`ILinearAlgebraProcessor<TMatrix, TScalar>`</a>. A linear processor performs common operations on linear algebra vector and matrix storage objects such as creation of matrix objects, accessing matrix elements, addition and subtraction of matrices and vectors, eigen decomposition of matrices, etc. 

The <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Processors/LinearAlgebra/ILinearAlgebraProcessor.cs" target="_blank">`ILinearAlgebraProcessor<TMatrix, TScalar>`</a> interface has two predefined class implementations:

* <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Processors/LinearAlgebra/LinearAlgebraFloat64Processor.cs" target="_blank">`LinearAlgebraFloat64Processor`</a> : Which is a wrapper around the <a href="https://numerics.mathdotnet.com/api/MathNet.Numerics.LinearAlgebra.Double/Matrix.htm" target="_blank">`MathNet.Numerics.LinearAlgebra.Double.Matrix`</a> class of the <a href="https://numerics.mathdotnet.com/" target="_blank">MathNet.Numerics</a> library.
* <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib.Mathematica/Processors/LinearAlgebraMathematicaProcessor.cs" target="_blank">`LinearAlgebraMathematicaProcessor`</a> : Which is also a wrapper around the `Expr` class of the Wofram Mathematica symbolic algebra system. Here, `Expr` objects are assumed to represent symbolic matrices.

The other interface, <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Processors/LinearAlgebra/ILinearAlgebraProcessor.cs" target="_blank">`ILinearAlgebraProcessor<T>`</a>, has a built-in implementation in GA-FuL; the <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Processors/LinearAlgebra/LinearAlgebraProcessor.cs" target="_blank">`LinearAlgebraProcessor<T>`</a> generic class. This class is a simpler generic version for processing GA-FuL linear algebra vector and matrix storage objects. Most of the basic geometric algebra processing operations can be done using this class.

The other built-in implementation of the <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Processors/LinearAlgebra/ILinearAlgebraProcessor.cs" target="_blank">`ILinearAlgebraProcessor<T>`</a> interface is the <a href="https://github.com/ga-explorer/GeometricAlgebraFulcrumLib/blob/main/GeometricAlgebraFulcrumLib/GeometricAlgebraFulcrumLib/Processors/SymbolicAlgebra/Context/SymbolicContext.cs" target="_blank">`SymbolicContext`</a> class, which is the core symbolic processing class for optimized code generation in GA-FuL.

#### 2.3 Geometric Algebra Processors



#### 2.4 Symbolic Algebra Processors

### 3. Algebra Layer

#### 3.1 Scalar Algebra Classes

#### 3.2 Linear Algebra Classes

#### 3.3 Geometric Algebra Classes

#### 3.4 Symbolic Algebra Classes

### 4. Geometry Layer

### 5. Utilities Layer

#### 5.1 Structure Classes

#### 5.2 Extension Utility Classes

#### 5.3 Factory Classes

#### 5.4 Composer Classes

## Examples

Typically, GA-FuL requires selecting a *processor* when making computations on multivectors. A processor is a special object capable of performing basic operations on scalars such as addition, subtraction, multiplication, division, negation, in addition to basic functions (sin, cos, exp, log, etc.). In GA-FuL, all processors implement the generic interface `IGaScalarProcessor<T>` under the `GeometricAlgebraFulcrumLib.Processing.Scalars` namespace, where `T` is the type of scalar used. Common operations on multivectors (for example the geometric and other products, the reverse, and grade involution) are all defined in terms of the methods of interface `IGaScalarProcessor<T>`. A processor can also be used for constructing multivectors of various kinds (scalars, vectors, bivectors, k-vectors, and general multivectors). Each kind of scalars has its own processor, and you can define your own processor for any new kind of scalars by properly implementing the `IGaScalarProcessor<T>` interface. 

### Numerical Geometric Algebra Computations

The following example shows how to use simplest of processors: the 64-bits floating point scalar processor. More examples can be found under the `GeometricAlgebraFulcrumLib.Samples.Numeric` namespace.

```csharp
using System;
using GeometricAlgebraFulcrumLib.Processing.Implementations.Float64;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.TextComposers;

namespace GeometricAlgebraFulcrumLib.Samples.Numeric
{
    public static class Sample1
    {
        public static void Execute()
        {
            // This is a pre-defined scalar processor for the standard
            // 64-bit floating point scalars
            var processor = GaScalarProcessorFloat64.DefaultProcessor;

            // This is a pre-defined text generator for displaying multivectors
            // with 64-bit floating point scalars
            var textComposer = GaTextComposerFloat64.DefaultComposer;

            // This is a pre-defined LaTeX generator for displaying multivectors
            // with 64-bit floating point scalars
            var latexComposer = GaLaTeXComposerFloat64.DefaultComposer;

            // Create two vectors each having 3 components (a 3-dimensional GA)
            var u = processor.CreateVector(1.2, -1, 1.25);
            var v = processor.CreateVector(2.1, 0.9, 2.1);

            // Compute their outer product as a bivector
            var bv = u.Op(v);

            // Display a text representation of the vectors and their outer product
            Console.WriteLine($@"u = {textComposer.GetMultivectorText(u)}");
            Console.WriteLine($@"v = {textComposer.GetMultivectorText(v)}");
            Console.WriteLine($@"u op v = {textComposer.GetMultivectorText(bv)}");
            Console.WriteLine();

            // Display a LaTeX representation of the vectors and their outer product
            Console.WriteLine($@"\boldsymbol{{u}} = {latexComposer.GetMultivectorText(u)}");
            Console.WriteLine($@"\boldsymbol{{v}} = {latexComposer.GetMultivectorText(v)}");
            Console.WriteLine($@"\boldsymbol{{u}}\wedge\boldsymbol{{v}} = {latexComposer.GetMultivectorText(bv)}");
            Console.WriteLine();
        }
    }
}
```

The final output is:

```plaintext
u = '1.2'<1>, '-1'<2>, '1.25'<3>
v = '2.1'<1>, '0.9'<2>, '2.1'<3>
u op v = '3.18'<1, 2>, '-0.105'<1, 3>, '-3.225'<2, 3>

\boldsymbol{u} = \left( 1.2 \right) \boldsymbol{e}_{1} + \left( -1 \right) \boldsymbol{e}_{2} + \left( 1.25 \right) \boldsymbol{e}_{3}
\boldsymbol{v} = \left( 2.1 \right) \boldsymbol{e}_{1} + \left( 0.9 \right) \boldsymbol{e}_{2} + \left( 2.1 \right) \boldsymbol{e}_{3}
\boldsymbol{u}\wedge\boldsymbol{v} = \left( 3.18 \right) \boldsymbol{e}_{1,2} + \left( -0.105 \right) \boldsymbol{e}_{1,3} + \left( -3.225 \right) \boldsymbol{e}_{2,3}
```

### Symbolic Geometric Algebra Computations

The following example shows how to use the pre-defined symbolic processors. This processor depends on <a href="https://www.wolfram.com/mathematica/" target="_blank">Wolfram Mathematica</a> and its <a href="https://reference.wolfram.com/language/NETLink/ref/net/Wolfram.NETLink.Expr.html" target="_blank">Expr object</a> for manipulating general scalar symbolic expressions. More examples can be found under the `GeometricAlgebraFulcrumLib.Samples.Symbolic` namespace. Note that the code is almost identical to the numeric example above.

```csharp
using System;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica;
using GeometricAlgebraFulcrumLib.Symbolic.Processors;
using GeometricAlgebraFulcrumLib.Symbolic.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic
{
    public static class Sample1
    {
        public static void Execute()
        {
            // This is a pre-defined scalar processor for the symbolic
            // Wolfram Mathematica scalars using Expr objects
            var processor = GaScalarProcessorMathematicaExpr.DefaultProcessor;

            // This is a pre-defined text generator for displaying multivectors
            // with symbolic Wolfram Mathematica scalars using Expr objects
            var textComposer = GaTextComposerMathematicaExpr.DefaultComposer;

            // This is a pre-defined LaTeX generator for displaying multivectors
            // with symbolic Wolfram Mathematica scalars using Expr objects
            var latexComposer = GaLaTeXComposerMathematicaExpr.DefaultComposer;

            // Create two vectors each having 3 components (a 3-dimensional GA)
            var u = processor.CreateVector(3, i => $"Subscript[u,{i + 1}]".ToExpr());
            var v = processor.CreateVector(3, i => $"Subscript[v,{i + 1}]".ToExpr());

            // Compute their outer product as a bivector
            var bv = u.Op(v);

            // Display a text representation of the vectors and their outer product
            Console.WriteLine($@"u = {textComposer.GetMultivectorText(u)}");
            Console.WriteLine($@"v = {textComposer.GetMultivectorText(v)}");
            Console.WriteLine($@"u op v = {textComposer.GetMultivectorText(bv)}");
            Console.WriteLine();

            // Display a LaTeX representation of the vectors and their outer product
            Console.WriteLine($@"\boldsymbol{{u}} = {latexComposer.GetMultivectorText(u)}");
            Console.WriteLine($@"\boldsymbol{{v}} = {latexComposer.GetMultivectorText(v)}");
            Console.WriteLine($@"\boldsymbol{{u}}\wedge\boldsymbol{{v}} = {latexComposer.GetMultivectorText(bv)}");
            Console.WriteLine();
        }
    }
}
```

The final output is:

```plaintext
v1 = 'Subscript[u,1]'<1>, 'Subscript[u,2]'<2>, 'Subscript[u,3]'<3>
v2 = 'Subscript[v,1]'<1>, 'Subscript[v,2]'<2>, 'Subscript[v,3]'<3>
v1 op v2 = 'Plus[Times[-1,Subscript[u,2],Subscript[v,1]],Times[Subscript[u,1],Subscript[v,2]]]'<1, 2>, 'Plus[Times[-1,Subscript[u,3],Subscript[v,1]],Times[Subscript[u,1],Subscript[v,3]]]'<1, 3>, 'Plus[Times[-1,Subscript[u,3],Subscript[v,2]],Times[Subscript[u,2],Subscript[v,3]]]'<2, 3>

\boldsymbol{u} = \left( u_1 \right) \boldsymbol{e}_{1} + \left( u_2 \right) \boldsymbol{e}_{2} + \left( u_3 \right) \boldsymbol{e}_{3}
\boldsymbol{v} = \left( v_1 \right) \boldsymbol{e}_{1} + \left( v_2 \right) \boldsymbol{e}_{2} + \left( v_3 \right) \boldsymbol{e}_{3}
\boldsymbol{u}\wedge\boldsymbol{v} = \left( u_1 v_2-u_2 v_1 \right) \boldsymbol{e}_{1,2} + \left( u_1 v_3-u_3 v_1 \right) \boldsymbol{e}_{1,3} + \left( u_2 v_3-u_3 v_2 \right) \boldsymbol{e}_{2,3}
```

### Code Generation using Geometric Algebra

The code generation capabilities of GA-FuL are comprehensive and sophisticated. They range from generating code for a single operation on multivectors to generating a full software library with nested folder\file structure and proper software architecture. The following example illustrates the bare minimum for generating an internal code representation for the outer product of two vectors in 3-dimensions. The differences between this code and the previous numeric and symbolic examples is mainly due to the special requirements imposed by code generation. These are not related to GA and its multivectors, but rather to concepts of <a href="https://en.wikipedia.org/wiki/Automatic_programming" target="_blank">Automatic Programming</a>.

```csharp
using System;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.CodeComposer.Composers;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Geometry.Rotors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Symbolic;

namespace GeometricAlgebraFulcrumLib.Samples.CodeComposer
{
    public static class Sample1
    {
        public static void Execute()
        {
            // The number of dimensions
            const int n = 3;

            // Stage 1: Define the symbolic context
            // The symbolic context is a special kind of processor for symbolic multivector
            // assignments
            var context = 
                new SymbolicContext()
                {
                    MergeExpressions = false,
                    ContextOptions = { ContextName = "TestCode" }
                };

            context.AttachMathematicaExprSimplifier();

            // Define a Euclidean multivectors processor for the context
            var processor = 
                context.CreateEuclideanProcessor(n);

            // Stage 2: Define the input parameters of the context
            // The input parameters are named variables created as scalar parts of multivectors
            // and used for later processing to compute some outputs

            // Define the first vector with a given set of scalar components u1, u2, ...
            var u =
                context.ParameterVariablesFactory.CreateDenseVector(
                    n, 
                    index => $"u{index + 1}"
                );

            // Define the second vector with a given set of scalar components v1, v2, ...
            var v =
                context.ParameterVariablesFactory.CreateDenseVector(
                    n, 
                    index => $"v{index + 1}"
                );

            // Define the 3rd vector with a given set of scalar components x1, x2, ...
            var x =
                context.ParameterVariablesFactory.CreateDenseVector(
                    n, 
                    index => $"x{index + 1}"
                );

            // Stage 3: Define computations and specify which variables are outputs
            var rotor = processor.CreateEuclideanRotor(u, v, true);
            
            // Here is another method for making the same computation using a rotation matrix
            //var rotor = u.CreateRotationMatrixToVector(v, n);

            var xRotated = 
                rotor.MapVector(x);

            // Define the final outputs for the computations for proper code generation
            xRotated.SetIsOutput(true);

            // Stage 4: Optimize computations in the symbolic context
            context.OptimizeContext();

            // Stage 5: Assign code generated variable names for all variables
            // Define code generated variable names for input variables
            v.SetExternalNamesByTermId(id => $"v.Scalar{id.PatternToString(n)}");
            u.SetExternalNamesByTermId(id => $"u.Scalar{id.PatternToString(n)}");
            x.SetExternalNamesByTermId(id => $"x.Scalar{id.PatternToString(n)}");
            
            // Define code generated variable names for output variables
            xRotated.SetExternalNamesByTermId(id => $"xRotated.Scalar{id.PatternToString(n)}");

            // Define code generated variable names for intermediate variables
            context.SetIntermediateExternalNamesByNameIndex(index => $"temp{index}");

            // Stage 6: Define a C# code composer with Wolfram Mathematica expressions converter
            var contextCodeComposer = context.CreateContextCodeComposer(
                GaLanguageServer.CSharpWithMathematica()
            );

            // Stage 7: Generate the final C# code
            var code = contextCodeComposer.Generate();

            Console.WriteLine("Generated Code:");
            Console.WriteLine(code);
            Console.WriteLine();
        }
    }
}
```

The final output is:

```plaintext
Generated Code:
//Begin GA-FuL Symbolic Context Code Generation, 2021-08-10T03:17:45.0913697+02:00
//SymbolicContext: TestCode
//Input Variables: 9 used, 0 not used, 9 total.
//Temp Variables: 91 sub-expressions, 0 generated temps, 91 total.
//Target Temp Variables: 11 total.
//Output Variables: 3 total.
//Computations: 1 average, 94 total.
//Memory Reads: 1.6702127659574468 average, 157 total.
//Memory Writes: 94 total.
//
//SymbolicContext Binding Data:
//   1 = constant: '1'
//   -1 = constant: '-1'
//   2 = constant: '2'
//   Rational[1,2] = constant: 'Rational[1,2]'
//   u1 = parameter: u.Scalar001
//   u2 = parameter: u.Scalar010
//   u3 = parameter: u.Scalar100
//   v1 = parameter: v.Scalar001
//   v2 = parameter: v.Scalar010
//   v3 = parameter: v.Scalar100
//   x1 = parameter: x.Scalar001
//   x2 = parameter: x.Scalar010
//   x3 = parameter: x.Scalar100

var temp0 = u.Scalar001 * v.Scalar001;
var temp1 = u.Scalar010 * v.Scalar010;
temp0 = temp0 + temp1;
temp1 = u.Scalar100 * v.Scalar100;
temp0 = temp0 + temp1;
temp1 = -1 * temp0;
temp1 = 1 + temp1;
temp1 = Rational[1,2] * temp1;
temp1 = Math.Pow(temp1, Rational[1,2]);
var temp2 = u.Scalar100 * v.Scalar001;
var temp3 = u.Scalar001 * v.Scalar100;
temp3 = -1 * temp3;
temp2 = temp2 + temp3;
temp3 = u.Scalar010 * v.Scalar001;
var temp4 = u.Scalar001 * v.Scalar010;
temp4 = -1 * temp4;
temp3 = temp3 + temp4;
temp4 = temp3 * temp3;
temp4 = -1 * temp4;
var temp5 = temp2 * temp2;
temp5 = -1 * temp5;
temp4 = temp4 + temp5;
temp5 = u.Scalar100 * v.Scalar010;
var temp6 = u.Scalar010 * v.Scalar100;
temp6 = -1 * temp6;
temp5 = temp5 + temp6;
temp6 = temp5 * temp5;
temp6 = -1 * temp6;
temp4 = temp4 + temp6;
temp4 = -1 * temp4;
temp4 = Math.Pow(temp4, Rational[1,2]);
temp4 = 1 / temp4;
temp2 = temp2 * temp4;
temp2 = temp1 * temp2;
temp6 = -1 * temp2;
temp0 = 1 + temp0;
temp0 = Rational[1,2] * temp0;
temp0 = Math.Pow(temp0, Rational[1,2]);
var temp7 = temp0 * x.Scalar001;
temp3 = temp3 * temp4;
temp3 = temp1 * temp3;
var temp8 = temp3 * x.Scalar010;
temp7 = temp7 + temp8;
temp8 = temp2 * x.Scalar100;
temp7 = temp7 + temp8;
temp8 = temp6 * temp7;
temp4 = temp5 * temp4;
temp1 = temp1 * temp4;
temp4 = -1 * temp1;
temp5 = temp0 * x.Scalar010;
var temp9 = temp3 * x.Scalar001;
temp9 = -1 * temp9;
temp5 = temp5 + temp9;
temp9 = temp1 * x.Scalar100;
temp5 = temp5 + temp9;
temp9 = temp4 * temp5;
temp8 = temp8 + temp9;
temp9 = temp0 * x.Scalar100;
var temp10 = temp2 * x.Scalar001;
temp10 = -1 * temp10;
temp9 = temp9 + temp10;
temp10 = temp1 * x.Scalar010;
temp10 = -1 * temp10;
temp9 = temp9 + temp10;
temp10 = temp0 * temp9;
temp8 = temp8 + temp10;
temp10 = -1 * temp3;
temp3 = temp3 * x.Scalar100;
temp2 = temp2 * x.Scalar010;
temp2 = -1 * temp2;
temp2 = temp3 + temp2;
temp1 = temp1 * x.Scalar001;
temp1 = temp2 + temp1;
temp2 = temp10 * temp1;
temp2 = -1 * temp2;
xRotated.Scalar100 = temp8 + temp2;

temp2 = temp7 * temp10;
temp3 = temp0 * temp5;
temp2 = temp2 + temp3;
temp3 = temp4 * temp9;
temp3 = -1 * temp3;
temp2 = temp2 + temp3;
temp3 = temp6 * temp1;
xRotated.Scalar010 = temp2 + temp3;

temp0 = temp0 * temp7;
temp2 = temp5 * temp10;
temp2 = -1 * temp2;
temp0 = temp0 + temp2;
temp2 = temp6 * temp9;
temp2 = -1 * temp2;
temp0 = temp0 + temp2;
temp1 = temp4 * temp1;
temp1 = -1 * temp1;
xRotated.Scalar001 = temp0 + temp1;

//Finish GA-FuL Symbolic Context Code Generation, 2021-08-10T03:17:45.1324377+02:00
```