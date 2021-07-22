# Geometric Algebra Fulcrum Library (GA-FuL)
Geometric Algebra Fulcrum Library (GA-FuL for short) is a unified generic C# library for Geometric Algebra computations using any kind of scalars (floating point, symbolic, etc.). GA-FuL can be used for prototyping geometric algorithms based on the powerful mathematics of geometric algebra. The GA-FuL code base can be used to perform numeric computations, symbolic manipulations, and optimized code generation.

## Why The Name *Geometric Algebra Fulcrum*
**Geometric Algebra** is a powerful mathematical language that unifies many algebraic tools under the same framework of mathematical operations. Such tools include, for example, real vectors, complex numbers, quaternions, octanions, spinors, matrices, among others; along with their algebraic operations. The most important feature of GA is, however, that it unifies the geometric reasoning process among many seemingly diverse fields of application domains. Thus, **Geometric Algebra** acts as a **Fulcrum** for geometric reasoning across scientific and engineering domains. We can use this **Geometric Algebra Fulcrum** to seamlessly balance the ideal needs of geometric reasoning with the concrete tools of algebraic manipulation under a unifying mathematical framework.

On the other side, there is a need for software tools that act as a pivot point, i.e. a **Fulcrum**, for prototyping and implementing several kinds of computations on multivectors. Commonly required computations include numerical, symbolic, and code generation, among others. Writing and maintaining a separate code base for each kind is highly impractical. GA-FuL is intended to play the role of a **Fulcrum** for prototyping algorithms and implementing software based on **Geometric Algebra**.

## Design Intentions 
GA-FuL is intended to:
1. abstract and unify the representation and manipulation of multivectors as a layer of higher level generic computations on any kind of useful scalars (i.e. numbers).
2. represent multivectors internally as sparsely as possible using several suitable data structures to reduce memory requirements for high-dimensional geometric algebras (up to 64 dimensions).
3. provide a simple unified and generic Application Programming Interface (API) for several classes of applications, including numerical computations, symbolic manipulations, and optimized code generation.
4. implement an extensible layered approach to allow users of different backgrounds to select the suitable level of coding they can handle, ranging from a very high-level of prototyping using GA operations to a low level of direct manipulation of scalars.

## Examples

### Numerical Geometric Algebra

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

### Symbolic Geometric Algebra

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
            // This is a pre-defined scalar processor for the standard
            // 64-bit floating point scalars
            var processor = GaScalarProcessorMathematicaExpr.DefaultProcessor;

            // This is a pre-defined text generator for displaying multivectors
            // with 64-bit floating point scalars
            var textComposer = GaTextComposerMathematicaExpr.DefaultComposer;

            // This is a pre-defined LaTeX generator for displaying multivectors
            // with 64-bit floating point scalars
            var latexComposer = GaLaTeXComposerMathematicaExpr.DefaultComposer;

            // Create two vectors each having 3 components (a 3-dimensional GA)
            var u = processor.CreateVector(3, i => $"Subscript[u,{i + 1}]".ToExpr());
            var v = processor.CreateVector(3, i => $"Subscript[v,{i + 1}]".ToExpr());

            // Compute their outer product as a bivector
            var bv = u.Op(v);

            // Display a text representation of the vectors and their outer product
            Console.WriteLine($@"v1 = {textComposer.GetMultivectorText(u)}");
            Console.WriteLine($@"v2 = {textComposer.GetMultivectorText(v)}");
            Console.WriteLine($@"v1 op v2 = {textComposer.GetMultivectorText(bv)}");
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

```csharp
using System;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.Samples.CodeComposer
{
    public static class Sample1
    {
        public static void Execute()
        {
            // The number of dimensions
            const int n = 3;

            // The context (a special kind of processor) for symbolic multivector
            // assignments
            var context = 
                new SymbolicContext()
                {
                    MergeExpressions = false
                };

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

            // Perform any required GA computations
            var bv = u.Op(v);

            // Define the final outputs for the computations for proper code generation
            bv.SetIsOutput(true);

            // Define code generated variable names for input parameters
            v.SetExternalNamesByTermId(id => $"v.Scalar{id.PatternToString(n)}");
            u.SetExternalNamesByTermId(id => $"u.Scalar{id.PatternToString(n)}");
            
            // Define code generated variable names for outputs
            bv.SetExternalNamesByTermId(id => $"bv.Scalar{id.PatternToString(n)}");

            // Optimize sequence computations inside context
            context.OptimizeContext();

            // Define code generated variable names for intermediate variables
            context.SetIntermediateExternalNamesByNameIndex(index => $"temp{index}");

            // Display an internal representation of the computations
            Console.WriteLine("Context Computations:");
            Console.WriteLine(context.ToString());
            Console.WriteLine();
        }
    }
}
```

The final output is:

```plaintext
Context Computations:
    Used Parameter       'u.Scalar001': u1
    Used Parameter       'u.Scalar010': u2
    Used Parameter       'u.Scalar100': u3
    Used Parameter       'v.Scalar001': v1
    Used Parameter       'v.Scalar010': v2
    Used Parameter       'v.Scalar100': v3
    Used Intermediate    'temp0': tmpVar10 = Times[u1, v2]
    Used Intermediate    'temp1': tmpVar11 = Times[u2, v1]
    Used Output          'bv.Scalar011': tmpVar4 = Subtract[tmpVar10, tmpVar11]
    Used Intermediate    'temp0': tmpVar12 = Times[u1, v3]
    Used Intermediate    'temp1': tmpVar13 = Times[u3, v1]
    Used Output          'bv.Scalar101': tmpVar7 = Subtract[tmpVar12, tmpVar13]
    Used Intermediate    'temp0': tmpVar14 = Times[u2, v3]
    Used Intermediate    'temp1': tmpVar15 = Times[u3, v2]
    Used Output          'bv.Scalar110': tmpVar9 = Subtract[tmpVar14, tmpVar15]
```