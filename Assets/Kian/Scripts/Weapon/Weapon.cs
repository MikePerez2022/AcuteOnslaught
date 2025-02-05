using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    [field: SerializeField] public GameObject Bullet { get; private set; }
    [field: SerializeField] public PoolType poolType { get; private set; }



    [field: SerializeField] public float FireRate { get; private set; }
    [field: SerializeField] public float SpreadAngle { get; private set; }
    [field: SerializeField] public int SpreadAmount { get; private set; }
    [field: SerializeField] public int BurstAmount { get; private set; }
    [field: SerializeField] public float BurstSpeed { get; private set; }
}
