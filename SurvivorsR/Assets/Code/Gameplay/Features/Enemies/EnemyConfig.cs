using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies
{
    [CreateAssetMenu(menuName = "SurvivorsR Configs/EnemyConfig", fileName = "EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        public EnemyTypeId EnemyTypeId;
        public int Speed;
        public int MaxHp;
        public int Damage;
        public float AttackRange;
        public EntityBehaviour Prefab;
        public string Description;
    }
}