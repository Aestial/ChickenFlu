using UnityEngine;
using CnControls;
using Rewired;

public class InputController : MonoBehaviour 
{
    public int playerId = 0;

    [SerializeField] private float speedMultiplier = 4f;
    
    public Rewired.Player rp { get { return ReInput.isReady ? ReInput.players.GetPlayer(playerId) : null; } }
    private Player player;
    private Rigidbody rb;

	private EggController egg;
    private float speed;

	void Start () 
    {
        this.player = GetComponent<Player>();
		this.rb = GetComponent<Rigidbody> ();
		this.egg = GetComponent<EggController>();
		this.playerId = this.player.Id;
	}
	void FixedUpdate ()
    {
        if(!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if(player == null) return;
        this.speed = this.speedMultiplier * this.player.Speed;
        if (StateManager.Instance.State == GameState.Battle ||
            StateManager.Instance.State == GameState.StressBattle)
        {
            Vector3 moveVector = Vector3.zero;
            moveVector.x = rp.GetAxis("Move Horizontal"); // get input by name or action id
            moveVector.z = rp.GetAxis("Move Vertical");
            bool fire = rp.GetButtonDown("Fire");
            
            if (moveVector != Vector3.zero)
            {
                this.rb.velocity = (moveVector * this.speed);
                this.transform.rotation = Quaternion.LookRotation(moveVector);
            }
        }
		if (rp.GetButtonDown ("Fire")) {
			egg.ThrowEgg ();
		}
	}
}
