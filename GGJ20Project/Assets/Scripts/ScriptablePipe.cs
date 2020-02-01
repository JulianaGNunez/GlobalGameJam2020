using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PipeData", menuName = "ScriptableObjects/PipeData", order = 1)]
public class ScriptablePipe : ScriptableObject
{
    public Pipe.Rotation objectRotation = Pipe.Rotation.zero;
    public int pipeDirections;
    public Animator animator;
    public bool endPipe = true;
}
