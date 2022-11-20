using System;

namespace Game.Drone
{
    public interface DroneState : IDisposable
    {
        DroneStateId GetId();
        void Enter(DroneAgent agent);
        void Update(DroneAgent agent);
        void Exit(DroneAgent agent);
    }
}