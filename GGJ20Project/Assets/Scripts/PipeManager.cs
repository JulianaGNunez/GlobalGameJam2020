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
    float bufferInput_max = 0.1f;

    Vector2 selectedTile_pos = new Vector2();

    public GameObject selectedTile_selector;
    public GameObject selectedTile_marker;

    public GameObject[,] matriz;
    public GameObject matrizObjectsHolder;

    GameObject firstTileToChange;


    [Header("Bordas da tela")]
    public Sprite bdTl_left_up;
    public Sprite bdTl_up;
    public Sprite bdTl_up_right;
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

    }

    void LayOutLevel()
    {
        selectedTile_marker.transform.localPosition = new Vector3(-1000, -1000, 0);

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
                    pipeObject = Instantiate(pipePrefab[Random.Range(0, pipePrefab.Length)]);
                    pipeObject.transform.SetParent(matrizObjectsHolder.transform);
                    pipeObject.transform.localPosition = new Vector3(i * 100, j * 100, 0);
                    matriz[i, j] = pipeObject;
                }
            }
        }

        var pipeIni_go = Instantiate(pipe_inicio);
        pipeIni_go.transform.SetParent(matrizObjectsHolder.transform);
        pipeIni_go.transform.localPosition = new Vector3(-100, (gridSizeY - 1) * 100);

        var pipeFim_go = Instantiate(pipe_fim);
        pipeFim_go.transform.SetParent(matrizObjectsHolder.transform);
        pipeFim_go.transform.localPosition = new Vector3(gridSizeX * 100, 0);


        //var tempGo = new GameObject().AddComponent(typeof(Image));
        //for (int i = 0; i < gridSizeX; ++i)
        //{
        //    tempGo.GetComponent<Image>().sprite = ;
        //    tempGo.transform.SetParent(matrizObjectsHolder.transform);
        //    tempGo.transform.localPosition = new Vector3(i * 100, j * 100, 0);
        //    Instantiate(tempGo);


        //}
        //for (int i = 0; i < gridSizeY; ++i)
        //{
        //    tempGo.GetComponent<Image>().sprite = ;
        //    tempGo.transform.SetParent(matrizObjectsHolder.transform);
        //    tempGo.transform.localPosition = new Vector3(i * 100, j * 100, 0);
        //    Instantiate(tempGo);


        //}


        // Colocar Finish


        // Colocar full random
    }

}
