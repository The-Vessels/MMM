using System;
using System.IO;
using PleOps.XdeltaSharp.Decoder;
using UndertaleModLib;

public class XDeltaTest
{
    public static void Test()
    {
        const string datapath  = "TestData/deltarune-ch4-mac.win";
        const string deltapath = "TestData/chapter4.xdelta";
        const string outpath   = "TestData/deltarune-ch4-modded-mac.win";

        using var datastream   = File.Open(datapath,  FileMode.Open);
        using var deltastream  = File.Open(deltapath, FileMode.Open);
        using var moddedstream = new MemoryStream();

        using var decoder = new Decoder(datastream, deltastream, moddedstream);
        decoder.ProgressChanged += progress => Console.WriteLine($"MY PROGRESS IS {progress}");

        decoder.Run();

        UndertaleData orig   = UndertaleIO.Read(datastream);
        UndertaleData modded = UndertaleIO.Read(moddedstream);
    }
}
