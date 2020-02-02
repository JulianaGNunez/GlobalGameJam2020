using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayingSpot : MonoBehaviour
{
    public GameObject player = null;

    Vector3 oldPos;

    bool isOnArcade = false;
    public Camera m_Camera;

    private void Awake()
    {
        oldPos.Set(0.01f, 1f, 0f);
    }
    private void Start()
    {
        zoomIn();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //        player = other.transform.parent.gameObject;
    //}
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Jump") && player != null && !isOnArcade)
        {
            zoomIn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isOnArcade = false;
        //player = null;
    }

    //private void Update()
    //{
    //    if (Input.GetButtonDown("Jump") && !inDoTween && player != null && !isOnArcade)
    //    {
    //        zoomIn();
    //    }

    //    //if (Input.GetButtonDown("Jump") && !inDoTween && player != null)
    //    //{
    //    //    zoomOut();
    //    //}

    //}

    public void zoomIn()
    {
        isOnArcade = true;

        player.transform.GetChild(0).GetComponent<FPS>().enabled = false;
        player.transform.GetChild(0).GetComponent<FPS>().UnlockCursor();
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(player.transform.GetChild(0).GetChild(0).DOMove(transform.GetChild(0).transform.position, 0.7f));
        //mySequence.Append(player.transform.GetChild(0).DOMove(transform.GetChild(0).transform.rotation,  0.7f));
        mySequence.Join((player.transform.GetChild(0).GetChild(0).DORotate(transform.GetChild(0).transform.eulerAngles, 0.7f)));
        //mySequence.Play();
        player.GetComponentInChildren<ModeManager>().Swap("2d");

    }
    public void zoomOut()
    {
        isOnArcade = false;

        Debug.Log("Fuiiii!");
        player.transform.GetChild(0).GetComponent<FPS>().enabled = true;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(player.transform.GetChild(0).GetChild(0).DOLocalMove(oldPos, 0.7f));

        player.GetComponentInChildren<ModeManager>().Swap("3d");
    }
}
