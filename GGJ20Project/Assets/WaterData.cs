using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterData : MonoBehaviour
{

    public ParticleSystem.MainModule StreamMainModule;
    public ParticleSystem.MainModule FoamMainModule;

    [Header("MAIN STREAM")]
    public float MainStream_MinSize;
    public float MainStream_MaxSize;

    public float StreamSpeed;

    public float StreamShapeRandom;
    public float StreamShapeSpharize;

    ParticleSystem.ShapeModule StreamShapeR;
    [Range(0, 1)]
    ParticleSystem.ShapeModule StreamShapeS;

    [Header("FOAM")]
    public float Foam_MinSize;
    public float Foam_MaxSize;

    public float FoamSpeed;


    void Start()
    {
        ParticleSystem Stream = GetComponent<ParticleSystem>();
        StreamMainModule = Stream.main;
        StreamMainModule.startSize = new ParticleSystem.MinMaxCurve(MainStream_MinSize, MainStream_MaxSize);

        StreamShapeR = Stream.shape;
        StreamShapeR.randomDirectionAmount = StreamShapeRandom;

        StreamShapeS = Stream.shape;
        StreamShapeS.randomDirectionAmount = StreamShapeSpharize;

        ParticleSystem FoamStream = GameObject.Find("Foam").GetComponent<ParticleSystem>();
        FoamMainModule = FoamStream.main;
        FoamMainModule.startSize = new ParticleSystem.MinMaxCurve(Foam_MinSize, Foam_MaxSize);
    }

    private void Update()
    {
        StreamMainModule.startSize = new ParticleSystem.MinMaxCurve(MainStream_MinSize, MainStream_MaxSize);
        FoamMainModule.startSize = new ParticleSystem.MinMaxCurve(Foam_MinSize, Foam_MaxSize);

        StreamShapeR.randomDirectionAmount = StreamShapeRandom;
        StreamShapeS.randomDirectionAmount = StreamShapeSpharize;

    }
}
