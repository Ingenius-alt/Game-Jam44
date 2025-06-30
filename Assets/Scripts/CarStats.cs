using System;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class CarStats : MonoBehaviour
{
    [SerializeField] float tireDMG = 5.0f;
    [SerializeField] float EngineDMG = 5.0f;
    [SerializeField] float GasDrainperSecond = 2f;
    [SerializeField] GameObject fuelHandle;
    [SerializeField] GameObject goldText;
    [SerializeField] GameObject NotificationText;
    [SerializeField] GameObject store;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject[] Respawn;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] AudioSource audiosfx;
    [SerializeField] GameObject[] storeB;
    private int indexClip;
    private float tireD = 100.0f;
    private float engineD = 75.0f;
    private float GasCanD = 0;
    private int money = 0;
    private int objFul = 0;
    private float waitTimeBetweenDrain = 2f;
    private float waitTimeBetweenStall = 10f;
    private float timer = 0;
    private float timer2 = 0;
    private Movement carMovement;
    private int loudUpgrade = 1;
    private float steerUpgrade = 0f;
    private int otherRoute = 0;
    void Start()
    {
        carMovement = GetComponent<Movement>();
        indexClip = 0;
        Restart();
        GasCanD = 0;
    }

    void Update()
    {
        if (!store.activeSelf)
        {
            timer += Time.deltaTime;
            timer2 += Time.deltaTime;
        }
        if (timer > waitTimeBetweenDrain && !store.activeSelf)
        {
            GasCanD += GasDrainperSecond;
            updateGas();
            timer -= waitTimeBetweenDrain;
        }
        if (timer2 > waitTimeBetweenStall && !store.activeSelf)
        {
            NotificationText.GetComponent<TMP_Text>().text = "Engine Stalled";
            GetComponent<CarInput>().enabled = false;
            timer2 -= waitTimeBetweenStall;
            audiosfx.clip = audioClips[10];
            audiosfx.Play();

        }
        if (timer2 > waitTimeBetweenStall / 4 && !GetComponent<CarInput>().enabled && !store.activeSelf)
        {
            NotificationText.GetComponent<TMP_Text>().text = "";
            GetComponent<CarInput>().enabled = true;
            timer2 -= waitTimeBetweenStall;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver.activeSelf)
        {
            store.SetActive(!store.activeSelf);
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
            if (indexClip == 1)
            {
                GetComponent<AudioSource>().clip = audioClips[0 + (loudUpgrade - 1) + otherRoute];
                GetComponent<AudioSource>().Play();
                indexClip = 0 + (loudUpgrade - 1) + otherRoute;
            }
            else
            {
                indexClip = 1;
                GetComponent<AudioSource>().clip = audioClips[1];
                GetComponent<AudioSource>().Play();
            }
        }
        if (GasCanD >= 100)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
        goldText.GetComponent<TMP_Text>().text = money.ToString();
    }

    void updateGas()
    {
        fuelHandle.transform.Rotate(0, 0, GasDrainperSecond);
    }

    public void damage(string damagetype)
    {
        switch (damagetype)
        {
            case "crash":
                engineD -= EngineDMG;
                audiosfx.clip = audioClips[7];
                audiosfx.Play();
                break;
            case "cone":
                tireD -= tireDMG;
                carMovement.steeringF = 3.5f + (100f - tireD) * 0.05f + steerUpgrade;
                NotificationText.GetComponent<TMP_Text>().text = "Tires Damaged";
                break;
            case "pothole":
                audiosfx.clip = audioClips[8];
                audiosfx.Play();
                break;
            case "person":
                break;
            case "car":
                audiosfx.clip = audioClips[8];
                audiosfx.Play();
                break;
            default:
                break;
        }

    }

    public void objectiveFulfilled()
    {
        objFul = objFul + 1;
        if (objFul >= 8)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
        money += 20 * loudUpgrade;
        //money sound
        audiosfx.clip = audioClips[6];
        audiosfx.Play();
    }

    public void upgrade(int i)
    {
        switch (i)
        {
            case 0:
                if (money >= 300)
                {
                    engineD = 100f;
                    audiosfx.clip = audioClips[9];
                    audiosfx.Play();
                    money -= 300;
                }
                break;
            case 1:
                if (money >= 50)
                {
                    tireD = 100f;
                    audiosfx.clip = audioClips[9];
                    audiosfx.Play();
                    money -= 50;
                }
                
                break;
            case 2:
                if (money >= 150)
                {
                    storeB[2].SetActive(false);
                    steerUpgrade = 0.5f;
                    audiosfx.clip = audioClips[9];
                    audiosfx.Play();
                    money -= 150;
                }
                break;
            case 3:
                if (money >= 200)
                {
                    storeB[3].SetActive(false);
                    GasDrainperSecond = 1f;
                    audiosfx.clip = audioClips[9];
                    audiosfx.Play();
                    money -= 200;
                }
                break;
            case 4:
                if (money >= 200)
                {
                    storeB[4].SetActive(false);
                    loudUpgrade = 2;
                    audiosfx.clip = audioClips[9];
                    audiosfx.Play();
                    money -= 200;
                }
                break;
            default:
                break;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        for (int i = 0; i < Respawn.Length; i++)
        {
            Respawn[i].SetActive(true);
        }
        transform.position = new Vector3(-25.94f, -25.97f, -1f);
        if (otherRoute == 0)
        {
            otherRoute = 3;
        }
        else
        {
            otherRoute = 0;
        }
        GasCanD -= 10f;
        objFul = 0;
        gameOver.SetActive(false);
    }

    public void goMain()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
