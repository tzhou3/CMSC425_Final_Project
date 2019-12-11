using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public GameObject WinText;
    public float distance;
    public Vector3 topOfRamp;
    private GameObject player;

    void Start()
    {
        distance = 0.0f;
        topOfRamp = new Vector3(154.3f, 120.36f, -70.6f);
        player = GameObject.FindGameObjectWithTag("Player");
        SceneNumber.instance.prevScene = 2;
        print(player);
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void JumpAgain()
    {
	SceneNumber.instance.prevScene = 2;
	SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
