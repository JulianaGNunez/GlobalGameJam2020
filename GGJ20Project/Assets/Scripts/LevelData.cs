using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelData : MonoBehaviour
{

    [System.Serializable]
    public class NecessaryPipes
    {  
    public int minAmount;  
    public ScriptablePipe scriptablePipe;
    }
    

    public int pipeEndX, pipeEndY;

    public NecessaryPipes[] necessaryPipes;



}
