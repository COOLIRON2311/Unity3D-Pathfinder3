using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent2Script : MonoBehaviour
{
    private NavMeshAgent agent;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                agent.SetDestination(hit.point);
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject target = GameObject.FindGameObjectWithTag("Target");
            if (target != null)
            {
                agent.SetDestination(target.transform.position);
            }
        }
    }
}
