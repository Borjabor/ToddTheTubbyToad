using UnityEngine;

public enum PlayerStates { IDLE,WALKING,TALKING}
[CreateAssetMenu(menuName = "Scriptable/States/PlayerState")]
public class PlayerState : TState<PlayerStates>
{
    private void OnValidate()
    {
        Value = PlayerStates.IDLE;
    }
}

