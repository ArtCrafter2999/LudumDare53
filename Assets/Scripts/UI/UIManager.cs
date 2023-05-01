using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LudumDare53.Leveling;
using LudumDare53.Nodes;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare53.UI
{
    public class UIManager : MonoBehaviour
    {
        [Serializable]
        public class NodeSequence
        {
            public List<NodeBase> sequence;
        }
        
        [Header("Screens")] [SerializeField] private Image darkScreen;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject youAreFiredScreen;
        [SerializeField] private GameObject dayIsOverScreen;
        [SerializeField] private GameObject mainMenuScreen;

        [Header("Tutorial")] 
        [SerializeField] private NodePlayer nodePlayer;
        [SerializeField] private List<NodeSequence> nodeSequences;

        [Header("Buttons")] [SerializeField] private Button continueButton;

        [Header("Other")] [SerializeField] private LevelTimer timer;


        private void Start()
        {
            timer.timePassed.AddListener(DayIsOver);
            DifficultyManager.DifficultyChanged.AddListener(() =>
            {
                if(DifficultyManager.Difficulty < nodeSequences.Count)
                    nodePlayer.nodes = nodeSequences[DifficultyManager.Difficulty]?.sequence;
            });
        }

        private bool _prevEscape = false;
        private bool _prevEnter = false;

        public void Update()
        {
            if (Input.GetKey(KeyCode.Escape) && !_prevEscape)
            {
                if (!PauseManager.IsPaused) Pause();
                else if (PauseManager.Cause == PauseManager.PauseCause.Player) Resume();
            }
            _prevEscape = Input.GetKey(KeyCode.Escape);
            if (Input.GetKey(KeyCode.Return) && !_prevEnter)
            {
                nodePlayer.SkipNode();
            }
            _prevEnter = Input.GetKey(KeyCode.Return);
            continueButton.interactable = PlayerPrefs.HasKey("DifficultyLevel");
        }

        private void SmoothFadeIn(float duration = 0.5f)
        {
            darkScreen.gameObject.SetActive(true);
            darkScreen.DOFade(0.40f, duration);
        }

        public void SmoothFadeOut(float duration = 0.5f)
        {
            darkScreen.DOFade(0, duration).OnComplete(() => darkScreen.gameObject.SetActive(false));
        }

        public void Pause()
        {
            SmoothFadeIn();
            pauseScreen.SetActive(true);
            PauseManager.SetPause(PauseManager.PauseCause.Player);
        }

        public void Resume()
        {
            SmoothFadeOut();
            pauseScreen.SetActive(false);
            youAreFiredScreen.SetActive(false);
            dayIsOverScreen.SetActive(false);
            mainMenuScreen.SetActive(false);
            PauseManager.SetResume(PauseManager.PauseCause.Player);
            PauseManager.SetResume(PauseManager.PauseCause.GameMenu);
            PauseManager.SetResume(PauseManager.PauseCause.Tutorial); //TODO: можливо доведеться видалити
        }

        public void YouAreFired()
        {
            SmoothFadeIn();
            youAreFiredScreen.SetActive(true);
            PauseManager.SetPause(PauseManager.PauseCause.GameMenu);
        }

        public void DayIsOver()
        {
            SmoothFadeIn();
            dayIsOverScreen.SetActive(true);
            PauseManager.SetPause(PauseManager.PauseCause.GameMenu);
        }

        public void NextDay()
        {
            DifficultyManager.SetDifficulty(DifficultyManager.Difficulty + 1);
            timer.Reload();
            SmoothFadeOut();
            dayIsOverScreen.SetActive(false);
            nodePlayer.StartSequence();
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
            mainMenuScreen.SetActive(false);
            nodePlayer.StartSequence();
        }

        public void Continue()
        {
            DifficultyManager.SetDifficulty(DifficultyManager.Difficulty);
            timer.Reload();
            Resume();
        }
    }
}