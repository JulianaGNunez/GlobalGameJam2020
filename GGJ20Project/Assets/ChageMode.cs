using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mode : MonoBehaviour{
    public void Swap(string newMode) {
        //"PipeMode" e "CharMode" sao substituidos pelo nome da classe dos controles de cada modo de jogo
        if(newMode == "3d"){
            FindObjectOfType<PipeMode>().enabled = false;
            FindObjectOfType<CharMode>().enabled = true;
        } else if(newMode == "2d"){
            FindObjectOfType<PipeMode>().enabled = true;
            FindObjectOfType<CharMode>().enabled = false;
        }
    }
}
