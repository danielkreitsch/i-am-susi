using System.Collections;
using DigitalRuby.Tween;
using UnityEngine;

namespace FGJ2022.Drone
{
    public class ShootState : DroneState
    {
        public DroneStateId GetId()
        {
            return DroneStateId.Shoot;
        }

        public void Enter(DroneAgent agent)
        {
            agent.StartCoroutine(this.Shoot_Coroutine(agent));
        }

        public void Update(DroneAgent agent)
        {
            
        }

        public void Exit(DroneAgent agent)
        {
            agent.Drone.Controller.IsStopped = false;
        }

        private IEnumerator Shoot_Coroutine(DroneAgent agent)
        {
            agent.Drone.Controller.IsStopped = true;

            agent.gameObject.Tween("Shoot", 0f, 1f, 1f, TweenScaleFunctions.Linear, t =>
            {
                var model = agent.Drone.Model;
                model.WeaponPosition = 0.37f + 0.2f * t.CurrentValue;
                model.WeaponRingAngle = t.CurrentValue * 360 * 3;
            });
            yield return new WaitForSeconds(1f);


            yield return new WaitForSeconds(2f);
            agent.StateMachine.ChangeState(DroneStateId.FocusTarget);
        }
    }
}