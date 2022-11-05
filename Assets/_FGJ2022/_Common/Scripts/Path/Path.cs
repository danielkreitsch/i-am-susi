using System.Collections.Generic;
using UnityEngine;

namespace FGJ2022
{
    public class Path : MonoBehaviour
    {
        private List<Waypoint> waypoints = new();
        
        public List<Waypoint> Waypoints => this.waypoints;
        
        private void Start()
        {
            foreach (Transform child in this.transform)
            {
                var waypoint = new Waypoint(child.position);
                this.waypoints.Add(waypoint);
                Debug.Log("Waypoint: " + waypoint.Position);
            }
        }
    }
}