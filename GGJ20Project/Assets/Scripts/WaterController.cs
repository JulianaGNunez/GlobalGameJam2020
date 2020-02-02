using UnityEngine;

public class WaterController : MonoBehaviour
{
    [Header("Objeto Agua - Usar Parent")]
    public GameObject WaterObject;

    [Header("Controlador Agua")]
    public float WaterStartLevel;
    public float WaterStartSpeed;

    public float CurrentWaterLevel;
    
    public float CurrentWaterSpeed;
    public bool isWaterRising;

    [Header("Game Over")]
    public float GameOverWaterLevel;
    public bool isWaterOnGameOver;

    public GameObject GameOverScreen;


    void Start()
    {
        isWaterOnGameOver = false;
        isWaterRising = false;

        CurrentWaterSpeed = WaterStartSpeed;
        CurrentWaterLevel = WaterStartLevel;
    }

    void Update()
    {

        if (isWaterRising)
            WaterObject.transform.Translate(Vector3.up * Time.deltaTime * CurrentWaterSpeed, Space.World);
        else
            WaterObject.transform.Translate(Vector3.up * Time.deltaTime * 0, Space.World);

        if (WaterObject.transform.position.y >= GameOverWaterLevel)
        {
            isWaterOnGameOver = true;
        } else
        {
            isWaterOnGameOver = false;
        }

        if (isWaterOnGameOver)
        {
            Initiate.Fade("Creditos", Color.black, 2);
        }
       

    }
}
