using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public Weapon currentWeapon;
    [System.NonSerialized] private float maxVerticalDistance = 4.25f;
    [SerializeField] private float speed = 1.0f;
    [HideInInspector] private float targetPosition;
    [HideInInspector] private float currentPosition;

    private void Start()
    {
        targetPosition = Calc.map(SwipeManager.instance.SWIPE_PERCENTAGE, 0.0f, 1.0f, -maxVerticalDistance, maxVerticalDistance);
        currentPosition = targetPosition;
        transform.localPosition = new Vector3(currentPosition, 0.0f, 0.0f);
    }
    void Update()
    {
        if (SwipeManager.instance.PRESSING)
        {
            targetPosition = Calc.map(SwipeManager.instance.SWIPE_PERCENTAGE, 0.0f, 1.0f, -maxVerticalDistance, maxVerticalDistance);
            currentPosition = Mathf.Lerp(transform.localPosition.x, targetPosition, Time.deltaTime * speed);
            transform.localPosition = new Vector3(currentPosition, 0.0f, 0.0f);
        }

        currentWeapon.Fire(SwipeManager.instance.PRESSING && GameManager.instance.PLAYING);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.PLAYING)
        {
            GameManager.instance.GameOver();
            //GameManager.instance.sceneState = 2;
            //GameManager.instance.OutputGameState();
        }
    }
}
