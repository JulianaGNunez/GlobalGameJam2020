﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PipeData", menuName = "ScriptableObjects/PipeData", order = 1)]
public class ScriptablePipe : ScriptableObject
{
    public GameObject pipePrefab;
    public Pipe.Rotation objectRotation = Pipe.Rotation.zero;
    public int pipeDirections;
    public bool endPipe = false;
}