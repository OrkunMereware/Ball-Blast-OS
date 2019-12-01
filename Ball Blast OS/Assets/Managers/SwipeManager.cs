using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static SwipeManager instance;

    [System.NonSerialized] public bool PRESSING = false;
    [System.NonSerialized] public float SWIPE_PERCENTAGE = 0.5f;
    [HideInInspector] private float clampPercentage = 0.15f;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SwipeManager.instance.PRESSING = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SwipeManager.instance.PRESSING = false;
        }

        if (SwipeManager.instance.PRESSING && GameManager.instance.PLAYING && Input.GetMouseButton(0))
        {
            SwipeManager.instance.SWIPE_PERCENTAGE = Mathf.Clamp(Calc.map(Input.mousePosition.x, Screen.width * clampPercentage, Screen.width - (Screen.width * clampPercentage), 0.0f, 1.0f), 0.0f, 1.0f);
        }

        
    }

}
