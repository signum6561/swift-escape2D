using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
    [SerializeField] private bool cycleLoop;
    [SerializeField] private bool chainVisible;
    [SerializeField] private List<Vector2> sawPoints;
    [SerializeField] private float sawSpeed;
    [SerializeField] private GameObject sawPrefab;
    private Transform sawPos;
    private int currentIndex;
    private Vector2 targetPos;
    private void Start()
    {
        GameObject sawObject = Instantiate(sawPrefab, sawPoints[currentIndex], Quaternion.identity, gameObject.transform);
        sawPos = sawObject.transform;
        currentIndex++;
        targetPos = sawPoints[currentIndex];
    }
    private void FixedUpdate()
    {
        sawPos.position = Vector2.MoveTowards(sawPos.position, targetPos, sawSpeed * Time.deltaTime);
        float distance = Vector2.Distance(sawPos.position, targetPos);
        if (distance <= 0.01f)
        {
            NextPoint();
        }
    }
    private void NextPoint()
    {
        if (cycleLoop)
        {
            currentIndex++;
            currentIndex %= sawPoints.Count;
            targetPos = sawPoints[currentIndex];
        }
        else
        {
            if (currentIndex >= sawPoints.Count - 1)
            {
                sawPoints.Reverse();
                currentIndex = 0;
            }
            currentIndex++;
            targetPos = sawPoints[currentIndex];
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (Vector2 point in sawPoints)
        {
            Gizmos.DrawSphere(point, 0.5f);
        }
    }
}
