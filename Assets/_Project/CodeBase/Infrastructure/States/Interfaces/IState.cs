namespace _Project.CodeBase.Infrastructure.States.Interfaces
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}