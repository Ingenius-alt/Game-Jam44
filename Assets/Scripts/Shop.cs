using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{

    //public Wallet creamCoins;
    //public VehicleStats creamCar;


    public void RepairBrakes()
    {
        //creamCoins.RemoveBalance(120);
    }
    public void RepairSteering()
    {
        //creamCoins.RemoveBalance(140);
    }
    public void RepairGasTank()
    {
        //creamCoins.RemoveBalance(150);
    }
    public void RepairEngine()
    {
        //creamCoins.RemoveBalance(300);
    }
    public void Refuel()
    {
        //creamCoins.RemoveBalance(50);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //creamCoins = FindObjectOfType<Wallet>();
        //creamCar = FindObjectOfType<VehicleStats>();
    }

    public void ExitShop()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        Time.timeScale = 1.0f;
    }
}
