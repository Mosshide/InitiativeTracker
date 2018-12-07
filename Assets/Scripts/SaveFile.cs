using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFile
{
    public string version = "0.1";
    public Dictionary<string, int> data = new Dictionary<string, int>();
    public int turn = 0;
    public int theme = 0;
}
