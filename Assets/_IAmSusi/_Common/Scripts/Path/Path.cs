using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class Path : MonoBehaviour {
        List<Waypoint> waypoints = new();

        public List<Waypoint> Waypoints => waypoints;

        void Start() {
            foreach (Transform child in transform) {
                var waypoint = new Waypoint(child.position);
                waypoints.Add(waypoint);
                child.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}