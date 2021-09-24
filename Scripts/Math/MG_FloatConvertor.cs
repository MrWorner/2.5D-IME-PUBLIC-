using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public static class MG_FloatConvertor
{
    /// <summary>
    /// Перевод из String в Float
    /// </summary>
    /// <param name="_str"></param>
    /// <returns></returns>
    public static float ProcessFloat(string _str)
    {
        float _result;
        if (_str != null)
        {
            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            _result = float.Parse(_str, NumberStyles.Any, ci);

            //_result = Convert.ToSingle(_str, new CultureInfo("en-US"));
            //float.TryParse(_str, NumberStyles.Any, new CultureInfo("en-US"), out _result);
            //return _result = float.Parse(_str);
            return _result;
        }

        else
            return 0f;
    }
}
