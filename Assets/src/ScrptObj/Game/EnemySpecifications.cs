using System;
using UnityEngine;
[Serializable]
public enum EnemyTypes
{
    Goblin,
    FlyEye,
    Mushroom,
    Skeletton
}
[Serializable]
public class EnemySpecificationPair
{
    public EnemyTypes type;
    public EnemySpecification specification;
}
[Serializable]
public class EnemySpecification
{
    public float MoveSpeed;
    public float AttackFrequency;
    public float Damage;
    public float Health;
    public RuntimeAnimatorController animator;
}
[Serializable]
[CreateAssetMenu(fileName = "EnemySpecification", menuName = "ScrptObj/EnemySpecification", order = 1)]
public class EnemySpecifications : ScriptableObject
{
    public EnemySpecificationPair[] specifications;
}
