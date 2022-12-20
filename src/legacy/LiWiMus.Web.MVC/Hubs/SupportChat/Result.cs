namespace LiWiMus.Web.MVC.Hubs.SupportChat;

public class Success<T> : Result
{
    public T Value { get; }

    public Success(T value)
    {
        IsSuccess = true;
        Value = value;
    }
}

public class Success : Result
{
    public Success()
    {
        IsSuccess = true;
    }
}

public class Failure : Result
{
    public string Error { get; }

    public Failure(string error)
    {
        IsSuccess = false;
        Error = error;
    }
}

public class Result
{
    public bool IsSuccess { get; set; }
    
    public static Success<T> Success<T>(T success)
    {
        return new Success<T>(success);
    }
    
    public static Success Success()
    {
        return new Success();
    }
    
    public static Failure Failure(string error = "")
    {
        return new Failure(error);
    }
}