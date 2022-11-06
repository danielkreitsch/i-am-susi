using System;
using System.Linq;
using FGJ2022.Cleaner;
using FGJ2022.Drone;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using MyBox;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace AssemblyCSharp {
    sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private Volume volume;
        
        private ChromaticAberration chromaticEffect;
        
        [SerializeField, ParamRef]
        string enemyCountParameter = string.Empty;
        RESULT enemyCountResult = RESULT.ERR_UNINITIALIZED;
        PARAMETER_DESCRIPTION enemyCountDescription;
        [SerializeField]
        int enemyCountOffset = 0;
        [SerializeField, ReadOnly]
        int currentEnemyCount = 0;

        [Space]
        [SerializeField, ReadOnly]
        CleanerAgent[] cleaners = Array.Empty<CleanerAgent>();
        int aggroCleaners => cleaners
            .Where(d => d && d.enabled)
            .Count(c => c.StateMachine.CurrentState == CleanerStateId.FocusTarget || c.StateMachine.CurrentState == CleanerStateId.Shoot);

        [SerializeField, ReadOnly]
        DroneAgent[] drones = Array.Empty<DroneAgent>();
        int aggroDrones => drones
            .Where(d => d && d.enabled)
            .Count(d =>  d.StateMachine.CurrentState == DroneStateId.FocusTarget || d.StateMachine.CurrentState == DroneStateId.Shoot);

        void Awake() {
            OnValidate();
            this.volume.profile.TryGet(out this.chromaticEffect);
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
            this.chromaticEffect.intensity.value = (1 + Mathf.Sin(Time.timeSinceLevelLoad) / 2) * this.currentEnemyCount / 10f;
            
            if (enemyCountResult == RESULT.OK) {
                currentEnemyCount = enemyCountOffset + aggroCleaners + aggroDrones;
                RuntimeManager.StudioSystem.setParameterByID(enemyCountDescription.id, currentEnemyCount);
            }
        }
    }
}
