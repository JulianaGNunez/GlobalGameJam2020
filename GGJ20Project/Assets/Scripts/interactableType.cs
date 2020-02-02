using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactableType : MonoBehaviour
{
    public enum interacType
    {
        pipes,
        wall,
        valve
    }
    public interacType tipo;
    public bool isBroken = false;

}
