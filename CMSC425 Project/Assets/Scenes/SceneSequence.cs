using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSequence : MonoBehaviour
{
    public GameObject cam;
    public GameObject mainCam;



    void Start()
    {
        StartCoroutine(Cutscene());
        mainCam.SetActive(true);
    }

    // Update is called once per frame
    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(7);
        mainCam.SetActive(true);
        cam.SetActive(false);
        
    }
}
