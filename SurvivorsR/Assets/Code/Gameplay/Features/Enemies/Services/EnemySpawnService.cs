using System;
using System.Collections.Generic;
using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.Enemies.Factory;
using UnityEngine;
using Code.Common.Extensions;
using Code.Gameplay.Features.LevelUp.Services;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Features.Enemies.Services
{
    public class EnemySpawnService : IEnemySpawnService
    {
        private readonly IEnemyFactory _enemyFactory;
        private readonly ITimeService _timeService;
        private readonly ICameraProvider _cameraProvider;
        private readonly ILevelUpService _levelUpService;

        private const float SpawnDistanceGap = 0.5f;
        private const int TimeOnLevelToAddNewTypeEnemy = 6;

        public EnemySpawnService(IEnemyFactory enemyFactory, ITimeService timeService, ICameraProvider cameraProvider, ILevelUpService levelUpService)
        {
            _enemyFactory = enemyFactory;
            _timeService = timeService;
            _cameraProvider = cameraProvider;
            _levelUpService = levelUpService;
        }

        public void SpawnWave(Vector2 heroWorldPosition)
        {
            List<EnemyPack> wave = GenerateEnemyWave();
            
            foreach (EnemyPack enemyPack in wave)
            {
                SpawnEnemyPack(heroWorldPosition, enemyPack);
            }
        }

        private void SpawnEnemyPack(Vector2 heroWorldPosition, EnemyPack enemyPack)
        {
            for (int i = 0; i < enemyPack.number; i++)
            {
                _enemyFactory.CreateEnemy(enemyPack.EnemyTypeId, RandomSpawnPosition(heroWorldPosition));
            }
        }

        private List<EnemyPack> GenerateEnemyWave()
        {
            var wave = new List<EnemyPack>();
            int enemyPacksCount = GetEnemyPacksCount();  

            for (int i = 0; i < enemyPacksCount; i++)
            {
                var enemyPack = GenerateEnemyPack();
                wave.Add(enemyPack);
            }
            
            return wave;
        }
        
        
        /// <summary>
        /// Dictates the number of enemy packs in a single enemy wave, serving as an aspect of game balance.
        /// The count of enemy packs increases linearly with the level of the hero,
        /// more complex formulas could be implemented in the future.
        /// </summary>
        /// <returns>Clamped number of enemy packs based on the current hero level.</returns>
        /// <returns></returns>
        private int GetEnemyPacksCount()
        {
            return Mathf.Clamp(_levelUpService.CurrentLevel, 1, 5);
        }

        private EnemyPack GenerateEnemyPack()
        {
            return new EnemyPack
            {
                EnemyTypeId = (EnemyTypeId) Random.Range(1, GetEnemyTypesCount()),
                number = Random.Range(1, 5)
            };
        }

        private int GetEnemyTypesCount()
        {
            var enemyTypeCount = Enum.GetNames(typeof(EnemyTypeId)).Length;
            float elapsedLevelTime = _timeService.GetElapsedTimeOnCurentLevel();

            // Let's start from the maximum possible count and decrease it based on the elapsed time.
            for (int i = enemyTypeCount; i >= 1; i--)
            {
                if (elapsedLevelTime > (i - 1) * TimeOnLevelToAddNewTypeEnemy)
                    return i;
            }

            return 1; // This is a fallback, just in case elapsedLevelTime is less than 10.
        }


        private Vector2 RandomSpawnPosition(Vector2 heroWorldPosition)
        {
            bool startWithHorizontal = Random.Range(0, 2) == 0;

            return startWithHorizontal 
                ? HorizontalSpawnPosition(heroWorldPosition) 
                : VerticalSpawnPosition(heroWorldPosition);
        }

        private Vector2 HorizontalSpawnPosition(Vector2 heroWorldPosition)
        {
            Vector2[] horizontalDirections = { Vector2.left, Vector2.right };
            Vector2 primaryDirection = horizontalDirections.PickRandom();
      
            float horizontalOffsetDistance = _cameraProvider.WorldScreenWidth / 2 + SpawnDistanceGap;
            float verticalRandomOffset = Random.Range(-_cameraProvider.WorldScreenHeight / 2, _cameraProvider.WorldScreenHeight / 2);

            return heroWorldPosition + primaryDirection * horizontalOffsetDistance + Vector2.up * verticalRandomOffset;
        }

        private Vector2 VerticalSpawnPosition(Vector2 heroWorldPosition)
        {
            Vector2[] verticalDirections = { Vector2.up, Vector2.down };
            Vector2 primaryDirection = verticalDirections.PickRandom();
      
            float verticalOffsetDistance = _cameraProvider.WorldScreenHeight / 2 + SpawnDistanceGap;
            float horizontalRandomOffset = Random.Range(-_cameraProvider.WorldScreenWidth / 2, _cameraProvider.WorldScreenWidth / 2);

            return heroWorldPosition + primaryDirection * verticalOffsetDistance + Vector2.right * horizontalRandomOffset;
        }
    }
}