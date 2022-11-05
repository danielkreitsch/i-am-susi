using System.Collections;
using DigitalRuby.Tween;
using UnityEngine;

namespace FGJ2022.Drone
{
    public class ShootState : DroneState
    {
        private DroneController controller;
        private DroneModel model;

        public DroneStateId GetId()
        {
            return DroneStateId.Shoot;
        }

        public void Enter(DroneAgent agent)
        {
            this.controller = agent.Drone.Controller;
            this.model = agent.Drone.Model;
          
            agent.StartCoroutine(this.Shoot_Coroutine(agent));
        }

        public void Update(DroneAgent agent)
        {
            this.model.Laser.Length = Vector3.Distance(this.model.transform.position, agent.Avatar.transform.position);
        }

        public void Exit(DroneAgent agent)
        {
            this.model.Laser.Length = 0;
            agent.Drone.Controller.IsStopped = false;
        }

        private IEnumerator Shoot_Coroutine(DroneAgent agent)
        {
            this.model.Laser.IsDeadly = false;
            this.controller.IsStopped = true;

            agent.Drone.onShoot.Invoke(agent.gameObject);

            agent.gameObject.Tween("Shoot", 0f, 1f, 1f, TweenScaleFunctions.Linear, t =>
            {
                this.model.WeaponPosition = 0.37f + 0.2f * t.CurrentValue;
                this.model.WeaponRingAngle = t.CurrentValue * 360 * 3;
                this.model.Laser.Thickness = 0.005f - t.CurrentValue * 0.005f;
            });
            yield return new WaitForSeconds(1f);

            this.model.Laser.IsDeadly = true;
            agent.gameObject.Tween("Shoot", 0f, 1f, 0.5f, TweenScaleFunctions.Linear, t =>
            {
                this.model.Laser.Thickness = 0.2f - t.CurrentValue * 0.2f;
            });
            yield return new WaitForSeconds(0.5f);
            this.model.Laser.IsDeadly = false;

            yield return new WaitForSeconds(0.2f);
            agent.Drone.ResetShootCooldown();
            agent.StateMachine.ChangeState(DroneStateId.FocusTarget);
        }
    }
}