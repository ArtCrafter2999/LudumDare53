using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare53.Leveling
{
    public class UIManager : MonoBehaviour
    {
        [Header("Screens")]
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject youAreFiredScreen;
        [SerializeField] private GameObject dayIsOverScreen;
        [Header("Other")]
        [SerializeField] private LevelTimer timer;
    
    
        private void Start()
        {
            timer.timePassed.AddListener(DayIsOver);
        }
    
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
            PauseManager.SetPause();
        }
    
        public void DayIsOver()
        {
            dayIsOverScreen.SetActive(true);
            PauseManager.SetPause();
        }
    
        public void NextDay()
        {
            
        }
    
        public void TryAgain()
        {
            
        }
    
        public void Menu()
        {
            
        }
    }
}