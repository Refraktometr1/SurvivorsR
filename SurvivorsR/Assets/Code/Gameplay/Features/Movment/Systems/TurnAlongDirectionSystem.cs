using Code.Common.Extensions;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.Systems
{
    public class TurnAlongDirectionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;

        public TurnAlongDirectionSystem (GameContext gameContext)
        {
            _movers = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TurnedAlongDirection,
                    GameMatcher.Direction,
                    GameMatcher.SpriteRenderer
                ));
        }

        public void Execute()
        {
            foreach (var mover in _movers)
            {
                var localScaleX = Mathf.Abs(mover.SpriteRenderer.transform.localScale.x);
                mover.SpriteRenderer.transform.SetScaleX(localScaleX * FaceDirection(mover));
            }
        }

        private float FaceDirection(GameEntity mover)
        {
            return mover.Direction.x <= 0 ? -1 : 1;
        }
    }
}