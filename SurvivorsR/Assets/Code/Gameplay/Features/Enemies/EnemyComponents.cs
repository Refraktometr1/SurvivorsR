using Entitas;

namespace Code.Gameplay.Features.Enemies
{
    public class EnemyComponents
    {
        [Game] public class Enemy : IComponent {  }
        [Game] public class AggroRadius : IComponent { public float Value; }
        [Game] public class PursuitRadius : IComponent { public float Value; }
    }
}