using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public int gridSizex, gridSizeY;

    int currentWaterIndexX = 0, currentWaterIndexY = 0;

    public GameObject pipePrefab;

    //public GameObject pipesHolder;

    // Public LevelData[] levelsData <- UnityAction?

    public List<Vector2> randomPositions;

    private void Start() {
        
    }

    void LayOutLevel()
    {
        int objectsLeft = gridSizex * gridSizeY;
        GameObject pipeObject;
        for(int i = 0; i < gridSizex; ++i){
            for (int j = 0; j < gridSizeY; ++j)
            {
                //randomPositions.Add(Vector2(i, j));
                // if(levelsData[0].endTile.x != i && levelsData[0].endTile.y != j)
                if(true){
                    pipeObject = Instantiate(pipePrefab);
                    pipeObject.transform.parent = this.transform;
                    pipeObject.transform.localPosition = new Vector3(i, j, 0);
                }
            }
        }

        // Colocar Finish



        // COlocar peças minimas

        // Colocar full random
    }
}
