namespace LiWiMus.SharedKernel.Helpers;

public static class PathHelper
{
    public static string ReplaceWithDirectorySeparatorChar(string rawPath, char rawDirectorySeparatorChar = ':')
    {
        return rawPath.Replace(rawDirectorySeparatorChar, Path.DirectorySeparatorChar);
    }
}