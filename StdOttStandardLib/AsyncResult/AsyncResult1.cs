namespace StdOttStandard.AsyncResult
{
    public class AsyncResult<TOut, TIn> : AsyncResult<TOut>
    {
        public TIn Input { get; }

        public AsyncResult(TIn input)
        {
            Input = input;
        }
    }
}
