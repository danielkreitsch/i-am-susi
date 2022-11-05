using Glowdragon.VariableDisplay;
using UnityEngine;
using Zenject;

namespace FGJ2022
{
    public class EnemyAgent : MonoBehaviour
    {
        [Inject]
        private VariableDisplay variableDisplay;
        
        [SerializeField]
        private Enemy enemy;

        [SerializeField]
        private GameObject avatar;
        
        [SerializeField]
        private EnemyStateId initialState;

        public Enemy Enemy => this.enemy;

        public GameObject Avatar => this.avatar;
        
        public EnemyStateMachine StateMachine { get; private set; }

        private void Start()
        {
            this.StateMachine = new EnemyStateMachine(this, this.variableDisplay);
            this.StateMachine.RegisterState(new FocusTargetState());
            this.StateMachine.RegisterState(new ShootState());
            this.StateMachine.RegisterState(new TargetKilledState());
            this.StateMachine.ChangeState(this.initialState);
        }
        
        private void Update()
        {
            this.StateMachine.Update();
        }
    }
}