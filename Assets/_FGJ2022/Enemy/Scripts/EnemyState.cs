namespace FGJ2022
{
    public interface EnemyState
    {
        EnemyStateId GetId();
        void Enter(EnemyAgent agent);
        void Update(EnemyAgent agent);
        void Exit(EnemyAgent agent);
    }
}