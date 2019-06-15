namespace StdOttStandard
{
    public class RefValue<T>
    {
        public T Value { get; set; }

        public RefValue()
        {
        }

        public RefValue(T value)
        {
            Value = value;
        }

        public static implicit operator T(RefValue<T> obj)
        {
            return obj.Value;
        }
    }
}
