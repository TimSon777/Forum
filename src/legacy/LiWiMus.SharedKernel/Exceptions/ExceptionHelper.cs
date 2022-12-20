namespace LiWiMus.SharedKernel.Exceptions;

public static class ExceptionHelper
{
    public static void ThrowWhenArgumentNull<TArgument>(TArgument arg, string name)
    {
        if (arg == null)
        {
            throw new ArgumentNullException(name);
        }
    }
}