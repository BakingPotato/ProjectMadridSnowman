using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    private TimeSpan timePlaying;
    private bool timerGoing = false;

    private float elapsedTime;
    [SerializeField] float objectiveTime;

    [SerializeField] LevelManager LM;

    IEnumerator countingFunc = null;
    Coroutine counting = null;

    private void Start()
    {
        string timePlayingStr = TimeSpan.FromSeconds(objectiveTime).ToString("mm':'ss'.'ff");
        LM.UIManager.UpdateTimerText(timePlayingStr);
    }

    public void BeginTimer()
    {
        //AudioManager.Instance.PlaySFXRandomPitch("TimerStart");
        FMODUnity.RuntimeManager.PlayOneShot("event:/OTHER/UI/timer");
        timerGoing = true;
        elapsedTime = objectiveTime;
        counting = StartCoroutine(UpdateTimerSequence());
    }

    public void ResumeTimer()
    {
        if (counting != null && countingFunc != null)
        {
            timerGoing = true;
            StartCoroutine(countingFunc);
        }
    }

    public void PauseTimer()
    {
        timerGoing = false;
        if (counting != null)
        {
            StopCoroutine(counting);
            counting = null;
        }
    }

    public void StopTimer()
    {
        timerGoing = false;
        float actualTime = UpdateTimer();
        if(counting != null)
        {
            StopCoroutine(counting);
            counting = null;
        }
        countingFunc = null;

        //Actualizamos el contador si esta activado
        string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
        LM.UIManager.UpdateTimerText(timePlayingStr);
    }

    public void Reset()
    {
        elapsedTime = objectiveTime;
    }

    public IEnumerator UpdateTimerSequence()
    {
        while (timerGoing)
        {
            UpdateTimer();

            //Actualizamos el contador si esta activado
            string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            LM.UIManager.UpdateTimerText(timePlayingStr);

            yield return null;
        }
    }

    public IEnumerator UpdateTimerSequenceSeconds()
    {
        while (timerGoing)
        {
            UpdateTimer();

            //Actualizamos el contador si esta activado
            string timePlayingStr = timePlaying.ToString("ss");
            LM.UIManager.UpdateTimerText(timePlayingStr);

            yield return null;
        }
    }

    private float UpdateTimer()
    {
        //Subimos el tiempo de juego
        elapsedTime -= Time.deltaTime;
        timePlaying = TimeSpan.FromSeconds(elapsedTime);

        //retornamos el tiempo jugado
        return elapsedTime;
    }

    public float getTimeInSeconds()
    {
        return elapsedTime;
    }

    public string secondsToString(float seconds, string format)
    {
        return TimeSpan.FromSeconds(seconds).ToString(format);
    }

    public float getElapsedTime()
    {
        return elapsedTime;
    }
}
