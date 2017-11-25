using UnityEngine;

public class BattleLevel : MonoBehaviour
{
    public static BattleLevel Instance;
    public GameObject Player, Enemy;

    private void Awake()
    {
        Instance = this;
    }
}