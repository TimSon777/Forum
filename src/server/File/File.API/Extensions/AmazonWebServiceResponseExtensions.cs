// ReSharper disable once CheckNamespace
namespace Amazon.Runtime;

public static class AmazonWebServiceResponseExtensions
{
    public static bool IsSuccess(this AmazonWebServiceResponse response)
    {
        return (int) response.HttpStatusCode 
            is >= 200 
            and <= 299;
    }
}