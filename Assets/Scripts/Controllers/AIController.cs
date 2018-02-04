using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Player player;
    private Transform target;
    private int multiplier = 1; // or more

    //public Transform testTransform;

    // Use this for initialization
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GetComponent<Player>();
    }

    void Start()
    {
        target = GameObject.Find("Player0").transform;        
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
        if (StateManager.Instance.State == GameState.Battle ||
            StateManager.Instance.State == GameState.StressBattle)
        {
            if (this.player.State == PlayerState.Infected ||
                this.player.State == PlayerState.MadChicken)
            {
                this.agent.SetDestination(target.position);
            } 
            else
            {
                if (Time.frameCount%2 == 0)
                {
                    Vector3 runTo = transform.position + ((transform.position - target.position) * multiplier);
                    this.agent.SetDestination(runTo);    
                }
                else 
                {
                    this.agent.SetDestination(target.position);
                }
            }
        }
    }
    void OnDisable()
    {
        this.agent.enabled = false;
    }

}
