using System.Collections.Generic;
using System.Linq;
using Game.Utility;
using UnityEngine;

namespace FGJ2022.Cleaner
{
    public class CleanRoomState : CleanerState
    {
        private Waypoint currentWaypoint;
        
        public CleanerStateId GetId()
        {
            return CleanerStateId.CleanRoom;
        }

        public void Enter(CleanerAgent agent)
        {
        }

        public void Update(CleanerAgent agent)
        {
            var myPos = agent.Cleaner.transform.position;
            var path = agent.Cleaner.CleaningPath;
            
            if (this.currentWaypoint == null)
            {
                this.currentWaypoint = path.Waypoints.OrderBy(w => w.Position.FlatDistanceTo(myPos)).First();
            }

            var distanceToWaypoint = myPos.FlatDistanceTo(this.currentWaypoint.Position);

            if (distanceToWaypoint < 10)
            {
                this.currentWaypoint = path.Waypoints[(path.Waypoints.IndexOf(this.currentWaypoint) + 1) % path.Waypoints.Count];
            }
            
            var moveTarget = this.currentWaypoint.Position;
            agent.Cleaner.Controller.SetTarget(moveTarget);
        }

        public void Exit(CleanerAgent agent)
        {
        }
    }
}