using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerItens : MonoBehaviour
{

    public GameObject[] objetos;
    private int index;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void spawnarObjeto(){
        GameObject go = Instantiate(objetos[index],transform.position,transform.rotation);
    }
}
