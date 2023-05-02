using System;
using System.Collections.Generic;
using LudumDare53.Boxes;
using LudumDare53.Leveling;
using LudumDare53.Nodes;
using LudumDare53.Truck;
using LudumDare53.UI;
using UnityEngine;

namespace LudumDare53.Tutorial
{
    public class TutorialEventsExecutor : MonoBehaviour
    {
        public enum Action
        {
            SpawnOutlinedBox,
            SpawnOutlinedTrash,
            SpawnOutlinedTruck,
            SetConveyorActiveEnabled,
            SetConveyorActiveDisabled,
            SetShredderActiveEnabled,
            SetShredderActiveDisabled,
            SetTruckControllerActiveEnabled,
            SetTruckControllerActiveDisabled,
            DarkScreenFadeOut,
            UIResume,
        }
        public enum TriggerAction
        {
            PlayerPutBoxIntoTrack,
            PlayerClickGoButton
        }
        [Serializable]
        public class EventAction
        {
            public EventNode node;
            public Action action;
        }
        [Serializable]
        public class ActionTrigger
        {
            public TriggerAction action;
            public TriggerNode node;
        }
        
        [SerializeField] private SurfaceEffector2D _conveyor;
        [SerializeField] private Shredder _shredder;
        [SerializeField] private TruckController _truckController;
        [SerializeField] private PrefabSpawner _outlinedTruckSpawner;
        [SerializeField] private PrefabSpawner _outlinedBoxSpawner;
        [SerializeField] private PrefabSpawner _outlinedTrashSpawner;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private List<EventAction> eventActions;
        [SerializeField] private List<ActionTrigger> actionTriggers;

        private void Start()
        {
            eventActions.ForEach(ea => ea.node.initialized.AddListener(() => Execute(ea)));
            actionTriggers.ForEach(BindTrigger);
        }

        private void Execute(EventAction eventAction)
        {
            switch (eventAction.action)
            {
                case Action.SpawnOutlinedBox:
                    SpawnOutlinedBox();
                    break;
                case Action.SpawnOutlinedTrash:
                    SpawnOutlinedTrash();
                    break;
                case Action.SpawnOutlinedTruck:
                    SpawnOutlinedTruck();
                    break;
                case Action.SetConveyorActiveEnabled:
                    SetConveyorActiveState(true);
                    break;
                case Action.SetConveyorActiveDisabled:
                    SetConveyorActiveState(false);
                    break;
                case Action.SetShredderActiveEnabled:
                    SetShredderActiveState(true);
                    break;
                case Action.SetShredderActiveDisabled:
                    SetShredderActiveState(false);
                    break;
                case Action.SetTruckControllerActiveEnabled:
                    SetTruckControllerActiveState(true);
                    break;
                case Action.SetTruckControllerActiveDisabled:
                    SetTruckControllerActiveState(false);
                    break;
                case Action.DarkScreenFadeOut:
                    uiManager.SmoothFadeOut();
                    break;
                case Action.UIResume:
                    uiManager.Resume();
                    break;
                default:
                    break;
            }
        }

        private void BindTrigger(ActionTrigger actionTrigger) //TODO Реалізувати прив'язку евентів до трігера
        {
            switch (actionTrigger.action)
            {
                case TriggerAction.PlayerPutBoxIntoTrack:
                    break;
                case TriggerAction.PlayerClickGoButton:
                    break;
                default:
                    break;
            }
        }

        private GameObject SpawnOutlinedBox()
        {
            return _outlinedBoxSpawner.Spawn();
        }

        private GameObject SpawnOutlinedTrash()
        {
            return _outlinedTrashSpawner.Spawn();
        }

        private GameObject SpawnOutlinedTruck()
        {
            return _outlinedTruckSpawner.Spawn();
        }

        private void SetConveyorActiveState(bool value)
        {
            if(_conveyor == null) return;
            _conveyor.enabled = value;
        }

        private void SetShredderActiveState(bool value)
        {
            if(_shredder == null) return;
            _shredder.enabled = value;
        }

        private void SetTruckControllerActiveState(bool value)
        {
            if(_truckController == null) return;
            _truckController.enabled = value;
        }
    }
}
