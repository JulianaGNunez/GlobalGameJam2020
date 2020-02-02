using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mode : MonoBehaviour{
    //FPS fPS;
    PipeManager pipeManager;

    private void Start() {
        //fPS = FindObjectOfType<FPS>();
        pipeManager = FindObjectOfType<PipeManager>();
    }

    public void Swap(string newMode) {
        string newString = newMode;

        if(newString == "3d"){
            //Call "animation" to go 3d
            //fPS.enable = true; <- Talvez fique no OnComplete() da animação
            //Send objects collected to spawn at the machine:
            //spawnRealLife.SpawnThis(pipeManager.Madeiras,pipemanager.Fitas,pipeManager.Registros);
            //Make one or more disaster occur
            pipeManager.canInteract = false;
        } else if(newString == "2d"){
            //Call "animation" to go 2d -- Arcade
            //fps.enable = false; <- Talvez fique no OnComplete() da animação
            pipeManager.canInteract = true;
            pipeManager.LayOutLevel();
        }
    }
}