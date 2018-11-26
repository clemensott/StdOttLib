﻿using System;

namespace StdOttUwp.Converters
{
    public class ULongConverter : ToStringTwoWayConverter<ulong>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, string language, out ulong newValue)
        {
            return ulong.TryParse(newText, out newValue);
        }
    }
}
