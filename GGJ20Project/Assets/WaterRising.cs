using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaterRising : MonoBehaviour
{
    
    float currentHealth = 100f;

    float second = 60f;

    private int activeDisasters = 2;

    public GameObject waterObject;


    public void MachineBroke(){
        ++activeDisasters;
    }

    public void MachineRepair(){
        --activeDisasters;
    }


    // Update is called once per frame
    private void FixedUpdate() {
        
        if((second -= Time.deltaTime) <= 0){
            second = 60f;
            // A rough second passed
            currentHealth -= activeDisasters;

            waterObject.transform.DOMoveY(waterObject.transform.position.y + 2f, 0.75f).SetEase(Ease.InQuad);
        }
    }
}
