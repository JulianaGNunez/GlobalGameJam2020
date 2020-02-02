using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomActions : MonoBehaviour
{
    public GameObject fakeWall;
    public GameObject Stream;
    public GameObject valveStream;

    public GameObject pipeSteamLeft,pipeSteamRight;

    public GameObject buraco, valve,valve1;

    public GameObject madeira,madeira2, pipeleft, pipeRight;

    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            print("eu estou aqui");
            BreakValvePipe();
            BreakWall();
            BreakPipeLeft();
            BreakPipeRight();

        }
        if(madeira2.activeSelf)
        {
            Stream.SetActive(false);
        }

        if(valve1.activeSelf){
            valveStream.SetActive(false);
        }

        
    }

     IEnumerator WaitAndForSplash(GameObject go)
     {
        yield return new WaitForSeconds(1);
        go.SetActive(true);
     }

   public void BreakValvePipe(){
        valveStream.SetActive(true);
        valve1.SetActive(false);
        valve.GetComponent<Animator>().SetBool("active", false);
        coroutine = WaitAndForSplash(valveStream);
        StartCoroutine(coroutine);

    }

    public void BreakWall()
    {
        Stream.SetActive(true);
        madeira.SetActive(false);
        madeira2.SetActive(false);
        fakeWall.SetActive(false);
        buraco.GetComponent<Animator>().SetBool("active", false);
        coroutine = WaitAndForSplash(Stream);
        StartCoroutine(coroutine);
    }

    public void BreakPipeLeft(){
        pipeSteamLeft.SetActive(true);
        pipeleft.GetComponent<Animator>().SetBool("active", false);
    }

    public void BreakPipeRight(){
        pipeSteamRight.SetActive(true);
        pipeRight.GetComponent<Animator>().SetBool("active", false);
    }
}
