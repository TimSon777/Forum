namespace FileMetadata.Consumer.Exceptions;

public class FileIdNotFoundException : Exception
{
    public FileIdNotFoundException()
        : base("File id not found")
    { }
}