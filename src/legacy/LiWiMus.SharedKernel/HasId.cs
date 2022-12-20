namespace LiWiMus.SharedKernel;

public class HaveId<T>
{
    public T Id { get; set; }
}

public class HasId : HaveId<int> { }