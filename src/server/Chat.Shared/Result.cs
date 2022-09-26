namespace Chat.Shared;

public class Result
{
    public IReadOnlyCollection<string> Errors { get; }
        
    public bool Succeeded { get; }

    public override string ToString()
    {
        var errors = string.Join(Environment.NewLine, Errors);

        if (Succeeded)
        {
            return "Result is success";
        }

        return string.Join(Environment.NewLine,
            "Result is failure",
            "Errors:",
            errors);
    }

    private Result(bool succeeded, params string[] errors)
    {
        Succeeded = succeeded;
        Errors = errors;
    }

    public static Result Success()
    {
        return new Result(true);
    }
    
    public static Result Failure(params string[] errors)
    {
        return new Result(false, errors);
    }
}