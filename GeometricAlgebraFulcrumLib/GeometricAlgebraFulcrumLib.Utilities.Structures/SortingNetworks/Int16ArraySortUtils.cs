namespace GeometricAlgebraFulcrumLib.Utilities.Structures.SortingNetworks;

public static class Int16ArraySortUtils
{
    /// <summary>
    /// Sort 2 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort2Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
    }
    
    /// <summary>
    /// Sort 3 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort3Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
    }
    
    /// <summary>
    /// Sort 4 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort4Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
    }
    
    /// <summary>
    /// Sort 5 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort5Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i3);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
    }
    
    /// <summary>
    /// Sort 6 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort6Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i5);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
    }
    
    /// <summary>
    /// Sort 7 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort7Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i6);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
    }
    
    /// <summary>
    /// Sort 8 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort8Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
    }
    
    /// <summary>
    /// Sort 9 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort9Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i3);
        Branchless.SwapIfGreaterThan(ref i1, ref i7);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i7);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
    }
    
    /// <summary>
    /// Sort 10 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort10Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i7);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
    }
    
    /// <summary>
    /// Sort 11 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort11Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i9);
        Branchless.SwapIfGreaterThan(ref i1, ref i6);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
    }
    
    /// <summary>
    /// Sort 12 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort12Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i7);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i10);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i3);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
    }
    
    /// <summary>
    /// Sort 13 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort13Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i12);
        Branchless.SwapIfGreaterThan(ref i1, ref i10);
        Branchless.SwapIfGreaterThan(ref i2, ref i9);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i11);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i1, ref i6);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i11);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i5);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
    }
    
    /// <summary>
    /// Sort 14 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort14Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i10);
        Branchless.SwapIfGreaterThan(ref i1, ref i6);
        Branchless.SwapIfGreaterThan(ref i2, ref i11);
        Branchless.SwapIfGreaterThan(ref i3, ref i13);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i11);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i4, ref i10);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
    }
    
    /// <summary>
    /// Sort 15 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort15Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i6);
        Branchless.SwapIfGreaterThan(ref i1, ref i10);
        Branchless.SwapIfGreaterThan(ref i2, ref i14);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i7);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i11);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i1, ref i13);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i3);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i13);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i3, ref i10);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
    }
    
    /// <summary>
    /// Sort 16 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort16Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i5);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i12);
        Branchless.SwapIfGreaterThan(ref i3, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i15);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i14);
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i13);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i11);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
    }
    
    /// <summary>
    /// Sort 17 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort17Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i15);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i15);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i2, ref i13);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i8, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i7);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i10);
        Branchless.SwapIfGreaterThan(ref i8, ref i14);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i9, ref i14);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i6, ref i11);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
    }
    
    /// <summary>
    /// Sort 18 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort18Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i6);
        Branchless.SwapIfGreaterThan(ref i1, ref i10);
        Branchless.SwapIfGreaterThan(ref i2, ref i15);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i16);
        Branchless.SwapIfGreaterThan(ref i8, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i12);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i5, ref i17);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i1, ref i13);
        Branchless.SwapIfGreaterThan(ref i2, ref i7);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i15);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i11);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i16);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i4, ref i10);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
    }
    
    /// <summary>
    /// Sort 19 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort19Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i11);
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i2, ref i13);
        Branchless.SwapIfGreaterThan(ref i3, ref i17);
        Branchless.SwapIfGreaterThan(ref i4, ref i10);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i13);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i14);
        Branchless.SwapIfGreaterThan(ref i6, ref i16);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i8, ref i15);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i11);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i12);
        Branchless.SwapIfGreaterThan(ref i6, ref i15);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i11);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
    }
    
    /// <summary>
    /// Sort 20 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort20Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i12);
        Branchless.SwapIfGreaterThan(ref i1, ref i13);
        Branchless.SwapIfGreaterThan(ref i2, ref i14);
        Branchless.SwapIfGreaterThan(ref i3, ref i15);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i17);
        Branchless.SwapIfGreaterThan(ref i6, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i19);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i12);
        Branchless.SwapIfGreaterThan(ref i2, ref i16);
        Branchless.SwapIfGreaterThan(ref i3, ref i17);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i18);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i10);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i18);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
    }
    
    /// <summary>
    /// Sort 21 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort21Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i7);
        Branchless.SwapIfGreaterThan(ref i1, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i13);
        Branchless.SwapIfGreaterThan(ref i9, ref i19);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i11);
        Branchless.SwapIfGreaterThan(ref i1, ref i15);
        Branchless.SwapIfGreaterThan(ref i2, ref i12);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i14);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i19);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i6);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i18);
        Branchless.SwapIfGreaterThan(ref i4, ref i15);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i20);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i18);
        Branchless.SwapIfGreaterThan(ref i8, ref i14);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i17);
        Branchless.SwapIfGreaterThan(ref i13, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i14, ref i19);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i15);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i18);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i19);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
    }
    
    /// <summary>
    /// Sort 22 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort22Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i14);
        Branchless.SwapIfGreaterThan(ref i1, ref i15);
        Branchless.SwapIfGreaterThan(ref i2, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i17);
        Branchless.SwapIfGreaterThan(ref i6, ref i20);
        Branchless.SwapIfGreaterThan(ref i7, ref i21);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i4, ref i14);
        Branchless.SwapIfGreaterThan(ref i5, ref i12);
        Branchless.SwapIfGreaterThan(ref i6, ref i15);
        Branchless.SwapIfGreaterThan(ref i7, ref i17);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i13, ref i21);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i18);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i8, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i14);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
    }
    
    /// <summary>
    /// Sort 23 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort23Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i21);
        Branchless.SwapIfGreaterThan(ref i17, ref i22);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i1, ref i10);
        Branchless.SwapIfGreaterThan(ref i2, ref i9);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i6, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i21);
        Branchless.SwapIfGreaterThan(ref i4, ref i17);
        Branchless.SwapIfGreaterThan(ref i5, ref i14);
        Branchless.SwapIfGreaterThan(ref i6, ref i13);
        Branchless.SwapIfGreaterThan(ref i7, ref i22);
        Branchless.SwapIfGreaterThan(ref i9, ref i18);
        Branchless.SwapIfGreaterThan(ref i10, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i10);
        Branchless.SwapIfGreaterThan(ref i2, ref i9);
        Branchless.SwapIfGreaterThan(ref i3, ref i17);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i18);
        Branchless.SwapIfGreaterThan(ref i6, ref i20);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i21);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i11, ref i21);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i18);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
    }
    
    /// <summary>
    /// Sort 24 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort24Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i18);
        Branchless.SwapIfGreaterThan(ref i2, ref i17);
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i4, ref i20);
        Branchless.SwapIfGreaterThan(ref i5, ref i22);
        Branchless.SwapIfGreaterThan(ref i6, ref i21);
        Branchless.SwapIfGreaterThan(ref i7, ref i23);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i5, ref i18);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i13, ref i21);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i12);
        Branchless.SwapIfGreaterThan(ref i3, ref i20);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i17);
        Branchless.SwapIfGreaterThan(ref i7, ref i19);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i16);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i20);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i13, ref i18);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
    }
    
    /// <summary>
    /// Sort 25 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort25Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i12);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i14);
        Branchless.SwapIfGreaterThan(ref i4, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i11);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i17, ref i22);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        Branchless.SwapIfGreaterThan(ref i19, ref i24);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i17);
        Branchless.SwapIfGreaterThan(ref i6, ref i20);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i16);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i21);
        Branchless.SwapIfGreaterThan(ref i6, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i23);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i18);
        Branchless.SwapIfGreaterThan(ref i9, ref i21);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i19);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i20);
        Branchless.SwapIfGreaterThan(ref i13, ref i22);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i19);
        Branchless.SwapIfGreaterThan(ref i14, ref i21);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
    }
    
    /// <summary>
    /// Sort 26 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort26Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i14);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i18);
        Branchless.SwapIfGreaterThan(ref i1, ref i19);
        Branchless.SwapIfGreaterThan(ref i2, ref i20);
        Branchless.SwapIfGreaterThan(ref i3, ref i21);
        Branchless.SwapIfGreaterThan(ref i4, ref i22);
        Branchless.SwapIfGreaterThan(ref i5, ref i23);
        Branchless.SwapIfGreaterThan(ref i6, ref i24);
        Branchless.SwapIfGreaterThan(ref i7, ref i25);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i18);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i20);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i22);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i24);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i3, ref i13);
        Branchless.SwapIfGreaterThan(ref i5, ref i15);
        Branchless.SwapIfGreaterThan(ref i6, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i19);
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i20);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i18);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i17, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i20);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
    }
    
    /// <summary>
    /// Sort 27 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort27Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i17);
        Branchless.SwapIfGreaterThan(ref i1, ref i20);
        Branchless.SwapIfGreaterThan(ref i2, ref i23);
        Branchless.SwapIfGreaterThan(ref i3, ref i24);
        Branchless.SwapIfGreaterThan(ref i4, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i18);
        Branchless.SwapIfGreaterThan(ref i6, ref i16);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i8, ref i15);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i21);
        Branchless.SwapIfGreaterThan(ref i19, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i2, ref i19);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i25);
        Branchless.SwapIfGreaterThan(ref i13, ref i24);
        Branchless.SwapIfGreaterThan(ref i14, ref i21);
        Branchless.SwapIfGreaterThan(ref i15, ref i20);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i23);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i19);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i17, ref i26);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i12);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i8, ref i18);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i4, ref i22);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i8, ref i13);
        Branchless.SwapIfGreaterThan(ref i9, ref i25);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i11);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i18);
        Branchless.SwapIfGreaterThan(ref i14, ref i26);
        Branchless.SwapIfGreaterThan(ref i16, ref i23);
        Branchless.SwapIfGreaterThan(ref i17, ref i22);
        Branchless.SwapIfGreaterThan(ref i20, ref i25);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i14);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i18, ref i25);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i7);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i19);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
    }
    
    /// <summary>
    /// Sort 28 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort28Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i20);
        Branchless.SwapIfGreaterThan(ref i1, ref i21);
        Branchless.SwapIfGreaterThan(ref i2, ref i22);
        Branchless.SwapIfGreaterThan(ref i3, ref i23);
        Branchless.SwapIfGreaterThan(ref i4, ref i24);
        Branchless.SwapIfGreaterThan(ref i5, ref i25);
        Branchless.SwapIfGreaterThan(ref i6, ref i26);
        Branchless.SwapIfGreaterThan(ref i7, ref i27);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i4, ref i20);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i23);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i18);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i12);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i16);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i25);
        Branchless.SwapIfGreaterThan(ref i17, ref i23);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i17);
        Branchless.SwapIfGreaterThan(ref i8, ref i14);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i22);
        Branchless.SwapIfGreaterThan(ref i13, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i11);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i16, ref i22);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i26);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i21);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i14);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i20);
        Branchless.SwapIfGreaterThan(ref i13, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i3, ref i10);
        Branchless.SwapIfGreaterThan(ref i5, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i17, ref i24);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
    }
    
    /// <summary>
    /// Sort 29 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort29Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i17, ref i24);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i22);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i25);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i2, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i4, ref i20);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i23);
        Branchless.SwapIfGreaterThan(ref i8, ref i24);
        Branchless.SwapIfGreaterThan(ref i11, ref i27);
        Branchless.SwapIfGreaterThan(ref i12, ref i28);
        Branchless.SwapIfGreaterThan(ref i13, ref i25);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i3, ref i17);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i21);
        Branchless.SwapIfGreaterThan(ref i6, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i20);
        Branchless.SwapIfGreaterThan(ref i10, ref i26);
        Branchless.SwapIfGreaterThan(ref i11, ref i23);
        Branchless.SwapIfGreaterThan(ref i14, ref i28);
        Branchless.SwapIfGreaterThan(ref i15, ref i27);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i16);
        Branchless.SwapIfGreaterThan(ref i7, ref i17);
        Branchless.SwapIfGreaterThan(ref i9, ref i21);
        Branchless.SwapIfGreaterThan(ref i10, ref i22);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i14, ref i24);
        Branchless.SwapIfGreaterThan(ref i15, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i28);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i22);
        Branchless.SwapIfGreaterThan(ref i14, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
    }
    
    /// <summary>
    /// Sort 30 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort30Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i14);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i13, ref i19);
        Branchless.SwapIfGreaterThan(ref i15, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i14);
        Branchless.SwapIfGreaterThan(ref i3, ref i17);
        Branchless.SwapIfGreaterThan(ref i4, ref i10);
        Branchless.SwapIfGreaterThan(ref i5, ref i11);
        Branchless.SwapIfGreaterThan(ref i6, ref i16);
        Branchless.SwapIfGreaterThan(ref i12, ref i26);
        Branchless.SwapIfGreaterThan(ref i13, ref i23);
        Branchless.SwapIfGreaterThan(ref i15, ref i27);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i13);
        Branchless.SwapIfGreaterThan(ref i2, ref i12);
        Branchless.SwapIfGreaterThan(ref i3, ref i15);
        Branchless.SwapIfGreaterThan(ref i4, ref i18);
        Branchless.SwapIfGreaterThan(ref i5, ref i19);
        Branchless.SwapIfGreaterThan(ref i6, ref i20);
        Branchless.SwapIfGreaterThan(ref i7, ref i21);
        Branchless.SwapIfGreaterThan(ref i8, ref i22);
        Branchless.SwapIfGreaterThan(ref i9, ref i23);
        Branchless.SwapIfGreaterThan(ref i10, ref i24);
        Branchless.SwapIfGreaterThan(ref i11, ref i25);
        Branchless.SwapIfGreaterThan(ref i14, ref i26);
        Branchless.SwapIfGreaterThan(ref i16, ref i28);
        Branchless.SwapIfGreaterThan(ref i17, ref i27);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i13);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i22);
        Branchless.SwapIfGreaterThan(ref i7, ref i23);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i16, ref i26);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i27);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i14);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i28);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i16, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i18);
        Branchless.SwapIfGreaterThan(ref i5, ref i20);
        Branchless.SwapIfGreaterThan(ref i7, ref i22);
        Branchless.SwapIfGreaterThan(ref i9, ref i24);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i26);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i21, ref i28);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i10);
        Branchless.SwapIfGreaterThan(ref i5, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i14);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i18);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i17, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
    }
    
    /// <summary>
    /// Sort 31 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort31Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i17, ref i24);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i22);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i25);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i2, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i4, ref i20);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i23);
        Branchless.SwapIfGreaterThan(ref i8, ref i24);
        Branchless.SwapIfGreaterThan(ref i11, ref i27);
        Branchless.SwapIfGreaterThan(ref i12, ref i28);
        Branchless.SwapIfGreaterThan(ref i13, ref i29);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i3, ref i17);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i21);
        Branchless.SwapIfGreaterThan(ref i6, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i20);
        Branchless.SwapIfGreaterThan(ref i10, ref i26);
        Branchless.SwapIfGreaterThan(ref i11, ref i23);
        Branchless.SwapIfGreaterThan(ref i13, ref i25);
        Branchless.SwapIfGreaterThan(ref i14, ref i28);
        Branchless.SwapIfGreaterThan(ref i15, ref i27);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i16);
        Branchless.SwapIfGreaterThan(ref i7, ref i17);
        Branchless.SwapIfGreaterThan(ref i9, ref i21);
        Branchless.SwapIfGreaterThan(ref i10, ref i22);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i14, ref i24);
        Branchless.SwapIfGreaterThan(ref i15, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i22);
        Branchless.SwapIfGreaterThan(ref i14, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i26, ref i29);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
    }
    
    /// <summary>
    /// Sort 32 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort32Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i15, ref i31);
        Branchless.SwapIfGreaterThan(ref i17, ref i24);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i22);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i25);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i2, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i4, ref i20);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i23);
        Branchless.SwapIfGreaterThan(ref i8, ref i24);
        Branchless.SwapIfGreaterThan(ref i11, ref i27);
        Branchless.SwapIfGreaterThan(ref i12, ref i28);
        Branchless.SwapIfGreaterThan(ref i13, ref i29);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i3, ref i17);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i21);
        Branchless.SwapIfGreaterThan(ref i6, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i20);
        Branchless.SwapIfGreaterThan(ref i10, ref i26);
        Branchless.SwapIfGreaterThan(ref i11, ref i23);
        Branchless.SwapIfGreaterThan(ref i13, ref i25);
        Branchless.SwapIfGreaterThan(ref i14, ref i28);
        Branchless.SwapIfGreaterThan(ref i15, ref i27);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i16);
        Branchless.SwapIfGreaterThan(ref i7, ref i17);
        Branchless.SwapIfGreaterThan(ref i9, ref i21);
        Branchless.SwapIfGreaterThan(ref i10, ref i22);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i14, ref i24);
        Branchless.SwapIfGreaterThan(ref i15, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i22);
        Branchless.SwapIfGreaterThan(ref i14, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i26, ref i29);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
    }
    
    /// <summary>
    /// Sort 33 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort33Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i2, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i4, ref i20);
        Branchless.SwapIfGreaterThan(ref i5, ref i21);
        Branchless.SwapIfGreaterThan(ref i6, ref i22);
        Branchless.SwapIfGreaterThan(ref i7, ref i23);
        Branchless.SwapIfGreaterThan(ref i8, ref i24);
        Branchless.SwapIfGreaterThan(ref i9, ref i25);
        Branchless.SwapIfGreaterThan(ref i10, ref i26);
        Branchless.SwapIfGreaterThan(ref i11, ref i27);
        Branchless.SwapIfGreaterThan(ref i12, ref i28);
        Branchless.SwapIfGreaterThan(ref i13, ref i29);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i15, ref i31);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i32);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i3, ref i17);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i20);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i22);
        Branchless.SwapIfGreaterThan(ref i11, ref i29);
        Branchless.SwapIfGreaterThan(ref i12, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i23);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i18);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i20);
        Branchless.SwapIfGreaterThan(ref i10, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i19, ref i26);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i2, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i13, ref i24);
        Branchless.SwapIfGreaterThan(ref i14, ref i25);
        Branchless.SwapIfGreaterThan(ref i15, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i32);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i0, ref i6);
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i18);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i24);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
    }
    
    /// <summary>
    /// Sort 34 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort34Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i33);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i2, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i4, ref i20);
        Branchless.SwapIfGreaterThan(ref i5, ref i21);
        Branchless.SwapIfGreaterThan(ref i6, ref i22);
        Branchless.SwapIfGreaterThan(ref i7, ref i23);
        Branchless.SwapIfGreaterThan(ref i8, ref i24);
        Branchless.SwapIfGreaterThan(ref i9, ref i25);
        Branchless.SwapIfGreaterThan(ref i10, ref i26);
        Branchless.SwapIfGreaterThan(ref i11, ref i27);
        Branchless.SwapIfGreaterThan(ref i12, ref i28);
        Branchless.SwapIfGreaterThan(ref i13, ref i29);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i15, ref i31);
        Branchless.SwapIfGreaterThan(ref i16, ref i32);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i13);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i11);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i29);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i27);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i22);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i21);
        Branchless.SwapIfGreaterThan(ref i7, ref i18);
        Branchless.SwapIfGreaterThan(ref i8, ref i19);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i32);
        Branchless.SwapIfGreaterThan(ref i12, ref i27);
        Branchless.SwapIfGreaterThan(ref i14, ref i25);
        Branchless.SwapIfGreaterThan(ref i15, ref i26);
        Branchless.SwapIfGreaterThan(ref i20, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i30);
        Branchless.SwapIfGreaterThan(ref i28, ref i31);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i4, ref i11);
        Branchless.SwapIfGreaterThan(ref i8, ref i14);
        Branchless.SwapIfGreaterThan(ref i10, ref i21);
        Branchless.SwapIfGreaterThan(ref i12, ref i23);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i29);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i15);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i22);
        Branchless.SwapIfGreaterThan(ref i18, ref i29);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i15);
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i12, ref i17);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i16, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i27);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i19);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i21);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
    }
    
    /// <summary>
    /// Sort 35 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort35Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i2, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i4, ref i20);
        Branchless.SwapIfGreaterThan(ref i5, ref i21);
        Branchless.SwapIfGreaterThan(ref i6, ref i22);
        Branchless.SwapIfGreaterThan(ref i7, ref i23);
        Branchless.SwapIfGreaterThan(ref i8, ref i24);
        Branchless.SwapIfGreaterThan(ref i9, ref i25);
        Branchless.SwapIfGreaterThan(ref i10, ref i26);
        Branchless.SwapIfGreaterThan(ref i11, ref i27);
        Branchless.SwapIfGreaterThan(ref i12, ref i28);
        Branchless.SwapIfGreaterThan(ref i13, ref i29);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i15, ref i31);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i34);
        Branchless.SwapIfGreaterThan(ref i16, ref i32);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i9, ref i19);
        Branchless.SwapIfGreaterThan(ref i10, ref i32);
        Branchless.SwapIfGreaterThan(ref i15, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i33);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i29);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i2, ref i15);
        Branchless.SwapIfGreaterThan(ref i3, ref i10);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i7, ref i33);
        Branchless.SwapIfGreaterThan(ref i9, ref i32);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i28, ref i31);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i15);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i8, ref i17);
        Branchless.SwapIfGreaterThan(ref i11, ref i20);
        Branchless.SwapIfGreaterThan(ref i12, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i33);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i16);
        Branchless.SwapIfGreaterThan(ref i7, ref i18);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i17);
        Branchless.SwapIfGreaterThan(ref i11, ref i22);
        Branchless.SwapIfGreaterThan(ref i13, ref i19);
        Branchless.SwapIfGreaterThan(ref i14, ref i24);
        Branchless.SwapIfGreaterThan(ref i20, ref i26);
        Branchless.SwapIfGreaterThan(ref i21, ref i32);
        Branchless.SwapIfGreaterThan(ref i23, ref i33);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i17);
        Branchless.SwapIfGreaterThan(ref i13, ref i21);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i24);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i22, ref i32);
        Branchless.SwapIfGreaterThan(ref i26, ref i29);
        Branchless.SwapIfGreaterThan(ref i27, ref i33);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i30, ref i33);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
    }
    
    /// <summary>
    /// Sort 36 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort36Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i20);
        Branchless.SwapIfGreaterThan(ref i1, ref i21);
        Branchless.SwapIfGreaterThan(ref i2, ref i22);
        Branchless.SwapIfGreaterThan(ref i3, ref i23);
        Branchless.SwapIfGreaterThan(ref i4, ref i24);
        Branchless.SwapIfGreaterThan(ref i5, ref i25);
        Branchless.SwapIfGreaterThan(ref i6, ref i26);
        Branchless.SwapIfGreaterThan(ref i7, ref i27);
        Branchless.SwapIfGreaterThan(ref i8, ref i28);
        Branchless.SwapIfGreaterThan(ref i9, ref i29);
        Branchless.SwapIfGreaterThan(ref i10, ref i30);
        Branchless.SwapIfGreaterThan(ref i11, ref i31);
        Branchless.SwapIfGreaterThan(ref i12, ref i32);
        Branchless.SwapIfGreaterThan(ref i13, ref i33);
        Branchless.SwapIfGreaterThan(ref i14, ref i34);
        Branchless.SwapIfGreaterThan(ref i15, ref i35);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i28);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i17);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i32);
        Branchless.SwapIfGreaterThan(ref i9, ref i22);
        Branchless.SwapIfGreaterThan(ref i10, ref i21);
        Branchless.SwapIfGreaterThan(ref i13, ref i26);
        Branchless.SwapIfGreaterThan(ref i14, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i30);
        Branchless.SwapIfGreaterThan(ref i19, ref i31);
        Branchless.SwapIfGreaterThan(ref i23, ref i29);
        Branchless.SwapIfGreaterThan(ref i27, ref i33);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i3, ref i18);
        Branchless.SwapIfGreaterThan(ref i5, ref i24);
        Branchless.SwapIfGreaterThan(ref i7, ref i19);
        Branchless.SwapIfGreaterThan(ref i8, ref i20);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i30);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i27);
        Branchless.SwapIfGreaterThan(ref i16, ref i28);
        Branchless.SwapIfGreaterThan(ref i17, ref i32);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i21);
        Branchless.SwapIfGreaterThan(ref i7, ref i18);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i13, ref i23);
        Branchless.SwapIfGreaterThan(ref i14, ref i29);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i20);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i30);
        Branchless.SwapIfGreaterThan(ref i17, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i26, ref i32);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i21);
        Branchless.SwapIfGreaterThan(ref i10, ref i20);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i28);
        Branchless.SwapIfGreaterThan(ref i15, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i23);
        Branchless.SwapIfGreaterThan(ref i19, ref i33);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i18);
        Branchless.SwapIfGreaterThan(ref i14, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i21);
        Branchless.SwapIfGreaterThan(ref i17, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i14);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i21, ref i28);
        Branchless.SwapIfGreaterThan(ref i26, ref i29);
        Branchless.SwapIfGreaterThan(ref i27, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        Branchless.SwapIfGreaterThan(ref i19, ref i26);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i29);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
    }
    
    /// <summary>
    /// Sort 37 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort37Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i35);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i2, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i4, ref i20);
        Branchless.SwapIfGreaterThan(ref i5, ref i21);
        Branchless.SwapIfGreaterThan(ref i6, ref i22);
        Branchless.SwapIfGreaterThan(ref i7, ref i23);
        Branchless.SwapIfGreaterThan(ref i8, ref i24);
        Branchless.SwapIfGreaterThan(ref i9, ref i25);
        Branchless.SwapIfGreaterThan(ref i10, ref i26);
        Branchless.SwapIfGreaterThan(ref i11, ref i27);
        Branchless.SwapIfGreaterThan(ref i12, ref i28);
        Branchless.SwapIfGreaterThan(ref i13, ref i29);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i15, ref i31);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i32);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i36);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i9, ref i31);
        Branchless.SwapIfGreaterThan(ref i12, ref i34);
        Branchless.SwapIfGreaterThan(ref i16, ref i32);
        Branchless.SwapIfGreaterThan(ref i17, ref i33);
        Branchless.SwapIfGreaterThan(ref i18, ref i35);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i2, ref i12);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i32);
        Branchless.SwapIfGreaterThan(ref i11, ref i35);
        Branchless.SwapIfGreaterThan(ref i13, ref i33);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i34);
        Branchless.SwapIfGreaterThan(ref i26, ref i31);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i5, ref i18);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i8, ref i20);
        Branchless.SwapIfGreaterThan(ref i9, ref i19);
        Branchless.SwapIfGreaterThan(ref i11, ref i33);
        Branchless.SwapIfGreaterThan(ref i12, ref i32);
        Branchless.SwapIfGreaterThan(ref i14, ref i26);
        Branchless.SwapIfGreaterThan(ref i15, ref i31);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i34);
        Branchless.SwapIfGreaterThan(ref i23, ref i35);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i20);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i17);
        Branchless.SwapIfGreaterThan(ref i11, ref i21);
        Branchless.SwapIfGreaterThan(ref i13, ref i24);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i28);
        Branchless.SwapIfGreaterThan(ref i19, ref i32);
        Branchless.SwapIfGreaterThan(ref i23, ref i29);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i30, ref i35);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i19);
        Branchless.SwapIfGreaterThan(ref i15, ref i32);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i34);
        Branchless.SwapIfGreaterThan(ref i30, ref i33);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i18);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i21);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i34);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i16);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i26, ref i32);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
    }
    
    /// <summary>
    /// Sort 38 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort38Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i22);
        Branchless.SwapIfGreaterThan(ref i1, ref i23);
        Branchless.SwapIfGreaterThan(ref i2, ref i24);
        Branchless.SwapIfGreaterThan(ref i3, ref i25);
        Branchless.SwapIfGreaterThan(ref i4, ref i26);
        Branchless.SwapIfGreaterThan(ref i5, ref i27);
        Branchless.SwapIfGreaterThan(ref i6, ref i28);
        Branchless.SwapIfGreaterThan(ref i7, ref i29);
        Branchless.SwapIfGreaterThan(ref i8, ref i30);
        Branchless.SwapIfGreaterThan(ref i9, ref i31);
        Branchless.SwapIfGreaterThan(ref i10, ref i32);
        Branchless.SwapIfGreaterThan(ref i11, ref i33);
        Branchless.SwapIfGreaterThan(ref i12, ref i34);
        Branchless.SwapIfGreaterThan(ref i13, ref i35);
        Branchless.SwapIfGreaterThan(ref i14, ref i36);
        Branchless.SwapIfGreaterThan(ref i15, ref i37);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i37);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i34);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i32);
        Branchless.SwapIfGreaterThan(ref i28, ref i31);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i4, ref i17);
        Branchless.SwapIfGreaterThan(ref i5, ref i15);
        Branchless.SwapIfGreaterThan(ref i7, ref i19);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i18, ref i30);
        Branchless.SwapIfGreaterThan(ref i20, ref i33);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i22, ref i32);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i35);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i2, ref i13);
        Branchless.SwapIfGreaterThan(ref i3, ref i23);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i14, ref i34);
        Branchless.SwapIfGreaterThan(ref i15, ref i25);
        Branchless.SwapIfGreaterThan(ref i16, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        Branchless.SwapIfGreaterThan(ref i24, ref i35);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i26);
        Branchless.SwapIfGreaterThan(ref i8, ref i18);
        Branchless.SwapIfGreaterThan(ref i9, ref i27);
        Branchless.SwapIfGreaterThan(ref i10, ref i28);
        Branchless.SwapIfGreaterThan(ref i11, ref i30);
        Branchless.SwapIfGreaterThan(ref i13, ref i23);
        Branchless.SwapIfGreaterThan(ref i14, ref i24);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i19, ref i29);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i25, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i17);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i9, ref i23);
        Branchless.SwapIfGreaterThan(ref i10, ref i21);
        Branchless.SwapIfGreaterThan(ref i11, ref i22);
        Branchless.SwapIfGreaterThan(ref i12, ref i18);
        Branchless.SwapIfGreaterThan(ref i14, ref i28);
        Branchless.SwapIfGreaterThan(ref i15, ref i26);
        Branchless.SwapIfGreaterThan(ref i16, ref i27);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i20, ref i35);
        Branchless.SwapIfGreaterThan(ref i24, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i21);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i26);
        Branchless.SwapIfGreaterThan(ref i18, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i24, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i9, ref i15);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i22, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i23);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i30, ref i33);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
    }
    
    /// <summary>
    /// Sort 39 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort39Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i14);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i15, ref i38);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i36);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i3);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i32);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i7);
        Branchless.SwapIfGreaterThan(ref i1, ref i10);
        Branchless.SwapIfGreaterThan(ref i2, ref i9);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i11);
        Branchless.SwapIfGreaterThan(ref i15, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i36);
        Branchless.SwapIfGreaterThan(ref i31, ref i38);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i15);
        Branchless.SwapIfGreaterThan(ref i1, ref i26);
        Branchless.SwapIfGreaterThan(ref i2, ref i25);
        Branchless.SwapIfGreaterThan(ref i3, ref i28);
        Branchless.SwapIfGreaterThan(ref i4, ref i30);
        Branchless.SwapIfGreaterThan(ref i5, ref i29);
        Branchless.SwapIfGreaterThan(ref i6, ref i27);
        Branchless.SwapIfGreaterThan(ref i7, ref i24);
        Branchless.SwapIfGreaterThan(ref i8, ref i32);
        Branchless.SwapIfGreaterThan(ref i9, ref i33);
        Branchless.SwapIfGreaterThan(ref i10, ref i34);
        Branchless.SwapIfGreaterThan(ref i11, ref i35);
        Branchless.SwapIfGreaterThan(ref i12, ref i36);
        Branchless.SwapIfGreaterThan(ref i13, ref i37);
        Branchless.SwapIfGreaterThan(ref i14, ref i31);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i7);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i34);
        Branchless.SwapIfGreaterThan(ref i30, ref i33);
        Branchless.SwapIfGreaterThan(ref i31, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i15);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i22);
        Branchless.SwapIfGreaterThan(ref i7, ref i16);
        Branchless.SwapIfGreaterThan(ref i8, ref i26);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i13, ref i27);
        Branchless.SwapIfGreaterThan(ref i14, ref i21);
        Branchless.SwapIfGreaterThan(ref i17, ref i32);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i17);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i10, ref i26);
        Branchless.SwapIfGreaterThan(ref i12, ref i24);
        Branchless.SwapIfGreaterThan(ref i13, ref i29);
        Branchless.SwapIfGreaterThan(ref i14, ref i25);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i21, ref i37);
        Branchless.SwapIfGreaterThan(ref i22, ref i38);
        Branchless.SwapIfGreaterThan(ref i27, ref i33);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i4, ref i18);
        Branchless.SwapIfGreaterThan(ref i5, ref i14);
        Branchless.SwapIfGreaterThan(ref i8, ref i20);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i17);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i21, ref i34);
        Branchless.SwapIfGreaterThan(ref i22, ref i29);
        Branchless.SwapIfGreaterThan(ref i23, ref i35);
        Branchless.SwapIfGreaterThan(ref i24, ref i33);
        Branchless.SwapIfGreaterThan(ref i25, ref i30);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i31, ref i38);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i18);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i13, ref i19);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i20, ref i26);
        Branchless.SwapIfGreaterThan(ref i21, ref i32);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i24, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i18);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i2, ref i7);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i15);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i12, ref i17);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i28);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i10, ref i15);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i18);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i27);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i35);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
    }
    
    /// <summary>
    /// Sort 40 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort40Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i39);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i24);
        Branchless.SwapIfGreaterThan(ref i1, ref i25);
        Branchless.SwapIfGreaterThan(ref i2, ref i26);
        Branchless.SwapIfGreaterThan(ref i3, ref i27);
        Branchless.SwapIfGreaterThan(ref i4, ref i28);
        Branchless.SwapIfGreaterThan(ref i5, ref i29);
        Branchless.SwapIfGreaterThan(ref i6, ref i30);
        Branchless.SwapIfGreaterThan(ref i7, ref i31);
        Branchless.SwapIfGreaterThan(ref i8, ref i32);
        Branchless.SwapIfGreaterThan(ref i9, ref i33);
        Branchless.SwapIfGreaterThan(ref i10, ref i34);
        Branchless.SwapIfGreaterThan(ref i11, ref i35);
        Branchless.SwapIfGreaterThan(ref i12, ref i36);
        Branchless.SwapIfGreaterThan(ref i13, ref i37);
        Branchless.SwapIfGreaterThan(ref i14, ref i38);
        Branchless.SwapIfGreaterThan(ref i15, ref i39);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i23, ref i39);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i26, ref i32);
        Branchless.SwapIfGreaterThan(ref i27, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i34);
        Branchless.SwapIfGreaterThan(ref i30, ref i33);
        Branchless.SwapIfGreaterThan(ref i31, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i22);
        Branchless.SwapIfGreaterThan(ref i4, ref i24);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i27);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i32);
        Branchless.SwapIfGreaterThan(ref i13, ref i21);
        Branchless.SwapIfGreaterThan(ref i15, ref i35);
        Branchless.SwapIfGreaterThan(ref i17, ref i36);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i18);
        Branchless.SwapIfGreaterThan(ref i4, ref i17);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i29);
        Branchless.SwapIfGreaterThan(ref i10, ref i32);
        Branchless.SwapIfGreaterThan(ref i13, ref i25);
        Branchless.SwapIfGreaterThan(ref i14, ref i26);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i21, ref i38);
        Branchless.SwapIfGreaterThan(ref i22, ref i35);
        Branchless.SwapIfGreaterThan(ref i27, ref i33);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i18);
        Branchless.SwapIfGreaterThan(ref i6, ref i13);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i14);
        Branchless.SwapIfGreaterThan(ref i10, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i21, ref i34);
        Branchless.SwapIfGreaterThan(ref i22, ref i29);
        Branchless.SwapIfGreaterThan(ref i25, ref i30);
        Branchless.SwapIfGreaterThan(ref i26, ref i33);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i24);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i27);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i36);
        Branchless.SwapIfGreaterThan(ref i26, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i18);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i28);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i25, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i20);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i30);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i32);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i31, ref i37);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i10);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i17, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i26);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i36);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
    }
    
    /// <summary>
    /// Sort 41 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort41Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i2, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i4, ref i20);
        Branchless.SwapIfGreaterThan(ref i5, ref i21);
        Branchless.SwapIfGreaterThan(ref i6, ref i22);
        Branchless.SwapIfGreaterThan(ref i7, ref i23);
        Branchless.SwapIfGreaterThan(ref i8, ref i24);
        Branchless.SwapIfGreaterThan(ref i9, ref i25);
        Branchless.SwapIfGreaterThan(ref i10, ref i26);
        Branchless.SwapIfGreaterThan(ref i11, ref i27);
        Branchless.SwapIfGreaterThan(ref i12, ref i28);
        Branchless.SwapIfGreaterThan(ref i13, ref i29);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i15, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i40);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i16, ref i33);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i2, ref i32);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i37);
        Branchless.SwapIfGreaterThan(ref i13, ref i36);
        Branchless.SwapIfGreaterThan(ref i17, ref i34);
        Branchless.SwapIfGreaterThan(ref i18, ref i38);
        Branchless.SwapIfGreaterThan(ref i19, ref i39);
        Branchless.SwapIfGreaterThan(ref i20, ref i35);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i24, ref i40);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i3, ref i34);
        Branchless.SwapIfGreaterThan(ref i6, ref i24);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i35);
        Branchless.SwapIfGreaterThan(ref i15, ref i39);
        Branchless.SwapIfGreaterThan(ref i18, ref i33);
        Branchless.SwapIfGreaterThan(ref i20, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i36);
        Branchless.SwapIfGreaterThan(ref i28, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i6, ref i17);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i8, ref i18);
        Branchless.SwapIfGreaterThan(ref i10, ref i33);
        Branchless.SwapIfGreaterThan(ref i12, ref i32);
        Branchless.SwapIfGreaterThan(ref i13, ref i19);
        Branchless.SwapIfGreaterThan(ref i14, ref i38);
        Branchless.SwapIfGreaterThan(ref i15, ref i29);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i25, ref i34);
        Branchless.SwapIfGreaterThan(ref i27, ref i39);
        Branchless.SwapIfGreaterThan(ref i28, ref i35);
        Branchless.SwapIfGreaterThan(ref i30, ref i40);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i20);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i32);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i36);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i34);
        Branchless.SwapIfGreaterThan(ref i23, ref i38);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i28, ref i33);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i16);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i20);
        Branchless.SwapIfGreaterThan(ref i10, ref i17);
        Branchless.SwapIfGreaterThan(ref i11, ref i25);
        Branchless.SwapIfGreaterThan(ref i12, ref i18);
        Branchless.SwapIfGreaterThan(ref i13, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i28);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i19, ref i34);
        Branchless.SwapIfGreaterThan(ref i22, ref i35);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i26, ref i33);
        Branchless.SwapIfGreaterThan(ref i27, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i38);
        Branchless.SwapIfGreaterThan(ref i30, ref i37);
        Branchless.SwapIfGreaterThan(ref i31, ref i39);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i38);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i16);
        Branchless.SwapIfGreaterThan(ref i7, ref i18);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i32);
        Branchless.SwapIfGreaterThan(ref i22, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i30);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i33);
        Branchless.SwapIfGreaterThan(ref i29, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i20);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i17, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i26, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i35);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
    }
    
    /// <summary>
    /// Sort 42 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort42Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i39);
        Branchless.SwapIfGreaterThan(ref i32, ref i40);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i26);
        Branchless.SwapIfGreaterThan(ref i1, ref i27);
        Branchless.SwapIfGreaterThan(ref i2, ref i28);
        Branchless.SwapIfGreaterThan(ref i3, ref i29);
        Branchless.SwapIfGreaterThan(ref i4, ref i30);
        Branchless.SwapIfGreaterThan(ref i5, ref i31);
        Branchless.SwapIfGreaterThan(ref i6, ref i32);
        Branchless.SwapIfGreaterThan(ref i7, ref i33);
        Branchless.SwapIfGreaterThan(ref i8, ref i34);
        Branchless.SwapIfGreaterThan(ref i9, ref i35);
        Branchless.SwapIfGreaterThan(ref i10, ref i36);
        Branchless.SwapIfGreaterThan(ref i11, ref i37);
        Branchless.SwapIfGreaterThan(ref i12, ref i38);
        Branchless.SwapIfGreaterThan(ref i13, ref i39);
        Branchless.SwapIfGreaterThan(ref i14, ref i40);
        Branchless.SwapIfGreaterThan(ref i15, ref i41);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i41);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i38);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i36);
        Branchless.SwapIfGreaterThan(ref i32, ref i35);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i22);
        Branchless.SwapIfGreaterThan(ref i14, ref i25);
        Branchless.SwapIfGreaterThan(ref i16, ref i27);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i31);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i40);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i26);
        Branchless.SwapIfGreaterThan(ref i2, ref i18);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i19);
        Branchless.SwapIfGreaterThan(ref i6, ref i21);
        Branchless.SwapIfGreaterThan(ref i9, ref i29);
        Branchless.SwapIfGreaterThan(ref i10, ref i28);
        Branchless.SwapIfGreaterThan(ref i12, ref i32);
        Branchless.SwapIfGreaterThan(ref i13, ref i31);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i40);
        Branchless.SwapIfGreaterThan(ref i20, ref i35);
        Branchless.SwapIfGreaterThan(ref i22, ref i36);
        Branchless.SwapIfGreaterThan(ref i23, ref i39);
        Branchless.SwapIfGreaterThan(ref i24, ref i27);
        Branchless.SwapIfGreaterThan(ref i25, ref i37);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i7, ref i23);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i24);
        Branchless.SwapIfGreaterThan(ref i11, ref i27);
        Branchless.SwapIfGreaterThan(ref i12, ref i28);
        Branchless.SwapIfGreaterThan(ref i13, ref i29);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i15, ref i25);
        Branchless.SwapIfGreaterThan(ref i16, ref i26);
        Branchless.SwapIfGreaterThan(ref i17, ref i32);
        Branchless.SwapIfGreaterThan(ref i18, ref i34);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i5, ref i16);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i29);
        Branchless.SwapIfGreaterThan(ref i12, ref i30);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i22, ref i38);
        Branchless.SwapIfGreaterThan(ref i25, ref i36);
        Branchless.SwapIfGreaterThan(ref i28, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i33, ref i39);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i8, ref i18);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i10, ref i26);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i17);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i31);
        Branchless.SwapIfGreaterThan(ref i21, ref i28);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i33);
        Branchless.SwapIfGreaterThan(ref i24, ref i29);
        Branchless.SwapIfGreaterThan(ref i25, ref i32);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i39);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i12, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i29);
        Branchless.SwapIfGreaterThan(ref i25, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i28, ref i34);
        Branchless.SwapIfGreaterThan(ref i32, ref i38);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i14);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i18);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i16, ref i21);
        Branchless.SwapIfGreaterThan(ref i19, ref i26);
        Branchless.SwapIfGreaterThan(ref i20, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i30);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i34);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
    }
    
    /// <summary>
    /// Sort 43 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort43Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i14);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i15, ref i42);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i40);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i3);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i36);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i7);
        Branchless.SwapIfGreaterThan(ref i1, ref i10);
        Branchless.SwapIfGreaterThan(ref i2, ref i9);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i11);
        Branchless.SwapIfGreaterThan(ref i15, ref i28);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i39);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        Branchless.SwapIfGreaterThan(ref i34, ref i40);
        Branchless.SwapIfGreaterThan(ref i35, ref i42);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i15);
        Branchless.SwapIfGreaterThan(ref i1, ref i30);
        Branchless.SwapIfGreaterThan(ref i2, ref i29);
        Branchless.SwapIfGreaterThan(ref i3, ref i32);
        Branchless.SwapIfGreaterThan(ref i4, ref i34);
        Branchless.SwapIfGreaterThan(ref i5, ref i33);
        Branchless.SwapIfGreaterThan(ref i6, ref i31);
        Branchless.SwapIfGreaterThan(ref i7, ref i28);
        Branchless.SwapIfGreaterThan(ref i8, ref i36);
        Branchless.SwapIfGreaterThan(ref i9, ref i37);
        Branchless.SwapIfGreaterThan(ref i10, ref i38);
        Branchless.SwapIfGreaterThan(ref i11, ref i39);
        Branchless.SwapIfGreaterThan(ref i12, ref i40);
        Branchless.SwapIfGreaterThan(ref i13, ref i41);
        Branchless.SwapIfGreaterThan(ref i14, ref i35);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i42);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i4, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i38);
        Branchless.SwapIfGreaterThan(ref i34, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i28);
        Branchless.SwapIfGreaterThan(ref i12, ref i23);
        Branchless.SwapIfGreaterThan(ref i14, ref i34);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i20, ref i30);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i3, ref i15);
        Branchless.SwapIfGreaterThan(ref i8, ref i17);
        Branchless.SwapIfGreaterThan(ref i9, ref i20);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i21);
        Branchless.SwapIfGreaterThan(ref i13, ref i24);
        Branchless.SwapIfGreaterThan(ref i14, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i28);
        Branchless.SwapIfGreaterThan(ref i19, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i32);
        Branchless.SwapIfGreaterThan(ref i23, ref i34);
        Branchless.SwapIfGreaterThan(ref i26, ref i33);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i4, ref i11);
        Branchless.SwapIfGreaterThan(ref i5, ref i19);
        Branchless.SwapIfGreaterThan(ref i6, ref i13);
        Branchless.SwapIfGreaterThan(ref i7, ref i18);
        Branchless.SwapIfGreaterThan(ref i12, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i27);
        Branchless.SwapIfGreaterThan(ref i16, ref i28);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i38);
        Branchless.SwapIfGreaterThan(ref i25, ref i35);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i36);
        Branchless.SwapIfGreaterThan(ref i31, ref i40);
        Branchless.SwapIfGreaterThan(ref i32, ref i37);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i9);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i10, ref i20);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i26);
        Branchless.SwapIfGreaterThan(ref i14, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i22);
        Branchless.SwapIfGreaterThan(ref i17, ref i29);
        Branchless.SwapIfGreaterThan(ref i21, ref i27);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i36);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i40);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i7);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i14);
        Branchless.SwapIfGreaterThan(ref i10, ref i15);
        Branchless.SwapIfGreaterThan(ref i12, ref i19);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i24, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i33);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i4, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i25, ref i34);
        Branchless.SwapIfGreaterThan(ref i26, ref i32);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i37);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i35);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i36, ref i39);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
    }
    
    /// <summary>
    /// Sort 44 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort44Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i39);
        Branchless.SwapIfGreaterThan(ref i32, ref i40);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        Branchless.SwapIfGreaterThan(ref i34, ref i42);
        Branchless.SwapIfGreaterThan(ref i35, ref i43);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i28);
        Branchless.SwapIfGreaterThan(ref i1, ref i29);
        Branchless.SwapIfGreaterThan(ref i2, ref i30);
        Branchless.SwapIfGreaterThan(ref i3, ref i31);
        Branchless.SwapIfGreaterThan(ref i4, ref i32);
        Branchless.SwapIfGreaterThan(ref i5, ref i33);
        Branchless.SwapIfGreaterThan(ref i6, ref i34);
        Branchless.SwapIfGreaterThan(ref i7, ref i35);
        Branchless.SwapIfGreaterThan(ref i8, ref i36);
        Branchless.SwapIfGreaterThan(ref i9, ref i37);
        Branchless.SwapIfGreaterThan(ref i10, ref i38);
        Branchless.SwapIfGreaterThan(ref i11, ref i39);
        Branchless.SwapIfGreaterThan(ref i12, ref i40);
        Branchless.SwapIfGreaterThan(ref i13, ref i41);
        Branchless.SwapIfGreaterThan(ref i14, ref i42);
        Branchless.SwapIfGreaterThan(ref i15, ref i43);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i43);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i40);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i38);
        Branchless.SwapIfGreaterThan(ref i34, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i9, ref i32);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i34);
        Branchless.SwapIfGreaterThan(ref i13, ref i23);
        Branchless.SwapIfGreaterThan(ref i15, ref i27);
        Branchless.SwapIfGreaterThan(ref i16, ref i28);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i20, ref i30);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i7, ref i21);
        Branchless.SwapIfGreaterThan(ref i9, ref i20);
        Branchless.SwapIfGreaterThan(ref i10, ref i17);
        Branchless.SwapIfGreaterThan(ref i11, ref i25);
        Branchless.SwapIfGreaterThan(ref i12, ref i28);
        Branchless.SwapIfGreaterThan(ref i14, ref i24);
        Branchless.SwapIfGreaterThan(ref i15, ref i31);
        Branchless.SwapIfGreaterThan(ref i18, ref i32);
        Branchless.SwapIfGreaterThan(ref i19, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i36);
        Branchless.SwapIfGreaterThan(ref i23, ref i34);
        Branchless.SwapIfGreaterThan(ref i26, ref i33);
        Branchless.SwapIfGreaterThan(ref i27, ref i39);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i10);
        Branchless.SwapIfGreaterThan(ref i2, ref i12);
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i5, ref i14);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i40);
        Branchless.SwapIfGreaterThan(ref i25, ref i35);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i41);
        Branchless.SwapIfGreaterThan(ref i33, ref i42);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i18);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i14, ref i26);
        Branchless.SwapIfGreaterThan(ref i15, ref i21);
        Branchless.SwapIfGreaterThan(ref i17, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i38);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i30, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i41);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i19);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i24, ref i30);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i41);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i9, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i23);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i34);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i40);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i12);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i16);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i38);
        Branchless.SwapIfGreaterThan(ref i34, ref i37);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i18);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i17, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i26);
        Branchless.SwapIfGreaterThan(ref i21, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i30);
        Branchless.SwapIfGreaterThan(ref i25, ref i32);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i31, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
    }
    
    /// <summary>
    /// Sort 45 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort45Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i5, ref i12);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i44);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i44);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i40);
        Branchless.SwapIfGreaterThan(ref i14, ref i42);
        Branchless.SwapIfGreaterThan(ref i15, ref i41);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i5);
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i4, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i32);
        Branchless.SwapIfGreaterThan(ref i14, ref i34);
        Branchless.SwapIfGreaterThan(ref i15, ref i33);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i35, ref i43);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i44);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i13);
        Branchless.SwapIfGreaterThan(ref i1, ref i36);
        Branchless.SwapIfGreaterThan(ref i2, ref i14);
        Branchless.SwapIfGreaterThan(ref i3, ref i15);
        Branchless.SwapIfGreaterThan(ref i4, ref i35);
        Branchless.SwapIfGreaterThan(ref i5, ref i32);
        Branchless.SwapIfGreaterThan(ref i6, ref i34);
        Branchless.SwapIfGreaterThan(ref i7, ref i33);
        Branchless.SwapIfGreaterThan(ref i8, ref i40);
        Branchless.SwapIfGreaterThan(ref i9, ref i37);
        Branchless.SwapIfGreaterThan(ref i10, ref i38);
        Branchless.SwapIfGreaterThan(ref i11, ref i43);
        Branchless.SwapIfGreaterThan(ref i12, ref i39);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i44);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i42);
        Branchless.SwapIfGreaterThan(ref i12, ref i41);
        Branchless.SwapIfGreaterThan(ref i14, ref i32);
        Branchless.SwapIfGreaterThan(ref i15, ref i36);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i23, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i33, ref i38);
        Branchless.SwapIfGreaterThan(ref i34, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i22);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i25, ref i40);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i7, ref i21);
        Branchless.SwapIfGreaterThan(ref i8, ref i25);
        Branchless.SwapIfGreaterThan(ref i10, ref i28);
        Branchless.SwapIfGreaterThan(ref i11, ref i23);
        Branchless.SwapIfGreaterThan(ref i12, ref i29);
        Branchless.SwapIfGreaterThan(ref i15, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i32);
        Branchless.SwapIfGreaterThan(ref i19, ref i33);
        Branchless.SwapIfGreaterThan(ref i22, ref i35);
        Branchless.SwapIfGreaterThan(ref i24, ref i36);
        Branchless.SwapIfGreaterThan(ref i26, ref i37);
        Branchless.SwapIfGreaterThan(ref i27, ref i41);
        Branchless.SwapIfGreaterThan(ref i30, ref i42);
        Branchless.SwapIfGreaterThan(ref i31, ref i40);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i13);
        Branchless.SwapIfGreaterThan(ref i2, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i15);
        Branchless.SwapIfGreaterThan(ref i5, ref i24);
        Branchless.SwapIfGreaterThan(ref i6, ref i19);
        Branchless.SwapIfGreaterThan(ref i7, ref i16);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i26);
        Branchless.SwapIfGreaterThan(ref i11, ref i22);
        Branchless.SwapIfGreaterThan(ref i12, ref i27);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i20, ref i32);
        Branchless.SwapIfGreaterThan(ref i21, ref i34);
        Branchless.SwapIfGreaterThan(ref i23, ref i39);
        Branchless.SwapIfGreaterThan(ref i25, ref i36);
        Branchless.SwapIfGreaterThan(ref i28, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i37);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i13);
        Branchless.SwapIfGreaterThan(ref i5, ref i14);
        Branchless.SwapIfGreaterThan(ref i8, ref i20);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i32);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i33);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i13);
        Branchless.SwapIfGreaterThan(ref i7, ref i14);
        Branchless.SwapIfGreaterThan(ref i8, ref i18);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i35);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i20, ref i26);
        Branchless.SwapIfGreaterThan(ref i21, ref i27);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i28, ref i33);
        Branchless.SwapIfGreaterThan(ref i32, ref i41);
        Branchless.SwapIfGreaterThan(ref i36, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i20);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i33);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i34, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i41);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i22, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i32, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i38, ref i41);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
    }
    
    /// <summary>
    /// Sort 46 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort46Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i14, ref i44);
        Branchless.SwapIfGreaterThan(ref i15, ref i45);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i40);
        Branchless.SwapIfGreaterThan(ref i15, ref i41);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i6);
        Branchless.SwapIfGreaterThan(ref i1, ref i7);
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i4, ref i10);
        Branchless.SwapIfGreaterThan(ref i5, ref i11);
        Branchless.SwapIfGreaterThan(ref i14, ref i32);
        Branchless.SwapIfGreaterThan(ref i15, ref i33);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i34, ref i42);
        Branchless.SwapIfGreaterThan(ref i35, ref i43);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i44);
        Branchless.SwapIfGreaterThan(ref i39, ref i45);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i14);
        Branchless.SwapIfGreaterThan(ref i1, ref i15);
        Branchless.SwapIfGreaterThan(ref i2, ref i36);
        Branchless.SwapIfGreaterThan(ref i3, ref i37);
        Branchless.SwapIfGreaterThan(ref i4, ref i34);
        Branchless.SwapIfGreaterThan(ref i5, ref i35);
        Branchless.SwapIfGreaterThan(ref i6, ref i32);
        Branchless.SwapIfGreaterThan(ref i7, ref i33);
        Branchless.SwapIfGreaterThan(ref i8, ref i40);
        Branchless.SwapIfGreaterThan(ref i9, ref i41);
        Branchless.SwapIfGreaterThan(ref i10, ref i42);
        Branchless.SwapIfGreaterThan(ref i11, ref i43);
        Branchless.SwapIfGreaterThan(ref i12, ref i38);
        Branchless.SwapIfGreaterThan(ref i13, ref i39);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i45);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i10);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i44);
        Branchless.SwapIfGreaterThan(ref i15, ref i36);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i23, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i22);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i44);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i40);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i5, ref i16);
        Branchless.SwapIfGreaterThan(ref i7, ref i21);
        Branchless.SwapIfGreaterThan(ref i8, ref i25);
        Branchless.SwapIfGreaterThan(ref i9, ref i23);
        Branchless.SwapIfGreaterThan(ref i11, ref i29);
        Branchless.SwapIfGreaterThan(ref i12, ref i28);
        Branchless.SwapIfGreaterThan(ref i13, ref i27);
        Branchless.SwapIfGreaterThan(ref i18, ref i34);
        Branchless.SwapIfGreaterThan(ref i19, ref i33);
        Branchless.SwapIfGreaterThan(ref i20, ref i32);
        Branchless.SwapIfGreaterThan(ref i22, ref i35);
        Branchless.SwapIfGreaterThan(ref i24, ref i36);
        Branchless.SwapIfGreaterThan(ref i26, ref i38);
        Branchless.SwapIfGreaterThan(ref i30, ref i44);
        Branchless.SwapIfGreaterThan(ref i31, ref i40);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i14);
        Branchless.SwapIfGreaterThan(ref i2, ref i20);
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i4, ref i18);
        Branchless.SwapIfGreaterThan(ref i6, ref i24);
        Branchless.SwapIfGreaterThan(ref i7, ref i16);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i22);
        Branchless.SwapIfGreaterThan(ref i10, ref i26);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i21, ref i37);
        Branchless.SwapIfGreaterThan(ref i23, ref i39);
        Branchless.SwapIfGreaterThan(ref i25, ref i36);
        Branchless.SwapIfGreaterThan(ref i27, ref i43);
        Branchless.SwapIfGreaterThan(ref i28, ref i42);
        Branchless.SwapIfGreaterThan(ref i29, ref i41);
        Branchless.SwapIfGreaterThan(ref i31, ref i38);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i40, ref i45);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i14);
        Branchless.SwapIfGreaterThan(ref i6, ref i15);
        Branchless.SwapIfGreaterThan(ref i8, ref i32);
        Branchless.SwapIfGreaterThan(ref i9, ref i19);
        Branchless.SwapIfGreaterThan(ref i10, ref i17);
        Branchless.SwapIfGreaterThan(ref i11, ref i33);
        Branchless.SwapIfGreaterThan(ref i12, ref i34);
        Branchless.SwapIfGreaterThan(ref i13, ref i35);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i30, ref i37);
        Branchless.SwapIfGreaterThan(ref i39, ref i44);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i3, ref i14);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i8, ref i18);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i25);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i22, ref i32);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i35);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i11, ref i20);
        Branchless.SwapIfGreaterThan(ref i12, ref i19);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i27, ref i34);
        Branchless.SwapIfGreaterThan(ref i28, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i14);
        Branchless.SwapIfGreaterThan(ref i10, ref i15);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i28);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i30);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i15, ref i20);
        Branchless.SwapIfGreaterThan(ref i16, ref i22);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i31);
        Branchless.SwapIfGreaterThan(ref i27, ref i36);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i38);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
    }
    
    /// <summary>
    /// Sort 47 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort47Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i14);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i15, ref i46);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i44);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i3);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i40);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i45);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i46);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i7);
        Branchless.SwapIfGreaterThan(ref i1, ref i10);
        Branchless.SwapIfGreaterThan(ref i2, ref i9);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i11);
        Branchless.SwapIfGreaterThan(ref i15, ref i32);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        Branchless.SwapIfGreaterThan(ref i34, ref i42);
        Branchless.SwapIfGreaterThan(ref i35, ref i43);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i45);
        Branchless.SwapIfGreaterThan(ref i38, ref i44);
        Branchless.SwapIfGreaterThan(ref i39, ref i46);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i15);
        Branchless.SwapIfGreaterThan(ref i1, ref i34);
        Branchless.SwapIfGreaterThan(ref i2, ref i33);
        Branchless.SwapIfGreaterThan(ref i3, ref i36);
        Branchless.SwapIfGreaterThan(ref i4, ref i38);
        Branchless.SwapIfGreaterThan(ref i5, ref i37);
        Branchless.SwapIfGreaterThan(ref i6, ref i35);
        Branchless.SwapIfGreaterThan(ref i7, ref i32);
        Branchless.SwapIfGreaterThan(ref i8, ref i40);
        Branchless.SwapIfGreaterThan(ref i9, ref i41);
        Branchless.SwapIfGreaterThan(ref i10, ref i42);
        Branchless.SwapIfGreaterThan(ref i11, ref i43);
        Branchless.SwapIfGreaterThan(ref i12, ref i44);
        Branchless.SwapIfGreaterThan(ref i13, ref i45);
        Branchless.SwapIfGreaterThan(ref i14, ref i39);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i46);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i7);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i23, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i42);
        Branchless.SwapIfGreaterThan(ref i38, ref i41);
        Branchless.SwapIfGreaterThan(ref i39, ref i45);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i22);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i25, ref i40);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i5, ref i21);
        Branchless.SwapIfGreaterThan(ref i6, ref i16);
        Branchless.SwapIfGreaterThan(ref i8, ref i25);
        Branchless.SwapIfGreaterThan(ref i10, ref i28);
        Branchless.SwapIfGreaterThan(ref i11, ref i23);
        Branchless.SwapIfGreaterThan(ref i12, ref i27);
        Branchless.SwapIfGreaterThan(ref i13, ref i29);
        Branchless.SwapIfGreaterThan(ref i18, ref i34);
        Branchless.SwapIfGreaterThan(ref i19, ref i37);
        Branchless.SwapIfGreaterThan(ref i20, ref i33);
        Branchless.SwapIfGreaterThan(ref i22, ref i35);
        Branchless.SwapIfGreaterThan(ref i24, ref i36);
        Branchless.SwapIfGreaterThan(ref i26, ref i41);
        Branchless.SwapIfGreaterThan(ref i30, ref i45);
        Branchless.SwapIfGreaterThan(ref i31, ref i40);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i15);
        Branchless.SwapIfGreaterThan(ref i2, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i20);
        Branchless.SwapIfGreaterThan(ref i4, ref i19);
        Branchless.SwapIfGreaterThan(ref i5, ref i16);
        Branchless.SwapIfGreaterThan(ref i7, ref i24);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i26);
        Branchless.SwapIfGreaterThan(ref i11, ref i22);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i17, ref i32);
        Branchless.SwapIfGreaterThan(ref i21, ref i38);
        Branchless.SwapIfGreaterThan(ref i23, ref i39);
        Branchless.SwapIfGreaterThan(ref i25, ref i36);
        Branchless.SwapIfGreaterThan(ref i27, ref i43);
        Branchless.SwapIfGreaterThan(ref i28, ref i42);
        Branchless.SwapIfGreaterThan(ref i29, ref i44);
        Branchless.SwapIfGreaterThan(ref i31, ref i41);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i6, ref i15);
        Branchless.SwapIfGreaterThan(ref i7, ref i17);
        Branchless.SwapIfGreaterThan(ref i8, ref i33);
        Branchless.SwapIfGreaterThan(ref i9, ref i32);
        Branchless.SwapIfGreaterThan(ref i10, ref i34);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i35);
        Branchless.SwapIfGreaterThan(ref i13, ref i37);
        Branchless.SwapIfGreaterThan(ref i14, ref i38);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i30, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i45);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i4, ref i15);
        Branchless.SwapIfGreaterThan(ref i5, ref i17);
        Branchless.SwapIfGreaterThan(ref i8, ref i18);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i25);
        Branchless.SwapIfGreaterThan(ref i14, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i32);
        Branchless.SwapIfGreaterThan(ref i22, ref i33);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i41);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i13, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i25, ref i32);
        Branchless.SwapIfGreaterThan(ref i26, ref i33);
        Branchless.SwapIfGreaterThan(ref i27, ref i34);
        Branchless.SwapIfGreaterThan(ref i28, ref i35);
        Branchless.SwapIfGreaterThan(ref i30, ref i40);
        Branchless.SwapIfGreaterThan(ref i38, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i15);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i26);
        Branchless.SwapIfGreaterThan(ref i21, ref i28);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i36);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i32, ref i35);
        Branchless.SwapIfGreaterThan(ref i34, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i18);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i36);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i30, ref i33);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
    }
    
    /// <summary>
    /// Sort 48 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort48Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i41, ref i45);
        Branchless.SwapIfGreaterThan(ref i42, ref i46);
        Branchless.SwapIfGreaterThan(ref i43, ref i47);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i40);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        Branchless.SwapIfGreaterThan(ref i34, ref i42);
        Branchless.SwapIfGreaterThan(ref i35, ref i43);
        Branchless.SwapIfGreaterThan(ref i36, ref i44);
        Branchless.SwapIfGreaterThan(ref i37, ref i45);
        Branchless.SwapIfGreaterThan(ref i38, ref i46);
        Branchless.SwapIfGreaterThan(ref i39, ref i47);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i32);
        Branchless.SwapIfGreaterThan(ref i1, ref i33);
        Branchless.SwapIfGreaterThan(ref i2, ref i34);
        Branchless.SwapIfGreaterThan(ref i3, ref i35);
        Branchless.SwapIfGreaterThan(ref i4, ref i36);
        Branchless.SwapIfGreaterThan(ref i5, ref i37);
        Branchless.SwapIfGreaterThan(ref i6, ref i38);
        Branchless.SwapIfGreaterThan(ref i7, ref i39);
        Branchless.SwapIfGreaterThan(ref i8, ref i40);
        Branchless.SwapIfGreaterThan(ref i9, ref i41);
        Branchless.SwapIfGreaterThan(ref i10, ref i42);
        Branchless.SwapIfGreaterThan(ref i11, ref i43);
        Branchless.SwapIfGreaterThan(ref i12, ref i44);
        Branchless.SwapIfGreaterThan(ref i13, ref i45);
        Branchless.SwapIfGreaterThan(ref i14, ref i46);
        Branchless.SwapIfGreaterThan(ref i15, ref i47);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i23, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i47);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i34, ref i40);
        Branchless.SwapIfGreaterThan(ref i35, ref i44);
        Branchless.SwapIfGreaterThan(ref i37, ref i42);
        Branchless.SwapIfGreaterThan(ref i38, ref i41);
        Branchless.SwapIfGreaterThan(ref i39, ref i45);
        Branchless.SwapIfGreaterThan(ref i43, ref i46);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i22);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i31);
        Branchless.SwapIfGreaterThan(ref i16, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i44);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i3, ref i32);
        Branchless.SwapIfGreaterThan(ref i6, ref i21);
        Branchless.SwapIfGreaterThan(ref i7, ref i23);
        Branchless.SwapIfGreaterThan(ref i10, ref i28);
        Branchless.SwapIfGreaterThan(ref i11, ref i29);
        Branchless.SwapIfGreaterThan(ref i12, ref i25);
        Branchless.SwapIfGreaterThan(ref i13, ref i27);
        Branchless.SwapIfGreaterThan(ref i15, ref i44);
        Branchless.SwapIfGreaterThan(ref i18, ref i36);
        Branchless.SwapIfGreaterThan(ref i19, ref i37);
        Branchless.SwapIfGreaterThan(ref i20, ref i34);
        Branchless.SwapIfGreaterThan(ref i22, ref i35);
        Branchless.SwapIfGreaterThan(ref i24, ref i40);
        Branchless.SwapIfGreaterThan(ref i26, ref i41);
        Branchless.SwapIfGreaterThan(ref i30, ref i46);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i16);
        Branchless.SwapIfGreaterThan(ref i2, ref i18);
        Branchless.SwapIfGreaterThan(ref i4, ref i20);
        Branchless.SwapIfGreaterThan(ref i5, ref i19);
        Branchless.SwapIfGreaterThan(ref i6, ref i32);
        Branchless.SwapIfGreaterThan(ref i7, ref i22);
        Branchless.SwapIfGreaterThan(ref i8, ref i24);
        Branchless.SwapIfGreaterThan(ref i9, ref i26);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i15, ref i41);
        Branchless.SwapIfGreaterThan(ref i17, ref i33);
        Branchless.SwapIfGreaterThan(ref i21, ref i38);
        Branchless.SwapIfGreaterThan(ref i23, ref i39);
        Branchless.SwapIfGreaterThan(ref i25, ref i40);
        Branchless.SwapIfGreaterThan(ref i27, ref i43);
        Branchless.SwapIfGreaterThan(ref i28, ref i42);
        Branchless.SwapIfGreaterThan(ref i29, ref i45);
        Branchless.SwapIfGreaterThan(ref i31, ref i46);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i16);
        Branchless.SwapIfGreaterThan(ref i7, ref i19);
        Branchless.SwapIfGreaterThan(ref i8, ref i17);
        Branchless.SwapIfGreaterThan(ref i9, ref i33);
        Branchless.SwapIfGreaterThan(ref i10, ref i34);
        Branchless.SwapIfGreaterThan(ref i11, ref i35);
        Branchless.SwapIfGreaterThan(ref i12, ref i36);
        Branchless.SwapIfGreaterThan(ref i13, ref i37);
        Branchless.SwapIfGreaterThan(ref i14, ref i38);
        Branchless.SwapIfGreaterThan(ref i28, ref i40);
        Branchless.SwapIfGreaterThan(ref i30, ref i39);
        Branchless.SwapIfGreaterThan(ref i31, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i16);
        Branchless.SwapIfGreaterThan(ref i6, ref i17);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i32);
        Branchless.SwapIfGreaterThan(ref i13, ref i25);
        Branchless.SwapIfGreaterThan(ref i14, ref i26);
        Branchless.SwapIfGreaterThan(ref i15, ref i36);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i33);
        Branchless.SwapIfGreaterThan(ref i22, ref i34);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i41);
        Branchless.SwapIfGreaterThan(ref i31, ref i42);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i11, ref i20);
        Branchless.SwapIfGreaterThan(ref i12, ref i19);
        Branchless.SwapIfGreaterThan(ref i13, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i27, ref i36);
        Branchless.SwapIfGreaterThan(ref i28, ref i35);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i24);
        Branchless.SwapIfGreaterThan(ref i15, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i26);
        Branchless.SwapIfGreaterThan(ref i21, ref i28);
        Branchless.SwapIfGreaterThan(ref i22, ref i32);
        Branchless.SwapIfGreaterThan(ref i23, ref i34);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i40);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i24);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i23, ref i33);
        Branchless.SwapIfGreaterThan(ref i25, ref i32);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i36);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
    }
    
    /// <summary>
    /// Sort 49 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort49Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        ref var i48 = ref itemArray[index + 48];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i20);
        Branchless.SwapIfGreaterThan(ref i1, ref i12);
        Branchless.SwapIfGreaterThan(ref i2, ref i16);
        Branchless.SwapIfGreaterThan(ref i3, ref i23);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i21);
        Branchless.SwapIfGreaterThan(ref i8, ref i14);
        Branchless.SwapIfGreaterThan(ref i9, ref i15);
        Branchless.SwapIfGreaterThan(ref i11, ref i22);
        Branchless.SwapIfGreaterThan(ref i13, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i3);
        Branchless.SwapIfGreaterThan(ref i1, ref i11);
        Branchless.SwapIfGreaterThan(ref i2, ref i7);
        Branchless.SwapIfGreaterThan(ref i4, ref i17);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i19);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i48);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i20);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i11);
        Branchless.SwapIfGreaterThan(ref i7, ref i14);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i12, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i36);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i27, ref i38);
        Branchless.SwapIfGreaterThan(ref i28, ref i33);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i35);
        Branchless.SwapIfGreaterThan(ref i31, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i46);
        Branchless.SwapIfGreaterThan(ref i42, ref i45);
        Branchless.SwapIfGreaterThan(ref i43, ref i48);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i14);
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i9, ref i20);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i16, ref i19);
        Branchless.SwapIfGreaterThan(ref i25, ref i42);
        Branchless.SwapIfGreaterThan(ref i27, ref i33);
        Branchless.SwapIfGreaterThan(ref i29, ref i41);
        Branchless.SwapIfGreaterThan(ref i30, ref i44);
        Branchless.SwapIfGreaterThan(ref i31, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i36, ref i46);
        Branchless.SwapIfGreaterThan(ref i39, ref i48);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i7);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i11);
        Branchless.SwapIfGreaterThan(ref i8, ref i15);
        Branchless.SwapIfGreaterThan(ref i9, ref i14);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i23);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i25, ref i40);
        Branchless.SwapIfGreaterThan(ref i27, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i45);
        Branchless.SwapIfGreaterThan(ref i30, ref i42);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i34, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i47);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i16, ref i19);
        Branchless.SwapIfGreaterThan(ref i17, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i40);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i42);
        Branchless.SwapIfGreaterThan(ref i33, ref i45);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i36, ref i43);
        Branchless.SwapIfGreaterThan(ref i39, ref i47);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i0, ref i24);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i34, ref i40);
        Branchless.SwapIfGreaterThan(ref i35, ref i44);
        Branchless.SwapIfGreaterThan(ref i37, ref i46);
        Branchless.SwapIfGreaterThan(ref i38, ref i41);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i19);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i1, ref i25);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i13, ref i18);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i23, ref i47);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i40);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i43);
        Branchless.SwapIfGreaterThan(ref i38, ref i45);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i26);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i22, ref i46);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i42, ref i45);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i3, ref i27);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i20, ref i44);
        Branchless.SwapIfGreaterThan(ref i21, ref i45);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i4, ref i28);
        Branchless.SwapIfGreaterThan(ref i5, ref i29);
        Branchless.SwapIfGreaterThan(ref i6, ref i30);
        Branchless.SwapIfGreaterThan(ref i7, ref i31);
        Branchless.SwapIfGreaterThan(ref i8, ref i32);
        Branchless.SwapIfGreaterThan(ref i9, ref i33);
        Branchless.SwapIfGreaterThan(ref i10, ref i34);
        Branchless.SwapIfGreaterThan(ref i11, ref i35);
        Branchless.SwapIfGreaterThan(ref i12, ref i36);
        Branchless.SwapIfGreaterThan(ref i13, ref i37);
        Branchless.SwapIfGreaterThan(ref i14, ref i38);
        Branchless.SwapIfGreaterThan(ref i15, ref i39);
        Branchless.SwapIfGreaterThan(ref i16, ref i40);
        Branchless.SwapIfGreaterThan(ref i17, ref i41);
        Branchless.SwapIfGreaterThan(ref i18, ref i42);
        Branchless.SwapIfGreaterThan(ref i19, ref i43);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i48);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i13, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i48);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        
        // Layer 19
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
    }
    
    /// <summary>
    /// Sort 50 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort50Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        ref var i48 = ref itemArray[index + 48];
        ref var i49 = ref itemArray[index + 49];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i46, ref i49);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i12);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i14);
        Branchless.SwapIfGreaterThan(ref i4, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i11);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i17, ref i22);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        Branchless.SwapIfGreaterThan(ref i19, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i37);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i28, ref i39);
        Branchless.SwapIfGreaterThan(ref i29, ref i34);
        Branchless.SwapIfGreaterThan(ref i30, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i36);
        Branchless.SwapIfGreaterThan(ref i32, ref i40);
        Branchless.SwapIfGreaterThan(ref i42, ref i47);
        Branchless.SwapIfGreaterThan(ref i43, ref i46);
        Branchless.SwapIfGreaterThan(ref i44, ref i49);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i17);
        Branchless.SwapIfGreaterThan(ref i6, ref i20);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i26, ref i43);
        Branchless.SwapIfGreaterThan(ref i28, ref i34);
        Branchless.SwapIfGreaterThan(ref i30, ref i42);
        Branchless.SwapIfGreaterThan(ref i31, ref i45);
        Branchless.SwapIfGreaterThan(ref i32, ref i38);
        Branchless.SwapIfGreaterThan(ref i36, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i47);
        Branchless.SwapIfGreaterThan(ref i40, ref i49);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i16);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i21);
        Branchless.SwapIfGreaterThan(ref i6, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i23);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i24, ref i49);
        Branchless.SwapIfGreaterThan(ref i26, ref i41);
        Branchless.SwapIfGreaterThan(ref i28, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i46);
        Branchless.SwapIfGreaterThan(ref i31, ref i43);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i48);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i18);
        Branchless.SwapIfGreaterThan(ref i9, ref i21);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i19);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i41);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i32, ref i43);
        Branchless.SwapIfGreaterThan(ref i34, ref i46);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i44);
        Branchless.SwapIfGreaterThan(ref i40, ref i48);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i0, ref i25);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i20);
        Branchless.SwapIfGreaterThan(ref i13, ref i22);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i41);
        Branchless.SwapIfGreaterThan(ref i36, ref i45);
        Branchless.SwapIfGreaterThan(ref i38, ref i47);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i40, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i26, ref i29);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i1, ref i26);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i19);
        Branchless.SwapIfGreaterThan(ref i14, ref i21);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i48);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i35);
        Branchless.SwapIfGreaterThan(ref i32, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i41);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i38, ref i44);
        Branchless.SwapIfGreaterThan(ref i39, ref i46);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i27);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i47);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i33);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i34, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i38, ref i41);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i46);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i3, ref i28);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i45);
        Branchless.SwapIfGreaterThan(ref i21, ref i46);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i4, ref i29);
        Branchless.SwapIfGreaterThan(ref i5, ref i30);
        Branchless.SwapIfGreaterThan(ref i6, ref i31);
        Branchless.SwapIfGreaterThan(ref i7, ref i32);
        Branchless.SwapIfGreaterThan(ref i8, ref i33);
        Branchless.SwapIfGreaterThan(ref i9, ref i34);
        Branchless.SwapIfGreaterThan(ref i10, ref i35);
        Branchless.SwapIfGreaterThan(ref i11, ref i36);
        Branchless.SwapIfGreaterThan(ref i12, ref i37);
        Branchless.SwapIfGreaterThan(ref i13, ref i38);
        Branchless.SwapIfGreaterThan(ref i14, ref i39);
        Branchless.SwapIfGreaterThan(ref i15, ref i40);
        Branchless.SwapIfGreaterThan(ref i16, ref i41);
        Branchless.SwapIfGreaterThan(ref i17, ref i42);
        Branchless.SwapIfGreaterThan(ref i18, ref i43);
        Branchless.SwapIfGreaterThan(ref i19, ref i44);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i16, ref i25);
        Branchless.SwapIfGreaterThan(ref i17, ref i26);
        Branchless.SwapIfGreaterThan(ref i18, ref i27);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i20, ref i29);
        Branchless.SwapIfGreaterThan(ref i21, ref i30);
        Branchless.SwapIfGreaterThan(ref i22, ref i31);
        Branchless.SwapIfGreaterThan(ref i23, ref i32);
        Branchless.SwapIfGreaterThan(ref i24, ref i33);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i13, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i39);
        Branchless.SwapIfGreaterThan(ref i32, ref i40);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i27);
        Branchless.SwapIfGreaterThan(ref i23, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i41, ref i45);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        
        // Layer 19
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
    }
    
    /// <summary>
    /// Sort 51 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort51Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        ref var i48 = ref itemArray[index + 48];
        ref var i49 = ref itemArray[index + 49];
        ref var i50 = ref itemArray[index + 50];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i39);
        Branchless.SwapIfGreaterThan(ref i34, ref i41);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i36, ref i42);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        Branchless.SwapIfGreaterThan(ref i43, ref i47);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i45, ref i49);
        Branchless.SwapIfGreaterThan(ref i46, ref i50);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i12);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i14);
        Branchless.SwapIfGreaterThan(ref i4, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i11);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i17, ref i22);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        Branchless.SwapIfGreaterThan(ref i19, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i43);
        Branchless.SwapIfGreaterThan(ref i26, ref i44);
        Branchless.SwapIfGreaterThan(ref i27, ref i45);
        Branchless.SwapIfGreaterThan(ref i28, ref i46);
        Branchless.SwapIfGreaterThan(ref i29, ref i47);
        Branchless.SwapIfGreaterThan(ref i30, ref i48);
        Branchless.SwapIfGreaterThan(ref i31, ref i49);
        Branchless.SwapIfGreaterThan(ref i32, ref i50);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i17);
        Branchless.SwapIfGreaterThan(ref i6, ref i20);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i26, ref i43);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i28, ref i45);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i47);
        Branchless.SwapIfGreaterThan(ref i31, ref i39);
        Branchless.SwapIfGreaterThan(ref i32, ref i49);
        Branchless.SwapIfGreaterThan(ref i34, ref i37);
        Branchless.SwapIfGreaterThan(ref i36, ref i44);
        Branchless.SwapIfGreaterThan(ref i38, ref i41);
        Branchless.SwapIfGreaterThan(ref i40, ref i48);
        Branchless.SwapIfGreaterThan(ref i42, ref i46);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i1, ref i16);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i21);
        Branchless.SwapIfGreaterThan(ref i6, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i23);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i28, ref i38);
        Branchless.SwapIfGreaterThan(ref i30, ref i40);
        Branchless.SwapIfGreaterThan(ref i31, ref i43);
        Branchless.SwapIfGreaterThan(ref i32, ref i44);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i45);
        Branchless.SwapIfGreaterThan(ref i37, ref i47);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i49);
        Branchless.SwapIfGreaterThan(ref i46, ref i50);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i18);
        Branchless.SwapIfGreaterThan(ref i9, ref i21);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i19);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i47);
        Branchless.SwapIfGreaterThan(ref i41, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i0, ref i25);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i20);
        Branchless.SwapIfGreaterThan(ref i13, ref i22);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i26, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i33);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i32, ref i38);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i43);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i42, ref i47);
        Branchless.SwapIfGreaterThan(ref i46, ref i49);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i31);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i32, ref i37);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i34, ref i39);
        Branchless.SwapIfGreaterThan(ref i36, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i43);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i44, ref i47);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i1, ref i26);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i19);
        Branchless.SwapIfGreaterThan(ref i14, ref i21);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i24, ref i49);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i30, ref i35);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i45);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i27);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        Branchless.SwapIfGreaterThan(ref i23, ref i48);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i3, ref i28);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i47);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i4, ref i29);
        Branchless.SwapIfGreaterThan(ref i5, ref i30);
        Branchless.SwapIfGreaterThan(ref i6, ref i31);
        Branchless.SwapIfGreaterThan(ref i7, ref i32);
        Branchless.SwapIfGreaterThan(ref i8, ref i33);
        Branchless.SwapIfGreaterThan(ref i9, ref i34);
        Branchless.SwapIfGreaterThan(ref i10, ref i35);
        Branchless.SwapIfGreaterThan(ref i11, ref i36);
        Branchless.SwapIfGreaterThan(ref i12, ref i37);
        Branchless.SwapIfGreaterThan(ref i13, ref i38);
        Branchless.SwapIfGreaterThan(ref i14, ref i39);
        Branchless.SwapIfGreaterThan(ref i15, ref i40);
        Branchless.SwapIfGreaterThan(ref i16, ref i41);
        Branchless.SwapIfGreaterThan(ref i17, ref i42);
        Branchless.SwapIfGreaterThan(ref i18, ref i43);
        Branchless.SwapIfGreaterThan(ref i19, ref i44);
        Branchless.SwapIfGreaterThan(ref i20, ref i45);
        Branchless.SwapIfGreaterThan(ref i21, ref i46);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i16, ref i25);
        Branchless.SwapIfGreaterThan(ref i17, ref i26);
        Branchless.SwapIfGreaterThan(ref i18, ref i27);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i20, ref i29);
        Branchless.SwapIfGreaterThan(ref i21, ref i30);
        Branchless.SwapIfGreaterThan(ref i22, ref i31);
        Branchless.SwapIfGreaterThan(ref i23, ref i32);
        Branchless.SwapIfGreaterThan(ref i24, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i50);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i13, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i39);
        Branchless.SwapIfGreaterThan(ref i32, ref i40);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i50);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i27);
        Branchless.SwapIfGreaterThan(ref i23, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i41, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i50);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        
        // Layer 19
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
    }
    
    /// <summary>
    /// Sort 52 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort52Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        ref var i48 = ref itemArray[index + 48];
        ref var i49 = ref itemArray[index + 49];
        ref var i50 = ref itemArray[index + 50];
        ref var i51 = ref itemArray[index + 51];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        Branchless.SwapIfGreaterThan(ref i49, ref i51);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i14);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i40);
        Branchless.SwapIfGreaterThan(ref i35, ref i42);
        Branchless.SwapIfGreaterThan(ref i36, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i43);
        Branchless.SwapIfGreaterThan(ref i38, ref i41);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i45, ref i49);
        Branchless.SwapIfGreaterThan(ref i46, ref i50);
        Branchless.SwapIfGreaterThan(ref i47, ref i51);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i18);
        Branchless.SwapIfGreaterThan(ref i1, ref i19);
        Branchless.SwapIfGreaterThan(ref i2, ref i20);
        Branchless.SwapIfGreaterThan(ref i3, ref i21);
        Branchless.SwapIfGreaterThan(ref i4, ref i22);
        Branchless.SwapIfGreaterThan(ref i5, ref i23);
        Branchless.SwapIfGreaterThan(ref i6, ref i24);
        Branchless.SwapIfGreaterThan(ref i7, ref i25);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i26, ref i44);
        Branchless.SwapIfGreaterThan(ref i27, ref i45);
        Branchless.SwapIfGreaterThan(ref i28, ref i46);
        Branchless.SwapIfGreaterThan(ref i29, ref i47);
        Branchless.SwapIfGreaterThan(ref i30, ref i48);
        Branchless.SwapIfGreaterThan(ref i31, ref i49);
        Branchless.SwapIfGreaterThan(ref i32, ref i50);
        Branchless.SwapIfGreaterThan(ref i33, ref i51);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i1, ref i18);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i20);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i22);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i24);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i27, ref i44);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i46);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i48);
        Branchless.SwapIfGreaterThan(ref i32, ref i40);
        Branchless.SwapIfGreaterThan(ref i33, ref i50);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i45);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i49);
        Branchless.SwapIfGreaterThan(ref i43, ref i47);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i3, ref i13);
        Branchless.SwapIfGreaterThan(ref i5, ref i15);
        Branchless.SwapIfGreaterThan(ref i6, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i19);
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i20);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i29, ref i39);
        Branchless.SwapIfGreaterThan(ref i31, ref i41);
        Branchless.SwapIfGreaterThan(ref i32, ref i44);
        Branchless.SwapIfGreaterThan(ref i33, ref i45);
        Branchless.SwapIfGreaterThan(ref i34, ref i37);
        Branchless.SwapIfGreaterThan(ref i36, ref i46);
        Branchless.SwapIfGreaterThan(ref i38, ref i48);
        Branchless.SwapIfGreaterThan(ref i40, ref i43);
        Branchless.SwapIfGreaterThan(ref i42, ref i50);
        Branchless.SwapIfGreaterThan(ref i47, ref i51);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i0, ref i26);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i25, ref i51);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i40, ref i48);
        Branchless.SwapIfGreaterThan(ref i42, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i18);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i17, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i34);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i39);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i44);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i45);
        Branchless.SwapIfGreaterThan(ref i43, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i50);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i33, ref i38);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i44);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i45, ref i48);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i1, ref i27);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i20);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i50);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i46);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i2, ref i28);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i49);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i3, ref i29);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i22, ref i48);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i4, ref i30);
        Branchless.SwapIfGreaterThan(ref i5, ref i31);
        Branchless.SwapIfGreaterThan(ref i6, ref i32);
        Branchless.SwapIfGreaterThan(ref i7, ref i33);
        Branchless.SwapIfGreaterThan(ref i8, ref i34);
        Branchless.SwapIfGreaterThan(ref i9, ref i35);
        Branchless.SwapIfGreaterThan(ref i10, ref i36);
        Branchless.SwapIfGreaterThan(ref i11, ref i37);
        Branchless.SwapIfGreaterThan(ref i12, ref i38);
        Branchless.SwapIfGreaterThan(ref i13, ref i39);
        Branchless.SwapIfGreaterThan(ref i14, ref i40);
        Branchless.SwapIfGreaterThan(ref i15, ref i41);
        Branchless.SwapIfGreaterThan(ref i16, ref i42);
        Branchless.SwapIfGreaterThan(ref i17, ref i43);
        Branchless.SwapIfGreaterThan(ref i18, ref i44);
        Branchless.SwapIfGreaterThan(ref i19, ref i45);
        Branchless.SwapIfGreaterThan(ref i20, ref i46);
        Branchless.SwapIfGreaterThan(ref i21, ref i47);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i16, ref i26);
        Branchless.SwapIfGreaterThan(ref i17, ref i27);
        Branchless.SwapIfGreaterThan(ref i18, ref i28);
        Branchless.SwapIfGreaterThan(ref i19, ref i29);
        Branchless.SwapIfGreaterThan(ref i20, ref i30);
        Branchless.SwapIfGreaterThan(ref i21, ref i31);
        Branchless.SwapIfGreaterThan(ref i22, ref i32);
        Branchless.SwapIfGreaterThan(ref i23, ref i33);
        Branchless.SwapIfGreaterThan(ref i24, ref i34);
        Branchless.SwapIfGreaterThan(ref i25, ref i35);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i13, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i39);
        Branchless.SwapIfGreaterThan(ref i32, ref i40);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        Branchless.SwapIfGreaterThan(ref i34, ref i42);
        Branchless.SwapIfGreaterThan(ref i35, ref i43);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i41, ref i45);
        Branchless.SwapIfGreaterThan(ref i42, ref i46);
        Branchless.SwapIfGreaterThan(ref i43, ref i47);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        
        // Layer 19
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
    }
    
    /// <summary>
    /// Sort 53 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort53Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        ref var i48 = ref itemArray[index + 48];
        ref var i49 = ref itemArray[index + 49];
        ref var i50 = ref itemArray[index + 50];
        ref var i51 = ref itemArray[index + 51];
        ref var i52 = ref itemArray[index + 52];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i28);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i36);
        Branchless.SwapIfGreaterThan(ref i19, ref i38);
        Branchless.SwapIfGreaterThan(ref i20, ref i32);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i35);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i30, ref i43);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i52);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i51);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i5);
        Branchless.SwapIfGreaterThan(ref i1, ref i7);
        Branchless.SwapIfGreaterThan(ref i2, ref i9);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i24);
        Branchless.SwapIfGreaterThan(ref i15, ref i33);
        Branchless.SwapIfGreaterThan(ref i16, ref i22);
        Branchless.SwapIfGreaterThan(ref i17, ref i23);
        Branchless.SwapIfGreaterThan(ref i18, ref i34);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i30, ref i44);
        Branchless.SwapIfGreaterThan(ref i32, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i45);
        Branchless.SwapIfGreaterThan(ref i39, ref i49);
        Branchless.SwapIfGreaterThan(ref i40, ref i46);
        Branchless.SwapIfGreaterThan(ref i41, ref i47);
        Branchless.SwapIfGreaterThan(ref i42, ref i50);
        Branchless.SwapIfGreaterThan(ref i43, ref i48);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i25);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i32);
        Branchless.SwapIfGreaterThan(ref i18, ref i35);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i36);
        Branchless.SwapIfGreaterThan(ref i23, ref i33);
        Branchless.SwapIfGreaterThan(ref i24, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i31);
        Branchless.SwapIfGreaterThan(ref i30, ref i39);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i40, ref i43);
        Branchless.SwapIfGreaterThan(ref i42, ref i51);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i52);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i10);
        Branchless.SwapIfGreaterThan(ref i5, ref i11);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i23);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i35);
        Branchless.SwapIfGreaterThan(ref i22, ref i34);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i37);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i40, ref i47);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i49);
        Branchless.SwapIfGreaterThan(ref i45, ref i51);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        Branchless.SwapIfGreaterThan(ref i50, ref i52);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i14);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i30);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i20, ref i32);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i31, ref i52);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i46);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        Branchless.SwapIfGreaterThan(ref i49, ref i51);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i13);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i23, ref i32);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i31, ref i38);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i44, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i15);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i22, ref i33);
        Branchless.SwapIfGreaterThan(ref i24, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i51);
        Branchless.SwapIfGreaterThan(ref i38, ref i52);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i24);
        Branchless.SwapIfGreaterThan(ref i2, ref i25);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i15, ref i37);
        Branchless.SwapIfGreaterThan(ref i16, ref i39);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i28, ref i50);
        Branchless.SwapIfGreaterThan(ref i29, ref i36);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i42, ref i45);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i2, ref i16);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i15, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i39);
        Branchless.SwapIfGreaterThan(ref i28, ref i35);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i36, ref i51);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i13);
        Branchless.SwapIfGreaterThan(ref i3, ref i26);
        Branchless.SwapIfGreaterThan(ref i4, ref i41);
        Branchless.SwapIfGreaterThan(ref i5, ref i42);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i47);
        Branchless.SwapIfGreaterThan(ref i11, ref i48);
        Branchless.SwapIfGreaterThan(ref i12, ref i34);
        Branchless.SwapIfGreaterThan(ref i16, ref i25);
        Branchless.SwapIfGreaterThan(ref i17, ref i40);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i49);
        Branchless.SwapIfGreaterThan(ref i35, ref i50);
        Branchless.SwapIfGreaterThan(ref i36, ref i39);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i3, ref i17);
        Branchless.SwapIfGreaterThan(ref i4, ref i18);
        Branchless.SwapIfGreaterThan(ref i5, ref i19);
        Branchless.SwapIfGreaterThan(ref i6, ref i20);
        Branchless.SwapIfGreaterThan(ref i7, ref i31);
        Branchless.SwapIfGreaterThan(ref i8, ref i22);
        Branchless.SwapIfGreaterThan(ref i9, ref i37);
        Branchless.SwapIfGreaterThan(ref i12, ref i27);
        Branchless.SwapIfGreaterThan(ref i21, ref i44);
        Branchless.SwapIfGreaterThan(ref i23, ref i46);
        Branchless.SwapIfGreaterThan(ref i26, ref i40);
        Branchless.SwapIfGreaterThan(ref i29, ref i43);
        Branchless.SwapIfGreaterThan(ref i30, ref i45);
        Branchless.SwapIfGreaterThan(ref i32, ref i47);
        Branchless.SwapIfGreaterThan(ref i33, ref i48);
        Branchless.SwapIfGreaterThan(ref i34, ref i49);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i5, ref i24);
        Branchless.SwapIfGreaterThan(ref i6, ref i29);
        Branchless.SwapIfGreaterThan(ref i7, ref i21);
        Branchless.SwapIfGreaterThan(ref i8, ref i30);
        Branchless.SwapIfGreaterThan(ref i9, ref i23);
        Branchless.SwapIfGreaterThan(ref i10, ref i32);
        Branchless.SwapIfGreaterThan(ref i11, ref i33);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i17, ref i26);
        Branchless.SwapIfGreaterThan(ref i18, ref i41);
        Branchless.SwapIfGreaterThan(ref i19, ref i42);
        Branchless.SwapIfGreaterThan(ref i20, ref i43);
        Branchless.SwapIfGreaterThan(ref i22, ref i45);
        Branchless.SwapIfGreaterThan(ref i27, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i44);
        Branchless.SwapIfGreaterThan(ref i37, ref i46);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i47);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i6, ref i16);
        Branchless.SwapIfGreaterThan(ref i7, ref i17);
        Branchless.SwapIfGreaterThan(ref i8, ref i13);
        Branchless.SwapIfGreaterThan(ref i9, ref i15);
        Branchless.SwapIfGreaterThan(ref i10, ref i25);
        Branchless.SwapIfGreaterThan(ref i11, ref i26);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i31);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i41);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i38);
        Branchless.SwapIfGreaterThan(ref i34, ref i45);
        Branchless.SwapIfGreaterThan(ref i35, ref i42);
        Branchless.SwapIfGreaterThan(ref i40, ref i48);
        Branchless.SwapIfGreaterThan(ref i43, ref i51);
        Branchless.SwapIfGreaterThan(ref i44, ref i52);
        Branchless.SwapIfGreaterThan(ref i46, ref i50);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i35);
        Branchless.SwapIfGreaterThan(ref i26, ref i33);
        Branchless.SwapIfGreaterThan(ref i28, ref i37);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i34, ref i41);
        Branchless.SwapIfGreaterThan(ref i36, ref i43);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i42, ref i46);
        Branchless.SwapIfGreaterThan(ref i48, ref i52);
        Branchless.SwapIfGreaterThan(ref i49, ref i51);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i24);
        Branchless.SwapIfGreaterThan(ref i20, ref i29);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i32);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i36, ref i39);
        Branchless.SwapIfGreaterThan(ref i38, ref i44);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i17, ref i23);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i22, ref i27);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i31);
        Branchless.SwapIfGreaterThan(ref i28, ref i35);
        Branchless.SwapIfGreaterThan(ref i30, ref i36);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i48, ref i51);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i33, ref i38);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i40, ref i46);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i44, ref i50);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i23);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i43);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i44, ref i47);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        
        // Layer 19
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        
        // Layer 20
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
    }
    
    /// <summary>
    /// Sort 54 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort54Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        ref var i48 = ref itemArray[index + 48];
        ref var i49 = ref itemArray[index + 49];
        ref var i50 = ref itemArray[index + 50];
        ref var i51 = ref itemArray[index + 51];
        ref var i52 = ref itemArray[index + 52];
        ref var i53 = ref itemArray[index + 53];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i13);
        Branchless.SwapIfGreaterThan(ref i1, ref i12);
        Branchless.SwapIfGreaterThan(ref i2, ref i15);
        Branchless.SwapIfGreaterThan(ref i3, ref i14);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i16, ref i35);
        Branchless.SwapIfGreaterThan(ref i17, ref i34);
        Branchless.SwapIfGreaterThan(ref i18, ref i37);
        Branchless.SwapIfGreaterThan(ref i19, ref i36);
        Branchless.SwapIfGreaterThan(ref i20, ref i30);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i33);
        Branchless.SwapIfGreaterThan(ref i24, ref i29);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i38, ref i51);
        Branchless.SwapIfGreaterThan(ref i39, ref i50);
        Branchless.SwapIfGreaterThan(ref i40, ref i53);
        Branchless.SwapIfGreaterThan(ref i41, ref i52);
        Branchless.SwapIfGreaterThan(ref i42, ref i46);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i49);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i5);
        Branchless.SwapIfGreaterThan(ref i1, ref i7);
        Branchless.SwapIfGreaterThan(ref i2, ref i9);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i13);
        Branchless.SwapIfGreaterThan(ref i8, ref i14);
        Branchless.SwapIfGreaterThan(ref i10, ref i15);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i16, ref i21);
        Branchless.SwapIfGreaterThan(ref i17, ref i23);
        Branchless.SwapIfGreaterThan(ref i18, ref i31);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i22, ref i35);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i30, ref i36);
        Branchless.SwapIfGreaterThan(ref i32, ref i37);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i38, ref i43);
        Branchless.SwapIfGreaterThan(ref i39, ref i45);
        Branchless.SwapIfGreaterThan(ref i40, ref i47);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i44, ref i51);
        Branchless.SwapIfGreaterThan(ref i46, ref i52);
        Branchless.SwapIfGreaterThan(ref i48, ref i53);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i24, ref i27);
        Branchless.SwapIfGreaterThan(ref i26, ref i29);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i10);
        Branchless.SwapIfGreaterThan(ref i5, ref i11);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i32);
        Branchless.SwapIfGreaterThan(ref i21, ref i33);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i48);
        Branchless.SwapIfGreaterThan(ref i43, ref i49);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i50, ref i52);
        Branchless.SwapIfGreaterThan(ref i51, ref i53);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i38);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i53);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i34);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i50);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i29);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i32);
        Branchless.SwapIfGreaterThan(ref i24, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i53);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i51);
        Branchless.SwapIfGreaterThan(ref i49, ref i52);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i39);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i52);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i44);
        Branchless.SwapIfGreaterThan(ref i47, ref i50);
        Branchless.SwapIfGreaterThan(ref i49, ref i51);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i2, ref i40);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i51);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i36, ref i52);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i2, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i27);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i30);
        Branchless.SwapIfGreaterThan(ref i26, ref i40);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i51);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i24);
        Branchless.SwapIfGreaterThan(ref i3, ref i41);
        Branchless.SwapIfGreaterThan(ref i4, ref i42);
        Branchless.SwapIfGreaterThan(ref i5, ref i43);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i48);
        Branchless.SwapIfGreaterThan(ref i11, ref i49);
        Branchless.SwapIfGreaterThan(ref i12, ref i50);
        Branchless.SwapIfGreaterThan(ref i13, ref i39);
        Branchless.SwapIfGreaterThan(ref i14, ref i40);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i28, ref i34);
        Branchless.SwapIfGreaterThan(ref i29, ref i51);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i4, ref i20);
        Branchless.SwapIfGreaterThan(ref i6, ref i44);
        Branchless.SwapIfGreaterThan(ref i7, ref i45);
        Branchless.SwapIfGreaterThan(ref i8, ref i46);
        Branchless.SwapIfGreaterThan(ref i9, ref i47);
        Branchless.SwapIfGreaterThan(ref i10, ref i32);
        Branchless.SwapIfGreaterThan(ref i12, ref i28);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i21, ref i43);
        Branchless.SwapIfGreaterThan(ref i25, ref i41);
        Branchless.SwapIfGreaterThan(ref i30, ref i38);
        Branchless.SwapIfGreaterThan(ref i33, ref i49);
        Branchless.SwapIfGreaterThan(ref i34, ref i50);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i6, ref i22);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i8, ref i30);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i26);
        Branchless.SwapIfGreaterThan(ref i11, ref i33);
        Branchless.SwapIfGreaterThan(ref i14, ref i32);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i20, ref i42);
        Branchless.SwapIfGreaterThan(ref i21, ref i39);
        Branchless.SwapIfGreaterThan(ref i23, ref i45);
        Branchless.SwapIfGreaterThan(ref i27, ref i43);
        Branchless.SwapIfGreaterThan(ref i28, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i47);
        Branchless.SwapIfGreaterThan(ref i36, ref i44);
        Branchless.SwapIfGreaterThan(ref i38, ref i46);
        Branchless.SwapIfGreaterThan(ref i41, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i52);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i1, ref i16);
        Branchless.SwapIfGreaterThan(ref i3, ref i13);
        Branchless.SwapIfGreaterThan(ref i5, ref i21);
        Branchless.SwapIfGreaterThan(ref i6, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i19);
        Branchless.SwapIfGreaterThan(ref i8, ref i24);
        Branchless.SwapIfGreaterThan(ref i11, ref i25);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i14, ref i26);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i17, ref i31);
        Branchless.SwapIfGreaterThan(ref i22, ref i36);
        Branchless.SwapIfGreaterThan(ref i27, ref i39);
        Branchless.SwapIfGreaterThan(ref i28, ref i42);
        Branchless.SwapIfGreaterThan(ref i29, ref i45);
        Branchless.SwapIfGreaterThan(ref i30, ref i38);
        Branchless.SwapIfGreaterThan(ref i32, ref i48);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        Branchless.SwapIfGreaterThan(ref i34, ref i46);
        Branchless.SwapIfGreaterThan(ref i35, ref i47);
        Branchless.SwapIfGreaterThan(ref i37, ref i52);
        Branchless.SwapIfGreaterThan(ref i40, ref i50);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i29);
        Branchless.SwapIfGreaterThan(ref i24, ref i30);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i31, ref i39);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i34, ref i42);
        Branchless.SwapIfGreaterThan(ref i35, ref i43);
        Branchless.SwapIfGreaterThan(ref i37, ref i49);
        Branchless.SwapIfGreaterThan(ref i40, ref i48);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i12, ref i24);
        Branchless.SwapIfGreaterThan(ref i15, ref i25);
        Branchless.SwapIfGreaterThan(ref i20, ref i30);
        Branchless.SwapIfGreaterThan(ref i21, ref i27);
        Branchless.SwapIfGreaterThan(ref i23, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i32);
        Branchless.SwapIfGreaterThan(ref i28, ref i38);
        Branchless.SwapIfGreaterThan(ref i29, ref i41);
        Branchless.SwapIfGreaterThan(ref i36, ref i44);
        Branchless.SwapIfGreaterThan(ref i37, ref i45);
        Branchless.SwapIfGreaterThan(ref i49, ref i51);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i25, ref i31);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i35);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i39);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i50);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i43);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i47);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        
        // Layer 19
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i48);
        
        // Layer 20
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
    }
    
    /// <summary>
    /// Sort 55 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort55Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        ref var i48 = ref itemArray[index + 48];
        ref var i49 = ref itemArray[index + 49];
        ref var i50 = ref itemArray[index + 50];
        ref var i51 = ref itemArray[index + 51];
        ref var i52 = ref itemArray[index + 52];
        ref var i53 = ref itemArray[index + 53];
        ref var i54 = ref itemArray[index + 54];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i1, ref i12);
        Branchless.SwapIfGreaterThan(ref i2, ref i13);
        Branchless.SwapIfGreaterThan(ref i3, ref i14);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i16, ref i37);
        Branchless.SwapIfGreaterThan(ref i17, ref i36);
        Branchless.SwapIfGreaterThan(ref i19, ref i38);
        Branchless.SwapIfGreaterThan(ref i20, ref i32);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i35);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i40, ref i53);
        Branchless.SwapIfGreaterThan(ref i41, ref i52);
        Branchless.SwapIfGreaterThan(ref i43, ref i54);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i51);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i5);
        Branchless.SwapIfGreaterThan(ref i1, ref i7);
        Branchless.SwapIfGreaterThan(ref i2, ref i9);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i8, ref i14);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i15, ref i33);
        Branchless.SwapIfGreaterThan(ref i16, ref i21);
        Branchless.SwapIfGreaterThan(ref i17, ref i23);
        Branchless.SwapIfGreaterThan(ref i18, ref i34);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i22, ref i37);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i39, ref i49);
        Branchless.SwapIfGreaterThan(ref i40, ref i45);
        Branchless.SwapIfGreaterThan(ref i41, ref i47);
        Branchless.SwapIfGreaterThan(ref i42, ref i50);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i46, ref i53);
        Branchless.SwapIfGreaterThan(ref i48, ref i54);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i35);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i32);
        Branchless.SwapIfGreaterThan(ref i23, ref i33);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i51);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i54);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i10);
        Branchless.SwapIfGreaterThan(ref i5, ref i11);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i35);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i51);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i24);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i39);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i34);
        Branchless.SwapIfGreaterThan(ref i20, ref i32);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i54);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i46);
        Branchless.SwapIfGreaterThan(ref i43, ref i50);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i49, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i15);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i22);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i23, ref i32);
        Branchless.SwapIfGreaterThan(ref i24, ref i39);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i38);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i46);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i52);
        Branchless.SwapIfGreaterThan(ref i51, ref i53);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i16);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i15, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i25, ref i40);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i53);
        Branchless.SwapIfGreaterThan(ref i38, ref i54);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i46);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i25);
        Branchless.SwapIfGreaterThan(ref i2, ref i26);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i36);
        Branchless.SwapIfGreaterThan(ref i14, ref i37);
        Branchless.SwapIfGreaterThan(ref i16, ref i40);
        Branchless.SwapIfGreaterThan(ref i17, ref i41);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i33);
        Branchless.SwapIfGreaterThan(ref i29, ref i52);
        Branchless.SwapIfGreaterThan(ref i30, ref i53);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i2, ref i17);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i29);
        Branchless.SwapIfGreaterThan(ref i16, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i26, ref i41);
        Branchless.SwapIfGreaterThan(ref i30, ref i37);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i52);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i15);
        Branchless.SwapIfGreaterThan(ref i3, ref i27);
        Branchless.SwapIfGreaterThan(ref i4, ref i43);
        Branchless.SwapIfGreaterThan(ref i5, ref i44);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i49);
        Branchless.SwapIfGreaterThan(ref i11, ref i50);
        Branchless.SwapIfGreaterThan(ref i12, ref i35);
        Branchless.SwapIfGreaterThan(ref i13, ref i25);
        Branchless.SwapIfGreaterThan(ref i17, ref i26);
        Branchless.SwapIfGreaterThan(ref i18, ref i42);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i32);
        Branchless.SwapIfGreaterThan(ref i28, ref i51);
        Branchless.SwapIfGreaterThan(ref i29, ref i36);
        Branchless.SwapIfGreaterThan(ref i30, ref i41);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i52, ref i54);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i3, ref i18);
        Branchless.SwapIfGreaterThan(ref i4, ref i19);
        Branchless.SwapIfGreaterThan(ref i5, ref i20);
        Branchless.SwapIfGreaterThan(ref i6, ref i21);
        Branchless.SwapIfGreaterThan(ref i7, ref i31);
        Branchless.SwapIfGreaterThan(ref i8, ref i23);
        Branchless.SwapIfGreaterThan(ref i9, ref i40);
        Branchless.SwapIfGreaterThan(ref i12, ref i28);
        Branchless.SwapIfGreaterThan(ref i14, ref i45);
        Branchless.SwapIfGreaterThan(ref i22, ref i46);
        Branchless.SwapIfGreaterThan(ref i27, ref i42);
        Branchless.SwapIfGreaterThan(ref i32, ref i48);
        Branchless.SwapIfGreaterThan(ref i33, ref i49);
        Branchless.SwapIfGreaterThan(ref i34, ref i50);
        Branchless.SwapIfGreaterThan(ref i35, ref i51);
        Branchless.SwapIfGreaterThan(ref i39, ref i47);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i22);
        Branchless.SwapIfGreaterThan(ref i8, ref i39);
        Branchless.SwapIfGreaterThan(ref i9, ref i32);
        Branchless.SwapIfGreaterThan(ref i10, ref i33);
        Branchless.SwapIfGreaterThan(ref i11, ref i34);
        Branchless.SwapIfGreaterThan(ref i12, ref i24);
        Branchless.SwapIfGreaterThan(ref i18, ref i27);
        Branchless.SwapIfGreaterThan(ref i19, ref i43);
        Branchless.SwapIfGreaterThan(ref i20, ref i44);
        Branchless.SwapIfGreaterThan(ref i21, ref i45);
        Branchless.SwapIfGreaterThan(ref i23, ref i47);
        Branchless.SwapIfGreaterThan(ref i28, ref i35);
        Branchless.SwapIfGreaterThan(ref i31, ref i46);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i40, ref i48);
        Branchless.SwapIfGreaterThan(ref i41, ref i49);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i6, ref i17);
        Branchless.SwapIfGreaterThan(ref i7, ref i18);
        Branchless.SwapIfGreaterThan(ref i8, ref i15);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i10, ref i26);
        Branchless.SwapIfGreaterThan(ref i11, ref i27);
        Branchless.SwapIfGreaterThan(ref i19, ref i24);
        Branchless.SwapIfGreaterThan(ref i20, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i31);
        Branchless.SwapIfGreaterThan(ref i23, ref i39);
        Branchless.SwapIfGreaterThan(ref i28, ref i43);
        Branchless.SwapIfGreaterThan(ref i29, ref i44);
        Branchless.SwapIfGreaterThan(ref i30, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i47);
        Branchless.SwapIfGreaterThan(ref i36, ref i48);
        Branchless.SwapIfGreaterThan(ref i37, ref i45);
        Branchless.SwapIfGreaterThan(ref i42, ref i50);
        Branchless.SwapIfGreaterThan(ref i46, ref i52);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i17);
        Branchless.SwapIfGreaterThan(ref i11, ref i18);
        Branchless.SwapIfGreaterThan(ref i12, ref i19);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i40);
        Branchless.SwapIfGreaterThan(ref i27, ref i34);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i43);
        Branchless.SwapIfGreaterThan(ref i36, ref i44);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i45, ref i51);
        Branchless.SwapIfGreaterThan(ref i50, ref i53);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i14, ref i21);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i22, ref i27);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i28, ref i39);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i32, ref i40);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i23);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i25);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i30, ref i35);
        Branchless.SwapIfGreaterThan(ref i33, ref i39);
        Branchless.SwapIfGreaterThan(ref i34, ref i40);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i46);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i15);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i33);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i43);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i47);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i35);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i36, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i40, ref i43);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i44, ref i47);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        
        // Layer 19
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        
        // Layer 20
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
    }
    
    /// <summary>
    /// Sort 56 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort56Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        ref var i48 = ref itemArray[index + 48];
        ref var i49 = ref itemArray[index + 49];
        ref var i50 = ref itemArray[index + 50];
        ref var i51 = ref itemArray[index + 51];
        ref var i52 = ref itemArray[index + 52];
        ref var i53 = ref itemArray[index + 53];
        ref var i54 = ref itemArray[index + 54];
        ref var i55 = ref itemArray[index + 55];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i13);
        Branchless.SwapIfGreaterThan(ref i1, ref i12);
        Branchless.SwapIfGreaterThan(ref i2, ref i15);
        Branchless.SwapIfGreaterThan(ref i3, ref i14);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i16, ref i37);
        Branchless.SwapIfGreaterThan(ref i17, ref i36);
        Branchless.SwapIfGreaterThan(ref i18, ref i39);
        Branchless.SwapIfGreaterThan(ref i19, ref i38);
        Branchless.SwapIfGreaterThan(ref i20, ref i32);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i35);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i40, ref i53);
        Branchless.SwapIfGreaterThan(ref i41, ref i52);
        Branchless.SwapIfGreaterThan(ref i42, ref i55);
        Branchless.SwapIfGreaterThan(ref i43, ref i54);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i51);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i5);
        Branchless.SwapIfGreaterThan(ref i1, ref i7);
        Branchless.SwapIfGreaterThan(ref i2, ref i9);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i13);
        Branchless.SwapIfGreaterThan(ref i8, ref i14);
        Branchless.SwapIfGreaterThan(ref i10, ref i15);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i16, ref i21);
        Branchless.SwapIfGreaterThan(ref i17, ref i23);
        Branchless.SwapIfGreaterThan(ref i18, ref i33);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i22, ref i37);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i38);
        Branchless.SwapIfGreaterThan(ref i34, ref i39);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i40, ref i45);
        Branchless.SwapIfGreaterThan(ref i41, ref i47);
        Branchless.SwapIfGreaterThan(ref i42, ref i49);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i46, ref i53);
        Branchless.SwapIfGreaterThan(ref i48, ref i54);
        Branchless.SwapIfGreaterThan(ref i50, ref i55);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i32);
        Branchless.SwapIfGreaterThan(ref i23, ref i33);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i55);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i10);
        Branchless.SwapIfGreaterThan(ref i5, ref i11);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i34);
        Branchless.SwapIfGreaterThan(ref i21, ref i35);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i50);
        Branchless.SwapIfGreaterThan(ref i45, ref i51);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i52, ref i54);
        Branchless.SwapIfGreaterThan(ref i53, ref i55);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i24);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i39);
        Branchless.SwapIfGreaterThan(ref i16, ref i40);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i36);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i55);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i52);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        Branchless.SwapIfGreaterThan(ref i49, ref i51);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i16);
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i31);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i32);
        Branchless.SwapIfGreaterThan(ref i23, ref i34);
        Branchless.SwapIfGreaterThan(ref i24, ref i40);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i55);
        Branchless.SwapIfGreaterThan(ref i41, ref i44);
        Branchless.SwapIfGreaterThan(ref i42, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i50);
        Branchless.SwapIfGreaterThan(ref i49, ref i53);
        Branchless.SwapIfGreaterThan(ref i51, ref i54);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i17);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i25, ref i41);
        Branchless.SwapIfGreaterThan(ref i31, ref i39);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i54);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i46);
        Branchless.SwapIfGreaterThan(ref i49, ref i52);
        Branchless.SwapIfGreaterThan(ref i51, ref i53);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i25);
        Branchless.SwapIfGreaterThan(ref i2, ref i26);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i37);
        Branchless.SwapIfGreaterThan(ref i14, ref i38);
        Branchless.SwapIfGreaterThan(ref i17, ref i41);
        Branchless.SwapIfGreaterThan(ref i18, ref i42);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i32);
        Branchless.SwapIfGreaterThan(ref i23, ref i33);
        Branchless.SwapIfGreaterThan(ref i29, ref i53);
        Branchless.SwapIfGreaterThan(ref i30, ref i54);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i52);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i2, ref i18);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i29);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i32);
        Branchless.SwapIfGreaterThan(ref i26, ref i42);
        Branchless.SwapIfGreaterThan(ref i30, ref i38);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i53);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i16);
        Branchless.SwapIfGreaterThan(ref i3, ref i27);
        Branchless.SwapIfGreaterThan(ref i4, ref i44);
        Branchless.SwapIfGreaterThan(ref i5, ref i45);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i50);
        Branchless.SwapIfGreaterThan(ref i11, ref i51);
        Branchless.SwapIfGreaterThan(ref i12, ref i36);
        Branchless.SwapIfGreaterThan(ref i13, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i43);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i28, ref i52);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i42);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i39, ref i53);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i4, ref i20);
        Branchless.SwapIfGreaterThan(ref i5, ref i21);
        Branchless.SwapIfGreaterThan(ref i6, ref i22);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i8, ref i32);
        Branchless.SwapIfGreaterThan(ref i9, ref i41);
        Branchless.SwapIfGreaterThan(ref i12, ref i28);
        Branchless.SwapIfGreaterThan(ref i14, ref i46);
        Branchless.SwapIfGreaterThan(ref i23, ref i47);
        Branchless.SwapIfGreaterThan(ref i27, ref i43);
        Branchless.SwapIfGreaterThan(ref i33, ref i49);
        Branchless.SwapIfGreaterThan(ref i34, ref i50);
        Branchless.SwapIfGreaterThan(ref i35, ref i51);
        Branchless.SwapIfGreaterThan(ref i36, ref i52);
        Branchless.SwapIfGreaterThan(ref i40, ref i48);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i23);
        Branchless.SwapIfGreaterThan(ref i8, ref i40);
        Branchless.SwapIfGreaterThan(ref i9, ref i33);
        Branchless.SwapIfGreaterThan(ref i10, ref i34);
        Branchless.SwapIfGreaterThan(ref i11, ref i35);
        Branchless.SwapIfGreaterThan(ref i12, ref i24);
        Branchless.SwapIfGreaterThan(ref i15, ref i47);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i44);
        Branchless.SwapIfGreaterThan(ref i21, ref i45);
        Branchless.SwapIfGreaterThan(ref i22, ref i46);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i31, ref i43);
        Branchless.SwapIfGreaterThan(ref i32, ref i48);
        Branchless.SwapIfGreaterThan(ref i41, ref i49);
        Branchless.SwapIfGreaterThan(ref i42, ref i50);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i6, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i19);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i26);
        Branchless.SwapIfGreaterThan(ref i11, ref i27);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i28, ref i44);
        Branchless.SwapIfGreaterThan(ref i29, ref i45);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i32, ref i40);
        Branchless.SwapIfGreaterThan(ref i36, ref i48);
        Branchless.SwapIfGreaterThan(ref i37, ref i49);
        Branchless.SwapIfGreaterThan(ref i38, ref i46);
        Branchless.SwapIfGreaterThan(ref i39, ref i47);
        Branchless.SwapIfGreaterThan(ref i43, ref i51);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i41);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i43);
        Branchless.SwapIfGreaterThan(ref i36, ref i44);
        Branchless.SwapIfGreaterThan(ref i37, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i52);
        Branchless.SwapIfGreaterThan(ref i51, ref i54);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i13, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i27);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i28, ref i40);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        Branchless.SwapIfGreaterThan(ref i34, ref i42);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i46, ref i50);
        Branchless.SwapIfGreaterThan(ref i47, ref i51);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i24);
        Branchless.SwapIfGreaterThan(ref i15, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i36);
        Branchless.SwapIfGreaterThan(ref i31, ref i41);
        Branchless.SwapIfGreaterThan(ref i34, ref i40);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i32);
        Branchless.SwapIfGreaterThan(ref i27, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i44);
        Branchless.SwapIfGreaterThan(ref i39, ref i45);
        Branchless.SwapIfGreaterThan(ref i42, ref i48);
        Branchless.SwapIfGreaterThan(ref i43, ref i49);
        Branchless.SwapIfGreaterThan(ref i47, ref i51);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i50);
        
        // Layer 19
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        
        // Layer 20
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
    }
    
    /// <summary>
    /// Sort 57 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort57Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        ref var i48 = ref itemArray[index + 48];
        ref var i49 = ref itemArray[index + 49];
        ref var i50 = ref itemArray[index + 50];
        ref var i51 = ref itemArray[index + 51];
        ref var i52 = ref itemArray[index + 52];
        ref var i53 = ref itemArray[index + 53];
        ref var i54 = ref itemArray[index + 54];
        ref var i55 = ref itemArray[index + 55];
        ref var i56 = ref itemArray[index + 56];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i3);
        Branchless.SwapIfGreaterThan(ref i1, ref i7);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i14);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i11, ref i21);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i24);
        Branchless.SwapIfGreaterThan(ref i20, ref i23);
        Branchless.SwapIfGreaterThan(ref i25, ref i30);
        Branchless.SwapIfGreaterThan(ref i26, ref i29);
        Branchless.SwapIfGreaterThan(ref i27, ref i37);
        Branchless.SwapIfGreaterThan(ref i28, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i40);
        Branchless.SwapIfGreaterThan(ref i36, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i46);
        Branchless.SwapIfGreaterThan(ref i42, ref i45);
        Branchless.SwapIfGreaterThan(ref i43, ref i53);
        Branchless.SwapIfGreaterThan(ref i44, ref i54);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i56);
        Branchless.SwapIfGreaterThan(ref i52, ref i55);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i7);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i10, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i23);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i26, ref i35);
        Branchless.SwapIfGreaterThan(ref i28, ref i31);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i30, ref i39);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i34, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i42, ref i51);
        Branchless.SwapIfGreaterThan(ref i44, ref i47);
        Branchless.SwapIfGreaterThan(ref i45, ref i48);
        Branchless.SwapIfGreaterThan(ref i46, ref i55);
        Branchless.SwapIfGreaterThan(ref i49, ref i52);
        Branchless.SwapIfGreaterThan(ref i50, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i56);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i20);
        Branchless.SwapIfGreaterThan(ref i13, ref i22);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i38);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i32, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i49);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i52);
        Branchless.SwapIfGreaterThan(ref i45, ref i54);
        Branchless.SwapIfGreaterThan(ref i46, ref i50);
        Branchless.SwapIfGreaterThan(ref i47, ref i51);
        Branchless.SwapIfGreaterThan(ref i48, ref i56);
        Branchless.SwapIfGreaterThan(ref i53, ref i55);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i44, ref i49);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i53);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i54);
        Branchless.SwapIfGreaterThan(ref i55, ref i56);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i41);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i56);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i49);
        Branchless.SwapIfGreaterThan(ref i47, ref i50);
        Branchless.SwapIfGreaterThan(ref i48, ref i52);
        Branchless.SwapIfGreaterThan(ref i51, ref i54);
        Branchless.SwapIfGreaterThan(ref i53, ref i55);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i25);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i36);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i52);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i55);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i0, ref i9);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i40);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i55);
        Branchless.SwapIfGreaterThan(ref i25, ref i41);
        Branchless.SwapIfGreaterThan(ref i26, ref i42);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i10);
        Branchless.SwapIfGreaterThan(ref i2, ref i11);
        Branchless.SwapIfGreaterThan(ref i5, ref i38);
        Branchless.SwapIfGreaterThan(ref i6, ref i39);
        Branchless.SwapIfGreaterThan(ref i7, ref i24);
        Branchless.SwapIfGreaterThan(ref i9, ref i25);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i22, ref i54);
        Branchless.SwapIfGreaterThan(ref i27, ref i43);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i40, ref i56);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i51);
        Branchless.SwapIfGreaterThan(ref i50, ref i52);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i5, ref i22);
        Branchless.SwapIfGreaterThan(ref i6, ref i23);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i24, ref i40);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i54);
        Branchless.SwapIfGreaterThan(ref i39, ref i55);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i1, ref i46);
        Branchless.SwapIfGreaterThan(ref i2, ref i47);
        Branchless.SwapIfGreaterThan(ref i3, ref i12);
        Branchless.SwapIfGreaterThan(ref i4, ref i13);
        Branchless.SwapIfGreaterThan(ref i8, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i15, ref i31);
        Branchless.SwapIfGreaterThan(ref i16, ref i32);
        Branchless.SwapIfGreaterThan(ref i22, ref i38);
        Branchless.SwapIfGreaterThan(ref i23, ref i39);
        Branchless.SwapIfGreaterThan(ref i28, ref i44);
        Branchless.SwapIfGreaterThan(ref i29, ref i45);
        Branchless.SwapIfGreaterThan(ref i33, ref i49);
        Branchless.SwapIfGreaterThan(ref i34, ref i50);
        Branchless.SwapIfGreaterThan(ref i35, ref i51);
        Branchless.SwapIfGreaterThan(ref i36, ref i52);
        Branchless.SwapIfGreaterThan(ref i37, ref i53);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i1, ref i26);
        Branchless.SwapIfGreaterThan(ref i2, ref i27);
        Branchless.SwapIfGreaterThan(ref i3, ref i48);
        Branchless.SwapIfGreaterThan(ref i4, ref i29);
        Branchless.SwapIfGreaterThan(ref i5, ref i14);
        Branchless.SwapIfGreaterThan(ref i6, ref i15);
        Branchless.SwapIfGreaterThan(ref i7, ref i16);
        Branchless.SwapIfGreaterThan(ref i8, ref i33);
        Branchless.SwapIfGreaterThan(ref i10, ref i42);
        Branchless.SwapIfGreaterThan(ref i11, ref i43);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i13, ref i45);
        Branchless.SwapIfGreaterThan(ref i17, ref i49);
        Branchless.SwapIfGreaterThan(ref i18, ref i34);
        Branchless.SwapIfGreaterThan(ref i19, ref i35);
        Branchless.SwapIfGreaterThan(ref i21, ref i53);
        Branchless.SwapIfGreaterThan(ref i30, ref i46);
        Branchless.SwapIfGreaterThan(ref i31, ref i47);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i3, ref i28);
        Branchless.SwapIfGreaterThan(ref i4, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i26);
        Branchless.SwapIfGreaterThan(ref i6, ref i27);
        Branchless.SwapIfGreaterThan(ref i8, ref i25);
        Branchless.SwapIfGreaterThan(ref i12, ref i44);
        Branchless.SwapIfGreaterThan(ref i13, ref i29);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i15, ref i31);
        Branchless.SwapIfGreaterThan(ref i17, ref i33);
        Branchless.SwapIfGreaterThan(ref i18, ref i42);
        Branchless.SwapIfGreaterThan(ref i19, ref i43);
        Branchless.SwapIfGreaterThan(ref i20, ref i36);
        Branchless.SwapIfGreaterThan(ref i21, ref i37);
        Branchless.SwapIfGreaterThan(ref i22, ref i46);
        Branchless.SwapIfGreaterThan(ref i23, ref i47);
        Branchless.SwapIfGreaterThan(ref i32, ref i48);
        Branchless.SwapIfGreaterThan(ref i34, ref i50);
        Branchless.SwapIfGreaterThan(ref i35, ref i51);
        Branchless.SwapIfGreaterThan(ref i49, ref i53);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i7, ref i28);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i32);
        Branchless.SwapIfGreaterThan(ref i17, ref i41);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i44);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i24, ref i48);
        Branchless.SwapIfGreaterThan(ref i34, ref i42);
        Branchless.SwapIfGreaterThan(ref i35, ref i43);
        Branchless.SwapIfGreaterThan(ref i36, ref i52);
        Branchless.SwapIfGreaterThan(ref i37, ref i45);
        Branchless.SwapIfGreaterThan(ref i38, ref i50);
        Branchless.SwapIfGreaterThan(ref i39, ref i51);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i26);
        Branchless.SwapIfGreaterThan(ref i15, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i34);
        Branchless.SwapIfGreaterThan(ref i23, ref i35);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i41);
        Branchless.SwapIfGreaterThan(ref i30, ref i42);
        Branchless.SwapIfGreaterThan(ref i31, ref i43);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i36, ref i44);
        Branchless.SwapIfGreaterThan(ref i38, ref i46);
        Branchless.SwapIfGreaterThan(ref i39, ref i47);
        Branchless.SwapIfGreaterThan(ref i40, ref i52);
        Branchless.SwapIfGreaterThan(ref i45, ref i49);
        Branchless.SwapIfGreaterThan(ref i51, ref i53);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i13, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i28);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i24, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i32, ref i44);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i40, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i52, ref i55);
        Branchless.SwapIfGreaterThan(ref i53, ref i56);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i3, ref i10);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i41, ref i45);
        Branchless.SwapIfGreaterThan(ref i48, ref i54);
        Branchless.SwapIfGreaterThan(ref i53, ref i55);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i24, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i32, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i36, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i40, ref i46);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i44, ref i50);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i19);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i20, ref i23);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i24, ref i27);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i35);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i36, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i40, ref i43);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i44, ref i47);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i54);
        
        // Layer 19
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        Branchless.SwapIfGreaterThan(ref i49, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        
        // Layer 20
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
    }
    
    /// <summary>
    /// Sort 58 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort58Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        ref var i48 = ref itemArray[index + 48];
        ref var i49 = ref itemArray[index + 49];
        ref var i50 = ref itemArray[index + 50];
        ref var i51 = ref itemArray[index + 51];
        ref var i52 = ref itemArray[index + 52];
        ref var i53 = ref itemArray[index + 53];
        ref var i54 = ref itemArray[index + 54];
        ref var i55 = ref itemArray[index + 55];
        ref var i56 = ref itemArray[index + 56];
        ref var i57 = ref itemArray[index + 57];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i17);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i52);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i29);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i33);
        Branchless.SwapIfGreaterThan(ref i23, ref i54);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i55);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i45);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i57);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i56);
        Branchless.SwapIfGreaterThan(ref i51, ref i53);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i3, ref i20);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i16);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i11, ref i48);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i15, ref i51);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i22, ref i32);
        Branchless.SwapIfGreaterThan(ref i23, ref i50);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i47);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i44);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i56);
        Branchless.SwapIfGreaterThan(ref i55, ref i57);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i3);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i19, ref i24);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i36);
        Branchless.SwapIfGreaterThan(ref i23, ref i35);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i31, ref i40);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i57);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i56);
        Branchless.SwapIfGreaterThan(ref i47, ref i55);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i54);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i20);
        Branchless.SwapIfGreaterThan(ref i2, ref i21);
        Branchless.SwapIfGreaterThan(ref i3, ref i35);
        Branchless.SwapIfGreaterThan(ref i4, ref i31);
        Branchless.SwapIfGreaterThan(ref i5, ref i16);
        Branchless.SwapIfGreaterThan(ref i6, ref i17);
        Branchless.SwapIfGreaterThan(ref i7, ref i24);
        Branchless.SwapIfGreaterThan(ref i9, ref i48);
        Branchless.SwapIfGreaterThan(ref i10, ref i49);
        Branchless.SwapIfGreaterThan(ref i11, ref i36);
        Branchless.SwapIfGreaterThan(ref i12, ref i19);
        Branchless.SwapIfGreaterThan(ref i13, ref i51);
        Branchless.SwapIfGreaterThan(ref i14, ref i52);
        Branchless.SwapIfGreaterThan(ref i15, ref i40);
        Branchless.SwapIfGreaterThan(ref i18, ref i43);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i26, ref i29);
        Branchless.SwapIfGreaterThan(ref i27, ref i53);
        Branchless.SwapIfGreaterThan(ref i30, ref i57);
        Branchless.SwapIfGreaterThan(ref i32, ref i37);
        Branchless.SwapIfGreaterThan(ref i33, ref i38);
        Branchless.SwapIfGreaterThan(ref i34, ref i46);
        Branchless.SwapIfGreaterThan(ref i39, ref i56);
        Branchless.SwapIfGreaterThan(ref i41, ref i47);
        Branchless.SwapIfGreaterThan(ref i42, ref i55);
        Branchless.SwapIfGreaterThan(ref i44, ref i50);
        Branchless.SwapIfGreaterThan(ref i45, ref i54);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i22);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i41);
        Branchless.SwapIfGreaterThan(ref i6, ref i42);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i8, ref i23);
        Branchless.SwapIfGreaterThan(ref i13, ref i25);
        Branchless.SwapIfGreaterThan(ref i14, ref i26);
        Branchless.SwapIfGreaterThan(ref i16, ref i28);
        Branchless.SwapIfGreaterThan(ref i17, ref i29);
        Branchless.SwapIfGreaterThan(ref i18, ref i27);
        Branchless.SwapIfGreaterThan(ref i19, ref i31);
        Branchless.SwapIfGreaterThan(ref i20, ref i50);
        Branchless.SwapIfGreaterThan(ref i21, ref i54);
        Branchless.SwapIfGreaterThan(ref i24, ref i40);
        Branchless.SwapIfGreaterThan(ref i30, ref i46);
        Branchless.SwapIfGreaterThan(ref i32, ref i44);
        Branchless.SwapIfGreaterThan(ref i33, ref i45);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i48);
        Branchless.SwapIfGreaterThan(ref i38, ref i49);
        Branchless.SwapIfGreaterThan(ref i43, ref i53);
        Branchless.SwapIfGreaterThan(ref i47, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i55);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i32);
        Branchless.SwapIfGreaterThan(ref i2, ref i33);
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i44);
        Branchless.SwapIfGreaterThan(ref i10, ref i45);
        Branchless.SwapIfGreaterThan(ref i11, ref i31);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i16, ref i47);
        Branchless.SwapIfGreaterThan(ref i17, ref i52);
        Branchless.SwapIfGreaterThan(ref i18, ref i34);
        Branchless.SwapIfGreaterThan(ref i20, ref i37);
        Branchless.SwapIfGreaterThan(ref i21, ref i38);
        Branchless.SwapIfGreaterThan(ref i24, ref i35);
        Branchless.SwapIfGreaterThan(ref i25, ref i41);
        Branchless.SwapIfGreaterThan(ref i26, ref i42);
        Branchless.SwapIfGreaterThan(ref i28, ref i51);
        Branchless.SwapIfGreaterThan(ref i29, ref i55);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        Branchless.SwapIfGreaterThan(ref i49, ref i54);
        Branchless.SwapIfGreaterThan(ref i53, ref i56);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i8, ref i22);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i10, ref i17);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i13, ref i32);
        Branchless.SwapIfGreaterThan(ref i14, ref i33);
        Branchless.SwapIfGreaterThan(ref i15, ref i24);
        Branchless.SwapIfGreaterThan(ref i20, ref i25);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i27, ref i34);
        Branchless.SwapIfGreaterThan(ref i28, ref i48);
        Branchless.SwapIfGreaterThan(ref i29, ref i49);
        Branchless.SwapIfGreaterThan(ref i30, ref i39);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i53);
        Branchless.SwapIfGreaterThan(ref i44, ref i47);
        Branchless.SwapIfGreaterThan(ref i45, ref i52);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        Branchless.SwapIfGreaterThan(ref i54, ref i55);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i8, ref i24);
        Branchless.SwapIfGreaterThan(ref i9, ref i20);
        Branchless.SwapIfGreaterThan(ref i10, ref i21);
        Branchless.SwapIfGreaterThan(ref i11, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i32);
        Branchless.SwapIfGreaterThan(ref i17, ref i33);
        Branchless.SwapIfGreaterThan(ref i23, ref i35);
        Branchless.SwapIfGreaterThan(ref i25, ref i37);
        Branchless.SwapIfGreaterThan(ref i26, ref i38);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i28, ref i44);
        Branchless.SwapIfGreaterThan(ref i29, ref i45);
        Branchless.SwapIfGreaterThan(ref i31, ref i36);
        Branchless.SwapIfGreaterThan(ref i34, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i47);
        Branchless.SwapIfGreaterThan(ref i42, ref i52);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        Branchless.SwapIfGreaterThan(ref i49, ref i54);
        Branchless.SwapIfGreaterThan(ref i53, ref i57);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i14, ref i21);
        Branchless.SwapIfGreaterThan(ref i16, ref i44);
        Branchless.SwapIfGreaterThan(ref i17, ref i45);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i32);
        Branchless.SwapIfGreaterThan(ref i26, ref i33);
        Branchless.SwapIfGreaterThan(ref i28, ref i37);
        Branchless.SwapIfGreaterThan(ref i29, ref i38);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i39, ref i46);
        Branchless.SwapIfGreaterThan(ref i41, ref i48);
        Branchless.SwapIfGreaterThan(ref i42, ref i49);
        Branchless.SwapIfGreaterThan(ref i43, ref i53);
        Branchless.SwapIfGreaterThan(ref i47, ref i50);
        Branchless.SwapIfGreaterThan(ref i52, ref i54);
        Branchless.SwapIfGreaterThan(ref i56, ref i57);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i26, ref i29);
        Branchless.SwapIfGreaterThan(ref i32, ref i37);
        Branchless.SwapIfGreaterThan(ref i33, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i41, ref i44);
        Branchless.SwapIfGreaterThan(ref i42, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i53);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i52);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i25);
        Branchless.SwapIfGreaterThan(ref i17, ref i26);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i41);
        Branchless.SwapIfGreaterThan(ref i33, ref i42);
        Branchless.SwapIfGreaterThan(ref i34, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i44);
        Branchless.SwapIfGreaterThan(ref i38, ref i45);
        Branchless.SwapIfGreaterThan(ref i43, ref i46);
        Branchless.SwapIfGreaterThan(ref i52, ref i57);
        Branchless.SwapIfGreaterThan(ref i53, ref i56);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i2, ref i19);
        Branchless.SwapIfGreaterThan(ref i5, ref i11);
        Branchless.SwapIfGreaterThan(ref i6, ref i22);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i23);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i20, ref i25);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i48);
        Branchless.SwapIfGreaterThan(ref i44, ref i47);
        Branchless.SwapIfGreaterThan(ref i45, ref i49);
        Branchless.SwapIfGreaterThan(ref i46, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i53);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i31);
        Branchless.SwapIfGreaterThan(ref i18, ref i32);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i35);
        Branchless.SwapIfGreaterThan(ref i26, ref i36);
        Branchless.SwapIfGreaterThan(ref i27, ref i37);
        Branchless.SwapIfGreaterThan(ref i29, ref i40);
        Branchless.SwapIfGreaterThan(ref i30, ref i41);
        Branchless.SwapIfGreaterThan(ref i34, ref i44);
        Branchless.SwapIfGreaterThan(ref i39, ref i47);
        Branchless.SwapIfGreaterThan(ref i42, ref i46);
        Branchless.SwapIfGreaterThan(ref i48, ref i52);
        Branchless.SwapIfGreaterThan(ref i49, ref i56);
        Branchless.SwapIfGreaterThan(ref i50, ref i54);
        Branchless.SwapIfGreaterThan(ref i53, ref i55);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i21, ref i27);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i24, ref i29);
        Branchless.SwapIfGreaterThan(ref i25, ref i30);
        Branchless.SwapIfGreaterThan(ref i28, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i32, ref i39);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i36, ref i41);
        Branchless.SwapIfGreaterThan(ref i37, ref i43);
        Branchless.SwapIfGreaterThan(ref i40, ref i45);
        Branchless.SwapIfGreaterThan(ref i44, ref i51);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i23);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i27);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i26, ref i31);
        Branchless.SwapIfGreaterThan(ref i29, ref i35);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i39);
        Branchless.SwapIfGreaterThan(ref i34, ref i37);
        Branchless.SwapIfGreaterThan(ref i36, ref i42);
        Branchless.SwapIfGreaterThan(ref i38, ref i44);
        Branchless.SwapIfGreaterThan(ref i40, ref i43);
        Branchless.SwapIfGreaterThan(ref i41, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i51);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i20);
        Branchless.SwapIfGreaterThan(ref i12, ref i19);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i47);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i46, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i53);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i31);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i35);
        Branchless.SwapIfGreaterThan(ref i30, ref i33);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i47);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i48);
        Branchless.SwapIfGreaterThan(ref i46, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i56);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i57);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i51);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i52, ref i56);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        Branchless.SwapIfGreaterThan(ref i55, ref i57);
        
        // Layer 19
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i56);
        
        // Layer 20
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        Branchless.SwapIfGreaterThan(ref i55, ref i56);
    }
    
    /// <summary>
    /// Sort 59 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort59Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        ref var i48 = ref itemArray[index + 48];
        ref var i49 = ref itemArray[index + 49];
        ref var i50 = ref itemArray[index + 50];
        ref var i51 = ref itemArray[index + 51];
        ref var i52 = ref itemArray[index + 52];
        ref var i53 = ref itemArray[index + 53];
        ref var i54 = ref itemArray[index + 54];
        ref var i55 = ref itemArray[index + 55];
        ref var i56 = ref itemArray[index + 56];
        ref var i57 = ref itemArray[index + 57];
        ref var i58 = ref itemArray[index + 58];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i9);
        Branchless.SwapIfGreaterThan(ref i1, ref i6);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i23);
        Branchless.SwapIfGreaterThan(ref i14, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i27, ref i32);
        Branchless.SwapIfGreaterThan(ref i28, ref i31);
        Branchless.SwapIfGreaterThan(ref i29, ref i39);
        Branchless.SwapIfGreaterThan(ref i30, ref i40);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i42);
        Branchless.SwapIfGreaterThan(ref i38, ref i41);
        Branchless.SwapIfGreaterThan(ref i43, ref i48);
        Branchless.SwapIfGreaterThan(ref i44, ref i47);
        Branchless.SwapIfGreaterThan(ref i45, ref i55);
        Branchless.SwapIfGreaterThan(ref i46, ref i56);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i58);
        Branchless.SwapIfGreaterThan(ref i54, ref i57);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i10);
        Branchless.SwapIfGreaterThan(ref i6, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i16, ref i25);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i20, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i33);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i32, ref i41);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i36, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i44, ref i53);
        Branchless.SwapIfGreaterThan(ref i46, ref i49);
        Branchless.SwapIfGreaterThan(ref i47, ref i50);
        Branchless.SwapIfGreaterThan(ref i48, ref i57);
        Branchless.SwapIfGreaterThan(ref i51, ref i54);
        Branchless.SwapIfGreaterThan(ref i52, ref i55);
        Branchless.SwapIfGreaterThan(ref i56, ref i58);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i24);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i40);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i43, ref i51);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i54);
        Branchless.SwapIfGreaterThan(ref i47, ref i56);
        Branchless.SwapIfGreaterThan(ref i48, ref i52);
        Branchless.SwapIfGreaterThan(ref i49, ref i53);
        Branchless.SwapIfGreaterThan(ref i50, ref i58);
        Branchless.SwapIfGreaterThan(ref i55, ref i57);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i14, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i30, ref i35);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i39);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i46, ref i51);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i55);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i56);
        Branchless.SwapIfGreaterThan(ref i57, ref i58);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i43);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i58);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i51);
        Branchless.SwapIfGreaterThan(ref i49, ref i52);
        Branchless.SwapIfGreaterThan(ref i50, ref i54);
        Branchless.SwapIfGreaterThan(ref i53, ref i56);
        Branchless.SwapIfGreaterThan(ref i55, ref i57);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i27);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i38);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i54);
        Branchless.SwapIfGreaterThan(ref i49, ref i51);
        Branchless.SwapIfGreaterThan(ref i50, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i55);
        Branchless.SwapIfGreaterThan(ref i56, ref i57);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i0, ref i11);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i12, ref i44);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i27, ref i43);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i57);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        Branchless.SwapIfGreaterThan(ref i55, ref i56);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i1, ref i28);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i11, ref i27);
        Branchless.SwapIfGreaterThan(ref i13, ref i45);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i25, ref i57);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i40, ref i56);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i53);
        Branchless.SwapIfGreaterThan(ref i52, ref i54);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i1, ref i12);
        Branchless.SwapIfGreaterThan(ref i2, ref i29);
        Branchless.SwapIfGreaterThan(ref i7, ref i42);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i56);
        Branchless.SwapIfGreaterThan(ref i25, ref i41);
        Branchless.SwapIfGreaterThan(ref i28, ref i44);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i55);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i2, ref i13);
        Branchless.SwapIfGreaterThan(ref i3, ref i30);
        Branchless.SwapIfGreaterThan(ref i4, ref i15);
        Branchless.SwapIfGreaterThan(ref i5, ref i16);
        Branchless.SwapIfGreaterThan(ref i6, ref i17);
        Branchless.SwapIfGreaterThan(ref i7, ref i26);
        Branchless.SwapIfGreaterThan(ref i8, ref i19);
        Branchless.SwapIfGreaterThan(ref i9, ref i20);
        Branchless.SwapIfGreaterThan(ref i10, ref i21);
        Branchless.SwapIfGreaterThan(ref i12, ref i28);
        Branchless.SwapIfGreaterThan(ref i14, ref i46);
        Branchless.SwapIfGreaterThan(ref i18, ref i34);
        Branchless.SwapIfGreaterThan(ref i22, ref i54);
        Branchless.SwapIfGreaterThan(ref i24, ref i40);
        Branchless.SwapIfGreaterThan(ref i29, ref i45);
        Branchless.SwapIfGreaterThan(ref i31, ref i47);
        Branchless.SwapIfGreaterThan(ref i32, ref i48);
        Branchless.SwapIfGreaterThan(ref i33, ref i49);
        Branchless.SwapIfGreaterThan(ref i35, ref i51);
        Branchless.SwapIfGreaterThan(ref i36, ref i52);
        Branchless.SwapIfGreaterThan(ref i37, ref i53);
        Branchless.SwapIfGreaterThan(ref i39, ref i55);
        Branchless.SwapIfGreaterThan(ref i42, ref i58);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i3, ref i50);
        Branchless.SwapIfGreaterThan(ref i4, ref i31);
        Branchless.SwapIfGreaterThan(ref i5, ref i32);
        Branchless.SwapIfGreaterThan(ref i6, ref i33);
        Branchless.SwapIfGreaterThan(ref i7, ref i18);
        Branchless.SwapIfGreaterThan(ref i8, ref i35);
        Branchless.SwapIfGreaterThan(ref i9, ref i36);
        Branchless.SwapIfGreaterThan(ref i10, ref i37);
        Branchless.SwapIfGreaterThan(ref i13, ref i29);
        Branchless.SwapIfGreaterThan(ref i15, ref i47);
        Branchless.SwapIfGreaterThan(ref i16, ref i48);
        Branchless.SwapIfGreaterThan(ref i17, ref i49);
        Branchless.SwapIfGreaterThan(ref i19, ref i51);
        Branchless.SwapIfGreaterThan(ref i20, ref i52);
        Branchless.SwapIfGreaterThan(ref i21, ref i53);
        Branchless.SwapIfGreaterThan(ref i23, ref i55);
        Branchless.SwapIfGreaterThan(ref i26, ref i42);
        Branchless.SwapIfGreaterThan(ref i30, ref i38);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i3, ref i14);
        Branchless.SwapIfGreaterThan(ref i4, ref i11);
        Branchless.SwapIfGreaterThan(ref i5, ref i12);
        Branchless.SwapIfGreaterThan(ref i6, ref i13);
        Branchless.SwapIfGreaterThan(ref i8, ref i27);
        Branchless.SwapIfGreaterThan(ref i9, ref i28);
        Branchless.SwapIfGreaterThan(ref i10, ref i29);
        Branchless.SwapIfGreaterThan(ref i15, ref i31);
        Branchless.SwapIfGreaterThan(ref i16, ref i32);
        Branchless.SwapIfGreaterThan(ref i17, ref i33);
        Branchless.SwapIfGreaterThan(ref i19, ref i35);
        Branchless.SwapIfGreaterThan(ref i20, ref i36);
        Branchless.SwapIfGreaterThan(ref i21, ref i37);
        Branchless.SwapIfGreaterThan(ref i22, ref i38);
        Branchless.SwapIfGreaterThan(ref i23, ref i39);
        Branchless.SwapIfGreaterThan(ref i30, ref i46);
        Branchless.SwapIfGreaterThan(ref i34, ref i50);
        Branchless.SwapIfGreaterThan(ref i40, ref i48);
        Branchless.SwapIfGreaterThan(ref i41, ref i49);
        Branchless.SwapIfGreaterThan(ref i51, ref i55);
        Branchless.SwapIfGreaterThan(ref i52, ref i56);
        Branchless.SwapIfGreaterThan(ref i53, ref i57);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i7, ref i14);
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i13);
        Branchless.SwapIfGreaterThan(ref i18, ref i34);
        Branchless.SwapIfGreaterThan(ref i19, ref i43);
        Branchless.SwapIfGreaterThan(ref i20, ref i44);
        Branchless.SwapIfGreaterThan(ref i21, ref i45);
        Branchless.SwapIfGreaterThan(ref i22, ref i46);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i50);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i54);
        Branchless.SwapIfGreaterThan(ref i39, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i52);
        Branchless.SwapIfGreaterThan(ref i49, ref i53);
        Branchless.SwapIfGreaterThan(ref i57, ref i58);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i14, ref i30);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i43);
        Branchless.SwapIfGreaterThan(ref i32, ref i44);
        Branchless.SwapIfGreaterThan(ref i33, ref i45);
        Branchless.SwapIfGreaterThan(ref i34, ref i46);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i42, ref i54);
        Branchless.SwapIfGreaterThan(ref i47, ref i51);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i8);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i46);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i41, ref i45);
        Branchless.SwapIfGreaterThan(ref i42, ref i50);
        Branchless.SwapIfGreaterThan(ref i54, ref i56);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i46);
        Branchless.SwapIfGreaterThan(ref i43, ref i47);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i45, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i52);
        Branchless.SwapIfGreaterThan(ref i56, ref i57);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i6, ref i11);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i19);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i23);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i27);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i31);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i35);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i39);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i43);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i47);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i51);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i55);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i3, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i23);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i40, ref i43);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        Branchless.SwapIfGreaterThan(ref i49, ref i51);
        Branchless.SwapIfGreaterThan(ref i54, ref i55);
        
        // Layer 19
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i54);
        Branchless.SwapIfGreaterThan(ref i53, ref i55);
        
        // Layer 20
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        Branchless.SwapIfGreaterThan(ref i55, ref i56);
    }
    
    /// <summary>
    /// Sort 60 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort60Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        ref var i48 = ref itemArray[index + 48];
        ref var i49 = ref itemArray[index + 49];
        ref var i50 = ref itemArray[index + 50];
        ref var i51 = ref itemArray[index + 51];
        ref var i52 = ref itemArray[index + 52];
        ref var i53 = ref itemArray[index + 53];
        ref var i54 = ref itemArray[index + 54];
        ref var i55 = ref itemArray[index + 55];
        ref var i56 = ref itemArray[index + 56];
        ref var i57 = ref itemArray[index + 57];
        ref var i58 = ref itemArray[index + 58];
        ref var i59 = ref itemArray[index + 59];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i17);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i53);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i45);
        Branchless.SwapIfGreaterThan(ref i23, ref i56);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i58);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i55);
        Branchless.SwapIfGreaterThan(ref i52, ref i54);
        Branchless.SwapIfGreaterThan(ref i57, ref i59);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i3, ref i20);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i16);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i52);
        Branchless.SwapIfGreaterThan(ref i22, ref i44);
        Branchless.SwapIfGreaterThan(ref i23, ref i51);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i57);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        Branchless.SwapIfGreaterThan(ref i55, ref i56);
        Branchless.SwapIfGreaterThan(ref i58, ref i59);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i3);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i8, ref i47);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i50);
        Branchless.SwapIfGreaterThan(ref i12, ref i19);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i54);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i27);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i31, ref i40);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i59);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i56);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i51, ref i55);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        Branchless.SwapIfGreaterThan(ref i57, ref i58);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i20);
        Branchless.SwapIfGreaterThan(ref i2, ref i21);
        Branchless.SwapIfGreaterThan(ref i3, ref i23);
        Branchless.SwapIfGreaterThan(ref i4, ref i31);
        Branchless.SwapIfGreaterThan(ref i5, ref i16);
        Branchless.SwapIfGreaterThan(ref i6, ref i17);
        Branchless.SwapIfGreaterThan(ref i7, ref i27);
        Branchless.SwapIfGreaterThan(ref i9, ref i48);
        Branchless.SwapIfGreaterThan(ref i10, ref i49);
        Branchless.SwapIfGreaterThan(ref i12, ref i24);
        Branchless.SwapIfGreaterThan(ref i13, ref i52);
        Branchless.SwapIfGreaterThan(ref i14, ref i53);
        Branchless.SwapIfGreaterThan(ref i15, ref i30);
        Branchless.SwapIfGreaterThan(ref i18, ref i43);
        Branchless.SwapIfGreaterThan(ref i19, ref i40);
        Branchless.SwapIfGreaterThan(ref i22, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i26, ref i29);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i46);
        Branchless.SwapIfGreaterThan(ref i36, ref i47);
        Branchless.SwapIfGreaterThan(ref i39, ref i50);
        Branchless.SwapIfGreaterThan(ref i41, ref i57);
        Branchless.SwapIfGreaterThan(ref i42, ref i58);
        Branchless.SwapIfGreaterThan(ref i44, ref i51);
        Branchless.SwapIfGreaterThan(ref i45, ref i55);
        Branchless.SwapIfGreaterThan(ref i54, ref i59);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i22);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i36);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i41);
        Branchless.SwapIfGreaterThan(ref i6, ref i42);
        Branchless.SwapIfGreaterThan(ref i7, ref i19);
        Branchless.SwapIfGreaterThan(ref i8, ref i32);
        Branchless.SwapIfGreaterThan(ref i11, ref i35);
        Branchless.SwapIfGreaterThan(ref i13, ref i25);
        Branchless.SwapIfGreaterThan(ref i14, ref i26);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i16, ref i28);
        Branchless.SwapIfGreaterThan(ref i17, ref i29);
        Branchless.SwapIfGreaterThan(ref i20, ref i51);
        Branchless.SwapIfGreaterThan(ref i21, ref i55);
        Branchless.SwapIfGreaterThan(ref i23, ref i47);
        Branchless.SwapIfGreaterThan(ref i24, ref i31);
        Branchless.SwapIfGreaterThan(ref i27, ref i40);
        Branchless.SwapIfGreaterThan(ref i30, ref i43);
        Branchless.SwapIfGreaterThan(ref i33, ref i44);
        Branchless.SwapIfGreaterThan(ref i34, ref i45);
        Branchless.SwapIfGreaterThan(ref i37, ref i48);
        Branchless.SwapIfGreaterThan(ref i38, ref i49);
        Branchless.SwapIfGreaterThan(ref i39, ref i56);
        Branchless.SwapIfGreaterThan(ref i46, ref i54);
        Branchless.SwapIfGreaterThan(ref i50, ref i59);
        Branchless.SwapIfGreaterThan(ref i52, ref i57);
        Branchless.SwapIfGreaterThan(ref i53, ref i58);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i33);
        Branchless.SwapIfGreaterThan(ref i2, ref i34);
        Branchless.SwapIfGreaterThan(ref i3, ref i24);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i44);
        Branchless.SwapIfGreaterThan(ref i10, ref i45);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i16, ref i52);
        Branchless.SwapIfGreaterThan(ref i17, ref i53);
        Branchless.SwapIfGreaterThan(ref i18, ref i35);
        Branchless.SwapIfGreaterThan(ref i19, ref i32);
        Branchless.SwapIfGreaterThan(ref i20, ref i37);
        Branchless.SwapIfGreaterThan(ref i21, ref i38);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i25, ref i41);
        Branchless.SwapIfGreaterThan(ref i26, ref i42);
        Branchless.SwapIfGreaterThan(ref i28, ref i57);
        Branchless.SwapIfGreaterThan(ref i29, ref i58);
        Branchless.SwapIfGreaterThan(ref i30, ref i39);
        Branchless.SwapIfGreaterThan(ref i31, ref i36);
        Branchless.SwapIfGreaterThan(ref i40, ref i47);
        Branchless.SwapIfGreaterThan(ref i43, ref i56);
        Branchless.SwapIfGreaterThan(ref i48, ref i51);
        Branchless.SwapIfGreaterThan(ref i49, ref i55);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i8, ref i22);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i10, ref i17);
        Branchless.SwapIfGreaterThan(ref i13, ref i33);
        Branchless.SwapIfGreaterThan(ref i14, ref i34);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i25);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i24, ref i31);
        Branchless.SwapIfGreaterThan(ref i27, ref i40);
        Branchless.SwapIfGreaterThan(ref i28, ref i48);
        Branchless.SwapIfGreaterThan(ref i29, ref i49);
        Branchless.SwapIfGreaterThan(ref i30, ref i46);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i54);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i52);
        Branchless.SwapIfGreaterThan(ref i45, ref i53);
        Branchless.SwapIfGreaterThan(ref i51, ref i57);
        Branchless.SwapIfGreaterThan(ref i55, ref i58);
        Branchless.SwapIfGreaterThan(ref i56, ref i59);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i8, ref i23);
        Branchless.SwapIfGreaterThan(ref i9, ref i20);
        Branchless.SwapIfGreaterThan(ref i10, ref i21);
        Branchless.SwapIfGreaterThan(ref i15, ref i30);
        Branchless.SwapIfGreaterThan(ref i16, ref i33);
        Branchless.SwapIfGreaterThan(ref i17, ref i34);
        Branchless.SwapIfGreaterThan(ref i18, ref i46);
        Branchless.SwapIfGreaterThan(ref i19, ref i31);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i37);
        Branchless.SwapIfGreaterThan(ref i26, ref i38);
        Branchless.SwapIfGreaterThan(ref i27, ref i32);
        Branchless.SwapIfGreaterThan(ref i28, ref i44);
        Branchless.SwapIfGreaterThan(ref i29, ref i45);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i54);
        Branchless.SwapIfGreaterThan(ref i41, ref i52);
        Branchless.SwapIfGreaterThan(ref i42, ref i53);
        Branchless.SwapIfGreaterThan(ref i43, ref i50);
        Branchless.SwapIfGreaterThan(ref i48, ref i51);
        Branchless.SwapIfGreaterThan(ref i49, ref i55);
        Branchless.SwapIfGreaterThan(ref i58, ref i59);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i14, ref i21);
        Branchless.SwapIfGreaterThan(ref i16, ref i44);
        Branchless.SwapIfGreaterThan(ref i17, ref i45);
        Branchless.SwapIfGreaterThan(ref i18, ref i30);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i24, ref i31);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i28, ref i37);
        Branchless.SwapIfGreaterThan(ref i29, ref i38);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i46);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i41, ref i48);
        Branchless.SwapIfGreaterThan(ref i42, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i54);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i55);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i8, ref i19);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i26, ref i29);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i44);
        Branchless.SwapIfGreaterThan(ref i42, ref i45);
        Branchless.SwapIfGreaterThan(ref i43, ref i46);
        Branchless.SwapIfGreaterThan(ref i48, ref i51);
        Branchless.SwapIfGreaterThan(ref i49, ref i53);
        Branchless.SwapIfGreaterThan(ref i50, ref i56);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i12, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i25);
        Branchless.SwapIfGreaterThan(ref i17, ref i26);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i27);
        Branchless.SwapIfGreaterThan(ref i30, ref i35);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        Branchless.SwapIfGreaterThan(ref i34, ref i42);
        Branchless.SwapIfGreaterThan(ref i37, ref i44);
        Branchless.SwapIfGreaterThan(ref i38, ref i45);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i46, ref i50);
        Branchless.SwapIfGreaterThan(ref i54, ref i56);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i2, ref i23);
        Branchless.SwapIfGreaterThan(ref i5, ref i12);
        Branchless.SwapIfGreaterThan(ref i6, ref i24);
        Branchless.SwapIfGreaterThan(ref i9, ref i19);
        Branchless.SwapIfGreaterThan(ref i10, ref i27);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i20, ref i25);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i28, ref i33);
        Branchless.SwapIfGreaterThan(ref i29, ref i34);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i51);
        Branchless.SwapIfGreaterThan(ref i43, ref i52);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i45, ref i49);
        Branchless.SwapIfGreaterThan(ref i46, ref i57);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        Branchless.SwapIfGreaterThan(ref i55, ref i56);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i10, ref i19);
        Branchless.SwapIfGreaterThan(ref i11, ref i33);
        Branchless.SwapIfGreaterThan(ref i13, ref i22);
        Branchless.SwapIfGreaterThan(ref i14, ref i31);
        Branchless.SwapIfGreaterThan(ref i15, ref i37);
        Branchless.SwapIfGreaterThan(ref i17, ref i32);
        Branchless.SwapIfGreaterThan(ref i18, ref i41);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i36);
        Branchless.SwapIfGreaterThan(ref i26, ref i40);
        Branchless.SwapIfGreaterThan(ref i29, ref i47);
        Branchless.SwapIfGreaterThan(ref i30, ref i44);
        Branchless.SwapIfGreaterThan(ref i35, ref i48);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i53);
        Branchless.SwapIfGreaterThan(ref i52, ref i55);
        Branchless.SwapIfGreaterThan(ref i57, ref i58);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i21);
        Branchless.SwapIfGreaterThan(ref i17, ref i23);
        Branchless.SwapIfGreaterThan(ref i18, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i15, ref i20);
        Branchless.SwapIfGreaterThan(ref i16, ref i26);
        Branchless.SwapIfGreaterThan(ref i18, ref i23);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i27, ref i32);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i37);
        Branchless.SwapIfGreaterThan(ref i31, ref i36);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i38, ref i44);
        Branchless.SwapIfGreaterThan(ref i39, ref i45);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i24, ref i29);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i40);
        Branchless.SwapIfGreaterThan(ref i35, ref i41);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i42, ref i48);
        Branchless.SwapIfGreaterThan(ref i43, ref i52);
        Branchless.SwapIfGreaterThan(ref i44, ref i51);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i57);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i24, ref i30);
        Branchless.SwapIfGreaterThan(ref i26, ref i32);
        Branchless.SwapIfGreaterThan(ref i27, ref i34);
        Branchless.SwapIfGreaterThan(ref i29, ref i36);
        Branchless.SwapIfGreaterThan(ref i31, ref i38);
        Branchless.SwapIfGreaterThan(ref i33, ref i40);
        Branchless.SwapIfGreaterThan(ref i35, ref i42);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i48);
        Branchless.SwapIfGreaterThan(ref i43, ref i49);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i51);
        Branchless.SwapIfGreaterThan(ref i47, ref i53);
        Branchless.SwapIfGreaterThan(ref i50, ref i52);
        Branchless.SwapIfGreaterThan(ref i54, ref i57);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i51);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i55);
        Branchless.SwapIfGreaterThan(ref i57, ref i58);
        
        // Layer 19
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i54);
        Branchless.SwapIfGreaterThan(ref i53, ref i55);
        Branchless.SwapIfGreaterThan(ref i56, ref i57);
        
        // Layer 20
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        Branchless.SwapIfGreaterThan(ref i55, ref i56);
        Branchless.SwapIfGreaterThan(ref i57, ref i58);
    }
    
    /// <summary>
    /// Sort 61 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort61Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        ref var i48 = ref itemArray[index + 48];
        ref var i49 = ref itemArray[index + 49];
        ref var i50 = ref itemArray[index + 50];
        ref var i51 = ref itemArray[index + 51];
        ref var i52 = ref itemArray[index + 52];
        ref var i53 = ref itemArray[index + 53];
        ref var i54 = ref itemArray[index + 54];
        ref var i55 = ref itemArray[index + 55];
        ref var i56 = ref itemArray[index + 56];
        ref var i57 = ref itemArray[index + 57];
        ref var i58 = ref itemArray[index + 58];
        ref var i59 = ref itemArray[index + 59];
        ref var i60 = ref itemArray[index + 60];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i21);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i17);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i49);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i29);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i57);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i55);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i53);
        Branchless.SwapIfGreaterThan(ref i52, ref i54);
        Branchless.SwapIfGreaterThan(ref i56, ref i58);
        Branchless.SwapIfGreaterThan(ref i59, ref i60);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i3, ref i20);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i7, ref i16);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i11, ref i48);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i56);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i59);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        Branchless.SwapIfGreaterThan(ref i55, ref i60);
        Branchless.SwapIfGreaterThan(ref i57, ref i58);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i3);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i12, ref i51);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i54);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i19, ref i24);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i50);
        Branchless.SwapIfGreaterThan(ref i23, ref i44);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i31, ref i40);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i60);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i58);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        Branchless.SwapIfGreaterThan(ref i55, ref i59);
        Branchless.SwapIfGreaterThan(ref i56, ref i57);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i20);
        Branchless.SwapIfGreaterThan(ref i2, ref i21);
        Branchless.SwapIfGreaterThan(ref i3, ref i44);
        Branchless.SwapIfGreaterThan(ref i4, ref i31);
        Branchless.SwapIfGreaterThan(ref i5, ref i16);
        Branchless.SwapIfGreaterThan(ref i6, ref i17);
        Branchless.SwapIfGreaterThan(ref i7, ref i24);
        Branchless.SwapIfGreaterThan(ref i9, ref i48);
        Branchless.SwapIfGreaterThan(ref i10, ref i49);
        Branchless.SwapIfGreaterThan(ref i11, ref i36);
        Branchless.SwapIfGreaterThan(ref i12, ref i19);
        Branchless.SwapIfGreaterThan(ref i13, ref i52);
        Branchless.SwapIfGreaterThan(ref i14, ref i53);
        Branchless.SwapIfGreaterThan(ref i15, ref i27);
        Branchless.SwapIfGreaterThan(ref i18, ref i43);
        Branchless.SwapIfGreaterThan(ref i23, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i26, ref i29);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i47);
        Branchless.SwapIfGreaterThan(ref i39, ref i58);
        Branchless.SwapIfGreaterThan(ref i40, ref i51);
        Branchless.SwapIfGreaterThan(ref i41, ref i55);
        Branchless.SwapIfGreaterThan(ref i42, ref i59);
        Branchless.SwapIfGreaterThan(ref i45, ref i56);
        Branchless.SwapIfGreaterThan(ref i46, ref i57);
        Branchless.SwapIfGreaterThan(ref i54, ref i60);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i23);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i41);
        Branchless.SwapIfGreaterThan(ref i6, ref i42);
        Branchless.SwapIfGreaterThan(ref i7, ref i40);
        Branchless.SwapIfGreaterThan(ref i8, ref i32);
        Branchless.SwapIfGreaterThan(ref i13, ref i25);
        Branchless.SwapIfGreaterThan(ref i14, ref i26);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i16, ref i28);
        Branchless.SwapIfGreaterThan(ref i17, ref i29);
        Branchless.SwapIfGreaterThan(ref i19, ref i31);
        Branchless.SwapIfGreaterThan(ref i20, ref i56);
        Branchless.SwapIfGreaterThan(ref i21, ref i57);
        Branchless.SwapIfGreaterThan(ref i22, ref i35);
        Branchless.SwapIfGreaterThan(ref i24, ref i51);
        Branchless.SwapIfGreaterThan(ref i27, ref i43);
        Branchless.SwapIfGreaterThan(ref i30, ref i54);
        Branchless.SwapIfGreaterThan(ref i33, ref i45);
        Branchless.SwapIfGreaterThan(ref i34, ref i46);
        Branchless.SwapIfGreaterThan(ref i36, ref i44);
        Branchless.SwapIfGreaterThan(ref i37, ref i48);
        Branchless.SwapIfGreaterThan(ref i38, ref i49);
        Branchless.SwapIfGreaterThan(ref i47, ref i50);
        Branchless.SwapIfGreaterThan(ref i52, ref i55);
        Branchless.SwapIfGreaterThan(ref i53, ref i59);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i33);
        Branchless.SwapIfGreaterThan(ref i2, ref i34);
        Branchless.SwapIfGreaterThan(ref i3, ref i19);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i45);
        Branchless.SwapIfGreaterThan(ref i10, ref i46);
        Branchless.SwapIfGreaterThan(ref i11, ref i31);
        Branchless.SwapIfGreaterThan(ref i12, ref i23);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i16, ref i52);
        Branchless.SwapIfGreaterThan(ref i17, ref i53);
        Branchless.SwapIfGreaterThan(ref i18, ref i35);
        Branchless.SwapIfGreaterThan(ref i20, ref i37);
        Branchless.SwapIfGreaterThan(ref i21, ref i38);
        Branchless.SwapIfGreaterThan(ref i24, ref i36);
        Branchless.SwapIfGreaterThan(ref i25, ref i41);
        Branchless.SwapIfGreaterThan(ref i26, ref i42);
        Branchless.SwapIfGreaterThan(ref i27, ref i39);
        Branchless.SwapIfGreaterThan(ref i28, ref i55);
        Branchless.SwapIfGreaterThan(ref i29, ref i59);
        Branchless.SwapIfGreaterThan(ref i30, ref i47);
        Branchless.SwapIfGreaterThan(ref i32, ref i40);
        Branchless.SwapIfGreaterThan(ref i43, ref i58);
        Branchless.SwapIfGreaterThan(ref i44, ref i51);
        Branchless.SwapIfGreaterThan(ref i48, ref i56);
        Branchless.SwapIfGreaterThan(ref i49, ref i57);
        Branchless.SwapIfGreaterThan(ref i50, ref i54);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i8, ref i23);
        Branchless.SwapIfGreaterThan(ref i9, ref i16);
        Branchless.SwapIfGreaterThan(ref i10, ref i17);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i13, ref i33);
        Branchless.SwapIfGreaterThan(ref i14, ref i34);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i20, ref i25);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i28, ref i48);
        Branchless.SwapIfGreaterThan(ref i29, ref i49);
        Branchless.SwapIfGreaterThan(ref i31, ref i40);
        Branchless.SwapIfGreaterThan(ref i35, ref i47);
        Branchless.SwapIfGreaterThan(ref i36, ref i44);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i45, ref i52);
        Branchless.SwapIfGreaterThan(ref i46, ref i53);
        Branchless.SwapIfGreaterThan(ref i50, ref i60);
        Branchless.SwapIfGreaterThan(ref i54, ref i58);
        Branchless.SwapIfGreaterThan(ref i55, ref i56);
        Branchless.SwapIfGreaterThan(ref i57, ref i59);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i8, ref i32);
        Branchless.SwapIfGreaterThan(ref i9, ref i20);
        Branchless.SwapIfGreaterThan(ref i10, ref i21);
        Branchless.SwapIfGreaterThan(ref i11, ref i23);
        Branchless.SwapIfGreaterThan(ref i16, ref i33);
        Branchless.SwapIfGreaterThan(ref i17, ref i34);
        Branchless.SwapIfGreaterThan(ref i18, ref i27);
        Branchless.SwapIfGreaterThan(ref i19, ref i24);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i25, ref i37);
        Branchless.SwapIfGreaterThan(ref i26, ref i38);
        Branchless.SwapIfGreaterThan(ref i28, ref i45);
        Branchless.SwapIfGreaterThan(ref i29, ref i46);
        Branchless.SwapIfGreaterThan(ref i31, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i60);
        Branchless.SwapIfGreaterThan(ref i39, ref i47);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i41, ref i52);
        Branchless.SwapIfGreaterThan(ref i42, ref i53);
        Branchless.SwapIfGreaterThan(ref i43, ref i50);
        Branchless.SwapIfGreaterThan(ref i48, ref i55);
        Branchless.SwapIfGreaterThan(ref i49, ref i57);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i14, ref i21);
        Branchless.SwapIfGreaterThan(ref i16, ref i45);
        Branchless.SwapIfGreaterThan(ref i17, ref i46);
        Branchless.SwapIfGreaterThan(ref i22, ref i27);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i28, ref i37);
        Branchless.SwapIfGreaterThan(ref i29, ref i38);
        Branchless.SwapIfGreaterThan(ref i30, ref i35);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i41, ref i48);
        Branchless.SwapIfGreaterThan(ref i42, ref i49);
        Branchless.SwapIfGreaterThan(ref i47, ref i50);
        Branchless.SwapIfGreaterThan(ref i52, ref i55);
        Branchless.SwapIfGreaterThan(ref i53, ref i57);
        Branchless.SwapIfGreaterThan(ref i54, ref i60);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i8, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i19);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i26, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i39);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i43);
        Branchless.SwapIfGreaterThan(ref i41, ref i45);
        Branchless.SwapIfGreaterThan(ref i42, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i54);
        Branchless.SwapIfGreaterThan(ref i48, ref i52);
        Branchless.SwapIfGreaterThan(ref i49, ref i53);
        Branchless.SwapIfGreaterThan(ref i50, ref i60);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i16, ref i25);
        Branchless.SwapIfGreaterThan(ref i17, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i24, ref i31);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        Branchless.SwapIfGreaterThan(ref i34, ref i42);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i45);
        Branchless.SwapIfGreaterThan(ref i38, ref i46);
        Branchless.SwapIfGreaterThan(ref i43, ref i47);
        Branchless.SwapIfGreaterThan(ref i50, ref i54);
        Branchless.SwapIfGreaterThan(ref i58, ref i60);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i1, ref i8);
        Branchless.SwapIfGreaterThan(ref i2, ref i23);
        Branchless.SwapIfGreaterThan(ref i5, ref i11);
        Branchless.SwapIfGreaterThan(ref i6, ref i24);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i31);
        Branchless.SwapIfGreaterThan(ref i13, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i17);
        Branchless.SwapIfGreaterThan(ref i20, ref i25);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i28, ref i33);
        Branchless.SwapIfGreaterThan(ref i29, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i52);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i55);
        Branchless.SwapIfGreaterThan(ref i43, ref i56);
        Branchless.SwapIfGreaterThan(ref i45, ref i48);
        Branchless.SwapIfGreaterThan(ref i46, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i57);
        Branchless.SwapIfGreaterThan(ref i58, ref i59);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i19);
        Branchless.SwapIfGreaterThan(ref i14, ref i32);
        Branchless.SwapIfGreaterThan(ref i15, ref i33);
        Branchless.SwapIfGreaterThan(ref i17, ref i36);
        Branchless.SwapIfGreaterThan(ref i18, ref i37);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i40);
        Branchless.SwapIfGreaterThan(ref i22, ref i41);
        Branchless.SwapIfGreaterThan(ref i26, ref i44);
        Branchless.SwapIfGreaterThan(ref i27, ref i45);
        Branchless.SwapIfGreaterThan(ref i29, ref i51);
        Branchless.SwapIfGreaterThan(ref i30, ref i48);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i52);
        Branchless.SwapIfGreaterThan(ref i55, ref i60);
        Branchless.SwapIfGreaterThan(ref i56, ref i58);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i14, ref i19);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i23);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i31);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i30, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i51);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i54, ref i55);
        Branchless.SwapIfGreaterThan(ref i57, ref i60);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i16, ref i26);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i25, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i28, ref i35);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i32, ref i38);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i46);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i40, ref i43);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i51);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i8, ref i15);
        Branchless.SwapIfGreaterThan(ref i11, ref i18);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i27);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i24, ref i29);
        Branchless.SwapIfGreaterThan(ref i25, ref i31);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i39);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i36, ref i42);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i47);
        Branchless.SwapIfGreaterThan(ref i43, ref i50);
        Branchless.SwapIfGreaterThan(ref i44, ref i54);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i51, ref i56);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i15);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i27);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i35);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i32, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i39);
        Branchless.SwapIfGreaterThan(ref i36, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i42, ref i47);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i46, ref i50);
        Branchless.SwapIfGreaterThan(ref i49, ref i54);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i56);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i16, ref i19);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i20, ref i23);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i54);
        Branchless.SwapIfGreaterThan(ref i53, ref i55);
        Branchless.SwapIfGreaterThan(ref i56, ref i58);
        
        // Layer 19
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        Branchless.SwapIfGreaterThan(ref i49, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i55);
        Branchless.SwapIfGreaterThan(ref i56, ref i57);
        Branchless.SwapIfGreaterThan(ref i58, ref i60);
        
        // Layer 20
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        Branchless.SwapIfGreaterThan(ref i55, ref i56);
        Branchless.SwapIfGreaterThan(ref i57, ref i58);
        Branchless.SwapIfGreaterThan(ref i59, ref i60);
    }
    
    /// <summary>
    /// Sort 62 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort62Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        ref var i48 = ref itemArray[index + 48];
        ref var i49 = ref itemArray[index + 49];
        ref var i50 = ref itemArray[index + 50];
        ref var i51 = ref itemArray[index + 51];
        ref var i52 = ref itemArray[index + 52];
        ref var i53 = ref itemArray[index + 53];
        ref var i54 = ref itemArray[index + 54];
        ref var i55 = ref itemArray[index + 55];
        ref var i56 = ref itemArray[index + 56];
        ref var i57 = ref itemArray[index + 57];
        ref var i58 = ref itemArray[index + 58];
        ref var i59 = ref itemArray[index + 59];
        ref var i60 = ref itemArray[index + 60];
        ref var i61 = ref itemArray[index + 61];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i20);
        Branchless.SwapIfGreaterThan(ref i3, ref i21);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i22, ref i56);
        Branchless.SwapIfGreaterThan(ref i23, ref i57);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        Branchless.SwapIfGreaterThan(ref i49, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i54);
        Branchless.SwapIfGreaterThan(ref i53, ref i55);
        Branchless.SwapIfGreaterThan(ref i58, ref i60);
        Branchless.SwapIfGreaterThan(ref i59, ref i61);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i55);
        Branchless.SwapIfGreaterThan(ref i56, ref i57);
        Branchless.SwapIfGreaterThan(ref i58, ref i59);
        Branchless.SwapIfGreaterThan(ref i60, ref i61);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i20);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i19);
        Branchless.SwapIfGreaterThan(ref i8, ref i48);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i51);
        Branchless.SwapIfGreaterThan(ref i12, ref i52);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i55);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i22, ref i44);
        Branchless.SwapIfGreaterThan(ref i23, ref i56);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i40, ref i58);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i61);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i57);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        Branchless.SwapIfGreaterThan(ref i59, ref i60);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i44);
        Branchless.SwapIfGreaterThan(ref i4, ref i40);
        Branchless.SwapIfGreaterThan(ref i5, ref i17);
        Branchless.SwapIfGreaterThan(ref i6, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i43);
        Branchless.SwapIfGreaterThan(ref i9, ref i49);
        Branchless.SwapIfGreaterThan(ref i10, ref i50);
        Branchless.SwapIfGreaterThan(ref i11, ref i21);
        Branchless.SwapIfGreaterThan(ref i12, ref i24);
        Branchless.SwapIfGreaterThan(ref i13, ref i53);
        Branchless.SwapIfGreaterThan(ref i14, ref i54);
        Branchless.SwapIfGreaterThan(ref i15, ref i27);
        Branchless.SwapIfGreaterThan(ref i16, ref i28);
        Branchless.SwapIfGreaterThan(ref i19, ref i31);
        Branchless.SwapIfGreaterThan(ref i22, ref i32);
        Branchless.SwapIfGreaterThan(ref i23, ref i45);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i47);
        Branchless.SwapIfGreaterThan(ref i36, ref i48);
        Branchless.SwapIfGreaterThan(ref i39, ref i51);
        Branchless.SwapIfGreaterThan(ref i41, ref i59);
        Branchless.SwapIfGreaterThan(ref i42, ref i60);
        Branchless.SwapIfGreaterThan(ref i46, ref i56);
        Branchless.SwapIfGreaterThan(ref i52, ref i58);
        Branchless.SwapIfGreaterThan(ref i55, ref i61);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i22);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i36);
        Branchless.SwapIfGreaterThan(ref i3, ref i45);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i41);
        Branchless.SwapIfGreaterThan(ref i6, ref i42);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i8, ref i32);
        Branchless.SwapIfGreaterThan(ref i10, ref i20);
        Branchless.SwapIfGreaterThan(ref i11, ref i35);
        Branchless.SwapIfGreaterThan(ref i13, ref i25);
        Branchless.SwapIfGreaterThan(ref i14, ref i26);
        Branchless.SwapIfGreaterThan(ref i16, ref i52);
        Branchless.SwapIfGreaterThan(ref i17, ref i29);
        Branchless.SwapIfGreaterThan(ref i18, ref i30);
        Branchless.SwapIfGreaterThan(ref i19, ref i55);
        Branchless.SwapIfGreaterThan(ref i21, ref i47);
        Branchless.SwapIfGreaterThan(ref i23, ref i33);
        Branchless.SwapIfGreaterThan(ref i24, ref i40);
        Branchless.SwapIfGreaterThan(ref i27, ref i43);
        Branchless.SwapIfGreaterThan(ref i28, ref i58);
        Branchless.SwapIfGreaterThan(ref i31, ref i61);
        Branchless.SwapIfGreaterThan(ref i34, ref i46);
        Branchless.SwapIfGreaterThan(ref i37, ref i49);
        Branchless.SwapIfGreaterThan(ref i38, ref i50);
        Branchless.SwapIfGreaterThan(ref i39, ref i57);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i53, ref i59);
        Branchless.SwapIfGreaterThan(ref i54, ref i60);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i23);
        Branchless.SwapIfGreaterThan(ref i2, ref i24);
        Branchless.SwapIfGreaterThan(ref i3, ref i37);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i33);
        Branchless.SwapIfGreaterThan(ref i10, ref i34);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i35);
        Branchless.SwapIfGreaterThan(ref i17, ref i53);
        Branchless.SwapIfGreaterThan(ref i18, ref i54);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i46);
        Branchless.SwapIfGreaterThan(ref i25, ref i41);
        Branchless.SwapIfGreaterThan(ref i26, ref i42);
        Branchless.SwapIfGreaterThan(ref i27, ref i39);
        Branchless.SwapIfGreaterThan(ref i28, ref i44);
        Branchless.SwapIfGreaterThan(ref i29, ref i59);
        Branchless.SwapIfGreaterThan(ref i30, ref i60);
        Branchless.SwapIfGreaterThan(ref i31, ref i51);
        Branchless.SwapIfGreaterThan(ref i32, ref i52);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i38, ref i56);
        Branchless.SwapIfGreaterThan(ref i43, ref i57);
        Branchless.SwapIfGreaterThan(ref i45, ref i49);
        Branchless.SwapIfGreaterThan(ref i47, ref i55);
        Branchless.SwapIfGreaterThan(ref i48, ref i58);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i25);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i23);
        Branchless.SwapIfGreaterThan(ref i14, ref i34);
        Branchless.SwapIfGreaterThan(ref i16, ref i22);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i21, ref i35);
        Branchless.SwapIfGreaterThan(ref i24, ref i36);
        Branchless.SwapIfGreaterThan(ref i26, ref i38);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i45);
        Branchless.SwapIfGreaterThan(ref i30, ref i50);
        Branchless.SwapIfGreaterThan(ref i31, ref i47);
        Branchless.SwapIfGreaterThan(ref i33, ref i53);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i40, ref i52);
        Branchless.SwapIfGreaterThan(ref i42, ref i56);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i46, ref i54);
        Branchless.SwapIfGreaterThan(ref i49, ref i59);
        Branchless.SwapIfGreaterThan(ref i51, ref i61);
        Branchless.SwapIfGreaterThan(ref i55, ref i57);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i15, ref i27);
        Branchless.SwapIfGreaterThan(ref i16, ref i32);
        Branchless.SwapIfGreaterThan(ref i17, ref i23);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i20, ref i34);
        Branchless.SwapIfGreaterThan(ref i21, ref i47);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i37);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i46);
        Branchless.SwapIfGreaterThan(ref i31, ref i43);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i41, ref i53);
        Branchless.SwapIfGreaterThan(ref i45, ref i49);
        Branchless.SwapIfGreaterThan(ref i48, ref i52);
        Branchless.SwapIfGreaterThan(ref i50, ref i60);
        Branchless.SwapIfGreaterThan(ref i51, ref i55);
        Branchless.SwapIfGreaterThan(ref i54, ref i56);
        Branchless.SwapIfGreaterThan(ref i57, ref i61);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i14, ref i26);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i17, ref i33);
        Branchless.SwapIfGreaterThan(ref i20, ref i46);
        Branchless.SwapIfGreaterThan(ref i21, ref i27);
        Branchless.SwapIfGreaterThan(ref i22, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i24, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i42);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i32, ref i40);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i41, ref i45);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i51);
        Branchless.SwapIfGreaterThan(ref i49, ref i53);
        Branchless.SwapIfGreaterThan(ref i50, ref i54);
        Branchless.SwapIfGreaterThan(ref i55, ref i57);
        Branchless.SwapIfGreaterThan(ref i56, ref i60);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i12, ref i22);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i16, ref i28);
        Branchless.SwapIfGreaterThan(ref i20, ref i26);
        Branchless.SwapIfGreaterThan(ref i21, ref i31);
        Branchless.SwapIfGreaterThan(ref i23, ref i29);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i37);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i47);
        Branchless.SwapIfGreaterThan(ref i43, ref i51);
        Branchless.SwapIfGreaterThan(ref i45, ref i49);
        Branchless.SwapIfGreaterThan(ref i46, ref i50);
        Branchless.SwapIfGreaterThan(ref i54, ref i56);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i23);
        Branchless.SwapIfGreaterThan(ref i16, ref i22);
        Branchless.SwapIfGreaterThan(ref i17, ref i29);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i30);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i46);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i42, ref i50);
        Branchless.SwapIfGreaterThan(ref i43, ref i47);
        Branchless.SwapIfGreaterThan(ref i51, ref i55);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i1, ref i12);
        Branchless.SwapIfGreaterThan(ref i3, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i22);
        Branchless.SwapIfGreaterThan(ref i6, ref i28);
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i32);
        Branchless.SwapIfGreaterThan(ref i14, ref i36);
        Branchless.SwapIfGreaterThan(ref i17, ref i23);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i49);
        Branchless.SwapIfGreaterThan(ref i31, ref i53);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i59);
        Branchless.SwapIfGreaterThan(ref i41, ref i45);
        Branchless.SwapIfGreaterThan(ref i42, ref i46);
        Branchless.SwapIfGreaterThan(ref i43, ref i56);
        Branchless.SwapIfGreaterThan(ref i47, ref i60);
        Branchless.SwapIfGreaterThan(ref i50, ref i54);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i29);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i24);
        Branchless.SwapIfGreaterThan(ref i11, ref i33);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i15, ref i37);
        Branchless.SwapIfGreaterThan(ref i17, ref i32);
        Branchless.SwapIfGreaterThan(ref i18, ref i40);
        Branchless.SwapIfGreaterThan(ref i19, ref i41);
        Branchless.SwapIfGreaterThan(ref i20, ref i44);
        Branchless.SwapIfGreaterThan(ref i21, ref i45);
        Branchless.SwapIfGreaterThan(ref i26, ref i48);
        Branchless.SwapIfGreaterThan(ref i30, ref i52);
        Branchless.SwapIfGreaterThan(ref i31, ref i46);
        Branchless.SwapIfGreaterThan(ref i34, ref i58);
        Branchless.SwapIfGreaterThan(ref i39, ref i54);
        Branchless.SwapIfGreaterThan(ref i43, ref i49);
        Branchless.SwapIfGreaterThan(ref i53, ref i55);
        Branchless.SwapIfGreaterThan(ref i57, ref i59);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i26);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i27, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i36);
        Branchless.SwapIfGreaterThan(ref i34, ref i40);
        Branchless.SwapIfGreaterThan(ref i35, ref i41);
        Branchless.SwapIfGreaterThan(ref i37, ref i52);
        Branchless.SwapIfGreaterThan(ref i38, ref i44);
        Branchless.SwapIfGreaterThan(ref i39, ref i45);
        Branchless.SwapIfGreaterThan(ref i42, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i53);
        Branchless.SwapIfGreaterThan(ref i50, ref i58);
        Branchless.SwapIfGreaterThan(ref i51, ref i57);
        Branchless.SwapIfGreaterThan(ref i55, ref i60);
        Branchless.SwapIfGreaterThan(ref i59, ref i61);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i7, ref i20);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i13, ref i30);
        Branchless.SwapIfGreaterThan(ref i15, ref i28);
        Branchless.SwapIfGreaterThan(ref i19, ref i34);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i26, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i44);
        Branchless.SwapIfGreaterThan(ref i31, ref i37);
        Branchless.SwapIfGreaterThan(ref i33, ref i50);
        Branchless.SwapIfGreaterThan(ref i35, ref i48);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i58);
        Branchless.SwapIfGreaterThan(ref i46, ref i52);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i21, ref i36);
        Branchless.SwapIfGreaterThan(ref i23, ref i38);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i40);
        Branchless.SwapIfGreaterThan(ref i27, ref i42);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i41, ref i50);
        Branchless.SwapIfGreaterThan(ref i43, ref i48);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i52);
        Branchless.SwapIfGreaterThan(ref i51, ref i58);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i26);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i30);
        Branchless.SwapIfGreaterThan(ref i25, ref i32);
        Branchless.SwapIfGreaterThan(ref i27, ref i34);
        Branchless.SwapIfGreaterThan(ref i29, ref i36);
        Branchless.SwapIfGreaterThan(ref i31, ref i38);
        Branchless.SwapIfGreaterThan(ref i33, ref i40);
        Branchless.SwapIfGreaterThan(ref i35, ref i42);
        Branchless.SwapIfGreaterThan(ref i37, ref i44);
        Branchless.SwapIfGreaterThan(ref i39, ref i46);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        Branchless.SwapIfGreaterThan(ref i49, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i54);
        Branchless.SwapIfGreaterThan(ref i56, ref i58);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i52);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i54);
        Branchless.SwapIfGreaterThan(ref i53, ref i56);
        Branchless.SwapIfGreaterThan(ref i57, ref i58);
        
        // Layer 19
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i52);
        Branchless.SwapIfGreaterThan(ref i51, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i56);
        Branchless.SwapIfGreaterThan(ref i55, ref i57);
        Branchless.SwapIfGreaterThan(ref i58, ref i60);
        
        // Layer 20
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        Branchless.SwapIfGreaterThan(ref i55, ref i56);
        Branchless.SwapIfGreaterThan(ref i57, ref i58);
        Branchless.SwapIfGreaterThan(ref i59, ref i60);
    }
    
    /// <summary>
    /// Sort 63 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort63Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        ref var i48 = ref itemArray[index + 48];
        ref var i49 = ref itemArray[index + 49];
        ref var i50 = ref itemArray[index + 50];
        ref var i51 = ref itemArray[index + 51];
        ref var i52 = ref itemArray[index + 52];
        ref var i53 = ref itemArray[index + 53];
        ref var i54 = ref itemArray[index + 54];
        ref var i55 = ref itemArray[index + 55];
        ref var i56 = ref itemArray[index + 56];
        ref var i57 = ref itemArray[index + 57];
        ref var i58 = ref itemArray[index + 58];
        ref var i59 = ref itemArray[index + 59];
        ref var i60 = ref itemArray[index + 60];
        ref var i61 = ref itemArray[index + 61];
        ref var i62 = ref itemArray[index + 62];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i21);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i57);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        Branchless.SwapIfGreaterThan(ref i49, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i54);
        Branchless.SwapIfGreaterThan(ref i53, ref i55);
        Branchless.SwapIfGreaterThan(ref i56, ref i58);
        Branchless.SwapIfGreaterThan(ref i59, ref i61);
        Branchless.SwapIfGreaterThan(ref i60, ref i62);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i3, ref i20);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i56);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i55);
        Branchless.SwapIfGreaterThan(ref i57, ref i58);
        Branchless.SwapIfGreaterThan(ref i59, ref i60);
        Branchless.SwapIfGreaterThan(ref i61, ref i62);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i3);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i19);
        Branchless.SwapIfGreaterThan(ref i8, ref i48);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i51);
        Branchless.SwapIfGreaterThan(ref i12, ref i52);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i55);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i23, ref i44);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i40, ref i59);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i62);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i58);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        Branchless.SwapIfGreaterThan(ref i56, ref i57);
        Branchless.SwapIfGreaterThan(ref i60, ref i61);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i20);
        Branchless.SwapIfGreaterThan(ref i2, ref i21);
        Branchless.SwapIfGreaterThan(ref i3, ref i44);
        Branchless.SwapIfGreaterThan(ref i4, ref i40);
        Branchless.SwapIfGreaterThan(ref i5, ref i17);
        Branchless.SwapIfGreaterThan(ref i6, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i43);
        Branchless.SwapIfGreaterThan(ref i9, ref i49);
        Branchless.SwapIfGreaterThan(ref i10, ref i50);
        Branchless.SwapIfGreaterThan(ref i11, ref i22);
        Branchless.SwapIfGreaterThan(ref i12, ref i24);
        Branchless.SwapIfGreaterThan(ref i13, ref i53);
        Branchless.SwapIfGreaterThan(ref i14, ref i54);
        Branchless.SwapIfGreaterThan(ref i15, ref i27);
        Branchless.SwapIfGreaterThan(ref i16, ref i28);
        Branchless.SwapIfGreaterThan(ref i19, ref i31);
        Branchless.SwapIfGreaterThan(ref i23, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i47);
        Branchless.SwapIfGreaterThan(ref i36, ref i48);
        Branchless.SwapIfGreaterThan(ref i39, ref i51);
        Branchless.SwapIfGreaterThan(ref i41, ref i60);
        Branchless.SwapIfGreaterThan(ref i42, ref i61);
        Branchless.SwapIfGreaterThan(ref i45, ref i56);
        Branchless.SwapIfGreaterThan(ref i46, ref i57);
        Branchless.SwapIfGreaterThan(ref i52, ref i59);
        Branchless.SwapIfGreaterThan(ref i55, ref i62);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i23);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i36);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i41);
        Branchless.SwapIfGreaterThan(ref i6, ref i42);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i8, ref i32);
        Branchless.SwapIfGreaterThan(ref i11, ref i35);
        Branchless.SwapIfGreaterThan(ref i13, ref i25);
        Branchless.SwapIfGreaterThan(ref i14, ref i26);
        Branchless.SwapIfGreaterThan(ref i16, ref i52);
        Branchless.SwapIfGreaterThan(ref i17, ref i29);
        Branchless.SwapIfGreaterThan(ref i18, ref i30);
        Branchless.SwapIfGreaterThan(ref i19, ref i55);
        Branchless.SwapIfGreaterThan(ref i20, ref i56);
        Branchless.SwapIfGreaterThan(ref i21, ref i57);
        Branchless.SwapIfGreaterThan(ref i22, ref i47);
        Branchless.SwapIfGreaterThan(ref i24, ref i40);
        Branchless.SwapIfGreaterThan(ref i27, ref i43);
        Branchless.SwapIfGreaterThan(ref i28, ref i59);
        Branchless.SwapIfGreaterThan(ref i31, ref i62);
        Branchless.SwapIfGreaterThan(ref i33, ref i45);
        Branchless.SwapIfGreaterThan(ref i34, ref i46);
        Branchless.SwapIfGreaterThan(ref i37, ref i49);
        Branchless.SwapIfGreaterThan(ref i38, ref i50);
        Branchless.SwapIfGreaterThan(ref i39, ref i58);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i53, ref i60);
        Branchless.SwapIfGreaterThan(ref i54, ref i61);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i33);
        Branchless.SwapIfGreaterThan(ref i2, ref i34);
        Branchless.SwapIfGreaterThan(ref i3, ref i24);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i45);
        Branchless.SwapIfGreaterThan(ref i10, ref i46);
        Branchless.SwapIfGreaterThan(ref i12, ref i23);
        Branchless.SwapIfGreaterThan(ref i15, ref i35);
        Branchless.SwapIfGreaterThan(ref i17, ref i53);
        Branchless.SwapIfGreaterThan(ref i18, ref i54);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i20, ref i37);
        Branchless.SwapIfGreaterThan(ref i21, ref i38);
        Branchless.SwapIfGreaterThan(ref i25, ref i41);
        Branchless.SwapIfGreaterThan(ref i26, ref i42);
        Branchless.SwapIfGreaterThan(ref i27, ref i39);
        Branchless.SwapIfGreaterThan(ref i28, ref i44);
        Branchless.SwapIfGreaterThan(ref i29, ref i60);
        Branchless.SwapIfGreaterThan(ref i30, ref i61);
        Branchless.SwapIfGreaterThan(ref i31, ref i51);
        Branchless.SwapIfGreaterThan(ref i32, ref i52);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i43, ref i58);
        Branchless.SwapIfGreaterThan(ref i47, ref i55);
        Branchless.SwapIfGreaterThan(ref i48, ref i59);
        Branchless.SwapIfGreaterThan(ref i49, ref i56);
        Branchless.SwapIfGreaterThan(ref i50, ref i57);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i33);
        Branchless.SwapIfGreaterThan(ref i14, ref i34);
        Branchless.SwapIfGreaterThan(ref i16, ref i23);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i25);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i22, ref i35);
        Branchless.SwapIfGreaterThan(ref i24, ref i36);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i29, ref i49);
        Branchless.SwapIfGreaterThan(ref i30, ref i50);
        Branchless.SwapIfGreaterThan(ref i31, ref i47);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i40, ref i52);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i45, ref i53);
        Branchless.SwapIfGreaterThan(ref i46, ref i54);
        Branchless.SwapIfGreaterThan(ref i51, ref i62);
        Branchless.SwapIfGreaterThan(ref i55, ref i58);
        Branchless.SwapIfGreaterThan(ref i56, ref i60);
        Branchless.SwapIfGreaterThan(ref i57, ref i61);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i9, ref i20);
        Branchless.SwapIfGreaterThan(ref i10, ref i21);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i15, ref i27);
        Branchless.SwapIfGreaterThan(ref i16, ref i32);
        Branchless.SwapIfGreaterThan(ref i17, ref i33);
        Branchless.SwapIfGreaterThan(ref i18, ref i34);
        Branchless.SwapIfGreaterThan(ref i22, ref i47);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i37);
        Branchless.SwapIfGreaterThan(ref i26, ref i38);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i45);
        Branchless.SwapIfGreaterThan(ref i30, ref i46);
        Branchless.SwapIfGreaterThan(ref i31, ref i43);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i41, ref i53);
        Branchless.SwapIfGreaterThan(ref i42, ref i54);
        Branchless.SwapIfGreaterThan(ref i48, ref i52);
        Branchless.SwapIfGreaterThan(ref i49, ref i56);
        Branchless.SwapIfGreaterThan(ref i50, ref i57);
        Branchless.SwapIfGreaterThan(ref i51, ref i55);
        Branchless.SwapIfGreaterThan(ref i58, ref i62);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i20);
        Branchless.SwapIfGreaterThan(ref i14, ref i21);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i17, ref i45);
        Branchless.SwapIfGreaterThan(ref i18, ref i46);
        Branchless.SwapIfGreaterThan(ref i22, ref i27);
        Branchless.SwapIfGreaterThan(ref i23, ref i28);
        Branchless.SwapIfGreaterThan(ref i24, ref i36);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i38);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i32, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i41, ref i49);
        Branchless.SwapIfGreaterThan(ref i42, ref i50);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i51);
        Branchless.SwapIfGreaterThan(ref i53, ref i56);
        Branchless.SwapIfGreaterThan(ref i54, ref i57);
        Branchless.SwapIfGreaterThan(ref i55, ref i58);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i12, ref i23);
        Branchless.SwapIfGreaterThan(ref i16, ref i28);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i18, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i31);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i47);
        Branchless.SwapIfGreaterThan(ref i41, ref i45);
        Branchless.SwapIfGreaterThan(ref i42, ref i46);
        Branchless.SwapIfGreaterThan(ref i43, ref i51);
        Branchless.SwapIfGreaterThan(ref i49, ref i53);
        Branchless.SwapIfGreaterThan(ref i50, ref i54);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i16, ref i23);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i20, ref i29);
        Branchless.SwapIfGreaterThan(ref i21, ref i30);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        Branchless.SwapIfGreaterThan(ref i34, ref i42);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i45);
        Branchless.SwapIfGreaterThan(ref i38, ref i46);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i47);
        Branchless.SwapIfGreaterThan(ref i51, ref i55);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i1, ref i12);
        Branchless.SwapIfGreaterThan(ref i2, ref i28);
        Branchless.SwapIfGreaterThan(ref i5, ref i16);
        Branchless.SwapIfGreaterThan(ref i6, ref i32);
        Branchless.SwapIfGreaterThan(ref i9, ref i23);
        Branchless.SwapIfGreaterThan(ref i10, ref i36);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i20, ref i25);
        Branchless.SwapIfGreaterThan(ref i21, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i53);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i56);
        Branchless.SwapIfGreaterThan(ref i35, ref i60);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i54);
        Branchless.SwapIfGreaterThan(ref i45, ref i49);
        Branchless.SwapIfGreaterThan(ref i46, ref i50);
        Branchless.SwapIfGreaterThan(ref i47, ref i57);
        Branchless.SwapIfGreaterThan(ref i51, ref i61);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i33);
        Branchless.SwapIfGreaterThan(ref i10, ref i23);
        Branchless.SwapIfGreaterThan(ref i11, ref i37);
        Branchless.SwapIfGreaterThan(ref i13, ref i24);
        Branchless.SwapIfGreaterThan(ref i14, ref i40);
        Branchless.SwapIfGreaterThan(ref i15, ref i41);
        Branchless.SwapIfGreaterThan(ref i18, ref i44);
        Branchless.SwapIfGreaterThan(ref i19, ref i45);
        Branchless.SwapIfGreaterThan(ref i20, ref i32);
        Branchless.SwapIfGreaterThan(ref i21, ref i48);
        Branchless.SwapIfGreaterThan(ref i22, ref i49);
        Branchless.SwapIfGreaterThan(ref i26, ref i52);
        Branchless.SwapIfGreaterThan(ref i30, ref i59);
        Branchless.SwapIfGreaterThan(ref i31, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i50);
        Branchless.SwapIfGreaterThan(ref i43, ref i53);
        Branchless.SwapIfGreaterThan(ref i55, ref i56);
        Branchless.SwapIfGreaterThan(ref i58, ref i60);
        Branchless.SwapIfGreaterThan(ref i61, ref i62);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i17);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i24);
        Branchless.SwapIfGreaterThan(ref i15, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i28);
        Branchless.SwapIfGreaterThan(ref i19, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i36);
        Branchless.SwapIfGreaterThan(ref i27, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i40);
        Branchless.SwapIfGreaterThan(ref i34, ref i44);
        Branchless.SwapIfGreaterThan(ref i35, ref i45);
        Branchless.SwapIfGreaterThan(ref i38, ref i48);
        Branchless.SwapIfGreaterThan(ref i39, ref i49);
        Branchless.SwapIfGreaterThan(ref i41, ref i52);
        Branchless.SwapIfGreaterThan(ref i46, ref i59);
        Branchless.SwapIfGreaterThan(ref i47, ref i55);
        Branchless.SwapIfGreaterThan(ref i51, ref i58);
        Branchless.SwapIfGreaterThan(ref i56, ref i57);
        Branchless.SwapIfGreaterThan(ref i60, ref i61);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i7, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i28);
        Branchless.SwapIfGreaterThan(ref i17, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i30);
        Branchless.SwapIfGreaterThan(ref i21, ref i32);
        Branchless.SwapIfGreaterThan(ref i22, ref i25);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i31, ref i41);
        Branchless.SwapIfGreaterThan(ref i33, ref i44);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i48);
        Branchless.SwapIfGreaterThan(ref i37, ref i46);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i42, ref i52);
        Branchless.SwapIfGreaterThan(ref i45, ref i59);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i7, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i22, ref i34);
        Branchless.SwapIfGreaterThan(ref i25, ref i36);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i38);
        Branchless.SwapIfGreaterThan(ref i29, ref i40);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i48);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i52);
        Branchless.SwapIfGreaterThan(ref i51, ref i59);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i23);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i52);
        Branchless.SwapIfGreaterThan(ref i51, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i59);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i4, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i23);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i51);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i55);
        Branchless.SwapIfGreaterThan(ref i58, ref i59);
        
        // Layer 19
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i54);
        Branchless.SwapIfGreaterThan(ref i53, ref i55);
        Branchless.SwapIfGreaterThan(ref i56, ref i58);
        Branchless.SwapIfGreaterThan(ref i57, ref i59);
        
        // Layer 20
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        Branchless.SwapIfGreaterThan(ref i55, ref i56);
        Branchless.SwapIfGreaterThan(ref i57, ref i58);
        Branchless.SwapIfGreaterThan(ref i59, ref i60);
    }
    
    /// <summary>
    /// Sort 64 items in array in-place
    /// </summary>
    /// <param name="itemArray">Array to be sorted</param>
    /// <param name="index">Index of first item in array</param>
    /// <returns></returns>
    public static void Sort64Items(this short[] itemArray, int index = 0)
    {
        ref var i0 = ref itemArray[index];
        ref var i1 = ref itemArray[index + 1];
        ref var i2 = ref itemArray[index + 2];
        ref var i3 = ref itemArray[index + 3];
        ref var i4 = ref itemArray[index + 4];
        ref var i5 = ref itemArray[index + 5];
        ref var i6 = ref itemArray[index + 6];
        ref var i7 = ref itemArray[index + 7];
        ref var i8 = ref itemArray[index + 8];
        ref var i9 = ref itemArray[index + 9];
        ref var i10 = ref itemArray[index + 10];
        ref var i11 = ref itemArray[index + 11];
        ref var i12 = ref itemArray[index + 12];
        ref var i13 = ref itemArray[index + 13];
        ref var i14 = ref itemArray[index + 14];
        ref var i15 = ref itemArray[index + 15];
        ref var i16 = ref itemArray[index + 16];
        ref var i17 = ref itemArray[index + 17];
        ref var i18 = ref itemArray[index + 18];
        ref var i19 = ref itemArray[index + 19];
        ref var i20 = ref itemArray[index + 20];
        ref var i21 = ref itemArray[index + 21];
        ref var i22 = ref itemArray[index + 22];
        ref var i23 = ref itemArray[index + 23];
        ref var i24 = ref itemArray[index + 24];
        ref var i25 = ref itemArray[index + 25];
        ref var i26 = ref itemArray[index + 26];
        ref var i27 = ref itemArray[index + 27];
        ref var i28 = ref itemArray[index + 28];
        ref var i29 = ref itemArray[index + 29];
        ref var i30 = ref itemArray[index + 30];
        ref var i31 = ref itemArray[index + 31];
        ref var i32 = ref itemArray[index + 32];
        ref var i33 = ref itemArray[index + 33];
        ref var i34 = ref itemArray[index + 34];
        ref var i35 = ref itemArray[index + 35];
        ref var i36 = ref itemArray[index + 36];
        ref var i37 = ref itemArray[index + 37];
        ref var i38 = ref itemArray[index + 38];
        ref var i39 = ref itemArray[index + 39];
        ref var i40 = ref itemArray[index + 40];
        ref var i41 = ref itemArray[index + 41];
        ref var i42 = ref itemArray[index + 42];
        ref var i43 = ref itemArray[index + 43];
        ref var i44 = ref itemArray[index + 44];
        ref var i45 = ref itemArray[index + 45];
        ref var i46 = ref itemArray[index + 46];
        ref var i47 = ref itemArray[index + 47];
        ref var i48 = ref itemArray[index + 48];
        ref var i49 = ref itemArray[index + 49];
        ref var i50 = ref itemArray[index + 50];
        ref var i51 = ref itemArray[index + 51];
        ref var i52 = ref itemArray[index + 52];
        ref var i53 = ref itemArray[index + 53];
        ref var i54 = ref itemArray[index + 54];
        ref var i55 = ref itemArray[index + 55];
        ref var i56 = ref itemArray[index + 56];
        ref var i57 = ref itemArray[index + 57];
        ref var i58 = ref itemArray[index + 58];
        ref var i59 = ref itemArray[index + 59];
        ref var i60 = ref itemArray[index + 60];
        ref var i61 = ref itemArray[index + 61];
        ref var i62 = ref itemArray[index + 62];
        ref var i63 = ref itemArray[index + 63];
        
        // Layer 1
        Branchless.SwapIfGreaterThan(ref i0, ref i2);
        Branchless.SwapIfGreaterThan(ref i1, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i6);
        Branchless.SwapIfGreaterThan(ref i5, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i14);
        Branchless.SwapIfGreaterThan(ref i13, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i50);
        Branchless.SwapIfGreaterThan(ref i49, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i54);
        Branchless.SwapIfGreaterThan(ref i53, ref i55);
        Branchless.SwapIfGreaterThan(ref i56, ref i58);
        Branchless.SwapIfGreaterThan(ref i57, ref i59);
        Branchless.SwapIfGreaterThan(ref i60, ref i62);
        Branchless.SwapIfGreaterThan(ref i61, ref i63);
        
        // Layer 2
        Branchless.SwapIfGreaterThan(ref i0, ref i1);
        Branchless.SwapIfGreaterThan(ref i2, ref i3);
        Branchless.SwapIfGreaterThan(ref i4, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i7);
        Branchless.SwapIfGreaterThan(ref i8, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i11);
        Branchless.SwapIfGreaterThan(ref i12, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i15);
        Branchless.SwapIfGreaterThan(ref i16, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i19);
        Branchless.SwapIfGreaterThan(ref i20, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i31);
        Branchless.SwapIfGreaterThan(ref i32, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i35);
        Branchless.SwapIfGreaterThan(ref i36, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i51);
        Branchless.SwapIfGreaterThan(ref i52, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i55);
        Branchless.SwapIfGreaterThan(ref i56, ref i57);
        Branchless.SwapIfGreaterThan(ref i58, ref i59);
        Branchless.SwapIfGreaterThan(ref i60, ref i61);
        Branchless.SwapIfGreaterThan(ref i62, ref i63);
        
        // Layer 3
        Branchless.SwapIfGreaterThan(ref i0, ref i20);
        Branchless.SwapIfGreaterThan(ref i1, ref i2);
        Branchless.SwapIfGreaterThan(ref i3, ref i23);
        Branchless.SwapIfGreaterThan(ref i4, ref i16);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i19);
        Branchless.SwapIfGreaterThan(ref i8, ref i48);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i51);
        Branchless.SwapIfGreaterThan(ref i12, ref i52);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i55);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i40, ref i60);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i63);
        Branchless.SwapIfGreaterThan(ref i44, ref i56);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i59);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        Branchless.SwapIfGreaterThan(ref i57, ref i58);
        Branchless.SwapIfGreaterThan(ref i61, ref i62);
        
        // Layer 4
        Branchless.SwapIfGreaterThan(ref i0, ref i8);
        Branchless.SwapIfGreaterThan(ref i1, ref i21);
        Branchless.SwapIfGreaterThan(ref i2, ref i22);
        Branchless.SwapIfGreaterThan(ref i3, ref i11);
        Branchless.SwapIfGreaterThan(ref i4, ref i40);
        Branchless.SwapIfGreaterThan(ref i5, ref i17);
        Branchless.SwapIfGreaterThan(ref i6, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i43);
        Branchless.SwapIfGreaterThan(ref i9, ref i49);
        Branchless.SwapIfGreaterThan(ref i10, ref i50);
        Branchless.SwapIfGreaterThan(ref i12, ref i24);
        Branchless.SwapIfGreaterThan(ref i13, ref i53);
        Branchless.SwapIfGreaterThan(ref i14, ref i54);
        Branchless.SwapIfGreaterThan(ref i15, ref i27);
        Branchless.SwapIfGreaterThan(ref i16, ref i28);
        Branchless.SwapIfGreaterThan(ref i19, ref i31);
        Branchless.SwapIfGreaterThan(ref i20, ref i56);
        Branchless.SwapIfGreaterThan(ref i23, ref i59);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i32, ref i44);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i47);
        Branchless.SwapIfGreaterThan(ref i36, ref i48);
        Branchless.SwapIfGreaterThan(ref i39, ref i51);
        Branchless.SwapIfGreaterThan(ref i41, ref i61);
        Branchless.SwapIfGreaterThan(ref i42, ref i62);
        Branchless.SwapIfGreaterThan(ref i45, ref i57);
        Branchless.SwapIfGreaterThan(ref i46, ref i58);
        Branchless.SwapIfGreaterThan(ref i52, ref i60);
        Branchless.SwapIfGreaterThan(ref i55, ref i63);
        
        // Layer 5
        Branchless.SwapIfGreaterThan(ref i0, ref i32);
        Branchless.SwapIfGreaterThan(ref i1, ref i9);
        Branchless.SwapIfGreaterThan(ref i2, ref i10);
        Branchless.SwapIfGreaterThan(ref i3, ref i35);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i5, ref i41);
        Branchless.SwapIfGreaterThan(ref i6, ref i42);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i8, ref i44);
        Branchless.SwapIfGreaterThan(ref i11, ref i47);
        Branchless.SwapIfGreaterThan(ref i13, ref i25);
        Branchless.SwapIfGreaterThan(ref i14, ref i26);
        Branchless.SwapIfGreaterThan(ref i16, ref i52);
        Branchless.SwapIfGreaterThan(ref i17, ref i29);
        Branchless.SwapIfGreaterThan(ref i18, ref i30);
        Branchless.SwapIfGreaterThan(ref i19, ref i55);
        Branchless.SwapIfGreaterThan(ref i20, ref i36);
        Branchless.SwapIfGreaterThan(ref i21, ref i57);
        Branchless.SwapIfGreaterThan(ref i22, ref i58);
        Branchless.SwapIfGreaterThan(ref i23, ref i39);
        Branchless.SwapIfGreaterThan(ref i24, ref i40);
        Branchless.SwapIfGreaterThan(ref i27, ref i43);
        Branchless.SwapIfGreaterThan(ref i28, ref i60);
        Branchless.SwapIfGreaterThan(ref i31, ref i63);
        Branchless.SwapIfGreaterThan(ref i33, ref i45);
        Branchless.SwapIfGreaterThan(ref i34, ref i46);
        Branchless.SwapIfGreaterThan(ref i37, ref i49);
        Branchless.SwapIfGreaterThan(ref i38, ref i50);
        Branchless.SwapIfGreaterThan(ref i48, ref i56);
        Branchless.SwapIfGreaterThan(ref i51, ref i59);
        Branchless.SwapIfGreaterThan(ref i53, ref i61);
        Branchless.SwapIfGreaterThan(ref i54, ref i62);
        
        // Layer 6
        Branchless.SwapIfGreaterThan(ref i0, ref i4);
        Branchless.SwapIfGreaterThan(ref i1, ref i33);
        Branchless.SwapIfGreaterThan(ref i2, ref i34);
        Branchless.SwapIfGreaterThan(ref i3, ref i7);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i9, ref i45);
        Branchless.SwapIfGreaterThan(ref i10, ref i46);
        Branchless.SwapIfGreaterThan(ref i11, ref i19);
        Branchless.SwapIfGreaterThan(ref i12, ref i32);
        Branchless.SwapIfGreaterThan(ref i15, ref i35);
        Branchless.SwapIfGreaterThan(ref i17, ref i53);
        Branchless.SwapIfGreaterThan(ref i18, ref i54);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i37);
        Branchless.SwapIfGreaterThan(ref i22, ref i38);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i25, ref i41);
        Branchless.SwapIfGreaterThan(ref i26, ref i42);
        Branchless.SwapIfGreaterThan(ref i28, ref i48);
        Branchless.SwapIfGreaterThan(ref i29, ref i61);
        Branchless.SwapIfGreaterThan(ref i30, ref i62);
        Branchless.SwapIfGreaterThan(ref i31, ref i51);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i52);
        Branchless.SwapIfGreaterThan(ref i47, ref i55);
        Branchless.SwapIfGreaterThan(ref i49, ref i57);
        Branchless.SwapIfGreaterThan(ref i50, ref i58);
        Branchless.SwapIfGreaterThan(ref i56, ref i60);
        Branchless.SwapIfGreaterThan(ref i59, ref i63);
        
        // Layer 7
        Branchless.SwapIfGreaterThan(ref i1, ref i5);
        Branchless.SwapIfGreaterThan(ref i2, ref i6);
        Branchless.SwapIfGreaterThan(ref i4, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i15);
        Branchless.SwapIfGreaterThan(ref i8, ref i20);
        Branchless.SwapIfGreaterThan(ref i9, ref i17);
        Branchless.SwapIfGreaterThan(ref i10, ref i18);
        Branchless.SwapIfGreaterThan(ref i11, ref i23);
        Branchless.SwapIfGreaterThan(ref i13, ref i33);
        Branchless.SwapIfGreaterThan(ref i14, ref i34);
        Branchless.SwapIfGreaterThan(ref i16, ref i32);
        Branchless.SwapIfGreaterThan(ref i19, ref i35);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i24, ref i36);
        Branchless.SwapIfGreaterThan(ref i27, ref i39);
        Branchless.SwapIfGreaterThan(ref i28, ref i44);
        Branchless.SwapIfGreaterThan(ref i29, ref i49);
        Branchless.SwapIfGreaterThan(ref i30, ref i50);
        Branchless.SwapIfGreaterThan(ref i31, ref i47);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i40, ref i52);
        Branchless.SwapIfGreaterThan(ref i43, ref i55);
        Branchless.SwapIfGreaterThan(ref i45, ref i53);
        Branchless.SwapIfGreaterThan(ref i46, ref i54);
        Branchless.SwapIfGreaterThan(ref i48, ref i56);
        Branchless.SwapIfGreaterThan(ref i51, ref i59);
        Branchless.SwapIfGreaterThan(ref i57, ref i61);
        Branchless.SwapIfGreaterThan(ref i58, ref i62);
        
        // Layer 8
        Branchless.SwapIfGreaterThan(ref i4, ref i8);
        Branchless.SwapIfGreaterThan(ref i5, ref i13);
        Branchless.SwapIfGreaterThan(ref i6, ref i14);
        Branchless.SwapIfGreaterThan(ref i7, ref i11);
        Branchless.SwapIfGreaterThan(ref i9, ref i21);
        Branchless.SwapIfGreaterThan(ref i10, ref i22);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i23);
        Branchless.SwapIfGreaterThan(ref i16, ref i44);
        Branchless.SwapIfGreaterThan(ref i17, ref i33);
        Branchless.SwapIfGreaterThan(ref i18, ref i34);
        Branchless.SwapIfGreaterThan(ref i19, ref i47);
        Branchless.SwapIfGreaterThan(ref i24, ref i32);
        Branchless.SwapIfGreaterThan(ref i25, ref i37);
        Branchless.SwapIfGreaterThan(ref i26, ref i38);
        Branchless.SwapIfGreaterThan(ref i27, ref i35);
        Branchless.SwapIfGreaterThan(ref i28, ref i36);
        Branchless.SwapIfGreaterThan(ref i29, ref i45);
        Branchless.SwapIfGreaterThan(ref i30, ref i46);
        Branchless.SwapIfGreaterThan(ref i31, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i48);
        Branchless.SwapIfGreaterThan(ref i41, ref i53);
        Branchless.SwapIfGreaterThan(ref i42, ref i54);
        Branchless.SwapIfGreaterThan(ref i43, ref i51);
        Branchless.SwapIfGreaterThan(ref i49, ref i57);
        Branchless.SwapIfGreaterThan(ref i50, ref i58);
        Branchless.SwapIfGreaterThan(ref i52, ref i56);
        Branchless.SwapIfGreaterThan(ref i55, ref i59);
        
        // Layer 9
        Branchless.SwapIfGreaterThan(ref i5, ref i9);
        Branchless.SwapIfGreaterThan(ref i6, ref i10);
        Branchless.SwapIfGreaterThan(ref i8, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i15);
        Branchless.SwapIfGreaterThan(ref i13, ref i21);
        Branchless.SwapIfGreaterThan(ref i14, ref i22);
        Branchless.SwapIfGreaterThan(ref i16, ref i20);
        Branchless.SwapIfGreaterThan(ref i17, ref i45);
        Branchless.SwapIfGreaterThan(ref i18, ref i46);
        Branchless.SwapIfGreaterThan(ref i19, ref i23);
        Branchless.SwapIfGreaterThan(ref i24, ref i28);
        Branchless.SwapIfGreaterThan(ref i25, ref i33);
        Branchless.SwapIfGreaterThan(ref i26, ref i34);
        Branchless.SwapIfGreaterThan(ref i27, ref i31);
        Branchless.SwapIfGreaterThan(ref i29, ref i37);
        Branchless.SwapIfGreaterThan(ref i30, ref i38);
        Branchless.SwapIfGreaterThan(ref i32, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i39);
        Branchless.SwapIfGreaterThan(ref i40, ref i44);
        Branchless.SwapIfGreaterThan(ref i41, ref i49);
        Branchless.SwapIfGreaterThan(ref i42, ref i50);
        Branchless.SwapIfGreaterThan(ref i43, ref i47);
        Branchless.SwapIfGreaterThan(ref i48, ref i52);
        Branchless.SwapIfGreaterThan(ref i51, ref i55);
        Branchless.SwapIfGreaterThan(ref i53, ref i57);
        Branchless.SwapIfGreaterThan(ref i54, ref i58);
        
        // Layer 10
        Branchless.SwapIfGreaterThan(ref i9, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i14);
        Branchless.SwapIfGreaterThan(ref i16, ref i24);
        Branchless.SwapIfGreaterThan(ref i17, ref i21);
        Branchless.SwapIfGreaterThan(ref i18, ref i22);
        Branchless.SwapIfGreaterThan(ref i19, ref i27);
        Branchless.SwapIfGreaterThan(ref i20, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i31);
        Branchless.SwapIfGreaterThan(ref i25, ref i29);
        Branchless.SwapIfGreaterThan(ref i26, ref i30);
        Branchless.SwapIfGreaterThan(ref i32, ref i40);
        Branchless.SwapIfGreaterThan(ref i33, ref i37);
        Branchless.SwapIfGreaterThan(ref i34, ref i38);
        Branchless.SwapIfGreaterThan(ref i35, ref i43);
        Branchless.SwapIfGreaterThan(ref i36, ref i44);
        Branchless.SwapIfGreaterThan(ref i39, ref i47);
        Branchless.SwapIfGreaterThan(ref i41, ref i45);
        Branchless.SwapIfGreaterThan(ref i42, ref i46);
        Branchless.SwapIfGreaterThan(ref i49, ref i53);
        Branchless.SwapIfGreaterThan(ref i50, ref i54);
        
        // Layer 11
        Branchless.SwapIfGreaterThan(ref i12, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i19);
        Branchless.SwapIfGreaterThan(ref i17, ref i25);
        Branchless.SwapIfGreaterThan(ref i18, ref i26);
        Branchless.SwapIfGreaterThan(ref i20, ref i24);
        Branchless.SwapIfGreaterThan(ref i21, ref i29);
        Branchless.SwapIfGreaterThan(ref i22, ref i30);
        Branchless.SwapIfGreaterThan(ref i23, ref i27);
        Branchless.SwapIfGreaterThan(ref i28, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i35);
        Branchless.SwapIfGreaterThan(ref i33, ref i41);
        Branchless.SwapIfGreaterThan(ref i34, ref i42);
        Branchless.SwapIfGreaterThan(ref i36, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i45);
        Branchless.SwapIfGreaterThan(ref i38, ref i46);
        Branchless.SwapIfGreaterThan(ref i39, ref i43);
        Branchless.SwapIfGreaterThan(ref i44, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i51);
        
        // Layer 12
        Branchless.SwapIfGreaterThan(ref i1, ref i16);
        Branchless.SwapIfGreaterThan(ref i2, ref i32);
        Branchless.SwapIfGreaterThan(ref i5, ref i20);
        Branchless.SwapIfGreaterThan(ref i6, ref i36);
        Branchless.SwapIfGreaterThan(ref i9, ref i24);
        Branchless.SwapIfGreaterThan(ref i10, ref i40);
        Branchless.SwapIfGreaterThan(ref i13, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i18);
        Branchless.SwapIfGreaterThan(ref i21, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i26);
        Branchless.SwapIfGreaterThan(ref i23, ref i53);
        Branchless.SwapIfGreaterThan(ref i27, ref i57);
        Branchless.SwapIfGreaterThan(ref i29, ref i33);
        Branchless.SwapIfGreaterThan(ref i30, ref i34);
        Branchless.SwapIfGreaterThan(ref i31, ref i61);
        Branchless.SwapIfGreaterThan(ref i37, ref i41);
        Branchless.SwapIfGreaterThan(ref i38, ref i42);
        Branchless.SwapIfGreaterThan(ref i39, ref i54);
        Branchless.SwapIfGreaterThan(ref i43, ref i58);
        Branchless.SwapIfGreaterThan(ref i45, ref i49);
        Branchless.SwapIfGreaterThan(ref i46, ref i50);
        Branchless.SwapIfGreaterThan(ref i47, ref i62);
        
        // Layer 13
        Branchless.SwapIfGreaterThan(ref i1, ref i4);
        Branchless.SwapIfGreaterThan(ref i2, ref i8);
        Branchless.SwapIfGreaterThan(ref i3, ref i33);
        Branchless.SwapIfGreaterThan(ref i6, ref i12);
        Branchless.SwapIfGreaterThan(ref i7, ref i37);
        Branchless.SwapIfGreaterThan(ref i10, ref i24);
        Branchless.SwapIfGreaterThan(ref i11, ref i41);
        Branchless.SwapIfGreaterThan(ref i13, ref i28);
        Branchless.SwapIfGreaterThan(ref i14, ref i44);
        Branchless.SwapIfGreaterThan(ref i15, ref i45);
        Branchless.SwapIfGreaterThan(ref i18, ref i48);
        Branchless.SwapIfGreaterThan(ref i19, ref i49);
        Branchless.SwapIfGreaterThan(ref i21, ref i36);
        Branchless.SwapIfGreaterThan(ref i22, ref i52);
        Branchless.SwapIfGreaterThan(ref i26, ref i56);
        Branchless.SwapIfGreaterThan(ref i27, ref i42);
        Branchless.SwapIfGreaterThan(ref i30, ref i60);
        Branchless.SwapIfGreaterThan(ref i35, ref i50);
        Branchless.SwapIfGreaterThan(ref i39, ref i53);
        Branchless.SwapIfGreaterThan(ref i51, ref i57);
        Branchless.SwapIfGreaterThan(ref i55, ref i61);
        Branchless.SwapIfGreaterThan(ref i59, ref i62);
        
        // Layer 14
        Branchless.SwapIfGreaterThan(ref i2, ref i4);
        Branchless.SwapIfGreaterThan(ref i3, ref i17);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i22);
        Branchless.SwapIfGreaterThan(ref i8, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i25);
        Branchless.SwapIfGreaterThan(ref i12, ref i20);
        Branchless.SwapIfGreaterThan(ref i14, ref i28);
        Branchless.SwapIfGreaterThan(ref i15, ref i29);
        Branchless.SwapIfGreaterThan(ref i18, ref i32);
        Branchless.SwapIfGreaterThan(ref i19, ref i33);
        Branchless.SwapIfGreaterThan(ref i23, ref i37);
        Branchless.SwapIfGreaterThan(ref i26, ref i40);
        Branchless.SwapIfGreaterThan(ref i30, ref i44);
        Branchless.SwapIfGreaterThan(ref i31, ref i45);
        Branchless.SwapIfGreaterThan(ref i34, ref i48);
        Branchless.SwapIfGreaterThan(ref i35, ref i49);
        Branchless.SwapIfGreaterThan(ref i38, ref i52);
        Branchless.SwapIfGreaterThan(ref i41, ref i56);
        Branchless.SwapIfGreaterThan(ref i43, ref i51);
        Branchless.SwapIfGreaterThan(ref i46, ref i60);
        Branchless.SwapIfGreaterThan(ref i47, ref i55);
        Branchless.SwapIfGreaterThan(ref i57, ref i58);
        Branchless.SwapIfGreaterThan(ref i59, ref i61);
        
        // Layer 15
        Branchless.SwapIfGreaterThan(ref i3, ref i18);
        Branchless.SwapIfGreaterThan(ref i7, ref i21);
        Branchless.SwapIfGreaterThan(ref i11, ref i32);
        Branchless.SwapIfGreaterThan(ref i15, ref i30);
        Branchless.SwapIfGreaterThan(ref i17, ref i26);
        Branchless.SwapIfGreaterThan(ref i19, ref i25);
        Branchless.SwapIfGreaterThan(ref i22, ref i36);
        Branchless.SwapIfGreaterThan(ref i23, ref i29);
        Branchless.SwapIfGreaterThan(ref i27, ref i41);
        Branchless.SwapIfGreaterThan(ref i31, ref i52);
        Branchless.SwapIfGreaterThan(ref i33, ref i48);
        Branchless.SwapIfGreaterThan(ref i34, ref i40);
        Branchless.SwapIfGreaterThan(ref i37, ref i46);
        Branchless.SwapIfGreaterThan(ref i38, ref i44);
        Branchless.SwapIfGreaterThan(ref i42, ref i56);
        Branchless.SwapIfGreaterThan(ref i45, ref i60);
        
        // Layer 16
        Branchless.SwapIfGreaterThan(ref i3, ref i16);
        Branchless.SwapIfGreaterThan(ref i7, ref i20);
        Branchless.SwapIfGreaterThan(ref i11, ref i24);
        Branchless.SwapIfGreaterThan(ref i15, ref i21);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i34);
        Branchless.SwapIfGreaterThan(ref i22, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i38);
        Branchless.SwapIfGreaterThan(ref i25, ref i40);
        Branchless.SwapIfGreaterThan(ref i26, ref i32);
        Branchless.SwapIfGreaterThan(ref i27, ref i33);
        Branchless.SwapIfGreaterThan(ref i29, ref i44);
        Branchless.SwapIfGreaterThan(ref i30, ref i36);
        Branchless.SwapIfGreaterThan(ref i31, ref i37);
        Branchless.SwapIfGreaterThan(ref i35, ref i41);
        Branchless.SwapIfGreaterThan(ref i39, ref i52);
        Branchless.SwapIfGreaterThan(ref i42, ref i48);
        Branchless.SwapIfGreaterThan(ref i43, ref i56);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i60);
        
        // Layer 17
        Branchless.SwapIfGreaterThan(ref i3, ref i9);
        Branchless.SwapIfGreaterThan(ref i7, ref i13);
        Branchless.SwapIfGreaterThan(ref i10, ref i16);
        Branchless.SwapIfGreaterThan(ref i11, ref i17);
        Branchless.SwapIfGreaterThan(ref i14, ref i20);
        Branchless.SwapIfGreaterThan(ref i15, ref i22);
        Branchless.SwapIfGreaterThan(ref i18, ref i24);
        Branchless.SwapIfGreaterThan(ref i19, ref i26);
        Branchless.SwapIfGreaterThan(ref i21, ref i28);
        Branchless.SwapIfGreaterThan(ref i23, ref i30);
        Branchless.SwapIfGreaterThan(ref i25, ref i32);
        Branchless.SwapIfGreaterThan(ref i27, ref i34);
        Branchless.SwapIfGreaterThan(ref i29, ref i36);
        Branchless.SwapIfGreaterThan(ref i31, ref i38);
        Branchless.SwapIfGreaterThan(ref i33, ref i40);
        Branchless.SwapIfGreaterThan(ref i35, ref i42);
        Branchless.SwapIfGreaterThan(ref i37, ref i44);
        Branchless.SwapIfGreaterThan(ref i39, ref i45);
        Branchless.SwapIfGreaterThan(ref i41, ref i48);
        Branchless.SwapIfGreaterThan(ref i43, ref i49);
        Branchless.SwapIfGreaterThan(ref i46, ref i52);
        Branchless.SwapIfGreaterThan(ref i47, ref i53);
        Branchless.SwapIfGreaterThan(ref i50, ref i56);
        Branchless.SwapIfGreaterThan(ref i54, ref i60);
        
        // Layer 18
        Branchless.SwapIfGreaterThan(ref i3, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i10);
        Branchless.SwapIfGreaterThan(ref i9, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i16);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i17);
        Branchless.SwapIfGreaterThan(ref i18, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i22);
        Branchless.SwapIfGreaterThan(ref i21, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i26);
        Branchless.SwapIfGreaterThan(ref i25, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i29);
        Branchless.SwapIfGreaterThan(ref i30, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i33);
        Branchless.SwapIfGreaterThan(ref i34, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i38);
        Branchless.SwapIfGreaterThan(ref i37, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i42);
        Branchless.SwapIfGreaterThan(ref i41, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i45);
        Branchless.SwapIfGreaterThan(ref i46, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i52);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i54);
        Branchless.SwapIfGreaterThan(ref i53, ref i56);
        Branchless.SwapIfGreaterThan(ref i55, ref i60);
        
        // Layer 19
        Branchless.SwapIfGreaterThan(ref i3, ref i5);
        Branchless.SwapIfGreaterThan(ref i6, ref i8);
        Branchless.SwapIfGreaterThan(ref i7, ref i9);
        Branchless.SwapIfGreaterThan(ref i10, ref i12);
        Branchless.SwapIfGreaterThan(ref i11, ref i13);
        Branchless.SwapIfGreaterThan(ref i14, ref i16);
        Branchless.SwapIfGreaterThan(ref i15, ref i18);
        Branchless.SwapIfGreaterThan(ref i17, ref i20);
        Branchless.SwapIfGreaterThan(ref i19, ref i21);
        Branchless.SwapIfGreaterThan(ref i22, ref i24);
        Branchless.SwapIfGreaterThan(ref i23, ref i25);
        Branchless.SwapIfGreaterThan(ref i26, ref i28);
        Branchless.SwapIfGreaterThan(ref i27, ref i30);
        Branchless.SwapIfGreaterThan(ref i29, ref i32);
        Branchless.SwapIfGreaterThan(ref i31, ref i34);
        Branchless.SwapIfGreaterThan(ref i33, ref i36);
        Branchless.SwapIfGreaterThan(ref i35, ref i37);
        Branchless.SwapIfGreaterThan(ref i38, ref i40);
        Branchless.SwapIfGreaterThan(ref i39, ref i41);
        Branchless.SwapIfGreaterThan(ref i42, ref i44);
        Branchless.SwapIfGreaterThan(ref i43, ref i46);
        Branchless.SwapIfGreaterThan(ref i45, ref i48);
        Branchless.SwapIfGreaterThan(ref i47, ref i49);
        Branchless.SwapIfGreaterThan(ref i50, ref i52);
        Branchless.SwapIfGreaterThan(ref i51, ref i53);
        Branchless.SwapIfGreaterThan(ref i54, ref i56);
        Branchless.SwapIfGreaterThan(ref i55, ref i57);
        Branchless.SwapIfGreaterThan(ref i58, ref i60);
        
        // Layer 20
        Branchless.SwapIfGreaterThan(ref i3, ref i4);
        Branchless.SwapIfGreaterThan(ref i5, ref i6);
        Branchless.SwapIfGreaterThan(ref i7, ref i8);
        Branchless.SwapIfGreaterThan(ref i9, ref i10);
        Branchless.SwapIfGreaterThan(ref i11, ref i12);
        Branchless.SwapIfGreaterThan(ref i13, ref i14);
        Branchless.SwapIfGreaterThan(ref i15, ref i16);
        Branchless.SwapIfGreaterThan(ref i17, ref i18);
        Branchless.SwapIfGreaterThan(ref i19, ref i20);
        Branchless.SwapIfGreaterThan(ref i21, ref i22);
        Branchless.SwapIfGreaterThan(ref i23, ref i24);
        Branchless.SwapIfGreaterThan(ref i25, ref i26);
        Branchless.SwapIfGreaterThan(ref i27, ref i28);
        Branchless.SwapIfGreaterThan(ref i29, ref i30);
        Branchless.SwapIfGreaterThan(ref i31, ref i32);
        Branchless.SwapIfGreaterThan(ref i33, ref i34);
        Branchless.SwapIfGreaterThan(ref i35, ref i36);
        Branchless.SwapIfGreaterThan(ref i37, ref i38);
        Branchless.SwapIfGreaterThan(ref i39, ref i40);
        Branchless.SwapIfGreaterThan(ref i41, ref i42);
        Branchless.SwapIfGreaterThan(ref i43, ref i44);
        Branchless.SwapIfGreaterThan(ref i45, ref i46);
        Branchless.SwapIfGreaterThan(ref i47, ref i48);
        Branchless.SwapIfGreaterThan(ref i49, ref i50);
        Branchless.SwapIfGreaterThan(ref i51, ref i52);
        Branchless.SwapIfGreaterThan(ref i53, ref i54);
        Branchless.SwapIfGreaterThan(ref i55, ref i56);
        Branchless.SwapIfGreaterThan(ref i57, ref i58);
        Branchless.SwapIfGreaterThan(ref i59, ref i60);
    }
    
}
