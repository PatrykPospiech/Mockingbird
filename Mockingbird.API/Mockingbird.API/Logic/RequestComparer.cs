namespace Mockingbird.API.Logic;

public class RequestComparer
{
    public static List<string> GetDifferencesLineByLine(string request1, string request2)
    {
        var lines1 = request1.Split("\n");
        var lines2 = request2.Split("\n");
        
        var differences = new List<string>();
        for (var i = 0; i < lines1.Length; i++)
        {
            if (lines1[i] != lines2[i])
            {
                differences.Add($"Line {i + 1}: {lines1[i]} != {lines2[i]}");
            }
        }
        return differences;
    }    
}