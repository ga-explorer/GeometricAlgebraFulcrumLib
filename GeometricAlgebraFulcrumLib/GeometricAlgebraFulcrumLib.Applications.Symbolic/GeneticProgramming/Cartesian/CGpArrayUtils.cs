// ----------------------------------------------------------------------------------------
// Copyright © 2006 - 2024 Tangible Software Solutions, Inc.
// This class can be used by anyone provided that the copyright notice remains intact.
//
// This class provides the ability to initialize and delete array elements.
// ----------------------------------------------------------------------------------------
namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.GeneticProgramming.Cartesian;

internal static class CGpArrayUtils
{
    public static T[] InitializeWithDefaultInstances<T>(int length) where T : class, new()
    {
        var array = new T[length];
        
        for (var i = 0; i < length; i++) 
            array[i] = new T();
        
        return array;
    }

    public static string[] InitializeStringArrayWithDefaultInstances(int length)
    {
        var array = new string[length];
        
        for (var i = 0; i < length; i++) 
            array[i] = "";
        
        return array;
    }

    public static T[] PadWithNull<T>(int length, T[] existingItems) where T : class
    {
        if (length <= existingItems.Length) 
            return existingItems;

        var array = new T[length];

        for (var i = 0; i < existingItems.Length; i++) 
            array[i] = existingItems[i];

        return array;

    }

    public static T[] PadValueTypeArrayWithDefaultInstances<T>(int length, T[] existingItems) where T : struct
    {
        if (length <= existingItems.Length) 
            return existingItems;

        var array = new T[length];

        for (var i = 0; i < existingItems.Length; i++) 
            array[i] = existingItems[i];

        return array;

    }

    public static T[] PadReferenceTypeArrayWithDefaultInstances<T>(int length, T[] existingItems) where T : class, new()
    {
        if (length <= existingItems.Length) 
            return existingItems;

        var array = new T[length];

        for (var i = 0; i < existingItems.Length; i++) 
            array[i] = existingItems[i];

        for (var i = existingItems.Length; i < length; i++) 
            array[i] = new T();

        return array;

    }

    public static string[] PadStringArrayWithDefaultInstances(int length, string[] existingItems)
    {
        if (length <= existingItems.Length) 
            return existingItems;

        var array = new string[length];

        for (var i = 0; i < existingItems.Length; i++) 
            array[i] = existingItems[i];

        for (var i = existingItems.Length; i < length; i++) 
            array[i] = "";

        return array;

    }

    public static void DeleteArray<T>(IEnumerable<T> array) where T: IDisposable
    {
        foreach (var element in array) 
            element.Dispose();
    }

    public static char[][] RectangularCharArray(int size1, int size2)
    {
        var newArray = new char[size1][];

        for (var array1 = 0; array1 < size1; array1++) 
            newArray[array1] = new char[size2];

        return newArray;
    }
}