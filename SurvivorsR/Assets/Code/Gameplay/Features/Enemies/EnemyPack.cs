using System;
using UnityEngine.Serialization;

namespace Code.Gameplay.Features.Enemies
{
    /// <summary>
    /// Represents a group of enemies of a certain type with an arbitrary number.
    /// Pack can be a component of the enemy wave.
    /// </summary>
    
    [Serializable]
    public class EnemyPack
    {
        public EnemyTypeId EnemyTypeId;
        public int number;
    }
}