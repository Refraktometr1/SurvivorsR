using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Services
{
    public interface IEnemySpawnService
    {
        void SpawnWave(Vector2 heroWorldPosition);
    }
}