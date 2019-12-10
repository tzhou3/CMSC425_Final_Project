﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSequence : MonoBehaviour
{
    public GameObject cam;
    public GameObject cam2;
    public GameObject mainCam;



    void Start()
    {
        cam.SetActive(true);
        mainCam.SetActive(false);
        cam2.SetActive(false);
        StartCoroutine(Cutscene());
        mainCam.SetActive(true);
    }

    // Update is called once per frame
    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(7);
        cam2.SetActive(true);
        cam.SetActive(false);
        yield return new WaitForSeconds(12);
        mainCam.SetActive(true);
        cam2.SetActive(false);

    }
}
