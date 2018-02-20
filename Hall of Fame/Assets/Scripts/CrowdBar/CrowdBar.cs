using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Author: Jason Lin
 * 
 * Description:
 * Singleton CrowdBar for controlling the crowdbar movement as well as visualize the movement
 * Range[-100, 100]
 * when the crowdExcitement value > 0, that means it's moving towards right
 * when the crowdExcitement value < 0, that menas it's moving towards left
 */
public class CrowdBar : MonoBehaviour
{
    private static CrowdBar crowdBar;
    private GameObject crowdImg;
    private Vector3 targetLocation;

    public static CrowdBar instance
    {
        get
        {
            if (!crowdBar)
            {
                crowdBar = FindObjectOfType(typeof(CrowdBar)) as CrowdBar;

                if (!crowdBar)
                {
                    Debug.LogError("CrowdBar Script must be attached to a gameobject in scene");
                }
                else
                {
                    crowdBar.Init();
                }
            }
            return crowdBar;
        }
    }

    public int crowdExcitementValue;

    private void Init()
    {
        targetLocation = Vector3.zero;
        crowdExcitementValue = 0;
    }

    private void Awake()
    {
        crowdImg = transform.Find("CrowdImg").gameObject;
    }

    public void IncreaseToPlayer1(int value)
    {
        Debug.Log(value);
        crowdExcitementValue += value;
        if (crowdExcitementValue > 100)
        {
            crowdExcitementValue = 100;
        }
    }

    public void IncreaseToPlayer2(int value)
    {
        crowdExcitementValue -= value;
        if (crowdExcitementValue < -100)
        {
            crowdExcitementValue = -100;
        }
    }

    private void UpdateTargetLocation()
    {
        float targetX = crowdExcitementValue / 100.0f * 763;
        targetLocation = new Vector3(targetX, 0, 0);
    }

    private void Update()
    {
        crowdImg.GetComponent<RectTransform>().localPosition = Vector3.Lerp(crowdImg.GetComponent<RectTransform>().localPosition, targetLocation, 0.1f);
    }
}
