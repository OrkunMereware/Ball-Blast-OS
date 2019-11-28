using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [System.NonSerialized] private PoolManager poolManager;
    [System.NonSerialized] private float speed = 1.0f;
    [System.NonSerialized] private Vector3 direction = Vector3.up;
    [System.NonSerialized] private Vector3 startPositon = Vector3.up;
    [System.NonSerialized] private float totalShift = 0.0f;

    void Update()
    {
        totalShift += Time.deltaTime * speed;
        transform.position = startPositon + (direction * totalShift);
    }

    public void IgniteFrom(PoolManager poolManager, Transform muzzle, float speed)
    {
        totalShift = 0.0f;
        this.poolManager = poolManager;
        this.speed = speed;
        this.startPositon = muzzle.position;
        this.direction = muzzle.up;
    }

    private void OnCollisionEnter(Collision collision)
    {
        SoftDestroy();
    }

    private void OnTriggerEnter(Collider other)
    {
        SoftDestroy();
    }

    private void SoftDestroy()
    {
        this.gameObject.transform.position = startPositon;
        poolManager.ReturnOne(this.gameObject);
    }

}
