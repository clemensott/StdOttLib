using StdOttStandard.Converter.MultipleInputs;
using System;
using System.Collections.Generic;

namespace StdOttFramework.Converters
{
    public abstract class MultipleInputsConverter<TEventArgs> : OutputElement where TEventArgs : EventArgs
    {
        private bool isUpdating;
        private readonly List<MultipleInputsConvertEventHandler<TEventArgs>> converts = new List<MultipleInputsConvertEventHandler<TEventArgs>>();
        private readonly List<MultipleInputsConvertEventHandler<TEventArgs>> convertRefs = new List<MultipleInputsConvertEventHandler<TEventArgs>>();

        public event MultipleInputsConvertEventHandler<TEventArgs> Convert
        {
            add
            {
                converts.Add(value);
                SetOutput(-1, null);
            }
            remove
            {
                converts.Remove(value);
                SetOutput(-1, null);
            }
        }

        public event MultipleInputsConvertEventHandler<TEventArgs> ConvertRef
        {
            add
            {
                convertRefs.Add(value);
                SetOutput(-1, null);
            }
            remove
            {
                convertRefs.Remove(value);
                SetOutput(-1, null);
            }
        }

        protected void SetOutput(int changedIndex, object oldValue)
        {
            if (isUpdating) return;
            isUpdating = true;

            try
            {
                if (converts.Count > 0) SetOutputNonRef(changedIndex, oldValue);
                else if (convertRefs.Count > 0) SetOutputRef(changedIndex, oldValue);
                else Output = null;
            }
            finally
            {
                isUpdating = false;
            }
        }

        protected MultipleInputsConvertEventHandler<TEventArgs> GetLastConvert()
        {
            return converts[converts.Count - 1];
        }

        protected MultipleInputsConvertEventHandler<TEventArgs> GetLastConvertRef()
        {
            return convertRefs[convertRefs.Count - 1];
        }

        protected abstract void SetOutputNonRef(int changedIndex, object oldValue);

        protected abstract void SetOutputRef(int changedIndex, object oldValue);
    }
}
