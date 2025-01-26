using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Gameplay
{
    public class GameManager : Singleton<GameManager>
    {
        public Action<DateTime> OnGameTimeChanged = delegate { };
        public DateTime GameTime;
        public Action<GameState> OnGameStateChanged = delegate { };
        public GameState currentState;
        [SerializeField] public Transform mainCanvas;
        public List<GameTimeCommand> waitingCommands = new();
        protected override void Awake()
        {
            base.Awake();
            GameTime = DateTime.Today;
            OnGameTimeChanged += CheckWaitingCommands;
        }

        private void OnDisable()
        {
            OnGameTimeChanged -= CheckWaitingCommands;
        }

        public void ChangeGameState(GameState state)
        {
            if (currentState != state)
            {
                currentState = state;
                OnGameStateChanged?.Invoke(currentState);
            }
        }        
        
        public void ForwardGameTime(int day)
        {
            GameTime = GameTime.AddDays(day);
            OnGameTimeChanged?.Invoke(GameTime);
        }

        public void AddToWaitingCommands(GameTimeCommand command)
        {
            waitingCommands.Add(command);
        }

        private void CheckWaitingCommands(DateTime dateTime)
        {
            List<GameTimeCommand> commands = waitingCommands.FindAll(x => dateTime >= x.targetTime);
            foreach (var command in commands)
            {
                waitingCommands.Remove(command);
                command.action?.Invoke();
            }
        }
    }
    
    public enum GameState
    {
        Default = 0,
        Playing = 1,
        InputDisabled = 2
    }

    public class GameTimeCommand
    {
        public DateTime targetTime;
        public Action action;

        public GameTimeCommand(DateTime targetTime, Action action)
        {
            this.targetTime = targetTime;
            this.action = action;
        }
    }
}
