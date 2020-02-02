using DG.Tweening;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public PipeManager pipeManager;
    public RoomActions roomActions;


    public void Swap(string newMode)
    {
        if (newMode == "3d" )
        {
            GetComponent<FPS>().enabled = true;
            GetComponentInChildren<Camera>().fieldOfView = 60f;

            //Call "animation" to go 3d
            //fPS.enable = true; <- Talvez fique no OnComplete() da animação`
            //Send objects collected to spawn at the machine:
            for (int i = 0; i < pipeManager.Fitas; i++)
                Instantiate(pipeManager.recursos3D[0], pipeManager.positionToSpawn.position + new Vector3(Random.Range(0, 2f), 0, 0), Quaternion.identity);

            pipeManager.Fitas = -1;
            pipeManager.AddFitas();

            for (int i = 0; i < pipeManager.Madeiras; i++)
                Instantiate(pipeManager.recursos3D[1], pipeManager.positionToSpawn.position + new Vector3(Random.Range(0, 2f), 0, 0), Quaternion.identity);

            pipeManager.Madeiras = -1;
            pipeManager.AddMadeiras();

            for (int i = 0; i < pipeManager.Registros; i++)
                Instantiate(pipeManager.recursos3D[2], pipeManager.positionToSpawn.position + new Vector3(Random.Range(0, 2f), 0, 0), Quaternion.identity);

            pipeManager.Registros = -1;
            pipeManager.AddRegistros();

            //Make one or more disaster occur
            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                switch (Random.Range(0, 4))
                {
                    case 0:
                        roomActions.BreakPipeLeft();
                        break;
                    case 1:
                        roomActions.BreakPipeRight();
                        break;
                    case 2:
                        roomActions.BreakValvePipe();
                        break;
                    case 3:
                        roomActions.BreakWall();
                        break;
                    default:
                        break;
                }
            }

            pipeManager.canInteract = false;
            pipeManager.DestroyAll();
        }
        else if (newMode == "2d" )
        {
            GetComponentInChildren<Camera>().fieldOfView = 40.9f;
            GetComponent<FPS>().enabled = false;
            DOTween.Sequence().Append(transform.DOMove(new Vector3(95.8f, 41.3f, -85.5f), 0.7f));
            //Call "animation" to go 2d -- Arcade
            //fps.enable = false; <- Talvez fique no OnComplete() da animação
            pipeManager.canInteract = true;
            pipeManager.LayOutLevel();
        }
    }
}