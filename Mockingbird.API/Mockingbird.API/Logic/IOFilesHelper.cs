using System.IO.Compression;
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
    
    public static string CombineZipFileForCarrierOutputs(string[] base64Requests, string[] base64Responses)
    {

            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);

            for (var i = 0; i < base64Requests.Length; i++)
            {
                var content = base64Requests[i];
                var filePath = DecodeBase64ToFile(content);
                string copiedFile1Path = Path.Combine(tempDir, $"file{i}.json");
                File.Copy(filePath, copiedFile1Path);
            }

            for (var i = 0; i < base64Responses.Length; i++)
            {
                var content = base64Responses[i];
                var filePath = DecodeBase64ToFile(content);
                string copiedFile1Path = Path.Combine(tempDir, $"file{i}.json");
                File.Copy(filePath, copiedFile1Path);
            }

            string zipFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.zip");
            ZipFile.CreateFromDirectory(tempDir, zipFilePath);

            return zipFilePath;
    }

}