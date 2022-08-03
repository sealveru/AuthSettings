using System.Diagnostics;
using System.Text.Json;
using AuthSettings.Models;

namespace AuthSettings;

public interface IFileReader
{
    public List<SettingsResponse> ReadAllFiles();
}


public class FileReader : IFileReader
{
    private const string FolderName = "Settings";
    
    public List<SettingsResponse> ReadAllFiles()
    {
        var basePath = "";
        if (Debugger.IsAttached)
        {
            //basePath = "bin/debug/net6.0/";
        }

        var files = Directory.GetFiles(basePath + FolderName);

        return files
            .Select(File.ReadAllText)
            .Select(fileContent => JsonSerializer.Deserialize<SettingsResponse>(fileContent))
            .ToList()!;
    }
}