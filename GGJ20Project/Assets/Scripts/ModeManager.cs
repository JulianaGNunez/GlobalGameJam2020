using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour{
   public PipeManager pipeManager;

    public void Swap(string newMode) {
        if(newMode == "3d"){
            //Call "animation" to go 3d
            //fPS.enable = true; <- Talvez fique no OnComplete() da animação
            //Send objects collected to spawn at the machine:
            //spawnRealLife.SpawnThis(pipeManager.Madeiras,pipemanager.Fitas,pipeManager.Registros);
            for (int i = 0; i < pipeManager.Fitas; i++)
                Instantiate(pipeManager.recursos3D[0], pipeManager.positionToSpawn.position + new Vector3(Random.Range(0,2f),0,0), Quaternion.identity);

            pipeManager.Fitas = -1;
            pipeManager.AddFitas(); 
            
            for (int i = 0; i < pipeManager.Madeiras; i++)
                Instantiate(pipeManager.recursos3D[1], pipeManager.positionToSpawn.position + new Vector3(Random.Range(0,2f),0,0), Quaternion.identity);

            pipeManager.Madeiras = -1;
            pipeManager.AddMadeiras();
            
            for (int i = 0; i < pipeManager.Registros; i++)
                Instantiate(pipeManager.recursos3D[2], pipeManager.positionToSpawn.position + new Vector3(Random.Range(0,2f),0,0), Quaternion.identity);

            pipeManager.Registros = -1;
            pipeManager.AddRegistros();

            //Make one or more disaster occur
            pipeManager.canInteract = false;
            pipeManager.DestroyAll();
        } else if(newMode == "2d"){
            //Call "animation" to go 2d -- Arcade
            //fps.enable = false; <- Talvez fique no OnComplete() da animação
            pipeManager.canInteract = true;
            pipeManager.LayOutLevel();
        }
    }
}