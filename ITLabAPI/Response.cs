namespace ITLabAPI
{
    public class Response<T>
    {
        public T Value { get; set; }

        public IDictionary<string, string> Links { get; set; }
    }

}
