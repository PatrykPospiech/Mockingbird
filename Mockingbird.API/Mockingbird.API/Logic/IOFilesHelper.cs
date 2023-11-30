using System.Text;

namespace Mockingbird.API.Logic;

public static class IOFilesHelper
{
    public static string LocationForRequestAndResponses = Directory.GetCurrentDirectory();
    public static void SaveFile(string outputFileData, string fileName, ILogger logger)
    {
        var fullPathToFile = LocationForRequestAndResponses + fileName + ".json";
        
        logger.LogInformation($"save file to: \"{fullPathToFile}\" ");
        using (FileStream fs = File.Create(fullPathToFile))
        {
            byte[] info = new UTF8Encoding(true).GetBytes(outputFileData);
            fs.Write(info, 0, info.Length);
        }
    }
}