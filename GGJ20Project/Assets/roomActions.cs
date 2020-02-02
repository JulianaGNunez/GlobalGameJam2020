using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomActions : MonoBehaviour
{
    public GameObject fakeWall;
    public GameObject Stream;
    public GameObject valveStream;
    public Animator buracoAnim;

    public GameObject madeira;
    // Start is called before the first frame update
    void Start()
    {
        BreakWall();
        BreakValvePipe();
    }

    // Update is called once per frame
    void Update()
    {
        if(madeira.activeSelf)
        {
            Stream.SetActive(false);
        }
    }

    void BreakValvePipe(){
        valveStream.SetActive(true);

    }

    void BreakWall()
    {
        Stream.SetActive(true);
        Destroy(fakeWall);
    }
}
