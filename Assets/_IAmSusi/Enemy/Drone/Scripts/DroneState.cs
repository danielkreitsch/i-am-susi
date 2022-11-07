namespace Game.Drone
{
    public interface DroneState
    {
        DroneStateId GetId();
        void Enter(DroneAgent agent);
        void Update(DroneAgent agent);
        void Exit(DroneAgent agent);
    }
}