using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LudumDare53.GameRules;
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

        [Header("Text")] [SerializeField] private TextAppearance pauseText;
        [SerializeField] private TextAppearance youAreFiredText;
        [SerializeField] private TextAppearance dayIsOverText;
        [SerializeField] private TextAppearance mainMenuText;

        [Header("Tutorial")] [SerializeField] private NodePlayer nodePlayer;
        [SerializeField] private List<NodeSequence> nodeSequences;
        [SerializeField] private NodeBase universalResumeNode;

        [Header("Buttons")] [SerializeField] private Button continueButton;

        [Header("Other")] [SerializeField] private LevelTimer timer;


        private void Start()
        {
            timer.timePassed.AddListener(DayIsOver);
            GameObject.FindGameObjectWithTag("Points").GetComponent<ReduceablePoints>().PointsChanged.AddListener(
                (c, v) =>
                {
                    if (c <= 0) YouAreFired();
                }
            );
            DifficultyManager.DifficultyChanged.AddListener(() =>
            {
                if (DifficultyManager.Difficulty < nodeSequences.Count)
                    nodePlayer.SetNodes(nodeSequences[DifficultyManager.Difficulty]?.sequence);
                else
                {
                    nodePlayer.SetNodes(new List<NodeBase> { universalResumeNode });
                }
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
            continueButton.interactable = PlayerPrefs.HasKey("DifficultyLevel") && DifficultyManager.Difficulty > 0;
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
            pauseText.Activate();
            PauseManager.SetPause(PauseManager.PauseCause.Player);
        }

        public void Resume()
        {
            SmoothFadeOut();
            if (pauseText.isActiveAndEnabled) pauseText.ForceEnd();
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
            youAreFiredText.Activate();
            PauseManager.SetPause(PauseManager.PauseCause.GameMenu);
        }

        public void DayIsOver()
        {
            SmoothFadeIn();
            if (DifficultyManager.Difficulty >= 4)
            {
                DifficultyManager.SetDifficulty(0);
                mainMenuScreen.SetActive(true);
                mainMenuText.Activate();
            }
            else
            {
                DifficultyManager.SetDifficulty(DifficultyManager.Difficulty + 1);
                dayIsOverScreen.SetActive(true);
                dayIsOverText.Activate();
            }

            PauseManager.SetPause(PauseManager.PauseCause.GameMenu);
        }

        public void NextDay()
        {
            timer.Reload();
            SmoothFadeOut();
            dayIsOverText.ForceEnd();
            dayIsOverScreen.SetActive(false);
            nodePlayer.StartSequence();
        }

        public void TryAgain()
        {
            DifficultyManager.SetDifficulty(DifficultyManager.Difficulty);
            youAreFiredText.ForceEnd();
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
            SmoothFadeOut();
            mainMenuScreen.SetActive(false);
            nodePlayer.StartSequence();
        }
    }
}