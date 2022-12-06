using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class showFPS : MonoBehaviour
{
    public GameObject fpsObject;
    public TMP_Text fpsText;
    float deltaTime;

    private void Start()
    {
        if(PlayerPrefs.GetInt("showFPS", 0) == 0)
        {
            fpsObject.SetActive(false);
        }
        else
        {
            fpsObject.SetActive(true);
        }
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString() + "FPS";
    }
}
