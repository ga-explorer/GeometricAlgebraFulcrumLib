using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Basis;

public sealed record LinBasisVectorPair3D : 
    IPair<LinBasisVector3D>
{
    public static LinBasisVectorPair3D PxPy { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Px,
            LinBasisVector3D.Py
        );

    public static LinBasisVectorPair3D PxNy { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Px,
            LinBasisVector3D.Ny
        );

    public static LinBasisVectorPair3D NxPy { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Nx,
            LinBasisVector3D.Py
        );

    public static LinBasisVectorPair3D NxNy { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Nx,
            LinBasisVector3D.Ny
        );


    public static LinBasisVectorPair3D PyPx { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Py,
            LinBasisVector3D.Px
        );

    public static LinBasisVectorPair3D PyNx { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Py,
            LinBasisVector3D.Nx
        );

    public static LinBasisVectorPair3D NyPx { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Ny,
            LinBasisVector3D.Px
        );

    public static LinBasisVectorPair3D NyNx { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Ny,
            LinBasisVector3D.Nx
        );


    public static LinBasisVectorPair3D PxPz { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Px,
            LinBasisVector3D.Pz
        );

    public static LinBasisVectorPair3D PxNz { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Px,
            LinBasisVector3D.Nz
        );

    public static LinBasisVectorPair3D NxPz { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Nx,
            LinBasisVector3D.Pz
        );

    public static LinBasisVectorPair3D NxNz { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Nx,
            LinBasisVector3D.Nz
        );
    

    public static LinBasisVectorPair3D PzPx { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Pz,
            LinBasisVector3D.Px
        );

    public static LinBasisVectorPair3D PzNx { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Pz,
            LinBasisVector3D.Nx
        );

    public static LinBasisVectorPair3D NzPx { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Nz,
            LinBasisVector3D.Px
        );

    public static LinBasisVectorPair3D NzNx { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Nz,
            LinBasisVector3D.Nx
        );
    

    public static LinBasisVectorPair3D PyPz { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Py,
            LinBasisVector3D.Pz
        );

    public static LinBasisVectorPair3D PyNz { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Py,
            LinBasisVector3D.Nz
        );

    public static LinBasisVectorPair3D NyPz { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Ny,
            LinBasisVector3D.Pz
        );

    public static LinBasisVectorPair3D NyNz { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Ny,
            LinBasisVector3D.Nz
        );


    public static LinBasisVectorPair3D PzPy { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Pz,
            LinBasisVector3D.Py
        );

    public static LinBasisVectorPair3D PzNy { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Pz,
            LinBasisVector3D.Ny
        );

    public static LinBasisVectorPair3D NzPy { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Nz,
            LinBasisVector3D.Py
        );

    public static LinBasisVectorPair3D NzNy { get; }
        = new LinBasisVectorPair3D(
            LinBasisVector3D.Nz,
            LinBasisVector3D.Ny
        );
    
    public static LinBasisVectorPair3D Create(LinBasisVector3D basisVector1, LinBasisVector3D basisVector2)
    {
        return basisVector1 switch
        {
            LinBasisVector3D.Px => basisVector2 switch
            {
                LinBasisVector3D.Py => PxPy,
                LinBasisVector3D.Pz => PxPz,
                LinBasisVector3D.Ny => PxNy,
                LinBasisVector3D.Nz => PxNz,
                
                _ => throw new InvalidOperationException()
            },

            LinBasisVector3D.Py => basisVector2 switch
            {
                LinBasisVector3D.Px => PyPx,
                LinBasisVector3D.Pz => PyPz,
                LinBasisVector3D.Nx => PyNx,
                LinBasisVector3D.Nz => PyNz,
                
                _ => throw new InvalidOperationException()
            },

            LinBasisVector3D.Pz => basisVector2 switch
            {
                LinBasisVector3D.Px => PzPx,
                LinBasisVector3D.Py => PzPy,
                LinBasisVector3D.Nx => PzNx,
                LinBasisVector3D.Ny => PzNy,
                
                _ => throw new InvalidOperationException()
            },

            LinBasisVector3D.Nx => basisVector2 switch
            {
                LinBasisVector3D.Py => NxPy,
                LinBasisVector3D.Pz => NxPz,
                LinBasisVector3D.Ny => NxNy,
                LinBasisVector3D.Nz => NxNz,
                
                _ => throw new InvalidOperationException()
            },
            
            LinBasisVector3D.Ny => basisVector2 switch
            {
                LinBasisVector3D.Px => NyPx,
                LinBasisVector3D.Pz => NyPz,
                LinBasisVector3D.Nx => NyNx,
                LinBasisVector3D.Nz => NyNz,
                
                _ => throw new InvalidOperationException()
            },
            
            LinBasisVector3D.Nz => basisVector2 switch
            {
                LinBasisVector3D.Px => NzPx,
                LinBasisVector3D.Py => NzPy,
                LinBasisVector3D.Nx => NzNx,
                LinBasisVector3D.Ny => NzNy,
                
                _ => throw new InvalidOperationException()
            },

            _ => throw new InvalidOperationException()
        };
    }


    public LinBasisVector3D Item1 { get; }

    public LinBasisVector3D Item2 { get; }
    
    public LinBasisVector3D RightNormal
    {
        get
        {
            return Item1 switch
            {
                LinBasisVector3D.Px => Item2 switch
                {
                    LinBasisVector3D.Py => LinBasisVector3D.Pz,
                    LinBasisVector3D.Pz => LinBasisVector3D.Ny,
                    LinBasisVector3D.Ny => LinBasisVector3D.Nz,
                    LinBasisVector3D.Nz => LinBasisVector3D.Py,
                    _ => throw new InvalidOperationException()
                },

                LinBasisVector3D.Py => Item2 switch
                {
                    LinBasisVector3D.Pz => LinBasisVector3D.Px,
                    LinBasisVector3D.Px => LinBasisVector3D.Nz,
                    LinBasisVector3D.Nz => LinBasisVector3D.Nx,
                    LinBasisVector3D.Nx => LinBasisVector3D.Pz,
                    _ => throw new InvalidOperationException()
                },

                LinBasisVector3D.Pz => Item2 switch
                {
                    LinBasisVector3D.Px => LinBasisVector3D.Py,
                    LinBasisVector3D.Py => LinBasisVector3D.Nx,
                    LinBasisVector3D.Nx => LinBasisVector3D.Ny,
                    LinBasisVector3D.Ny => LinBasisVector3D.Px,
                    _ => throw new InvalidOperationException()
                },

                LinBasisVector3D.Nx => Item2 switch
                {
                    LinBasisVector3D.Py => LinBasisVector3D.Nz,
                    LinBasisVector3D.Pz => LinBasisVector3D.Py,
                    LinBasisVector3D.Ny => LinBasisVector3D.Pz,
                    LinBasisVector3D.Nz => LinBasisVector3D.Ny,
                    _ => throw new InvalidOperationException()
                },
                
                LinBasisVector3D.Ny => Item2 switch
                {
                    LinBasisVector3D.Pz => LinBasisVector3D.Nx,
                    LinBasisVector3D.Px => LinBasisVector3D.Pz,
                    LinBasisVector3D.Nz => LinBasisVector3D.Px,
                    LinBasisVector3D.Nx => LinBasisVector3D.Nz,
                    _ => throw new InvalidOperationException()
                },
                
                LinBasisVector3D.Nz => Item2 switch
                {
                    LinBasisVector3D.Px => LinBasisVector3D.Ny,
                    LinBasisVector3D.Py => LinBasisVector3D.Px,
                    LinBasisVector3D.Nx => LinBasisVector3D.Py,
                    LinBasisVector3D.Ny => LinBasisVector3D.Nx,
                    _ => throw new InvalidOperationException()
                },

                _ => throw new InvalidOperationException()
            };
        }
    }
    
    public LinBasisVector3D LeftNormal
    {
        get
        {
            return Item1 switch
            {
                LinBasisVector3D.Px => Item2 switch
                {
                    LinBasisVector3D.Py => LinBasisVector3D.Nz,
                    LinBasisVector3D.Pz => LinBasisVector3D.Py,
                    LinBasisVector3D.Ny => LinBasisVector3D.Pz,
                    LinBasisVector3D.Nz => LinBasisVector3D.Ny,
                    _ => throw new InvalidOperationException()
                },
                
                LinBasisVector3D.Py => Item2 switch
                {
                    LinBasisVector3D.Pz => LinBasisVector3D.Nx,
                    LinBasisVector3D.Px => LinBasisVector3D.Pz,
                    LinBasisVector3D.Nz => LinBasisVector3D.Px,
                    LinBasisVector3D.Nx => LinBasisVector3D.Nz,
                    _ => throw new InvalidOperationException()
                },
                
                LinBasisVector3D.Pz => Item2 switch
                {
                    LinBasisVector3D.Px => LinBasisVector3D.Ny,
                    LinBasisVector3D.Py => LinBasisVector3D.Px,
                    LinBasisVector3D.Nx => LinBasisVector3D.Py,
                    LinBasisVector3D.Ny => LinBasisVector3D.Nx,
                    _ => throw new InvalidOperationException()
                },

                LinBasisVector3D.Nx => Item2 switch
                {
                    LinBasisVector3D.Py => LinBasisVector3D.Pz,
                    LinBasisVector3D.Pz => LinBasisVector3D.Ny,
                    LinBasisVector3D.Ny => LinBasisVector3D.Nz,
                    LinBasisVector3D.Nz => LinBasisVector3D.Py,
                    _ => throw new InvalidOperationException()
                },

                LinBasisVector3D.Ny => Item2 switch
                {
                    LinBasisVector3D.Pz => LinBasisVector3D.Px,
                    LinBasisVector3D.Px => LinBasisVector3D.Nz,
                    LinBasisVector3D.Nz => LinBasisVector3D.Nx,
                    LinBasisVector3D.Nx => LinBasisVector3D.Pz,
                    _ => throw new InvalidOperationException()
                },

                LinBasisVector3D.Nz => Item2 switch
                {
                    LinBasisVector3D.Px => LinBasisVector3D.Py,
                    LinBasisVector3D.Py => LinBasisVector3D.Nx,
                    LinBasisVector3D.Nx => LinBasisVector3D.Ny,
                    LinBasisVector3D.Ny => LinBasisVector3D.Px,
                    _ => throw new InvalidOperationException()
                },

                _ => throw new InvalidOperationException()
            };
        }
    }


    private LinBasisVectorPair3D(LinBasisVector3D item1, LinBasisVector3D item2)
    {
        Item1 = item1;
        Item2 = item2;
    }
    
    public void Deconstruct(out LinBasisVector3D item1, out LinBasisVector3D item2)
    {
        item1 = Item1;
        item2 = Item2;
    }


    public LinFloat64Vector3D MidVector
    {
        get
        {
            var sqrt2 = Math.Sqrt(2);

            var x = 0d;
            var y = 0d;
            var z = 0d;

            if (Item1 == LinBasisVector3D.Px || Item2 == LinBasisVector3D.Px)
                x = sqrt2;

            else if (Item1 == LinBasisVector3D.Nx || Item2 == LinBasisVector3D.Nx)
                x = -sqrt2;

            if (Item1 == LinBasisVector3D.Py || Item2 == LinBasisVector3D.Py)
                y = sqrt2;

            else if (Item1 == LinBasisVector3D.Ny || Item2 == LinBasisVector3D.Ny)
                y = -sqrt2;
            
            if (Item1 == LinBasisVector3D.Pz || Item2 == LinBasisVector3D.Pz)
                z = sqrt2;

            else if (Item1 == LinBasisVector3D.Nz || Item2 == LinBasisVector3D.Nz)
                z = -sqrt2;
            
            return LinFloat64Vector3D.Create(x, y, z);
        }
    }
}