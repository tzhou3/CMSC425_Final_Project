using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSequence : MonoBehaviour
{
    public GameObject cam;
    public GameObject cam2;
    public GameObject mainCam;
    public GameObject canvas;



    void Start()
    {
		if (SceneNumber.instance.prevScene == 0)
        {
            cam.SetActive(true);
            canvas.SetActive(false);
            mainCam.SetActive(false);
            cam2.SetActive(false);
            StartCoroutine(Cutscene());
            mainCam.SetActive(true);
        }
        else
        {
            cam.SetActive(false);
            cam2.SetActive(false);
        }
    }

    // Update is called once per frame
    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(7);
        cam2.SetActive(true);
        cam.SetActive(false);
        yield return new WaitForSeconds(12);
        mainCam.SetActive(true);
        canvas.SetActive(true);
        cam2.SetActive(false);
        


    }
}
