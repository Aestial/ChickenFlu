using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] private float multiplier = 4.0f;

    private Player player;
    private int index;
    private GameObject[] players;
    private Rigidbody rb;
    private NavMeshAgent agent;
    private Transform target;
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
        this.players = GameObject.FindGameObjectsWithTag("Player");
        this.index = this.player.Id;
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
                this.target = this.FindNearest();
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
                    this.agent.SetDestination(this.transform.position);
                }
            }
            this.rb.velocity = this.agent.velocity;
            this.transform.rotation = Quaternion.LookRotation(this.rb.velocity);
        }
        else
        {
            this.rb.velocity = Vector3.zero;
            this.agent.enabled = false;    
        }
    }

    private Transform FindNearest()
    {
        Vector3 position = this.transform.position;
        Transform nearest = this.players[0].transform;
        float distance = Vector3.Distance(position, nearest.transform.position);
        for (int i = 0; i < this.players.Length; i++) 
        {
            if (this.index != i)
            {
                Transform other = this.players[i].transform;
                float otherDistance = Vector3.Distance(position, other.position);
                if (otherDistance < distance) 
                {
                    distance = otherDistance;
                    nearest = other;
                }
            }
        }
        return nearest;
    }

    void OnDisable()
    {
        this.agent.enabled = false;
    }

}
