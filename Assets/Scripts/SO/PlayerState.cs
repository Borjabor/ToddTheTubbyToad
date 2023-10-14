using UnityEngine;

public enum PlayerStates { NORMAL,INBUBBLE}
[CreateAssetMenu(menuName = "Scriptable/States/PlayerState")]
public class PlayerState : TState<PlayerStates>
{
    private void OnValidate()
    {
        Value = PlayerStates.NORMAL;
    }
}

