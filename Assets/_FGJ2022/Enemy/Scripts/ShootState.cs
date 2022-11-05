using System.Collections;
using DigitalRuby.Tween;
using UnityEngine;

namespace FGJ2022
{
    public class ShootState : EnemyState
    {
        public EnemyStateId GetId()
        {
            return EnemyStateId.Shoot;
        }

        public void Enter(EnemyAgent agent)
        {
            agent.StartCoroutine(this.Shoot_Coroutine(agent));
        }

        public void Update(EnemyAgent agent)
        {
            
        }

        public void Exit(EnemyAgent agent)
        {
            agent.Enemy.Controller.IsStopped = false;
        }

        private IEnumerator Shoot_Coroutine(EnemyAgent agent)
        {
            agent.Enemy.Controller.IsStopped = true;

            agent.gameObject.Tween("Shoot", 0f, 1f, 1f, TweenScaleFunctions.Linear, t =>
            {
                var model = agent.Enemy.Model;
                model.WeaponPosition = 0.37f + 0.2f * t.CurrentValue;
                model.WeaponRingAngle = t.CurrentValue * 360 * 3;
            });
            yield return new WaitForSeconds(1f);


            yield return new WaitForSeconds(2f);
            agent.StateMachine.ChangeState(EnemyStateId.FocusTarget);
        }
    }
}