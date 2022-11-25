using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    public Toggle toggle_fullscreen;
    public Toggle toggle_showFPS;

    public TMP_Dropdown dropdown_resolutions;
    public TMP_Dropdown dropdown_fps;
    Resolution[] resolutions;
    public int[] framerates;

    // Start is called before the first frame update
    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle_fullscreen.isOn = true;
        }
        else
        {
            toggle_fullscreen.isOn = false;
        }

        if (PlayerPrefs.GetInt("showFPS", 0) == 1)
        {
            toggle_showFPS.isOn = true;
        }
        else
        {
            toggle_showFPS.isOn = false;
        }

        selectOldResolution();

        //Seleccionamos los FPS guardados
        dropdown_fps.value = PlayerPrefs.GetInt("selectedFPS", 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void changeShowFPS(bool showFPS)
    {
        int i;
        if (showFPS == true)
            i = 1;
        else
            i = 0;
       PlayerPrefs.SetInt("showFPS", i);
    }

    public void changeFPS(int index)
    {
        PlayerPrefs.SetInt("selectedFPS", dropdown_fps.value);

        int selectedFPS = int.Parse(dropdown_fps.options[index].text);
        if (index != 1) //60 fps o Unlimited
        {
            QualitySettings.vSyncCount = 1;
        }
        else if(index == 1) //30 fps
        {
            QualitySettings.vSyncCount = 0;
        }

        Application.targetFrameRate = selectedFPS;

    }

    public void reviewResolutions()
    {
        resolutions = Screen.resolutions;
        dropdown_resolutions.ClearOptions();
        List<string> options = new List<string>();
        int actualResolution = 0;

        //A�adimos las posibles resoluciones al dropdown
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            //Detectamos la resoluci�n actual
            if(Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                actualResolution = i;
        }
        dropdown_resolutions.AddOptions(options);
        dropdown_resolutions.value = actualResolution;
        dropdown_resolutions.RefreshShownValue();

        dropdown_resolutions.value = PlayerPrefs.GetInt("selectedResolution", actualResolution);
    }

    public void selectOldResolution()
    {
        dropdown_resolutions.value = PlayerPrefs.GetInt("selectedResolution", 0);

        changeResolution(dropdown_resolutions.value);
    }

    public void changeResolution(int index)
    {
        PlayerPrefs.SetInt("selectedResolution", dropdown_resolutions.value);
        //Resolution resolution = resolutions[index];
        string[] resolution = dropdown_resolutions.options[index].text.Split(" x ");
        Screen.SetResolution(int.Parse(resolution[0]), int.Parse(resolution[1]), Screen.fullScreen);
    }
}
