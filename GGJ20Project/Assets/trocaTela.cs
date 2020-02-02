using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trocaTela : MonoBehaviour
{
  
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            Initiate.Fade("3D", Color.black, 2);
        }
    }
}
