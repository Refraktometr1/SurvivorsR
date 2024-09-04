using Code.Common.Extensions;
using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Common;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.Enemies.Factory;
using Code.Gameplay.Features.Enemies.Services;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Systems
{
  public class EnemySpawnSystem : IExecuteSystem
  {
    private readonly ITimeService _time;
    private readonly IEnemySpawnService _enemySpawnService;
    private readonly IGroup<GameEntity> _timers;
    private readonly IGroup<GameEntity> _heroes;

    public EnemySpawnSystem(GameContext game, ITimeService time, IEnemySpawnService enemySpawnService)
    {
      _time = time;
      _enemySpawnService = enemySpawnService;

      _timers = game.GetGroup(GameMatcher.SpawnTimer);
      _heroes = game.GetGroup(GameMatcher
        .AllOf(
          GameMatcher.Hero,
          GameMatcher.WorldPosition));
    }

    public void Execute()
    {
      foreach (GameEntity hero in _heroes)
      foreach (GameEntity timer in _timers)
      {
        timer.ReplaceSpawnTimer(timer.SpawnTimer - _time.DeltaTime);
        if (timer.SpawnTimer <= 0)
        {
          timer.ReplaceSpawnTimer(GameplayConstants.EnemySpawnTimer);
          _enemySpawnService.SpawnWave(hero.WorldPosition);
        }
      }
    }
  }
}