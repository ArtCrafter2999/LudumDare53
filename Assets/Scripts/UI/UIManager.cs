using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LudumDare53.Leveling;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare53.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Screens")]
        [SerializeField] private Image darkScreen;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject youAreFiredScreen;
        [SerializeField] private GameObject dayIsOverScreen;
        [SerializeField] private GameObject mainMenuScreen;

        [Header("Buttons")] 
        [SerializeField] private Button continueButton;
        
        [Header("Other")]
        [SerializeField] private LevelTimer timer;

        private bool _isPausedFromGame = true;


        private void Start()
        {
            timer.timePassed.AddListener(DayIsOver);
        }

        private bool _prevEscape = false;
        public void Update()
        {
            if (Input.GetKey(KeyCode.Escape) && !_prevEscape)
            {
                if(!PauseManager.IsPaused) Pause();
                else if(!_isPausedFromGame) Resume();
            }
            _prevEscape = Input.GetKey(KeyCode.Escape);
            continueButton.interactable = PlayerPrefs.HasKey("DifficultyLevel");
        }

        private void SmoothFadeIn(float duration = 0.5f)
        {
            darkScreen.gameObject.SetActive(true);
            darkScreen.DOFade(0.40f, duration);
        }
        private void SmoothFadeOut(float duration = 0.5f)
        {
            darkScreen.DOFade(0, duration).OnComplete(() => darkScreen.gameObject.SetActive(false));
        }

        public void Pause()
        {
            SmoothFadeIn();
            pauseScreen.SetActive(true);
            PauseManager.SetPause();
        }
    
        public void Resume()
        {
            _isPausedFromGame = false;
            SmoothFadeOut();
            pauseScreen.SetActive(false);
            youAreFiredScreen.SetActive(false);
            dayIsOverScreen.SetActive(false);
            mainMenuScreen.SetActive(false);
            PauseManager.SetResume();
        }
    
        public void YouAreFired()
        {
            _isPausedFromGame = true;   
            SmoothFadeIn();
            youAreFiredScreen.SetActive(true);
            PauseManager.SetPause();
        }
    
        public void DayIsOver()
        {
            _isPausedFromGame = true;
            SmoothFadeIn();
            dayIsOverScreen.SetActive(true);
            PauseManager.SetPause();
        }
    
        public void NextDay()
        {
            DifficultyManager.SetDifficulty(DifficultyManager.Difficulty + 1);
            timer.Reload();
            Resume();
        }
    
        public void TryAgain()
        {
            timer.Reload();
            Resume();
        }
    
        public void Quit()
        {
            Debug.Log("Quit the game");
            Application.Quit();
        }
        public void NewGame()
        {
            DifficultyManager.SetDifficulty(0);
            timer.Reload();
            Resume();
        }
        public void Continue()
        {
            timer.Reload();
            Resume();
        }
    }
}