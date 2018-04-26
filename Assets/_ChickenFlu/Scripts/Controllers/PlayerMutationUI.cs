using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMutationUI : MonoBehaviour {

    [SerializeField] private Text mutateButtonText;
	private Player player;
    private bool isInfected = false;

    private void Start() 
    {
        this.player = GetComponent<Player>();
    }

	public void MutateButton() 
    {
        this.isInfected = !this.isInfected;
        this.mutateButtonText.text = this.isInfected ? "Cure!" : "Mutate!";
        this.player.Mutate(this.isInfected ? PlayerState.Infected : PlayerState.Human);
    }
    public void WinButton() 
    {
        this.player.Win();
    }
}
