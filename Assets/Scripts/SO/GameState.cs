using UnityEngine;

public enum States { NORMAL,DIALOGUE, PAUSED}
[CreateAssetMenu(menuName = "Scriptable/States/GameState")]
public class GameState : TState<States>
{
    private void OnValidate()
    {
        Value = States.NORMAL;
    }
}

