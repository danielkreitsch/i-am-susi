using UnityEngine;

namespace FGJ2022
{
    public class EnemyStateMachine
    {
        public EnemyState[] States;
        public EnemyAgent Agent;
        public EnemyStateId CurrentState;

        public EnemyStateMachine(EnemyAgent agent)
        {
            this.Agent = agent;
            int numStates = System.Enum.GetNames(typeof(EnemyStateId)).Length;
            this.States = new EnemyState[numStates];
        }

        public void RegisterState(EnemyState state)
        {
            int index = (int)state.GetId();
            this.States[index] = state;
        }

        public EnemyState GetState(EnemyStateId stateId)
        {
            int index = (int)stateId;
            return this.States[index];
        }

        public void ChangeState(EnemyStateId newState)
        {
            Debug.Log("[Enemy] Change state from " + this.CurrentState + " to " + newState);
            this.GetState(this.CurrentState)?.Exit(this.Agent);
            this.CurrentState = newState;
            this.GetState(this.CurrentState).Enter(this.Agent);
        }

        public void Update()
        {
            this.GetState(this.CurrentState)?.Update(this.Agent);
        }
    }
}