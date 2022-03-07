namespace Abstractions
{
    public interface IAttackable: IHealthHolder, IGameObjectHolder
    {
        void AddDamage(int amount);
    }
}