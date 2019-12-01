using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Transform spawnLocations;
    [SerializeField] private Transform ballParent;
    [System.NonSerialized] private Coroutine spawnRoroutine;

    // TODO : merge 2 funcs
    public void Spawn(LevelProperties.balls ballProperties)
    {
        Ball ballClone = MonoBehaviour.Instantiate(ballPrefab);
        ballClone.transform.parent = ballParent;
        ballClone.SetProperties(1.0f, Vector3.up * GameManager.instance.gameProperties.gravity, ballProperties.hp, ballProperties.splits);
        ballClone.IgniteFrom(spawnLocations.GetChild(Random.Range(0, spawnLocations.childCount)));
    }

    public void SplitSpawn(Vector3 position, int[] hpList)
    {
        for (int i = 0; i < hpList.Length; i++)
        {
            Ball ballClone = MonoBehaviour.Instantiate(ballPrefab);
            ballClone.transform.parent = ballParent;
            ballClone.SetProperties(1.0f, Vector3.up * GameManager.instance.gameProperties.gravity, hpList[i], null);
            float direction = i % 2 == 0 ? 1.0f : -1.0f;
            ballClone.IgniteFrom(position + Vector3.right * direction * 0.25f + Vector3.up * 0.5f, Vector3.right * direction);
        }
    }

    private IEnumerator SpawnRoutine(LevelProperties.balls[] balls)
    {
        GameManager.instance.currentScore = 0;
        GameManager.instance.totalScore = 0;
        for (int i = 0; i < balls.Length; i++)
        {
            GameManager.instance.totalScore += balls[i].hp + balls[i].splits[0] * 2;
        }
        for (int i = 0; i < balls.Length; i++)
        {
            int prevDelay = i == 0 ? 0 : balls[i - 1].delay;
            yield return new WaitForSeconds(balls[i].delay - prevDelay);
            Spawn(balls[i]);
        }
    }

    public void SpawnBalls(LevelProperties.balls[] balls)
    {
        if (spawnRoroutine != null)
        {
            StopCoroutine(spawnRoroutine);
        }
        spawnRoroutine = StartCoroutine(SpawnRoutine(balls));
    }

    public void SpawnBallsByHp(int totalHP, float percentage)
    {
        int hpLeft = (int)(totalHP * percentage);
        int ballCount = 4;
        LevelProperties.balls[] balls = new LevelProperties.balls[ballCount];
        for (int i = 0; i < ballCount; i++)
        {
            int hp = 0;
            int split = 0;
            if (i == ballCount - 1)
            {
                hp = (int)(hpLeft * Random.Range(0.5f, 0.9f));
                hp += hp % 2 == 0 ? 0 : 1;
                hpLeft -= hp;
                split = hpLeft / 2;
            }
            else
            {
                hp = (int)(hpLeft * Random.Range(0.05f, 0.3f) * (Calc.map(i, 0, ballCount - 1, 1, 2) * percentage));
                hp += hp % 2 == 0 ? 0 : 1;
                split = (int)(hp * Random.Range(0.2f, 0.8f));
                hpLeft -= (hp + split);
            }
            balls[i] = new LevelProperties.balls(hp, new int[] {split, split}, 4 * i);
        }
        StartCoroutine(SpawnRoutine(balls));
    }

    public void Reset()
    {
        StopCoroutine(spawnRoroutine);
        foreach (Transform child in ballParent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

}
