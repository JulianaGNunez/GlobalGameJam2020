using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{

    public enum PipeDirections{
        Up = (1 << 0), //1 in decimal
        Right = (1 << 1), //2 in decimal
        Down = (1 << 2), //4 in decimal
        Left = (1 << 3), //8 in decimal
        All = 15
    }

    public enum Rotation{
        zero = 0,
        quarter = 90,
        half = 180,
        threeQuarter = 270
    }
    

    [HideInInspector]
    public bool filledPipe = false;

    [HideInInspector]
    public PipeDirections pipeDirections, exitDirection;

    [HideInInspector]
    public Animator animator;

    public ScriptablePipe scriptablePipe;

    PipeManager pipeManager;

    private void Start() {
        Init(scriptablePipe);
    }

    public void Init(ScriptablePipe scriptablePipe){ //, PipeManager newPipeManager){
        //pipeManager = newPipeManager;
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, (float)scriptablePipe.objectRotation, transform.eulerAngles.z);
        pipeDirections = (PipeDirections)scriptablePipe.pipeDirections;
    }

    public void EnterWater(PipeDirections enterDirection){
        if(filledPipe == false && pipeDirections != PipeDirections.All){
            exitDirection = (pipeDirections & enterDirection);
            if(exitDirection == 0){
                Debug.Log("Pipes não se conectam");
            }
            exitDirection = (PipeDirections)((int)pipeDirections - (int)exitDirection);

            Debug.Log("Exit Direction " + (int)exitDirection + " enterDirection: " + (int)enterDirection);
            
            if((int)exitDirection > (int)enterDirection){
                //Animação no "sentido horario"
                if(!(((int)(exitDirection) + (int)enterDirection) == 9))
                    print("hor: " + (((int)(exitDirection) + (int)enterDirection)));
                else
                    print("ant");
            }
            else{
                if(!(((int)(exitDirection) + (int)enterDirection) == 9))
                    print("ant: " + (((int)(exitDirection) + (int)enterDirection)));
                else
                    print("hor");
                // animação no sentido "anti-horario"
            }
        }
        /*
        else{
            if((int)exitDirection > (int)enterDirection){
                // Animação Esq
            }
            else{
                // Animação Dir
            }
        }
        */
    }

    // Will be called at the end of the animation
    public void CallNextPipe(){
        switch(exitDirection){
            case PipeDirections.Left:
                //pipeManager.CallEnterWave(PipeDirections.Right);
            break;
            case PipeDirections.Right:
                //pipeManager.CallEnterWave(PipeDirections.Left);
            break;
            case PipeDirections.Up:
                //pipeManager.CallEnterWave(PipeDirections.Down);
            break;
            case PipeDirections.Down:
                //pipeManager.CallEnterWave(PipeDirections.Up);
            break;
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            EnterWater(PipeDirections.Down);
        }
    }
}
