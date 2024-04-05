using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private bool cycleLoop;
    [SerializeField] private bool chainVisible;
    [SerializeField] private float objectSpeed = 3f;
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private GameObject chainPrefab;
    [SerializeField] private List<Vector2> MovePoints;
    private Transform objectPos;
    private int currentIndex;
    private Vector2 targetPos;
    private float distance;
    protected virtual void Start()
    {
        GameObject sawObject = Instantiate(objectPrefab, MovePoints[currentIndex], Quaternion.identity, transform);
        objectPos = sawObject.transform;
        currentIndex++;
        targetPos = MovePoints[currentIndex];
        if (chainVisible)
        {
            RenderChainLine();
        }
    }
    protected virtual void FixedUpdate()
    {
        objectPos.position = Vector2.MoveTowards(objectPos.position, targetPos, objectSpeed * Time.fixedDeltaTime);
        distance = Vector2.Distance(objectPos.position, targetPos);
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
            currentIndex %= MovePoints.Count;
            targetPos = MovePoints[currentIndex];
        }
        else
        {
            if (currentIndex >= MovePoints.Count - 1)
            {
                MovePoints.Reverse();
                currentIndex = 0;
            }
            currentIndex++;
            targetPos = MovePoints[currentIndex];
        }
    }
    private void RenderChainLine()
    {
        for (int i = 0; i < MovePoints.Count - 1; i++)
        {
            GameObject chain = Instantiate(chainPrefab, MovePoints[i], Quaternion.identity, transform);
            ChainRender chainRender = chain.GetComponent<ChainRender>();
            chainRender.DrawLineChain(MovePoints[i], MovePoints[i + 1]);
        }
        if (cycleLoop)
        {
            GameObject chain = Instantiate(chainPrefab, MovePoints[^1], Quaternion.identity, transform);
            ChainRender chainRender = chain.GetComponent<ChainRender>();
            chainRender.DrawLineChain(MovePoints[^1], MovePoints[0]);
        }
    }
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (Vector2 point in MovePoints)
        {
            Gizmos.DrawWireSphere(point, 0.5f);
        }
    }
}
