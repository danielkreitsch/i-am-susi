using Game;
using Glowdragon.VariableDisplay;
using UnityEngine;
using Zenject;

namespace FGJ2022.Cleaner
{
    public class CleanerAgent : MonoBehaviour
    {
        [Inject]
        private VariableDisplay variableDisplay;
        
        [SerializeField]
        private Cleaner cleaner;

        [SerializeField]
        private CleanerStateId initialState;

        [SerializeField]
        private float optimalDistanceToShoot;

        public Cleaner Cleaner => this.cleaner;

        public IVacuumTarget VacuumTarget { get; set; }

        public float OptimalDistanceToShoot => this.optimalDistanceToShoot;

        public CleanerStateMachine StateMachine { get; private set; }

        private void Start()
        {
            this.StateMachine = new CleanerStateMachine(this, this.variableDisplay);
            this.StateMachine.RegisterState(new CleanRoomState());
            this.StateMachine.RegisterState(new FocusTargetState());
            this.StateMachine.ChangeState(this.initialState);
        }
        
        private void Update()
        {
            this.StateMachine.Update();
        }
    }
}