namespace FGJ2022.Cleaner
{
    public interface CleanerState
    {
        CleanerStateId GetId();
        void Enter(CleanerAgent agent);
        void Update(CleanerAgent agent);
        void Exit(CleanerAgent agent);
    }
}