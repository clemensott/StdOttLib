namespace StdOttStandard.AsyncResult
{
    public class AsyncResultS<TInOut> : AsyncResult<TInOut>
    {
        public TInOut Input { get; }

        public AsyncResultS(TInOut input)
        {
            Input = input;
        }
    }
}
