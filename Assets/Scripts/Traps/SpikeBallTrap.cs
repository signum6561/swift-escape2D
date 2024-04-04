using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SpikeBallTrap : MonoBehaviour
{
    [SerializeField] private bool cycleLoop;
    [SerializeField] private bool reverse;
    [SerializeField] private float radius = 5f;
    [Range(0f, 20f)]
    [SerializeField] private float angularSpeed = 10f;
    [Range(0f, 360f)]
    [SerializeField] private float startAngle;
    [Range(0f, 360f)]
    [SerializeField] private float rangeAngle;
    [SerializeField] private GameObject spikeBallPrefab;
    [SerializeField] private GameObject chainPrefab;
    private Vector3 startAnglePoint;
    private Vector3 endAnglePoint;
    private Vector2 targetPos;
    private float distance;
    private Transform spikeBallPos;
    private Transform chainPos;
    private float tempAngle;
    private Vector3 tempAnglePoint;
    private bool switchTargetPos;
    private void Start()
    {
        startAnglePoint = CalculatePointOnArc2D(startAngle);
        endAnglePoint = CalculatePointOnArc2D(startAngle + rangeAngle);
        targetPos = endAnglePoint;
        tempAngle = startAngle;
        if (reverse)
        {
            angularSpeed *= -1;
        }
        spikeBallPos = Instantiate(spikeBallPrefab, startAnglePoint, Quaternion.identity, transform).transform;
        RenderChainLine();
    }
    private void Update()
    {
        spikeBallPos.position = tempAnglePoint;
        tempAngle += angularSpeed * 10f * Time.deltaTime;
        tempAngle %= 360f;
        tempAnglePoint = CalculatePointOnArc2D(tempAngle);
        // spikeBallPos.RotateAround(transform.position, Vector3.forward, angularSpeed * Time.fixedDeltaTime);
        // chainPos.RotateAround(transform.position, Vector3.forward, angularSpeed * Time.fixedDeltaTime);
        chainPos.right = spikeBallPos.position - transform.position;
        distance = Vector2.Distance(spikeBallPos.position, targetPos);
        if (!cycleLoop)
        {
            CheckDistance();
        }
    }
    private void CheckDistance()
    {
        if (distance <= 0.1f)
        {
            switchTargetPos = !switchTargetPos;
            if (switchTargetPos)
            {
                targetPos = startAnglePoint;
            }
            else
            {
                targetPos = endAnglePoint;
            }
            angularSpeed *= -1;
        }
    }
    private Vector3 CalculatePointOnArc2D(float angle)
    {
        float angleRadians = angle * Mathf.Deg2Rad;
        return transform.position + new Vector3(radius * Mathf.Cos(angleRadians), radius * Mathf.Sin(angleRadians), 0f);
    }
    private void RenderChainLine()
    {
        GameObject chain = Instantiate(chainPrefab, transform.position, Quaternion.identity, transform);
        chainPos = chain.transform;
        ChainRender chainRender = chain.GetComponent<ChainRender>();
        chainRender.DrawLineChain(transform.position, startAnglePoint);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, CalculatePointOnArc2D(startAngle));
        if (!cycleLoop)
        {
            Gizmos.DrawLine(transform.position, CalculatePointOnArc2D(startAngle + rangeAngle));
        }
        // Gizmos.DrawLine(spikeBallPos.position, targetPos);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
