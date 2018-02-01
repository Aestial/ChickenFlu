using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationController : MonoBehaviour
{
    private NavMeshAgent agent;
    //public Transform testTransform;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = testTransform.position;
    }

    void Update()
    {
        //if (Input.GetMouseButtonUp(0))
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
        //    {
        //        agent.destination = hit.point;
        //    }    
        //}
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
        {
            agent.destination = hit.point;
        }

    }
}
