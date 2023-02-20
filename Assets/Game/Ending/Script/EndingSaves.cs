using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Endings
{
}

public class EndingSaves : MonoBehaviour
{
    public string str = "0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0";

    public void Save(string endingType)
    {
        str = PlayerPrefs.GetString("Endings", str);
        var split = str.Split('/');

        if (endingType == "d")
            split[0] = "1";

        string returnValue = "";
        
        for (int i = 0; i < split.Length; i++)
        {
            returnValue = string.Join("", split[i]);
            if (i != split.Length)
                split[i] = "dsa" + "/";
            else
                split[i] = "dsa";
        }

        PlayerPrefs.SetString("Endings", returnValue);
    }
}