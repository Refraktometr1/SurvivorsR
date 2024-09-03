using Entitas;

namespace Code.Gameplay.Features.Hero.Systems
{
    public class AnimateHeroMovementSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _inputs;
        private readonly IGroup<GameEntity> _heroes;

        public AnimateHeroMovementSystem(GameContext gameContext )
        {
            _heroes =  gameContext.GetGroup(GameMatcher.
                AllOf(GameMatcher.Hero, GameMatcher.HeroAnimator));
        }

        public void Execute()
        {
            foreach (var hero in _heroes)
            {
                if (hero.isMoving)
                {
                    hero.HeroAnimator.PlayMove();
                }
                else
                {
                    hero.HeroAnimator.PlayIdle();
                }
            }
           
        }
    }
}