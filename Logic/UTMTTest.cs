using System;
using System.IO;
using UndertaleModLib;

namespace mmm;

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
}
