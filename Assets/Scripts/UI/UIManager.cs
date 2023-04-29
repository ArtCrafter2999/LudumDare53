using System.Collections;
using System.Collections.Generic;
using LudumDare53.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject youAreFiredScreen;
    [SerializeField] private GameObject theDayIsOverScreen;


    public void Pause()
    {
        pauseScreen.SetActive(true);
        PauseManager.SetPause();
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        PauseManager.SetResume();
    }

    public void YouAreFired()
    {
        youAreFiredScreen.SetActive(true);
    }

    public void TheDayIsOver()
    {
        theDayIsOverScreen.SetActive(true);
    }

    public void NextDay()
    {
        
    }

    public void TryAgain()
    {
        
    }
}