using System;

namespace Code.Gameplay.Common.Time
{
  public class UnityTimeService : ITimeService
  {
    private bool _paused;
    private float _startLevelTime;

    public float DeltaTime => !_paused ? UnityEngine.Time.deltaTime : 0;

    public DateTime UtcNow => DateTime.UtcNow;
    
    public void StopTime() => _paused = true;
    public void StartTime() => _paused = false;
    public float GetElapsedTimeOnCurentLevel()
    {
      return UnityEngine.Time.time - _startLevelTime;
    }
    
    public void StartLevelTimer()
    {
      _startLevelTime = UnityEngine.Time.time;
    }
  }
}