using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class EnemyAggroSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _heroes;
        private readonly IGroup<GameEntity> _enemies;

        public EnemyAggroSystem(GameContext gameContext )
        {
            _heroes =  gameContext.GetGroup(GameMatcher.Hero);
            _enemies = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Enemy, GameMatcher.AggroRadius, GameMatcher.PursuitRadius));
        }

        public void Execute()
        {
            foreach (var enemy in _enemies)
            foreach (var hero in _heroes)
            {
                var sqrDistanceToHero = (hero.WorldPosition - enemy.WorldPosition).sqrMagnitude;
                if (sqrDistanceToHero < enemy.AggroRadius * enemy.AggroRadius && sqrDistanceToHero < enemy.PursuitRadius * enemy.PursuitRadius)
                {
                    enemy.isMoving = true;
                }
                else
                {
                    enemy.isMoving = false;
                }
            }
        }
    }
}