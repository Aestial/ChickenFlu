using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    /** Initial */
    Human,
    /** Infected */
    Infected,
    /** Dying */
    MadChicken,
    /** Dead */
    Chicken,
    Damaged
}
[System.Serializable]
public struct StateCustoms
{
    public PlayerState state;
    public Transform mesh;
    public float speed;
}

public class Player : MonoBehaviour 
{
    [SerializeField] private AudioClip sneezeAudioFX;
    [SerializeField] private GameObject sneezeFX;
    [SerializeField] private float headHeight = 0.6f;
    [SerializeField] private float infectedDamage;
    [SerializeField] private StateCustoms[] stateCustoms;
    [SerializeField] private AudioClip dieSoundFX;
    [SerializeField] private GameObject dieFX;
    [SerializeField] private int id;
    
    private float health;
    private PlayerState state;
    private float speed;
    private bool canBeInfected;
    private Transform mesh;
    private Transform botarga;
    private bool playable;

    private AnimController anim;
 
    private Notifier notifier;
    public const string ON_DIE = "OnDie";

    public int Id
    {
        get { return id; }
        set { Config(value); }
    }
    public PlayerState State
    {
        get { return state; }
        set { state = value; }
    }
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public bool CanBeInfected
    {
        get { return canBeInfected; }
        set { canBeInfected = value; }
    }
    public bool Playable 
    {
        get { return playable; }
        set { ControlConfig(value); }
    }

    void Start()
    {
        this.health = 1.0f;
        this.state = PlayerState.Human;
        this.speed = stateCustoms[(int)this.state].speed;
        this.mesh = stateCustoms[(int)this.state].mesh;
        this.mesh.gameObject.SetActive(true);
        this.canBeInfected = true;
    }
    void Awake()
    {
        // Notifier
        notifier = new Notifier();
    }

    private void Config(int num)
    {
        this.id = num;
		this.name = "Player" + this.id.ToString();
    }
    private void ControlConfig(bool playable)
    {
        this.anim = GetComponent<AnimController>();
    }
    private void CheckHealth()
    {
        if (this.health <= 0.0f) 
        {
			Debug.Log("Player " + this.id + " is dead!");
            notifier.Notify(ON_DIE);
            AudioManager.Instance.PlayOneShoot(dieSoundFX, this.transform.position);
            Instantiate(dieFX,this.transform.position + new Vector3(0, 1), Quaternion.identity);
            this.Mutate(PlayerState.MadChicken);
        }
    }

    public void UpdateHealth(float amount)
    {
        this.health += amount;
        this.CheckHealth();
    }

    public void Mutate(PlayerState newState)
    {
        this.state = newState;
        if (this.state == PlayerState.Infected)
        {
            this.MutateEffect();
        }
        else
        {
            this.mesh.gameObject.SetActive(false);
            this.botarga.gameObject.SetActive(false);
            this.mesh = stateCustoms[(int)this.state].mesh;
            this.mesh.gameObject.SetActive(true);       
        }
        this.speed = stateCustoms[(int)this.state].speed;
    }

    private void MutateEffect() 
    {
        this.botarga = stateCustoms[(int)this.state].mesh;
        this.botarga.gameObject.SetActive(true);
        Vector3 headPos = this.transform.position + new Vector3(0, headHeight);
        Vector3 offset = headPos - Camera.main.transform.position;
        offset = -offset.normalized;
        Instantiate(sneezeFX, headPos + offset, Quaternion.identity);
        AudioManager.Instance.PlayOneShoot(sneezeAudioFX, this.transform.position);  
    }

    public void Win()
    {
        this.anim.UpdateAnimatorsParam("Winner", true);
    }

    void Update () 
    {
        if (this.state == PlayerState.Infected) 
        {
            this.UpdateHealth(-infectedDamage*Time.deltaTime);
        }
	}

    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }

}
