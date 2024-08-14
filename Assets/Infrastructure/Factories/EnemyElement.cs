using Enemy;

namespace Infrastructure.Factories
{
    public class EnemyElement
    {
        public IHealth Health{ get; private set; }
        public EnemyView View { get; private set; }
        public int KillPoints { get; private set; }

        public EnemyElement(IHealth health, EnemyView enemyView, int killPoints)
        {
            Health = health;
            View = enemyView;
            KillPoints = killPoints;
        }
    }
}