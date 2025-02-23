using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Bullet Stuff")]
    [field: SerializeField] public GameObject Bullet { get; private set; }
    [field: SerializeField] public PoolType poolType { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public Vector2 RandomSpeedAddition { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float Scale { get; private set; }
    [field: SerializeField] public Vector2 RandomScaleAddition { get; private set; }
    [field: SerializeField, Tooltip("makes scale act like a damage multiplier")] public bool ScaleEffectsDamage { get; private set; }
    

    [Header("Gun Stuff")]
    [field: SerializeField] public float FireRate { get; private set; }
    [field: SerializeField] public float SpreadAngle { get; private set; }
    [field: SerializeField] public int SpreadAmount { get; private set; }
    [field: SerializeField] public int BurstAmount { get; private set; }
    [field: SerializeField] public float BurstSpeed { get; private set; }
    [field: SerializeField] public float AddedAngle { get; private set; }
    [field: SerializeField] public Vector2 RandomAngle { get; private set; }
}
