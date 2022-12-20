namespace LiWiMus.SharedKernel.Helpers;

public static class FileHelper
{
    public static void DeleteIfExists(string pathToFile)
    {
        if (File.Exists(pathToFile))
        {
            File.Delete(pathToFile);
        }
    }
}