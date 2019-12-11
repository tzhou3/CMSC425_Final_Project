using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNumber : MonoBehaviour
{
    public static SceneNumber instance = null;

    public int prevScene = -1;

    void Awake()
    {
   
        if (!instance)
            instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
