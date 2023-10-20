using UnityEngine;

public enum States { NORMAL,DIALOGUE, PAUSED, CUTSCENE}
[CreateAssetMenu(menuName = "ScriptableObjects/States/GameState")]
public class GameState : TState<States>
{
    private void OnValidate()
    {
        Value = States.NORMAL;
    }
}

