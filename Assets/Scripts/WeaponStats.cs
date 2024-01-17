using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/WeaponStats")]
public class WeaponStats : ScriptableObject
{
    public float projectileVelocity = 12f;
    public float fireRate = 2f;

}
