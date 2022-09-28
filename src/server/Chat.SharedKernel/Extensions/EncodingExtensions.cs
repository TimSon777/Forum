using System.Text.Json;

// ReSharper disable once CheckNamespace
namespace System.Text;

public static class EncodingExtensions
{
    public static T GetFromJson<T>(this Encoding encoding, ReadOnlyMemory<byte> src)
    {
        Guard.Against.Null(encoding);

        var str = encoding.GetString(src.ToArray());
        var obj = JsonSerializer.Deserialize<T>(str);
        
        return Guard.Against.Null(obj);
    }
}