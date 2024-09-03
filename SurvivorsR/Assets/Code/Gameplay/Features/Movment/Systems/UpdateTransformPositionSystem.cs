using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.Systems
{
    public class UpdateTransformPositionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;

        public UpdateTransformPositionSystem(GameContext gameContext)
        {
            _movers = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Transform
                ));
        }

        public void Execute()
        {
            foreach (var mover in _movers)
            {
                mover.Transform.position = mover.WorldPosition;
            }
        }
    }
}