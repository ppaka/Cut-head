using UnityEngine;
public static class PlayerPosition
{
    public static int Calculate()
    {
        var i = PlayerPrefs.GetInt("gameProgress", 0);
        int value;

        if (i < 0)
            value = 0;
        else if (i < 30)
            value = 1;
        else if (i < 60)
            value = 2;
        else if (i < 90)
            value = 3;
        else if (i < 130)
            value = 4;
        else if (i < 170)
            value = 5;
        else
            value = 6;
        
        return value;
    }
}