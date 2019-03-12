using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticInfo
{

    private static int numPlays;

    public static void AddPlay()
    {
        numPlays++;
    }

    public static int GetNumberOfPlays()
    {
        return numPlays;
    }

}
