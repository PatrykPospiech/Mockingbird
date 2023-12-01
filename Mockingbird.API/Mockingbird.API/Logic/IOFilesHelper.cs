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

    public static string DecodeBase64ToFile(string base64)
    {
        byte[] fileBytes = Convert.FromBase64String(base64);

        string tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
        File.WriteAllBytes(tempFilePath, fileBytes);

        return tempFilePath;
    }
}