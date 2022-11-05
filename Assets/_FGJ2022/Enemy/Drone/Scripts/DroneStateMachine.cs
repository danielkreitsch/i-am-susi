using Glowdragon.VariableDisplay;
using UnityEngine;

namespace FGJ2022.Drone
{
    public class DroneStateMachine
    {
        public DroneState[] States;
        public DroneAgent Agent;
        public DroneStateId CurrentState;
        
        private VariableDisplay variableDisplay;

        public DroneStateMachine(DroneAgent agent, VariableDisplay variableDisplay)
        {
            this.Agent = agent;
            this.variableDisplay = variableDisplay;
            
            int numStates = System.Enum.GetNames(typeof(DroneStateId)).Length;
            this.States = new DroneState[numStates];
        }

        public void RegisterState(DroneState state)
        {
            int index = (int)state.GetId();
            this.States[index] = state;
        }

        public DroneState GetState(DroneStateId stateId)
        {
            int index = (int)stateId;
            return this.States[index];
        }

        public void ChangeState(DroneStateId newState)
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