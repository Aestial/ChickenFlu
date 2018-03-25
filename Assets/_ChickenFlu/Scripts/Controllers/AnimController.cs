using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour 
{
    [System.Serializable]
    public struct AnimatorState
    {
        public PlayerState state;
        public Animator animator;
    }

    [SerializeField] private AnimatorState[] animatorStates;

    private Player player;
    private Rigidbody rb;

    private Animator anim;

    void Start()
    {
        this.player = GetComponent<Player>();
        this.rb = GetComponent<Rigidbody>();
    }
    void LateUpdate()
    {
        this.UpdateAnimatorsParam("Speed", this.rb.velocity.magnitude);
    }
    void UpdateAnimatorsParam(string floatName, float value)
    {
        for (int i = 0; i < this.animatorStates.Length; i++)
        {
            if (this.animatorStates[i].state == this.player.State)
            {
                this.anim = this.animatorStates[i].animator;
                this.anim.SetFloat(floatName, value);    
            }
        }
    }

    public void UpdateAnimatorsParam(string boolName, bool value)
    {
        for (int i = 0; i < this.animatorStates.Length; i++)
        {
            if (this.animatorStates[i].state == this.player.State)
            {
                this.anim = this.animatorStates[i].animator;
                this.anim.SetBool(boolName, value);
            }
        }
    }
}
