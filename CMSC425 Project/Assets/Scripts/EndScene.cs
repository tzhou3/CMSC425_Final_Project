using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public float distance;
    public Vector3 topOfRamp;
    private GameObject player;

    void Start()
    {
        SceneNumber.instance.prevScene = 2;
    }
    public void Restart()
    {
        SceneNumber.setState(false);
        SceneManager.LoadScene(1);
    }

    public void JumpAgain()
    {
	    SceneNumber.instance.prevScene = 2;
        SceneNumber.setState(true);
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
