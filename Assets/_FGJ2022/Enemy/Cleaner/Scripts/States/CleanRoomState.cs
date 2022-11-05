using System.Collections.Generic;
using UnityEngine;

namespace FGJ2022.Cleaner
{
    public class CleanRoomState : CleanerState
    {
        private List<Waypoint> waypoints;
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
            var path = agent.Cleaner.CleaningPath;
            
            if (this.currentWaypoint == null)
            {
                this.currentWaypoint = path.Waypoints[0];
            }

            var distanceToWaypoint = Vector2.Distance(
                new Vector2(agent.Cleaner.transform.position.x, agent.Cleaner.transform.position.z),
                new Vector2(this.currentWaypoint.Position.x, this.currentWaypoint.Position.z));

            if (distanceToWaypoint < 1)
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