     using System.Runtime.CompilerServices;
using UnityEngine;

public class CarStats : MonoBehaviour
{
    [SerializeField] public float tireDMG = 5.0f;
    [SerializeField] public float EngineDMG = 5.0f;
    public float tireD = 100.0f;
    public float engineD = 75.0f;
    public float brakeD = 75.0f;
    public float GasCanD = 0f;
    private int money = 0;
    private int objFul = 0;

    void Update()
    {

    }

    public void damage(bool coneFlag)
    {
        if (coneFlag)
        {
            engineD -= EngineDMG;
        }
    }

    public void objectiveFulfilled()
    {
        objFul = objFul + 1;
        if (objFul == 8)
        {
            //do something you win or something
        }
        money = money + 200;
    }
}
