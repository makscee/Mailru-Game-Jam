using UnityEngine;

public enum PlayerSmth
{
    Cloth,
    Pillow
}

public class PlayerParts
{
    public SpriteRenderer Body;
    public SpriteRenderer Suit;
    public SpriteRenderer Paws;
    public SpriteRenderer Nose;
    public SpriteRenderer Tail;
    public SpriteRenderer Eyes;
    public SpriteRenderer Pillow;
    public SpriteRenderer Shadow;
}

public class ShopGameLogic : MonoBehaviour
{
    private GameObject _playerView;

    public PetAnim PetAnim;

    public static ShopGameLogic Instance;

    [SerializeField] private Camera _camera;

    private void Awake()
    {
        Instance = this;
        
        if (_camera == null)
        {
            _camera = Camera.main;
        }

        _playerView = (GameObject) Instantiate(Resources.Load("PlayerView", typeof(GameObject)));

        var playerObj = (GameObject) Instantiate(Resources.Load("ShopPlayerIO", typeof(GameObject)));
        _playerView.transform.parent = playerObj.transform;
        
        playerObj.GetComponent<ShopPlayerIO>().ShopGameLogic = this;
        
        playerObj.transform.position = new Vector3(0, 2f, -2f);

        PetAnim = new PetAnim(_playerView.transform);

        PlayerView.EyeType = EyeType.Center3;
        PlayerView.NoseType = NoseType.Nose1;
        PlayerView.PawsType = PawsType.Down;
        PlayerView.TailState = TailState.Normal;
        PlayerView.SuitType = SuitType.None;
    }

    private void Start()
    {
        PetAnim.StartBreath();
        
        Utils.InvokeDelayed(() =>
        {
            PetAnim.Jump();
        }, 1f);
    }

    public void ClickOnPlayer()
    {
        if (PetAnim.Jump())
        {
            PetAnim.UpdateFace();
        }
    }
}
