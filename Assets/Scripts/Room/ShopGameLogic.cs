using UnityEngine;

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
    private PlayerView pv;

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
        pv = new PlayerView(PetAnim);
        PetAnim.View = pv;

        pv.EyeType = EyeType.Center3;
        pv.NoseType = NoseType.Nose1;
        pv.PawsType = PawsType.Down;
        pv.TailState = TailState.Normal;
        pv.SuitType = SuitType.None;
        if (Results.NewSuit)
        {
            pv.SuitType = SuitType.Pig;
        }
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
