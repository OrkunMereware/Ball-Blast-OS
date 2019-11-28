using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    [HideInInspector] private Rigidbody rigBody;

    [System.NonSerialized] private float speed;
    [System.NonSerialized] private Vector3 gravity;
    [System.NonSerialized] private int hp = 1;
    [System.NonSerialized] private int[] splitHpList;
    [SerializeField] private TextMeshPro hpCountText;

    void Awake()
    {
        rigBody = GetComponent<Rigidbody>();    
    }

    public void SetProperties(float speed, Vector3 gravity, int hp, int[] splitHpList)
    {
        this.speed = speed;
        this.gravity = gravity;
        this.hp = hp;
        hpCountText.SetText(hp.ToString());
        this.splitHpList = splitHpList;
    }

    public void IgniteFrom(Transform spawnLoc)
    {
        transform.position = spawnLoc.position;
        rigBody.velocity = -spawnLoc.right;
    }

    public void IgniteFrom(Vector3 position, Vector3 direction)
    {
        transform.position = position;
        rigBody.velocity = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Wall"))
        {
            rigBody.velocity = Vector3.Reflect(rigBody.velocity, other.transform.up);
            if (other.transform.up == Vector3.up)
            {
                rigBody.velocity = rigBody.velocity.normalized * 14.0f;
            }
        }
        else if (other.name.Contains("Bullet"))
        {
            int startHP = hp;
            hp -= GameManager.instance.character.currentWeapon.damage;
            hp = Mathf.Clamp(hp, 0 , int.MaxValue);
            GameManager.instance.currentScore += (startHP - hp);
            GameManager.instance.CheckLevelCompletion();
            if (hp <= 0)
            {
                if (splitHpList != null)
                {
                    GameManager.instance.ballGenerator.SplitSpawn(transform.position, splitHpList);
                }
                
                Destroy(this.gameObject);
            }
            else
            {
                hpCountText.SetText(hp.ToString());
            }
        }
    }
   
}
