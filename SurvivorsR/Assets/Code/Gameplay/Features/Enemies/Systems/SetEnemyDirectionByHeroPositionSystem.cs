using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class SetEnemyDirectionByHeroPositionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _heroes;
        private readonly IGroup<GameEntity> _enemies;

        public SetEnemyDirectionByHeroPositionSystem(GameContext gameContext )
        {
            _heroes =  gameContext.GetGroup(GameMatcher.Hero);
            _enemies = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Direction, GameMatcher.Enemy));
        }

        public void Execute()
        {
            foreach (var enemy in _enemies)
            foreach (var hero in _heroes)
            {
                enemy.ReplaceDirection((hero.WorldPosition - enemy.WorldPosition).normalized);
            }
        }
    }
}