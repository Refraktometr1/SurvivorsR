using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.Systems
{
    public class DirectionalDeltaMoveSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _movers;

        public DirectionalDeltaMoveSystem(GameContext gameContext, ITimeService timeService)
        {
            _timeService = timeService;
            _movers = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Direction,
                    GameMatcher.Speed,
                    GameMatcher.Moving
                ));
        }

        public void Execute()
        {
            foreach (var mover in _movers)
            {
                mover.ReplaceWorldPosition((Vector2)mover.WorldPosition + mover.Direction * mover.Speed * _timeService.DeltaTime);
            }
        }
    }
}