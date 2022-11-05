using System.Collections;
using DigitalRuby.Tween;
using UnityEngine;

namespace FGJ2022.Cleaner
{
    public class ShootState : CleanerState
    {
        public CleanerStateId GetId()
        {
            return CleanerStateId.Shoot;
        }

        public void Enter(CleanerAgent agent)
        {
            agent.StartCoroutine(this.Shoot_Coroutine(agent));
        }

        public void Update(CleanerAgent agent)
        {
            
        }

        public void Exit(CleanerAgent agent)
        {
            agent.Cleaner.Controller.IsStopped = false;
        }

        private IEnumerator Shoot_Coroutine(CleanerAgent agent)
        {
            agent.Cleaner.Controller.IsStopped = true;

            agent.gameObject.Tween("Shoot", 0f, 1f, 1f, TweenScaleFunctions.Linear, t =>
            {
                var model = agent.Cleaner.Model;
                model.WeaponPosition = 0.37f + 0.2f * t.CurrentValue;
            });
            yield return new WaitForSeconds(1f);


            yield return new WaitForSeconds(2f);
            agent.StateMachine.ChangeState(CleanerStateId.FocusTarget);
        }
    }
}