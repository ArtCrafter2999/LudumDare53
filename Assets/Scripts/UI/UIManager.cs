using System;
using System.Collections;
using System.Collections.Generic;
using LudumDare53.Leveling;
using UnityEngine;

namespace LudumDare53.UI
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

        private bool _prevEscape = false;
        public void Update()
        {
            if (Input.GetKey(KeyCode.Escape) && !_prevEscape)
            {
                if(!PauseManager.IsPaused)Pause();
                else Resume();
            }
            _prevEscape = Input.GetKey(KeyCode.Escape);
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