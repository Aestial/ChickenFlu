using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Player player;
    private Rigidbody rb;

    private Transform target;
    private int multiplier = 1; // or more

    private float speed;

    void Awake()
    {
        this.agent = GetComponent<NavMeshAgent>();
        this.player = GetComponent<Player>();
        this.rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        this.target = GameObject.Find("Player0").transform;
        this.agent.updatePosition = false;
        this.agent.updateRotation = false;
    }

    void FixedUpdate()
    {
        this.speed = this.multiplier * this.player.Speed;
        if (StateManager.Instance.State == GameState.Battle ||
            StateManager.Instance.State == GameState.StressBattle)
        {
            this.agent.enabled = true;
            this.agent.speed = this.speed;
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
            this.rb.velocity = this.agent.velocity;
            this.transform.rotation = Quaternion.LookRotation(this.rb.velocity);
            Debug.Log(this.rb.velocity);
        }
        else
        {
            this.rb.velocity = Vector3.zero;
            this.agent.enabled = false;    
        }
    }
    void OnDisable()
    {
        this.agent.enabled = false;
    }

}
