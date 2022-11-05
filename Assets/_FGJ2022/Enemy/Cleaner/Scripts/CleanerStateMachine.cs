using Glowdragon.VariableDisplay;
using UnityEngine;

namespace FGJ2022.Cleaner
{
    public class CleanerStateMachine
    {
        public CleanerState[] States;
        public CleanerAgent Agent;
        public CleanerStateId CurrentState;
        
        private VariableDisplay variableDisplay;

        public CleanerStateMachine(CleanerAgent agent, VariableDisplay variableDisplay)
        {
            this.Agent = agent;
            this.variableDisplay = variableDisplay;
            
            int numStates = System.Enum.GetNames(typeof(CleanerStateId)).Length;
            this.States = new CleanerState[numStates];
        }

        public void RegisterState(CleanerState state)
        {
            int index = (int)state.GetId();
            this.States[index] = state;
        }

        public CleanerState GetState(CleanerStateId stateId)
        {
            int index = (int)stateId;
            return this.States[index];
        }

        public void ChangeState(CleanerStateId newState)
        {
            Debug.Log("[" + this.Agent.gameObject.name + "] Change state from " + this.CurrentState + " to " + newState);
            this.GetState(this.CurrentState)?.Exit(this.Agent);
            this.CurrentState = newState;
            this.variableDisplay.Set(this.Agent.gameObject.name, "State", this.CurrentState);
            this.GetState(this.CurrentState).Enter(this.Agent);
        }

        public void Update()
        {
            this.GetState(this.CurrentState)?.Update(this.Agent);
        }
    }
}