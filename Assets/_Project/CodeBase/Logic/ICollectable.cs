using _Project.CodeBase.Logic.Player;
using _Project.CodeBase.Logic.Stickman;

namespace _Project.CodeBase.Logic
{
    public interface ICollectable
    {
        void Construct(HeroStickmanBehaviour stickmanBehaviour, StickmanType type);
        void ChangeColor();
    }
}