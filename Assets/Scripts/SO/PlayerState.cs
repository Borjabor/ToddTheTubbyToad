using UnityEngine;

public enum PlayerStates { NORMAL,INBUBBLE, RESPAWN}
[CreateAssetMenu(menuName = "ScriptableObjects/States/PlayerState")]
public class PlayerState : TState<PlayerStates>
{
    private void OnValidate()
    {
        Value = PlayerStates.NORMAL;
    }
}

