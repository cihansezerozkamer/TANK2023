using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy_SO", order = 1)]
public class Enem_SO : ScriptableObject
{
     public int Health; 
     public int MoveSpeed;
     public float Fire_Rate;
     public Material TankColor;
     public Vector3 Y;
     public Vector3 Scale;
     public Vector3[] SpawnPoints;
    



}
