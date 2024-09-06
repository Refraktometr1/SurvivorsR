using System;
using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.CharacterStats;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Factory
{
  public class EnemyFactory : IEnemyFactory
  {
    private readonly IIdentifierService _identifiers;
    private readonly IStaticDataService _staticDataService;

    public EnemyFactory(IIdentifierService identifiers, IStaticDataService staticDataService)
    {
      _identifiers = identifiers;
      _staticDataService = staticDataService;
    }
    
    public GameEntity CreateEnemy(EnemyTypeId typeId, Vector3 position)
    {
      switch (typeId)
      {
        case EnemyTypeId.Goblin:
          return CreateGoblin(position);
        case EnemyTypeId.PigDogs:
          return CreatePigDogs(position);
      }

      throw new Exception($"Enemy with type id {typeId} does not exist");
    }

    private GameEntity CreatePigDogs(Vector2 position)
    {
      var baseEnemy = CreateBaseEnemy(EnemyTypeId.Goblin);
      
      return baseEnemy
        .AddWorldPosition(position)
        .AddTargetBuffer(new List<int>(1))
        .AddCollectTargetsInterval(0.5f)
        .AddCollectTargetsTimer(0f)
        .AddLayerMask(CollisionLayer.Hero.AsMask())
        .With(x => x.isTurnedAlongDirection = true)
        .With(x => x.isMovementAvailable = true);
    }

    private GameEntity CreateGoblin(Vector2 position)
    {
      var baseEnemy = CreateBaseEnemy(EnemyTypeId.PigDogs);
      
      return baseEnemy
          .AddWorldPosition(position)
          .AddTargetBuffer(new List<int>(1))
          .AddCollectTargetsInterval(0.5f)
          .AddCollectTargetsTimer(0f)
          .AddLayerMask(CollisionLayer.Hero.AsMask())
          .With(x => x.isTurnedAlongDirection = true)
          .With(x => x.isMovementAvailable = true);
    }

    private GameEntity CreateBaseEnemy(EnemyTypeId enemyTypeId)
    {
      EnemyConfig config = _staticDataService.GetEnemyConfig(enemyTypeId);

      Dictionary<Stats, float> baseStats = InitStats.Enemy(config);

      GameEntity baseEnemy = CreateEntity.Empty()
          .AddId(_identifiers.Next())
          .AddEnemyTypeId(config.EnemyTypeId)
          .AddDirection(Vector2.zero)
          .AddBaseStats(baseStats)
          .AddStatModifiers(InitStats.EmptyStatDictionary())
          .AddRadius(0.3f)
          .AddSpeed(config.Speed)
          .AddCurrentHp(config.MaxHp)
          .AddMaxHp(config.MaxHp)
          .AddViewPrefab(config.Prefab)
          .AddEffectSetups(new List<EffectSetup>{new EffectSetup(){EffectTypeId = EffectTypeId.Damage, Value = config.Damage}})
          .With(x => x.isEnemy = true);
      
      return baseEnemy;
    }
  }
}