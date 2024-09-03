using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Movement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Gameplay.Features.Enemies.Registrars
{
    public class EnemyRegistrar : MonoBehaviour
    {
        public float Speed;
        public float AggroRadius;
        public float PursuitRadius;
        
        private GameEntity _entity;

        private void Awake()
        {
            _entity = CreateEntity.Empty()
                .AddSpeed(Speed)
                .AddTransform(transform)
                .AddWorldPosition(transform.position)
                .AddDirection(Vector2.zero)
                .AddAggroRadius(AggroRadius)
                .AddPursuitRadius(PursuitRadius)
                .With(x => x.isEnemy = true)
                .With(x => x.isMoving = false);
        }
    }
}