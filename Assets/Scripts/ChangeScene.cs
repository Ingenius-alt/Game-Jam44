using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void switchScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
