using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.Enemies;
using Code.Gameplay.Features.Hero;
using Code.Gameplay.Features.Hero.Systems;
using Code.Gameplay.Features.Movement;
using Code.Gameplay.Input.Service;
using Code.Gameplay.Input.Systems;

namespace Code.Gameplay
{
    public class BattleFeature : Feature
    {
        public BattleFeature(GameContext gameContext, ITimeService timeService, IInputService inputService, ICameraProvider cameraProvider)
        {
            Add(new InputFeature(gameContext, inputService));
            Add(new HeroFeature(gameContext, cameraProvider));
            Add(new EnemyFeature(gameContext));
            Add(new MovementFeature(gameContext, timeService));
        }
    }
}