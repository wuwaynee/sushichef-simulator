using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject
{

    public float score=0;
    public int assemblyCount=0;
    public bool isMultiple=false;
    public bool isStart = false;
    private void OnEnable()
    {
        score = 0;
        assemblyCount = 0;
        isMultiple = false;
        isStart = false;
    }
}
