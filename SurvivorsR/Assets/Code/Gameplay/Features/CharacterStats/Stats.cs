using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common.Extensions;
using Code.Gameplay.Features.Enemies;

namespace Code.Gameplay.Features.CharacterStats
{
  public enum Stats
  {
    Unknown = 0,
    Speed = 1,
    MaxHp = 2,
    Damage = 3
  }

  public static class InitStats
  {
    public static Dictionary<Stats, float> EmptyStatDictionary()
    {
      return Enum.GetValues(typeof(Stats))
        .Cast<Stats>()
        .Except(new[] {Stats.Unknown})
        .ToDictionary(x => x, _ => 0f);
    }

    public static Dictionary<Stats, float> Enemy(EnemyConfig config)
    {
      return InitStats.EmptyStatDictionary()
        .With(x => x[Stats.Speed] = config.Speed)
        .With(x => x[Stats.MaxHp] = config.MaxHp)
        .With(x => x[Stats.Damage] = config.Damage);
    }
  }
}