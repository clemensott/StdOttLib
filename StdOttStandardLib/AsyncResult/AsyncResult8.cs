namespace StdOttStandard.AsyncResult
{
    public class AsyncResult<TOut, TIn0, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7> : AsyncResult<TOut>
    {
        public TIn0 Input0 { get; }

        public TIn1 Input1 { get; }

        public TIn2 Input2 { get; }

        public TIn3 Input3 { get; }

        public TIn4 Input4 { get; }

        public TIn5 Input5 { get; }

        public TIn6 Input6 { get; }

        public TIn7 Input7 { get; }

        public AsyncResult(TIn0 input0, TIn1 input1, TIn2 input2, TIn3 input3,
            TIn4 input4, TIn5 input5, TIn6 input6, TIn7 input7)
        {
            Input0 = input0;
            Input1 = input1;
            Input2 = input2;
            Input3 = input3;
            Input4 = input4;
            Input5 = input5;
            Input6 = input6;
            Input7 = input7;
        }
    }
}
