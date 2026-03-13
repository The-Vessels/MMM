using System;
using System.Formats.Tar;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

// TODO I want to make OS-detection compile-time.
// (1) does this substantially reduce filesize and is actually better?
// (2) how easy/hard would it be to implement?

// Remember that HttpClient should 

class SteamCMDInstaller
{
    const string STEAMCMD_URL_BASE = "https://steamcdn-a.akamaihd.net";
    const string STEAMCMD_URL_PREFIX = $"/client/installer";
    const string WINDOWS_URL = $"{STEAMCMD_URL_PREFIX}/steamcmd.zip";
    const string LINUX_URL   = $"{STEAMCMD_URL_PREFIX}/steamcmd_linux.tar.gz";
    const string MAC_URL     = $"{STEAMCMD_URL_PREFIX}/steamcmd_osx.tar.gz";

    public static async Task WindowsDownload(HttpClient client, string outDir)
    {
        Stream stream = await client.GetStreamAsync(WINDOWS_URL);
        ZipFile.ExtractToDirectory(stream, outDir);
    }
    public static async Task UnixDownload(HttpClient client, string outDir)
    {
        // TODO for Linux, we have to do some weird user group things
        // TODO do I have to close any of these streams?

        string url = OperatingSystem.IsMacOS() ? MAC_URL : LINUX_URL;

        Stream stream = await client.GetStreamAsync(WINDOWS_URL);
        GZipStream gz = new GZipStream(stream, CompressionMode.Decompress);

        if (Directory.Exists(outDir))
        {
            Directory.Delete(outDir, true);
            Directory.CreateDirectory(outDir);
        }

        TarFile.ExtractToDirectory(gz, outDir, true);
    }

    public static async Task Download()
    {
        var outDir = "./steamcmd/";

        HttpClient client = new()
        {
            BaseAddress = new Uri(STEAMCMD_URL_BASE),
        };

        if (OperatingSystem.IsWindows())
        {
            await WindowsDownload(client, outDir);
        }
        else
        {
            await UnixDownload(client, outDir);
        }
    }
}

public class Steam
{
    public static void Download()
    {
        SteamCMDInstaller.Download().GetAwaiter().GetResult();
    }
}

