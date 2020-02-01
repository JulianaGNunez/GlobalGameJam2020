using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public int gridSizeX, gridSizeY;

    int currentWaterIndexX = 0, currentWaterIndexY = 0;

    public GameObject[] pipePrefab;

    //public GameObject pipesHolder;

    // Public LevelData[] levelsData <- UnityAction?

    public List<Vector2> randomPositions;

    float bufferInput = 0;
    float bufferInput_max = 0.1f;

    Vector2 selectedTile_pos = new Vector2();

    public GameObject selectedTile_selector;
    public GameObject selectedTile_marker;

    public GameObject[,] matriz;

    private Pipe currentPipe = null;


    private Vector2 GetPositionFinish(Vector2 pos)
    {
        randomPositions.Remove(pos);
        return pos;
    }

    private Vector2 GetRandomPosition()
    {
        int randomIndex = (int)Random.Range(0, randomPositions.Count);
        Vector2 pos = randomPositions[randomIndex];
        randomPositions.Remove(pos);
        return pos;
    }
    private void Start()
    {
        LayOutLevel();
    }

    private void Update()
    {
        if (bufferInput <= bufferInput_max)
            bufferInput += Time.deltaTime;

        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (movement.sqrMagnitude > 0 && bufferInput > bufferInput_max)
        {
            Vector2 tempCheck = (selectedTile_pos + movement);
            //if ((tempCheck.x <= gridSizeX && tempCheck.x >= 0) && (tempCheck.y <= gridSizeY && tempCheck.y >= 0))
            //{
            bufferInput = 0;
            selectedTile_pos += movement * 100;
            selectedTile_selector.transform.localPosition = selectedTile_pos;
            //}
        }

        if (Input.GetKeyDown("space")){
            CallEnterWater(Pipe.PipeDirections.Down);
        }

    }

    void LayOutLevel()
    {
        matriz = new GameObject[gridSizeX, gridSizeY];

        int objectsLeft = gridSizeX * gridSizeY;
        GameObject pipeObject;
        for (int i = 0; i < gridSizeX; ++i)
        {
            for (int j = 0; j < gridSizeY; ++j)
            {
                randomPositions.Add(new Vector2(i, j));

                // Deveria estar lá embaixo
                if (true)
                {
                    pipeObject = Instantiate(pipePrefab[Random.Range(0, pipePrefab.Length - 1)]);
                    pipeObject.transform.SetParent(this.transform);
                    pipeObject.transform.localPosition = new Vector3(i * 100, j * 100, 0);
                    matriz[i, j] = pipeObject;
                    pipeObject.GetComponent<Pipe>().Init(this);
                }
            }
        }

        // Colocar Finish


        // Colocar full random

        currentPipe = matriz[1,0].GetComponent<Pipe>();
    }

    public void CallEnterWater(Pipe.PipeDirections enterDirection){
        Vector2 nextPipe = currentPipe.transform.localPosition / 100;
        switch(enterDirection){
            case Pipe.PipeDirections.Left:
                nextPipe += new Vector2(1, 0);
            break;
            case Pipe.PipeDirections.Right:
                nextPipe += new Vector2(-1, 0);
            break;
            case Pipe.PipeDirections.Down:
                nextPipe += new Vector2(0, 1);
            break;
            case Pipe.PipeDirections.Up:
                nextPipe += new Vector2(0, -1);
            break;
        }

        currentPipe = matriz[(int)nextPipe.x, (int)nextPipe.y].GetComponent<Pipe>();

        currentPipe.GetComponent<Pipe>().EnterWater(enterDirection);
    }

    void ChangeTiles()
    {

    }


}
