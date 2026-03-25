using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using UndertaleModLib;

namespace mmm;



/*
A B
---
a a
b b
c c
d d
e e
f 1
g 2
h 3
i f
j g
k h

Our objective is to say "1, 2, and 3 were added before e", then move Bi until it gets to f.


*/

public class UTMTTest
{
    public static UndertaleData Test(string datapath)
    {
        Console.WriteLine("UTMT Test.");
        FileStream stream = File.OpenRead(datapath);
        return UndertaleIO.Read(stream,
            (message, important) => Console.WriteLine($"UTMTLib WARNING{(important ? " (IMPORTANT)" : "")}: \"{message}\""),
            (message) => Console.WriteLine($"UTMTLib MESSAGE: \"{message}\"")
        );
    }

    public delegate bool Equivalent<T>(T x, T y);

    public static void FindDifferencesInLists<T>(IList<T> a, IList<T> b, Equivalent<T> eqCb)
        where T : UndertaleObject
    {
        // WARNING: This code uses AI.
        int ai = 0;
        int bi = 0;

        while (ai < a.Count && bi < b.Count)
        {
            if (eqCb(a[ai], b[bi]))
            {
                ai++;
                bi++;
            }
            else
            {
                
            }
        }
    }

    public static void FindDifferences(UndertaleData orig, UndertaleData modded)
    {
    }
}
