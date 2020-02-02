using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelData : MonoBehaviour
{

    public int levelSizeX = 5, levelSizeY = 5;

    public float initialTimer = 8f;

    public bool finalLevel = false;

    [System.Serializable]
    public class NecessaryPipes
    {  
        public int minAmount;  
        public int PrefabIndex;
    }

    public NecessaryPipes[] necessaryPipes;
}
