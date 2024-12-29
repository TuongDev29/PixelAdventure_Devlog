public class RefValue<T>
{
    public T Value { get; set; }

    public RefValue(T value)
    {
        this.Value = value;
    }
}