using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapon : MonoBehaviour
{
    [System.NonSerialized] private float spawnRate;
    [System.NonSerialized] public int damage;
    [System.NonSerialized] private float prevSpawn;
    [System.NonSerialized] private bool firing = false;

    public void SetPropeties(float spawnRate, int damage)
    {
        this.spawnRate = spawnRate;
        this.damage = damage;
    }

    private void Update()
    {
        if (Time.time - prevSpawn > spawnRate)
        {
            if (firing)
            {
                Ignite();
                prevSpawn = Time.time;
            }
        }
    }

    public abstract void Ignite();

    public void Fire(bool state)
    {
        firing = state;
    }

}
