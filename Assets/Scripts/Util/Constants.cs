using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EOperation : int
{
    Addition = 0,
    Subtraction,
    Multiplication,
    Division
}

public static class EOperationExtension
{
    const short maxAdd = 51;
    const short maxSubs = 31;
    const short maxDivision = 6;
    const short maxMultiplication = 5;
    public static string Symbol(this EOperation i) => i switch
    {
        EOperation.Addition => "+",
        EOperation.Subtraction => "-",
        EOperation.Multiplication => "x",
        EOperation.Division => "/",
        _ => "NaN"
    };

    public static int Calculate(this EOperation i, int n1, int n2) => i switch
    {
        EOperation.Addition => n1 + n2,
        EOperation.Subtraction => n1 - n2,
        EOperation.Multiplication => n1 * n2,
        EOperation.Division => n1 / n2,
        _ => 0
    };

    public static Material GetMaterial(this EOperation i, Material positive, Material negative) => i switch
    {
        EOperation.Addition => positive,
        EOperation.Subtraction => negative,
        EOperation.Multiplication => positive,
        EOperation.Division => negative,
        _ => positive
    };

    public static short GetMaxValue(this EOperation i) => i switch
    {
        EOperation.Addition => maxAdd,
        EOperation.Subtraction => maxSubs,
        EOperation.Multiplication => maxMultiplication,
        EOperation.Division => maxDivision,
        _ => 0
    };

    public static EOperation GetRandomOperation(bool positive, out short number)
    {
        EOperation operation = EOperation.Addition;
        if (positive)
            operation = Random.Range(0, 2) == 0 ? EOperation.Addition : EOperation.Multiplication;
        else
            operation = Random.Range(0, 2) == 0 ? EOperation.Subtraction : EOperation.Division;

        number = (short)Random.Range(1,operation.GetMaxValue());
        return operation;
    }
}