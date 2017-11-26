using UnityEngine;

public class BattleLevel : MonoBehaviour
{
    public static BattleLevel Instance;
    public GameObject Player, Enemy;
    public PetAnim PAnim, EAnim;

    private void Awake()
    {
        Instance = this;
        PAnim = new PetAnim(Player.transform);
        EAnim = new PetAnim(Enemy.transform);
        var pv = new PlayerView(PAnim);
        PAnim.View = pv;
        var ev = new PlayerView(EAnim);
        EAnim.View = ev;
        PAnim.StartBreath();
        EAnim.StartBreath();
    }
}