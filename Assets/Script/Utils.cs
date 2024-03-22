using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : IComparer<Data>
{
    private static Utils instance;

    private Utils() { }

    public static Utils Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new Utils();
            }
            return instance;
        }
    }

    public int Compare(Data x, Data y)
    {
        return y.Count.CompareTo(x.Count);
    }
}
