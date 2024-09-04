using Code.Common.Entity;
using Code.Gameplay.Common.Time;
using Code.Gameplay.StaticData;

namespace Code.Gameplay.Features.LevelUp.Services
{
  public class LevelUpService : ILevelUpService
  {
    public float CurrentExperience { get; private set; }
    public int CurrentLevel { get; private set; }

    public float ExperienceForLevelUp => _staticData.ExperienceForLevel(CurrentLevel + 1);

    private readonly IStaticDataService _staticData;
    private readonly ITimeService _timeService;

    public LevelUpService(IStaticDataService staticData, ITimeService timeService)
    {
      _staticData = staticData;
      _timeService = timeService;
    }

    public void AddExperience(float value)
    {
      CurrentExperience += value;
      UpdateLevel();
    }

    private void UpdateLevel()
    {
      if (CurrentLevel >= _staticData.MaxLevel())
        return;

      float experienceForLevelUp = _staticData.ExperienceForLevel(CurrentLevel + 1);

      if (CurrentExperience < experienceForLevelUp) 
        return;
      
      CurrentExperience -= experienceForLevelUp;
      CurrentLevel++;
      _timeService.StartLevelTimer();

      CreateEntity.Empty().isLevelUp = true;

      UpdateLevel();
    }
  }
}