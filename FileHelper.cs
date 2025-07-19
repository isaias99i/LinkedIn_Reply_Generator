namespace LinkedInScraperOpenAI;

public static class FileHelper
{
    public static string GenerateUniqueOutputPath(string baseFileName)
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(basePath, baseFileName);
        int count = 1;
        while (File.Exists(path))
        {
            path = Path.Combine(basePath, Path.GetFileNameWithoutExtension(baseFileName) + $"_{count}" + Path.GetExtension(baseFileName));
            count++;
        }
        return path;
    }
}
