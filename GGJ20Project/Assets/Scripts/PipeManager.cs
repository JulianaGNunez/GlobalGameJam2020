using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeManager : MonoBehaviour
{
    public int gridSizeX, gridSizeY;

    int currentWaterIndexX = 0, currentWaterIndexY = 0;

    public GameObject[] pipePrefab;
    public GameObject pipe_inicio;
    public GameObject pipe_fim;
    //public GameObject pipesHolder;

    // Public LevelData[] levelsData <- UnityAction?

    public List<Vector2> randomPositions;

    float bufferInput = 0;
    float bufferInput_max = 0.2f;

    Vector2 selectedTile_pos = new Vector2();

    public GameObject selectedTile_selector;
    public GameObject selectedTile_marker;

    public GameObject[,] matriz;
    public GameObject pipesObjectsHolder;

    GameObject firstTileToChange;

    private Pipe currentPipe = null;

    public List<GameObject> recursos = new List<GameObject>();
    List<Vector2> recursosPositions = new List<Vector2>();
    public GameObject recursosObjectsHolder;

    [Header("Bordas da tela")]
    public Sprite bdTl_left_up;
    public Sprite bdTl_up_outer;
    public Sprite bdTl_up;
    public Sprite bdTl_up_right;
    public Sprite bdTl_up_right_outer;
    public Sprite bdTl_right;
    public Sprite bdTl_right_down;
    public Sprite bdTl_down;
    public Sprite bdTl_down_left;
    public Sprite bdTl_left;

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
            if ((tempCheck.x < gridSizeX && tempCheck.x >= 0) && (tempCheck.y < gridSizeY && tempCheck.y >= 0))
            {
                bufferInput = 0;
                selectedTile_pos += movement;
                selectedTile_selector.transform.localPosition = selectedTile_pos * 100;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            var selectedGO = matriz[(int)selectedTile_selector.transform.localPosition.x / 100, (int)selectedTile_selector.transform.localPosition.y / 100];
            if (!selectedGO.name.Contains("pipe_empty"))
            {
                if (firstTileToChange == null)
                {
                    selectedTile_marker.transform.localPosition = selectedTile_selector.transform.localPosition;
                    firstTileToChange = selectedGO;
                }
                else
                {
                    var secondTileToChange = selectedGO;

                    matriz[(int)secondTileToChange.transform.localPosition.x / 100, (int)secondTileToChange.transform.localPosition.y / 100] = firstTileToChange;
                    matriz[(int)firstTileToChange.transform.localPosition.x / 100, (int)firstTileToChange.transform.localPosition.y / 100] = secondTileToChange;

                    var saveTileToChange = secondTileToChange.transform.localPosition;
                    secondTileToChange.transform.localPosition = firstTileToChange.transform.localPosition;
                    firstTileToChange.transform.localPosition = saveTileToChange;

                    selectedTile_marker.transform.localPosition = new Vector3(-1000, -1000, 0);
                    firstTileToChange = null;
                }
            }
        }

        if (Input.GetKeyDown("k"))
        {
            CallEnterWater(Pipe.PipeDirections.Down);
        }

    }

    void LayOutLevel()
    {
        selectedTile_marker.transform.localPosition = new Vector3(-1000, -1000, 0);

        selectedTile_pos = new Vector2(0, gridSizeY - 1);
        selectedTile_selector.transform.localPosition = selectedTile_pos * 100;

        matriz = new GameObject[gridSizeX, gridSizeY];

        //Camera.main.transform.position = new Vector3(gridSizeX*100,);

        int objectsLeft = gridSizeX * gridSizeY;
        GameObject pipeObject;

        var posIni = new Vector3(-100, (gridSizeY - 1) * 100);
        var posFim = new Vector3(gridSizeX * 100, 0);

        GameObject pipePrefabSpawn = null;

        for (int i = 0; i < gridSizeX; ++i)
        {
            for (int j = 0; j < gridSizeY; ++j)
            {
                randomPositions.Add(new Vector2(i, j));
                var tempPos = new Vector3(i * 100, j * 100, 0);
                // Deveria estar lá embaixo
                if (true)
                {
                    if ((tempPos == posIni + new Vector3(+100, 0, 0)
                    || tempPos == posIni + new Vector3(+200, 0, 0)
                    || tempPos == posIni + new Vector3(+100, -100, 0)
                    || tempPos == posIni + new Vector3(+200, -100, 0))
                    || (tempPos == posFim + new Vector3(-100, 0, 0)
                    || tempPos == posFim + new Vector3(-200, 0, 0)
                    || tempPos == posFim + new Vector3(-100, +100, 0)
                    || tempPos == posFim + new Vector3(-200, +100, 0)
                    )
                    )
                    {
                        pipePrefabSpawn = pipePrefab[Random.Range(1, pipePrefab.Length)];
                    }
                    else
                    {
                        pipePrefabSpawn = pipePrefab[Random.Range(0, pipePrefab.Length)];
                    }

                    pipeObject = Instantiate(pipePrefabSpawn);
                    pipeObject.transform.SetParent(pipesObjectsHolder.transform);
                    pipeObject.transform.localPosition = tempPos;
                    matriz[i, j] = pipeObject;
                    pipeObject.GetComponent<Pipe>().Init(this);

                    if (!pipePrefabSpawn.name.Contains("empty") && Random.Range(0, 10f) >= 9f)
                    {
                        var recGo = Instantiate(recursos[Random.Range(0, recursos.Count)]);
                        recGo.GetComponent<Animator>().SetFloat("Offset", Random.Range(0.0f, 1.0f));
                        recGo.transform.SetParent(recursosObjectsHolder.transform);
                        recGo.transform.localPosition = tempPos;
                        recursosPositions.Add(tempPos);
                    }
                }
            }
        }

        var pipeIni_go = Instantiate(pipe_inicio);
        pipeIni_go.transform.SetParent(pipesObjectsHolder.transform);
        pipeIni_go.transform.localPosition = new Vector3(-100, (gridSizeY - 1) * 100);

        var pipeFim_go = Instantiate(pipe_fim);
        pipeFim_go.transform.SetParent(pipesObjectsHolder.transform);
        pipeFim_go.transform.localPosition = new Vector3(gridSizeX * 100, 0);

        createBorders();

        // Colocar full random

        currentPipe = matriz[1, 0].GetComponent<Pipe>();
    }

    void createBorders()
    {

        for (int i = 0; i < gridSizeY; ++i)
        {

            if (new Vector3(-100, i * 100, 0) != new Vector3(-100, (gridSizeY - 1) * 100)
                && new Vector3(-100, i * 100, 0) != new Vector3(-100, (gridSizeY - 2) * 100))
                setBorda(bdTl_left, new Vector3(-100, i * 100, 0));

            if (new Vector3(gridSizeY * 100, i * 100, 0) != new Vector3(gridSizeX * 100, 0)
                && new Vector3(gridSizeY * 100, i * 100, 0) != new Vector3(gridSizeX * 100, 100))
                setBorda(bdTl_right, new Vector3(gridSizeY * 100, i * 100, 0));
        }

        for (int i = 0; i < gridSizeX; ++i)
        {
            setBorda(bdTl_up, new Vector3(i * 100, gridSizeY * 100, 0));
            setBorda(bdTl_down, new Vector3(i * 100, -100, 0));
        }

        var posIni = new Vector3(-100, (gridSizeY - 1) * 100);
        setBorda(bdTl_up, posIni + new Vector3(0, 100, 0));
        setBorda(bdTl_left_up, posIni + new Vector3(-100, 100, 0));
        setBorda(bdTl_left, posIni + new Vector3(-100, 0, 0));
        setBorda(bdTl_down_left, posIni + new Vector3(-100, -100, 0));
        setBorda(bdTl_up_right_outer, posIni + new Vector3(0, -100, 0));
        setBorda(bdTl_up_right, new Vector3((gridSizeX * 100), (gridSizeY * 100), 0));
        setBorda(bdTl_down_left, new Vector3(-100, -100, 0));

        var posFim = new Vector3(gridSizeX * 100, 0);
        setBorda(bdTl_down, posFim + new Vector3(0, -100, 0));
        setBorda(bdTl_right_down, posFim + new Vector3(+100, -100, 0));
        setBorda(bdTl_right, posFim + new Vector3(+100, 0, 0));
        setBorda(bdTl_up_right, posFim + new Vector3(+100, +100, 0));
        setBorda(bdTl_up_outer, posFim + new Vector3(0, +100, 0));


    }

    void setBorda(Sprite sprite, Vector3 pos)
    {
        var borda = new GameObject().AddComponent(typeof(Image));
        borda.GetComponent<Image>().sprite = sprite;
        borda.transform.SetParent(pipesObjectsHolder.transform);
        borda.transform.localPosition = pos;
    }


    public void CallEnterWater(Pipe.PipeDirections enterDirection)
    {
        Vector2 nextPipe = currentPipe.transform.localPosition / 100;
        switch (enterDirection)
        {
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



    // Colocar Finish


    // Colocar full random


}
