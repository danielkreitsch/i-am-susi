namespace FGJ2022.Drone
{
    public interface DroneState
    {
        DroneStateId GetId();
        void Enter(DroneAgent agent);
        void Update(DroneAgent agent);
        void Exit(DroneAgent agent);
    }
}