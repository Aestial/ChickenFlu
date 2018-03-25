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
//<<<<<<< HEAD:Assets/Scripts/Models/Player.cs
    MovementDisabled,
//=======
    Damaged
//>>>>>>> master:Assets/_ChickenFlu/Scripts/Models/Player.cs
}
[System.Serializable]
public struct StateCustoms
{
    public PlayerState state;
    public Transform mesh;
    public float speed;
}
[System.Serializable]
public struct PlayerCustoms
{
    public Texture texture;
    public Color color;
}

public class Player : MonoBehaviour 
{
    [SerializeField] private float infectedDamage;
    [SerializeField] private StateCustoms[] stateCustoms;
    [SerializeField] private PlayerCustoms[] playerCustoms;
    [SerializeField] private AudioClip dieSoundFX;
    [SerializeField] private GameObject dieFX;
    [SerializeField] private SkinnedMeshRenderer coatMesh;
    [SerializeField] private Text markerText;
    [SerializeField] private Image markerImage;
    [SerializeField] private int id;
    
    private float health;
    private PlayerState state;
    private float speed;
    private bool canBeInfected;
    private Transform mesh;
    private Transform botarga;
    private Texture texture;
    private Color color;
    private bool playable;

    private AnimController anim;
    private InputController input;
    private AIController ai;

    private PlayerUIController ui;

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
    public PlayerUIController UI
    {
        get { return ui; }
        set { ui = value; }
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
        this.texture = playerCustoms[this.id].texture;
		this.color = playerCustoms[this.id].color;
        this.transform.LookAt(new Vector3(0, this.transform.position.y, 0));
        this.coatMesh.material.mainTexture = this.texture;
		this.markerText.text = (this.id + 1) + "P";
        this.markerText.color = this.color;
        this.markerImage.color = this.color;
    }
    private void ControlConfig(bool playable)
    {
        this.anim = GetComponent<AnimController>();
        this.input= GetComponent<InputController>();
        this.ai = GetComponent<AIController>();
        this.ai.enabled = !playable;
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
        this.ui.UpdateHealth(this.health);
    }

    public void Mutate(PlayerState newState)
    {
        this.state = newState;
        if (this.state == PlayerState.Infected)
        {
            this.botarga = stateCustoms[(int)this.state].mesh;
            this.botarga.gameObject.SetActive(true);
        }
        else if (this.state == PlayerState.Damaged)
        {
            
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
