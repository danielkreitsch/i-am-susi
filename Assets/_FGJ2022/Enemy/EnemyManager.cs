using System;
using System.Linq;
using FGJ2022.Cleaner;
using FGJ2022.Drone;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using MyBox;
using UnityEngine;

namespace AssemblyCSharp {
    sealed class EnemyManager : MonoBehaviour {
        [SerializeField, ParamRef]
        string enemyCountParameter = string.Empty;
        RESULT enemyCountResult = RESULT.ERR_UNINITIALIZED;
        PARAMETER_DESCRIPTION enemyCountDescription;

        [Space]
        [SerializeField, ReadOnly]
        CleanerAgent[] cleaners = Array.Empty<CleanerAgent>();
        int aggroCleaners => cleaners
            .Where(d => d)
            .Count(c => c.StateMachine.CurrentState == CleanerStateId.FocusTarget || c.StateMachine.CurrentState == CleanerStateId.Shoot);

        [SerializeField, ReadOnly]
        DroneAgent[] drones = Array.Empty<DroneAgent>();
        int aggroDrones => drones
            .Where(d => d)
            .Count(d => d.StateMachine.CurrentState == DroneStateId.FocusTarget || d.StateMachine.CurrentState == DroneStateId.Shoot);

        void Awake() {
            OnValidate();
        }
        [ContextMenu(nameof(OnValidate))]
        void OnValidate() {
            cleaners = FindObjectsOfType<CleanerAgent>(true);
            drones = FindObjectsOfType<DroneAgent>(true);
        }
        void Start() {
            enemyCountResult = RuntimeManager.StudioSystem.getParameterDescriptionByName(enemyCountParameter, out enemyCountDescription);
        }
        void Update() {
            if (enemyCountResult == RESULT.OK) {
                int enemyCount = aggroCleaners + aggroDrones;
                UnityEngine.Debug.Log(enemyCount);
                RuntimeManager.StudioSystem.setParameterByID(enemyCountDescription.id, enemyCount);
            }
        }
    }
}
