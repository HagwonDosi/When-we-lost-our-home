using UnityEngine;
using System.Collections;

public class CustomMath
{
    /// <summary>
    /// 정해진 자리까지만 반올림하는 함수
    /// </summary>
    /// <param name="fDecimalPlace">반올림 할 소수점 아래 자리수</param>
    /// <param name="fOri">원래 수</param>
    /// <returns>결과값</returns>
    public static double CustomRound(int fDecimalPlace, double fOri)
    {
        float pow10 = Mathf.Pow(10, fDecimalPlace);

        double powNum = fOri * pow10;
        int nPart = (int)powNum;
        double decimalPart = powNum - nPart;

        int checkNum = (int)(decimalPart * 10);

        if(checkNum >= 5)
        {
            nPart += 1;
        }

        pow10 = Mathf.Pow(10, -fDecimalPlace);

        return (double)nPart * pow10;
    }

}
