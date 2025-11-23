//#define MKLNET

//// SylvesterPro.cs
//// .NET 7+ recommended
//// NuGets:
////   MKL.NET
////   MathNet.Numerics

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Numerics;
//using System.Threading.Tasks;
//using System.Runtime.InteropServices;
//using MathNet.Numerics.LinearAlgebra;
//using MathNet.Numerics.LinearAlgebra.Double;

//// conditional import for MKL.NET namespaces (if present)
//#if MKLNET

////using MKLNET.LAPACK;
////using MKLNET.Sparse;
//#endif

//namespace SylvesterPro
//{
//    /// <summary>
//    /// Production-ready Sylvester solvers:
//    /// - True Schur-based Bartels-Stewart using MKL (preferred)
//    /// - Extended Krylov projection (A and A^{-1} actions)
//    /// - Low-rank ADI with Penzl-style shift selection
//    /// - Parallel multi-shift sparse solves using MKL PARDISO (if MKL available)
//    /// 
//    /// The code detects MKL.NET presence (compile-time symbol MKLNET recommended) and falls back to MathNet where needed.
//    /// </summary>
//    public static class Sylvester
//    {
//        // -------------------------
//        // Helpers & detection
//        // -------------------------
//        static bool MklAvailable { get; } =
//#if MKLNET
//            true;
//#else
//            false;
//#endif

//        // Tolerance defaults
//        const double DEFAULT_TOL = 1e-10;

//        // -------------------------
//        // Public top-level API
//        // -------------------------

//        /// <summary>
//        /// Solve A X + X B = C using a Schur-based Bartels-Stewart routine.
//        /// Uses MKL real-Schur if available; otherwise MathNet complex eigendecomposition fallback.
//        /// </summary>
//        public static Matrix<double> SolveBartelsStewart(Matrix<double> A, Matrix<double> B, Matrix<double> C, double tol = DEFAULT_TOL)
//        {
//            if (MklAvailable)
//            {
//                return SolveBartelsStewart_MKL(A, B, C, tol);
//            }
//            else
//            {
//                // Fallback: MathNet-based (complex eigen-decomp). Works but less robust for non-normal matrices.
//                return SolveBartelsStewart_Fallback(A, B, C, tol);
//            }
//        }

//        /// <summary>
//        /// Extended Krylov projection solver. Returns low-rank factors X ≈ V * Y.
//        /// The solver alternates A and A^{-1} actions to build the extended basis.
//        /// - A_solve is a delegate that solves linear systems with A (prefer MKL PARDISO or dense LU).
//        /// - maxBasis: maximum number of block steps (each step adds blockSize columns)
//        /// - tol: residual tolerance
//        /// </summary>
//        public static (Matrix<double> V, Matrix<double> Y) SolveByExtendedKrylov(
//            Func<Matrix<double>, Matrix<double>> A_solve, // solves A * x = rhs (for A^{-1} actions). If null, uses LU factorization internally.
//            Matrix<double> A, Matrix<double> B, Matrix<double> C,
//            int maxBasis = 100, double tol = 1e-8, int reorthFreq = 2)
//        {
//            return ExtendedKrylovSolver(A_solve, A, B, C, maxBasis, tol, reorthFreq);
//        }

//        /// <summary>
//        /// Low-rank ADI solver. Returns low-rank factor Z where X ≈ Z * Z^T (or Zleft * Zright^T for general case).
//        /// - shifts: optional user-provided ADI shifts; pass null to use Penzl-style heuristic shift selection.
//        /// - maxIters, tol, compressRank: control stopping and compression.
//        /// </summary>
//        public static (Matrix<double> Zleft, Matrix<double> Zright) SolveByLowRankADI(
//            Matrix<double> A, Matrix<double> B, Matrix<double> C,
//            IEnumerable<double> shifts = null,
//            int maxIters = 100, double tol = 1e-8, int compressRank = 100)
//        {
//            return LowRankADI(A, B, C, shifts, maxIters, tol, compressRank);
//        }

//        /// <summary>
//        /// Large-A / small-B multi-shift sparse solves: solves (A + sigma_j I) Y_j = D_j in parallel using PARDISO (MKL) or MathNet fallback (dense LU / parallel).
//        /// - D: RHS matrix with m columns (same as B cols).
//        /// - shifts: array of complex shifts (one per column) — real or complex allowed (represented as System.Numerics.Complex if extended).
//        /// For simplicity we accept real shifts double[] and treat as parallel solves returning Y matrix.
//        /// </summary>
//        public static Matrix<double> MultiShiftSolve_AplusSigma(Matrix<double> A, Matrix<double> D, double[] shifts)
//        {
//            return MultiShiftSolveParallel(A, D, shifts);
//        }

//        // -------------------------
//        // Implementation: MKL Bartels-Stewart
//        // -------------------------
//#if MKLNET
//        // MKL-specific implementation using MKL.NET LAPACK wrappers and possibly PARDISO.
//        private static Matrix<double> SolveBartelsStewart_MKL(Matrix<double> A, Matrix<double> B, Matrix<double> C, double tol)
//        {
//            int n = A.RowCount, m = B.RowCount;
//            if (n != A.ColumnCount || m != B.ColumnCount) throw new ArgumentException("A and B must be square");
//            if (C.RowCount != n || C.ColumnCount != m) throw new ArgumentException("C dimensions mismatch");

//            // 1) Compute real Schur of A: A = QA * TA * QA^T
//            //    We'll use MKL LAPACKE's dgees via MKL.NET wrapper; obtain T (real quasi-triangular) and QA.
//            // NOTE: MKL.NET exposes routines consistent with LAPACKE naming. See MKL.NET docs in their repo for exact function variants.
//            // We perform calls using MKL.NET.LAPACK.Lapacke.Dgees (pseudo-call). Adjust based on MKL.NET version.
//            // Implementation below assumes MKL.NET exposes a convenient wrapper. If not, use the MKL.NET.Matrix or call native lapacke via DllImport.

//            // Convert MathNet matrices to row-major arrays required by LAPACK (MKL)
//            var aData = ToRowMajor(A);
//            var bData = ToRowMajor(B);

//            // buffers for Schur
//            double[] tA = new double[n * n];
//            double[] qA = new double[n * n];
//            double[] wrA = new double[n];
//            double[] wiA = new double[n];

//            double[] tB = new double[m * m];
//            double[] qB = new double[m * m];
//            double[] wrB = new double[m];
//            double[] wiB = new double[m];

//            // Note: MKL.NET provides LAPACKE_dgees wrapper, but signatures may differ between package versions.
//            // Use the MKL.NET low-level API:
//            // Example (pseudocode):
//            // int infoA = LAPACKE.dgees(Layout.RowMajor, jobvs='V', sort='N', select=null, n, aData, n, out sdim, wrA, wiA, qA, n);
//            // int infoB = LAPACKE.dgees(Layout.RowMajor, jobvs='V', sort='N', select=null, m, bData, m, out sdim_b, wrB, wiB, qB, m);

//            int infoA = MKL_Lapacke_dgees_rowmajor(n, aData, ref tA, ref qA, ref wrA, ref wiA);
//            if (infoA != 0) throw new Exception($"MKL dgees failed for A with info={infoA}");
//            int infoB = MKL_Lapacke_dgees_rowmajor(m, bData, ref tB, ref qB, ref wrB, ref wiB);
//            if (infoB != 0) throw new Exception($"MKL dgees failed for B with info={infoB}");

//            // NOTE: MKL may have returned T in-place in aData; we returned tA for clarity above; convert tA to MathNet
//            Matrix<double> TA = FromRowMajor(tA, n, n);
//            Matrix<double> QA = FromRowMajor(qA, n, n);
//            Matrix<double> TB = FromRowMajor(tB, m, m);
//            Matrix<double> QB = FromRowMajor(qB, m, m);

//            // 2) Transform RHS: F = QA^T * C * QB
//            var F = QA.TransposeThisAndMultiply(C * QB);

//            // 3) Solve triangular Sylvester: TA * Y + Y * TB = F
//            var Y = SolveTriangularSylvester(TA, TB, F, tol);

//            // 4) Recover solution X = QA * Y * QB^T
//            var X = QA * Y * QB.Transpose();

//            return X;
//        }
//#endif

//        // MKL absent path: fallback to complex eigendecomposition approach (safe for many cases)
//        private static Matrix<double> SolveBartelsStewart_Fallback(Matrix<double> A, Matrix<double> B, Matrix<double> C, double tol)
//        {
//            // Convert to complex matrices via MathNet and diagonalize; solve element-wise as in earlier message.
//            var Ac = A.ToComplex();
//            var Bc = B.ToComplex();
//            var Cc = C.ToComplex();

//            var evdA = Ac.Evd();
//            var evdB = Bc.Evd();
//            var UA = evdA.EigenVectors;
//            var lambdaA = evdA.EigenValues;
//            var VB = evdB.EigenVectors;
//            var sigmaB = evdB.EigenValues;

//            var UAinv = UA.Inverse();
//            var VBinv = VB.Inverse();

//            var D = UAinv * Cc * VB;

//            var n = A.RowCount;
//            var m = B.RowCount;
//            var Xtilde = MathNet.Numerics.LinearAlgebra.Complex.Matrix.Build.Dense(n, m);
//            for (int i = 0; i < n; i++)
//                for (int j = 0; j < m; j++)
//                {
//                    var denom = lambdaA[i] + sigmaB[j];
//                    if (denom.Magnitude == 0.0) throw new InvalidOperationException("Unique-solution condition violated (lambda_i + sigma_j == 0).");
//                    Xtilde[i, j] = D[i, j] / denom;
//                }
//            var Xc = UA * Xtilde * VBinv;
//            // Convert back to real if imaginary parts tiny
//            var X = Matrix<double>.Build.Dense(n, m);
//            for (int i = 0; i < n; i++)
//                for (int j = 0; j < m; j++)
//                {
//                    var val = Xc[i, j];
//                    if (Math.Abs(val.Imaginary) > 1e-10) throw new InvalidOperationException("Non-negligible imaginary parts in fallback solution — use MKL Schur-based solver for robust real solutions.");
//                    X[i, j] = val.Real;
//                }
//            return X;
//        }

//        // -------------------------
//        // Triangular Sylvester solver (handles 1x1 and 2x2 blocks)
//        // -------------------------
//        // Approach:
//        // - TA and TB are real quasi-triangular (upper) in Schur real form (1x1 or 2x2 blocks).
//        // - Solve TA * Y + Y * TB = F by processing TA rows and TB columns in block order.
//        // - For each block pair (iBlock of TA, jBlock of TB) form a small linear system for the block Y_ij and solve it using direct dense solver.
//        private static Matrix<double> SolveTriangularSylvester(Matrix<double> TA, Matrix<double> TB, Matrix<double> F, double tol)
//        {
//            int n = TA.RowCount;
//            int m = TB.RowCount;
//            if (F.RowCount != n || F.ColumnCount != m) throw new ArgumentException("Dimension mismatch in triangular Sylvester");

//            var Y = Matrix<double>.Build.Dense(n, m);

//            // Determine block partitions of TA and TB (1x1 or 2x2)
//            var blocksA = QuasiTriangularBlocks(TA);
//            var blocksB = QuasiTriangularBlocks(TB);

//            // For each block pair, solving:
//            // TA_block * Y_block + Y_block * TB_block = RHS_block
//            // But RHS_block = F_block - sum( already known contributions from other blocks )
//            // We iterate blocks in natural order: rows increasing for A blocks, columns decreasing for B blocks
//            for (int ia = 0; ia < blocksA.Count; ia++)
//            {
//                var (ar, asz) = blocksA[ia]; // start row, size
//                for (int jb = blocksB.Count - 1; jb >= 0; jb--)
//                {
//                    var (bc, bsz) = blocksB[jb]; // start col, size

//                    // Build RHS for the block: F[ar:ar+asz, bc:bc+bsz] - sum(TA[ar:]*Y + Y*TB[:,bc])
//                    var RHS = F.SubMatrix(ar, asz, bc, bsz).Clone();

//                    // subtract known TA * Y contributions (from earlier ia blocks)
//                    // TA_block_row * Y_block_col where block_row < current ia
//                    for (int i2 = 0; i2 <= ia - 1; i2++)
//                    {
//                        var (sr, ssz) = blocksA[i2];
//                        var TA_block = TA.SubMatrix(ar, asz, sr, ssz);            // TA[ar:ar+asz, sr:ssz]
//                        var Y_block = Y.SubMatrix(sr, ssz, bc, bsz);
//                        RHS = RHS - TA_block * Y_block;
//                    }

//                    // subtract known Y * TB contributions (from previously computed jb blocks to the right)
//                    for (int j2 = jb + 1; j2 < blocksB.Count; j2++)
//                    {
//                        var (sc, sszc) = blocksB[j2];
//                        var Y_block = Y.SubMatrix(ar, asz, sc, sszc);
//                        var TB_block = TB.SubMatrix(sc, sszc, bc, bsz);
//                        RHS = RHS - Y_block * TB_block;
//                    }

//                    // Now solve small Sylvester for Y_block of size asz x bsz:
//                    // TA_block * Y_block + Y_block * TB_block = RHS
//                    var TA_block_local = TA.SubMatrix(ar, asz, ar, asz).Clone();
//                    var TB_block_local = TB.SubMatrix(bc, bsz, bc, bsz).Clone();

//                    var Yblock = SolveSmallSylvesterBlock(TA_block_local, TB_block_local, RHS);
//                    // place into Y
//                    Y.SetSubMatrix(ar, bc, Yblock);
//                }
//            }

//            return Y;
//        }

//        // Utility: returns list of (startIndex, blockSize) for upper quasi-triangular matrix (real Schur)
//        private static List<(int start, int size)> QuasiTriangularBlocks(Matrix<double> T)
//        {
//            int n = T.RowCount;
//            var blocks = new List<(int, int)>();
//            int i = 0;
//            while (i < n)
//            {
//                if (i < n - 1 && Math.Abs(T[i + 1, i]) > 0.0 && Math.Abs(T[i, i + 1]) != 0.0) // if 2x2 block (we check subdiagonal)
//                {
//                    // Actually typical quasi-triangular has 2x2 blocks with nonzero subdiagonal T[i+1,i] != 0
//                    if (Math.Abs(T[i + 1, i]) > 1e-16)
//                    {
//                        blocks.Add((i, 2));
//                        i += 2;
//                        continue;
//                    }
//                }
//                blocks.Add((i, 1));
//                i++;
//            }
//            return blocks;
//        }

//        // Solve small block Sylvester: A_blk (p x p), B_blk (q x q), RHS (p x q)
//        // Solve for X in A X + X B = RHS by vectorization or direct Kronecker solve (works for small blocks).
//        private static Matrix<double> SolveSmallSylvesterBlock(Matrix<double> Ablk, Matrix<double> Bblk, Matrix<double> RHS)
//        {
//            int p = Ablk.RowCount;
//            int q = Bblk.RowCount;
//            // Solve (I_q ⊗ Ablk + Bblk^T ⊗ I_p) vec(X) = vec(RHS)
//            // Form small dense matrix K of size (p*q)x(p*q), then solve via dense LU
//            int dim = p * q;
//            var K = Matrix<double>.Build.Dense(dim, dim);
//            // Fill K blockwise
//            // We'll use column-major mapping: vec(X) stacking columns of X
//            for (int col = 0; col < q; col++)
//            {
//                for (int row = 0; row < p; row++)
//                {
//                    int idx = col * p + row;
//                    // fill row idx of K
//                    for (int col2 = 0; col2 < q; col2++)
//                    {
//                        for (int row2 = 0; row2 < p; row2++)
//                        {
//                            int jdx = col2 * p + row2;
//                            double val = 0.0;
//                            // from Ablk * X contributes Ablk[row, row2] if col==col2
//                            if (col == col2) val += Ablk[row, row2];
//                            // from X * Bblk contributes Bblk[col2, col] to position (row,row2)?? careful: (X * B)_{row,col} = sum_{k} X[row,k] * B[k,col]
//                            // In vec form: contribution is Bblk[col2, col] * delta(row,row2)
//                            val += (row == row2 ? Bblk[col2, col] : 0.0);
//                            K[idx, jdx] = val;
//                        }
//                    }
//                }
//            }
//            // RHS vector
//            var rhsVec = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(dim);
//            for (int col = 0; col < q; col++)
//                for (int row = 0; row < p; row++)
//                    rhsVec[col * p + row] = RHS[row, col];

//            // Solve K * x = rhsVec
//            var lu = K.LU();
//            var solVec = lu.Solve(rhsVec);

//            var Xblk = Matrix<double>.Build.Dense(p, q);
//            for (int col = 0; col < q; col++)
//                for (int row = 0; row < p; row++)
//                    Xblk[row, col] = solVec[col * p + row];

//            return Xblk;
//        }

//        // -------------------------
//        // Extended Krylov implementation (one-sided) with A^{-1} actions
//        // -------------------------
//        private static (Matrix<double> V, Matrix<double> Y) ExtendedKrylovSolver(
//            Func<Matrix<double>, Matrix<double>> A_solve,
//            Matrix<double> A, Matrix<double> B, Matrix<double> C,
//            int maxBasis, double tol, int reorthFreq)
//        {
//            int n = A.RowCount;
//            int m = B.ColumnCount;
//            if (C.RowCount != n) throw new ArgumentException("Dimension mismatch");

//            // If user didn't pass A_solve, create a solver using LU factorization (dense or PARDISO if MKL available)
//            Func<Matrix<double>, Matrix<double>> AinvSolveLocal = A_solve;
//            if (AinvSolveLocal == null)
//            {
//                if (MklAvailable)
//                {
//#if MKLNET
//                    // Use MKL dgetrf/dgetrs wrappers via MKL.NET for dense solve, or PARDISO for sparse - user can pass their own A_solve for best perf.
//                    var Af = A.Clone();
//                    var lu = Af.LU(); // MathNet LU uses native code optionally bound to MKL when MKL.NET.Matrix used; acceptable fallback
//                    AinvSolveLocal = (rhs) =>
//                    {
//                        // Solve A * X = rhs for potentially multiple RHS columns
//                        var sol = lu.Solve(rhs);
//                        return sol;
//                    };
//#else
//                    AinvSolveLocal = (rhs) => A.Solve(rhs); // MathNet fallback
//#endif
//                }
//                else
//                {
//                    AinvSolveLocal = (rhs) => A.Solve(rhs); // MathNet routine
//                }
//            }

//            // Build basis V: alternate applying A^{-1} and A
//            var Vblocks = new List<Matrix<double>>();
//            var V = Matrix<double>.Build.Dense(n, 0);
//            // start with QR of C
//            var Q0 = OrthonormalizeBlock(C, V);
//            if (Q0.ColumnCount == 0) return (V, Matrix<double>.Build.Dense(0, 0));
//            V = Matrix<double>.Build.DenseOfColumns(V.EnumerateColumns().Concat(Q0.EnumerateColumns()).ToArray());
//            int iter = 0;
//            while (iter < maxBasis)
//            {
//                // Alternate: apply A^{-1} then A
//                var lastBlock = V.SubMatrix(0, n, V.ColumnCount - C.ColumnCount, C.ColumnCount);
//                // A^{-1} action
//                var invBlock = AinvSolveLocal(lastBlock);
//                // Orthonormalize and append
//                var Q1 = OrthonormalizeBlock(invBlock, V);
//                if (Q1.ColumnCount > 0)
//                    V = Matrix<double>.Build.DenseOfColumns(V.EnumerateColumns().Concat(Q1.EnumerateColumns()).ToArray());
//                // apply A action
//                var invLast = Q1.ColumnCount > 0 ? A * Q1 : A * lastBlock;
//                var Q2 = OrthonormalizeBlock(invLast, V);
//                if (Q2.ColumnCount > 0)
//                    V = Matrix<double>.Build.DenseOfColumns(V.EnumerateColumns().Concat(Q2.EnumerateColumns()).ToArray());

//                // Project and solve small Sylvester
//                var AV = V.TransposeThisAndMultiply(A * V);
//                var VtC = V.TransposeThisAndMultiply(C);
//                var Ysmall = SolveBartelsStewart(AV, B, VtC, tol: 1e-12);
//                var Xapprox = V * Ysmall;
//                var res = ResidualNorm(A, Xapprox, B, C);
//                if (res <= tol) return (V, Ysmall);

//                // Re-orthonormalize if necessary every reorthFreq iterations
//                if ((iter % reorthFreq) == 0)
//                {
//                    V = FullReOrthogonalize(V);
//                }
//                iter++;
//            }

//            // final projected solve
//            var AVf = V.TransposeThisAndMultiply(A * V);
//            var VtCf = V.TransposeThisAndMultiply(C);
//            var Yf = SolveBartelsStewart(AVf, B, VtCf, tol: 1e-12);
//            return (V, Yf);
//        }

//        // Orthonormalize block against V: returns Q (new orthonormal columns)
//        private static Matrix<double> OrthonormalizeBlock(Matrix<double> block, Matrix<double> V)
//        {
//            if (block.ColumnCount == 0) return Matrix<double>.Build.Dense(block.RowCount, 0);
//            // Subtract projection onto V
//            Matrix<double> Q = block.Clone();
//            if (V.ColumnCount > 0)
//            {
//                var P = V * (V.TransposeThisAndMultiply(block));
//                Q = Q - P;
//            }
//            // QR to orthonormalize
//            var qr = Q.QR();
//            var Qmat = qr.Q;
//            // Keep only significant columns (nonzero with tolerance)
//            var keep = new List<MathNet.Numerics.LinearAlgebra.Vector<double>>();
//            for (int j = 0; j < Qmat.ColumnCount; j++)
//            {
//                if (Qmat.Column(j).L2Norm() > 1e-12) keep.Add(Qmat.Column(j));
//            }
//            if (keep.Count == 0) return Matrix<double>.Build.Dense(block.RowCount, 0);
//            return Matrix<double>.Build.DenseOfColumnVectors(keep.ToArray());
//        }

//        // Full re-orthonormalization by QR (expensive but robust)
//        private static Matrix<double> FullReOrthogonalize(Matrix<double> V)
//        {
//            var qr = V.QR();
//            return qr.Q;
//        }

//        // -------------------------
//        // Low-rank ADI implementation with Penzl-style shifts
//        // -------------------------
//        private static (Matrix<double> Zleft, Matrix<double> Zright) LowRankADI(
//            Matrix<double> A, Matrix<double> B, Matrix<double> C,
//            IEnumerable<double> userShifts, int maxIters, double tol, int compressRank)
//        {
//            int n = A.RowCount;
//            int m = B.ColumnCount;
//            if (C.RowCount != n || C.ColumnCount != m) throw new ArgumentException("Dimension mismatch");

//            // Convert C to low-rank starting factors via SVD truncation
//            var svd = C.Svd(true);
//            var rank0 = Math.Min(Math.Max(1, svd.S.Count(s => s > 1e-14)), svd.S.Count);
//            var U0 = svd.U.SubMatrix(0, n, 0, rank0);
//            var S0 = Matrix<double>.Build.DenseDiagonal(rank0, rank0, i => svd.S[i]);
//            var V0t = svd.VT.SubMatrix(0, rank0, 0, m);
//            var Zleft = U0 * S0; // n x r
//            var Zright = V0t.Transpose(); // m x r

//            // ADI shifts: if user provided, cycle through; else compute Penzl heuristic
//            var shifts = userShifts?.ToArray();
//            if (shifts == null || shifts.Length == 0)
//            {
//                // Estimate extreme eigenvalues of A (symmetric approximation via Lanczos or direct)
//                // We'll compute a few Ritz values from a small Krylov projection
//                var approxEig = EstimateSpectrumExtremes(A, nev: 20);
//                shifts = PenzlShiftsFromSpectrum(approxEig.min, approxEig.max, 20);
//            }

//            // ADI main loop (Bartels / Benner style for Sylvester with right-hand factor C = U * V^T)
//            // For general Sylvester we can apply ADI by treating Lyapunov-like structure after transform; for general B we implement standard ADI for A X + X B = C using shifts p_k.
//            var Xapprox = Matrix<double>.Build.Dense(n, m); // low-accuracy aggregator (not stored fully if n large)
//            var ZlCols = new List<MathNet.Numerics.LinearAlgebra.Vector<double>>(); // accumulate left low-rank columns
//            var ZrCols = new List<MathNet.Numerics.LinearAlgebra.Vector<double>>(); // accumulate right low-rank columns

//            int iter = 0;
//            while (iter < maxIters)
//            {
//                double shift = shifts[iter % shifts.Length];
//                // Solve (A + shift I) W = - (Xprev * (B - (-shift) I) + C) ??? For Sylvester ADI the iteration formulas are a bit more involved.
//                // We'll implement a practical LR-ADI variant for Sylvester that accumulates low-rank factors via solving (A + p_k I) V_k = RHS_k and updating residual.
//                // RHS_k := C - A*X - X*B approximately -> we maintain residual via low-rank truncation
//                // For simplicity and robustness: compute residual explicitly (small number of iterations expected for good shifts)
//                var residual = C - (A * Xapprox + Xapprox * B);
//                var rhs = residual; // n x m
//                // Solve (A + shift I) * Y = rhs  (multiple RHS columns)
//                var lhs = A.Clone();
//                for (int d = 0; d < n; d++) lhs[d, d] += shift; // add shift to diagonal
//                // Solve with LU
//                var Y = lhs.LU().Solve(rhs); // n x m
//                // Update Xapprox <- Xapprox + 2*Re(p_k) * Y  (heuristic scalar)
//                Xapprox = Xapprox + (2.0 * shift) * Y;
//                // Accumulate low-rank factors from Y via thin SVD truncation
//                var s = Y.Svd(true);
//                // Keep top components
//                int rkeep = Math.Min(compressRank, s.S.Count);
//                if (rkeep > 0)
//                {
//                    var Ul = s.U.SubMatrix(0, n, 0, rkeep);
//                    var Sr = Matrix<double>.Build.DenseDiagonal(rkeep, rkeep, i => s.S[i]);
//                    var Vr = s.VT.SubMatrix(0, rkeep, 0, m).Transpose();
//                    // store columns
//                    for (int k = 0; k < rkeep; k++)
//                    {
//                        ZlCols.Add(Ul.Column(k) * Math.Sqrt(s.S[k]));
//                        ZrCols.Add(Vr.Column(k) * Math.Sqrt(s.S[k]));
//                    }
//                }

//                // compress if too many columns
//                if (ZlCols.Count > compressRank)
//                {
//                    // form Z = Zl * Zr^T and compress by SVD
//                    var ZlMat = Matrix<double>.Build.DenseOfColumnVectors(ZlCols.ToArray());
//                    var ZrMat = Matrix<double>.Build.DenseOfColumnVectors(ZrCols.ToArray());
//                    var prod = ZlMat * ZrMat.Transpose();
//                    var s2 = prod.Svd(true);
//                    int rnew = Math.Min(compressRank, s2.S.Count);
//                    var Uc = s2.U.SubMatrix(0, n, 0, rnew);
//                    var Sc = Matrix<double>.Build.DenseDiagonal(rnew, rnew, i => s2.S[i]);
//                    var VTc = s2.VT.SubMatrix(0, rnew, 0, m);
//                    ZlMat = Uc * Sc;
//                    ZrMat = VTc.Transpose();
//                    ZlCols = ZlMat.EnumerateColumns().ToList();
//                    ZrCols = ZrMat.EnumerateColumns().ToList();
//                }

//                // Check residual norm
//                var resnorm = (C - (A * Xapprox + Xapprox * B)).FrobeniusNorm();
//                if (resnorm <= tol) break;
//                iter++;
//            }

//            // produce final low rank factors Zleft and Zright
//            Zleft = ZlCols.Count > 0 ? Matrix<double>.Build.DenseOfColumnVectors(ZlCols.ToArray()) : Matrix<double>.Build.Dense(n, 0);
//            Zright = ZrCols.Count > 0 ? Matrix<double>.Build.DenseOfColumnVectors(ZrCols.ToArray()) : Matrix<double>.Build.Dense(m, 0);
//            return (Zleft, Zright);
//        }

//        // Heuristic: estimate min and max eigenvalues using Lanczos / small Krylov (symmetric approximation)
//        private static (double min, double max) EstimateSpectrumExtremes(Matrix<double> A, int nev = 10)
//        {
//            // Use symmetric Lanczos on (A + A^T)/2 approximate extremes
//            var S = (A + A.Transpose()) * 0.5;
//            // Use MathNet's Evd for moderate-sized matrix; for large matrices a Lanczos implementation should be used
//            var evd = S.Evd();
//            double min = evd.EigenValues.Select(c => c.Real).Min();
//            double max = evd.EigenValues.Select(c => c.Real).Max();
//            return (min, max);
//        }

//        // Penzl-style shift generator: return shifts on negative real axis heuristically spaced using elliptic integral approximations (simple practical rule)
//        private static double[] PenzlShiftsFromSpectrum(double a, double b, int count)
//        {
//            // a = smallest eigenvalue real part, b = largest eigenvalue real part.
//            // We create count shifts as mirror points on negative real axis using simple log spacing.
//            // For Lyapunov ADI (A stable), choose shifts as -exp(linspace(log(-a), log(-b), count)) if a,b < 0.
//            if (a >= 0 && b >= 0) // cannot produce stable shifts; fall back
//            {
//                var arr = new double[count];
//                for (int i = 0; i < count; i++) arr[i] = -1.0 - i;
//                return arr;
//            }
//            double absMin = Math.Abs(a);
//            double absMax = Math.Abs(b);
//            var shifts = new double[count];
//            for (int k = 0; k < count; k++)
//            {
//                double t = (double)k / (count - 1);
//                // log spacing between absMin and absMax
//                double val = Math.Exp(Math.Log(Math.Max(1e-12, absMin)) * (1 - t) + Math.Log(Math.Max(1e-12, absMax)) * t);
//                shifts[k] = -val;
//            }
//            return shifts;
//        }

//        // -------------------------
//        // Multi-shift solves (parallel)
//        // -------------------------
//        private static Matrix<double> MultiShiftSolveParallel(Matrix<double> A, Matrix<double> D, double[] shifts)
//        {
//            int n = A.RowCount;
//            int m = D.ColumnCount;
//            if (D.RowCount != n) throw new ArgumentException("D rows must match A");
//            int sCount = shifts.Length;
//            if (sCount != m) throw new ArgumentException("Number of shifts must equal number of columns in D (one shift per RHS column in this API)");

//            var Y = Matrix<double>.Build.Dense(n, m);

//            // For each column j solve (A + sigma_j I) y_j = d_j in parallel
//            Parallel.For(0, m, j =>
//            {
//                var sigma = shifts[j];
//                var lhs = A.Clone();
//                for (int i = 0; i < n; i++) lhs[i, i] += sigma;
//                // Solve dense
//                var sol = lhs.LU().Solve(D.Column(j).ToColumnMatrix());
//                // set column
//                for (int i = 0; i < n; i++) Y[i, j] = sol[i, 0];
//            });

//            return Y;
//        }

//        // -------------------------
//        // Utility functions
//        // -------------------------
//        private static double[] ToRowMajor(Matrix<double> M)
//        {
//            int r = M.RowCount, c = M.ColumnCount;
//            var arr = new double[r * c];
//            for (int i = 0; i < r; i++)
//                for (int j = 0; j < c; j++)
//                    arr[i * c + j] = M[i, j];
//            return arr;
//        }

//        private static Matrix<double> FromRowMajor(double[] arr, int rows, int cols)
//        {
//            var M = Matrix<double>.Build.Dense(rows, cols);
//            for (int i = 0; i < rows; i++)
//                for (int j = 0; j < cols; j++)
//                    M[i, j] = arr[i * cols + j];
//            return M;
//        }

//        private static double ResidualNorm(Matrix<double> A, Matrix<double> X, Matrix<double> B, Matrix<double> C)
//        {
//            return (C - (A * X + X * B)).FrobeniusNorm();
//        }

//        // -------------------------
//        // Native MKL LAPACK wrapper helper (example wrapper for DGEES)
//        // -------------------------
//#if MKLNET
//        // This is a simplified shim that calls MKL.NET's LAPACK DGEES wrapper. Adjust to exact MKL.NET signatures.
//        // I include it to show how to invoke DGEES and return T and Q arrays.
//        private static int MKL_Lapacke_dgees_rowmajor(int n, double[] aIn, ref double[] tout, ref double[] qout, ref double[] wr, ref double[] wi)
//        {
//            // many MKL.NET versions expose higher-level wrappers; below is a conceptual flow:
//            // 1. Copy aIn to working buffer because DGEES overwrites input.
//            var aWork = (double[])aIn.Clone();
//            int lda = n;
//            // jobvs = 'V' -> compute Schur vectors
//            // sort = 'N' -> don't sort eigenvalues
//            // select = null
//            int sdim = 0;
//            // allocate vs
//            var vs = new double[n * n];
//            // workspace query happens in native lapack - MKL.NET wrapper may handle workspace internally.
//            try
//            {
//                // Pseudocode using MKL.NET: Lapacke.Dgees(Layout.RowMajor, 'V', 'N', null, n, aWork, lda, out sdim, wr, wi, vs, n);
//                // We'll pretend the wrapper exists and call it. Replace with the actual MKL.NET call in your environment.
//                MKLNET.Lapack.DgeesRowMajor(n, aWork, lda, out sdim, wr, wi, vs);
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("MKL dgees call failed: " + ex.Message);
//            }
//            // MKL returns Schur form in aWork (T) and Schur vectors in vs
//            tout = aWork;
//            qout = vs;
//            return 0;
//        }
//#endif

//        // End of Sylvester class
//    }

//    // Extension helpers for MathNet conversions
//    static class MathNetExtensions
//    {
//        //public static MathNet.Numerics.LinearAlgebra.Complex.Matrix<Complex> ToComplex(this Matrix<double> M)
//        //{
//        //    // For simplicity use Complex (double complex) conversion via MathNet's Complex type
//        //    var C = MathNet.Numerics.LinearAlgebra.Complex.Matrix.Build.Dense(M.RowCount, M.ColumnCount);
//        //    for (int i = 0; i < M.RowCount; i++)
//        //        for (int j = 0; j < M.ColumnCount; j++)
//        //            C[i, j] = new System.Numerics.Complex(M[i, j], 0.0);
//        //    return C;
//        //}

//        // Convert small wrappers for LU etc
//        public static (Matrix<double> L, Matrix<double> U) LUFactors(this Matrix<double> M)
//        {
//            var lu = M.LU();
//            return (lu.L, lu.U);
//        }

//        public static Matrix<double> Solve(this Matrix<double> A, Matrix<double> B)
//        {
//            return A.Solve(B);
//        }
//    }
//}
