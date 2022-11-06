using Glowdragon.VariableDisplay;
using UnityEngine;
using Zenject;

namespace FGJ2022.Drone
{
    public class DroneAgent : MonoBehaviour
    {
        [Inject]
        private ApplicationManager applicationManager;
        
        [Inject]
        private VariableDisplay variableDisplay;
        
        [SerializeField]
        private Drone drone;

        [SerializeField]
        private GameObject avatar;
        
        [SerializeField]
        private DroneStateId initialState;

        [SerializeField]
        private float optimalDistanceToShoot;

        public ApplicationManager ApplicationManager => this.applicationManager;
        
        public Drone Drone => this.drone;

        public GameObject Avatar => this.avatar;

        public float OptimalDistanceToShoot => this.optimalDistanceToShoot;

        public DroneStateMachine StateMachine { get; private set; }

        private void Start()
        {
            this.StateMachine = new DroneStateMachine(this, this.variableDisplay);
            this.StateMachine.RegisterState(new FocusTargetState());
            this.StateMachine.RegisterState(new ShootState());
            this.StateMachine.RegisterState(new IdleState());
            this.StateMachine.ChangeState(this.initialState);
        }
        
        private void Update()
        {
            this.StateMachine.Update();
        }
    }
}