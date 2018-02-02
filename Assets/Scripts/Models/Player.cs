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
    Chicken
}

[System.Serializable]
public struct GraphicState
{
    public PlayerState state;
    public Transform mesh;
};

[System.Serializable]
public struct SpeedState
{
    public PlayerState state;
    public float speed;
};
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
    [SerializeField] private AudioClip dieFX;
    [SerializeField] private SkinnedMeshRenderer coatMesh;
    [SerializeField] private Text markerText;
    [SerializeField] private Image markerImage;

    private int number;
    private float health;
    private PlayerState state;
    private float speed;
    private bool canBeInfected;
    private Transform mesh;
    private Texture texture;
    private Color color;
    private PlayerUIController ui;

    private Notifier notifier;
    public const string ON_DIE = "OnDie";

    public int Number
    {
        get { return number; }
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
        this.number = num;
        this.name = "Player" + this.number.ToString();
        this.texture = playerCustoms[this.number].texture;
        this.color = playerCustoms[this.number].color;
        this.transform.LookAt(new Vector3(0, this.transform.position.y, 0));
        this.coatMesh.material.mainTexture = this.texture;
        this.markerText.text = (this.number + 1) + "P";
        this.markerText.color = this.color;
        this.markerImage.color = this.color;
    }
    private void CheckHealth()
    {
        if (this.health <= 0.0f) 
        {
            Debug.Log("Player " + this.number + " is dead!");
            notifier.Notify(ON_DIE);
            AudioManager.Instance.PlayOneShoot(dieFX, this.transform.position);
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
        this.mesh.gameObject.SetActive(false);
        this.state = newState;
        this.speed = stateCustoms[(int)this.state].speed;
        this.mesh = stateCustoms[(int)this.state].mesh;
        this.mesh.gameObject.SetActive(true);   
    }

    void Update () 
    {
        if (this.state == PlayerState.Infected) 
        {
            this.UpdateHealth(-infectedDamage);
        }
	}
    void OnDestroy()
    {
        notifier.UnsubcribeAll();
    }
}
