using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Shape")]
public class EvolutionData : ScriptableObject
{
    public float Health;
    public float Speed;
    public float Damage;
    public float FireRate;

    public Sprite Sprite;
    public Sprite UtilitySprite;

    public GameObject Bullet;


}
