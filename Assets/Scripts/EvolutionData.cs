using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Shape")]
public class EvolutionData : MonoBehaviour
{
    public int Health;
    public float Speed;
    public int Damage;
    public float FireRate;

    public int pathId;
    public int pathEvolution;

    private void Start()
    {
        Weapon weapon = transform.GetComponent<Weapon>();
        weapon.fireRate = FireRate;
        weapon.damage = Damage;
    }


}
