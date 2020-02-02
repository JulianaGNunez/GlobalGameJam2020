using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class editshaderinRuntime : MonoBehaviour
{
 public Material rend;
 [Range(0.0f,1.0f)]
 public float time;

    private void Update() {

    rend.SetFloat("_TimeX", time);

    }
}
