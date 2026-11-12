using UnityEngine;
[CreateAssetMenu(fileName = "PlayerControls", menuName = "Player/Controls")]
public class PlayerControls : ScriptableObject
{
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode reftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    //public KeyCode parryKey = KeyCode.F?;
}
