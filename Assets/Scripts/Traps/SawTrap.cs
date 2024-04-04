using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
    [SerializeField] private bool cycleLoop;
    [SerializeField] private bool chainVisible;
    [SerializeField] private float sawSpeed;
    [SerializeField] private GameObject sawPrefab;
    [SerializeField] private GameObject chainPrefab;
    [SerializeField] private List<Vector2> SawPoints;
    private Transform sawPos;
    private int currentIndex;
    private Vector2 targetPos;
    private float distance;
    private void Start()
    {
        GameObject sawObject = Instantiate(sawPrefab, SawPoints[currentIndex], Quaternion.identity, transform);
        sawPos = sawObject.transform;
        currentIndex++;
        Debug.Log(currentIndex);
        targetPos = SawPoints[currentIndex];
        if (chainVisible)
        {
            RenderChainLine();
        }
    }
    private void FixedUpdate()
    {
        sawPos.position = Vector2.MoveTowards(sawPos.position, targetPos, sawSpeed * Time.fixedDeltaTime);
        distance = Vector2.Distance(sawPos.position, targetPos);
        if (distance <= 0.01f)
        {
            NextPoint();
        }
    }
    public void CreateSawPoint()
    {
        SawPoints.Add(transform.position);
    }
    private void NextPoint()
    {
        if (cycleLoop)
        {
            currentIndex++;
            currentIndex %= SawPoints.Count;
            targetPos = SawPoints[currentIndex];
        }
        else
        {
            if (currentIndex >= SawPoints.Count - 1)
            {
                SawPoints.Reverse();
                currentIndex = 0;
            }
            currentIndex++;
            targetPos = SawPoints[currentIndex];
        }
    }
    private void RenderChainLine()
    {
        for (int i = 0; i < SawPoints.Count - 1; i++)
        {
            GameObject chain = Instantiate(chainPrefab, SawPoints[i], Quaternion.identity, transform);
            ChainRender chainRender = chain.GetComponent<ChainRender>();
            chainRender.DrawLineChain(SawPoints[i], SawPoints[i + 1]);
        }
        if (cycleLoop)
        {
            GameObject chain = Instantiate(chainPrefab, SawPoints[^1], Quaternion.identity, transform);
            ChainRender chainRender = chain.GetComponent<ChainRender>();
            chainRender.DrawLineChain(SawPoints[^1], SawPoints[0]);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (Vector2 point in SawPoints)
        {
            Gizmos.DrawWireSphere(point, 0.5f);
        }
    }
}
