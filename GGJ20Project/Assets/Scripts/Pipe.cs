using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class Pipe : MonoBehaviour
{

    public enum PipeDirections
    {
        None = 0,
        Up = (1 << 0), //1 in decimal
        Right = (1 << 1), //2 in decimal
        Down = (1 << 2), //4 in decimal
        Left = (1 << 3), //8 in decimal
        All = 15
    }

    public enum Rotation
    {
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

    public int pipeDirectionsInt;

    public bool endPipe = false;

    PipeManager pipeManager;

    public GameObject particlesCollectItem;

    public void Init(PipeManager newPipeManager)
    {
        pipeManager = newPipeManager;
        pipeDirections = (PipeDirections)pipeDirectionsInt;
    }

    public void RotateClockwise(bool isClockwise)
    {
        Image fill = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        fill.fillClockwise = isClockwise;
        if (pipeDirectionsInt == 5)
        {
            if (isClockwise)
            {
                fill.fillOrigin = (int)Image.OriginVertical.Bottom;
            }
            else
            {
                fill.fillOrigin = (int)Image.OriginVertical.Top;
            }
        }
        if (pipeDirectionsInt == 10)
        {
            if (isClockwise)
            {
                fill.fillOrigin = (int)Image.OriginHorizontal.Left;
            }
            else
            {
                fill.fillOrigin = (int)Image.OriginHorizontal.Right;
            }
        }
        fill.DOFillAmount(1, 3f).SetEase(Ease.Linear).OnComplete(
            () =>
            {
                CallNextPipe();
            }
        );
    }

    public void EnterWater(PipeDirections enterDirection)
    {
        bool clockwise = true;

        Debug.Log("Pipes SÃO IRADAS");

        if (pipeDirections == PipeDirections.None)
        {
            //Troca do jogo 2D para 3D
            Debug.Log("Pipes não se conectam");
            pipeManager.modeManager.Swap("3d");
        }

        if (endPipe)
        {
            WonTheLevel();
        }

        if (filledPipe == false && pipeDirections != PipeDirections.All)
        {
            exitDirection = (pipeDirections & enterDirection);
            if (exitDirection != 0)
            {
                Debug.Log("Pipes se conectam");

                checkItemToCollect();

                filledPipe = true;

                if (!endPipe)
                {
                    // Still not won the level
                    exitDirection = (PipeDirections)((int)pipeDirections - (int)exitDirection);
                    Debug.Log("Exit Direction " + (int)exitDirection + " enterDirection: " + (int)enterDirection);

                    if ((int)exitDirection > (int)enterDirection)
                    {
                        //Animação no "sentido anti-horario"
                        if (!(((int)(exitDirection) + (int)enterDirection) == 9))
                            clockwise = false;
                        else
                            clockwise = true;
                    }
                    else
                    {
                        //Animação no sentido "horario"
                        if (!(((int)(exitDirection) + (int)enterDirection) == 9))
                            clockwise = true;
                        else
                            clockwise = false;
                    }

                    RotateClockwise(clockwise);
                }
                else
                {
                    // Won the Level!
                    WonTheLevel();
                }
            }
            else
            {
                // Troca do jogo 2D para 3D
                Debug.Log("Pipes não se conectaram");
                pipeManager.DestroyAll();
                pipeManager.modeManager.Swap("3d");
            }
        }
    }

    public void WonTheLevel()
    {
        Image fill = transform.GetChild(1).GetChild(0).GetComponent<Image>();

        fill.DOFillAmount(1, 3f).SetEase(Ease.Linear).OnComplete(
            () =>
            {
                AskForNextLevel();
            }
        );

        print("You won the level!");
    }

    public void AskForNextLevel()
    {
        print("AskedForLevel");
        pipeManager.DestroyAll();
    }

    // Will be called at the end of the animation
    public void CallNextPipe()
    {
        switch (exitDirection)
        {
            case PipeDirections.Left:
                pipeManager.CallEnterWater(PipeDirections.Right);
                break;
            case PipeDirections.Right:
                pipeManager.CallEnterWater(PipeDirections.Left);
                break;
            case PipeDirections.Up:
                pipeManager.CallEnterWater(PipeDirections.Down);
                break;
            case PipeDirections.Down:
                pipeManager.CallEnterWater(PipeDirections.Up);
                break;
        }
    }

    void checkItemToCollect()
    {
        // Collect an item if it is in the way
        if (pipeManager.recursosPositions.Any(x => x.transform.localPosition == new Vector3((int)transform.localPosition.x, (int)transform.localPosition.y)))
        {
            var item = pipeManager.recursosPositions.Find(x => x.transform.localPosition == new Vector3((int)transform.localPosition.x, (int)transform.localPosition.y));
            pipeManager.recursosPositions.Remove(item);

            DOTween.Sequence().SetDelay(1.4f).OnComplete(() =>
            {
                if (item.name.Contains("madeira"))
                    pipeManager.AddMadeiras();
                else if (item.name.Contains("fita"))
                    pipeManager.AddFitas();
                else if (item.name.Contains("registro"))
                    pipeManager.AddRegistros();

                Instantiate(particlesCollectItem, item.transform.position + new Vector3(0, 0, -1.5f), Quaternion.identity);

                Destroy(item);
            });

        }
    }

}
