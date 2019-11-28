using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon
{
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private Bullet bulletPrefab;
    //[SerializeField] private Transform bulletParent;
    [SerializeField] private float bulletSpeed = 1.0f;

    public override void Ignite()
    {
        //Bullet bulletClone = MonoBehaviour.Instantiate(bulletPrefab);
        Bullet bulletClone = poolManager.GetOne().GetComponent<Bullet>();
        //bulletClone.transform.parent = bulletParent;
        bulletClone.transform.position = muzzle.transform.position;
        bulletClone.IgniteFrom(poolManager, muzzle.transform, bulletSpeed);
        bulletClone.gameObject.SetActive(true);
    }

}
