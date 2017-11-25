using UnityEngine;

public enum PlayerSmth
{
    Cloth,
    Pillow
}

public class ShopGameLogic : MonoBehaviour
{
    private GameObject _playerView;
    private GameObject _playerObj;

    public static ShopGameLogic Instance;

    private SpriteRenderer _playerClothSpriteRenderer;
    private SpriteRenderer _playerPillowSpriteRenderer;
    private SpriteRenderer _playerPillowShadowSpriteRenderer;

    [SerializeField] private Camera _camera;
    
    private Vector2 _moveForce;

    private bool _isDirLeft = true;

    private bool _inAnim;
    
    private void Awake()
    {
        Instance = this;
        
        if (_camera == null)
        {
            _camera = Camera.main;
        }

        _playerView = (GameObject) Instantiate(Resources.Load("Player", typeof(GameObject)));

        _playerObj = (GameObject) Instantiate(Resources.Load("ShopPlayerIO", typeof(GameObject)));
        _playerView.transform.parent = _playerObj.transform;
        
       _playerObj.GetComponent<ShopPlayerIO>().ShopGameLogic = this;
        
        _playerObj.transform.position = new Vector3(0, -1f, -2f);
        
        PlayerState.LoadFromPrefs();
        
        Clothes.Load();
        Pillows.Load();
        
        var playerCloth = _playerView.transform.Find("Cloth");        
        _playerClothSpriteRenderer = playerCloth.GetComponent<SpriteRenderer>();
        if (PlayerState.ClothIndex >= Clothes.Sprites.Count)
        {
            PlayerState.ClothIndex = 0;
        }
        _playerClothSpriteRenderer.sprite = Clothes.Sprites[PlayerState.ClothIndex];
        playerCloth.gameObject.SetActive(false);
        
        var playerPillow = GameObject.Find("Pillow");
        _playerPillowSpriteRenderer = playerPillow.GetComponent<SpriteRenderer>();
        if (PlayerState.PillowIndex >= Pillows.Sprites.Count)
        {
            PlayerState.PillowIndex = 0;
        }
        _playerPillowSpriteRenderer.sprite = Pillows.Sprites[PlayerState.PillowIndex][PillowState.Idle];
        //playerPillow.gameObject.SetActive(true);
        
        _playerPillowShadowSpriteRenderer = GameObject.Find("PillowShadow").GetComponent<SpriteRenderer>();
        if (null == _playerPillowShadowSpriteRenderer)
        {
            Application.Quit();
        }
    }

    public void ChangeSmth(PlayerSmth smth, bool next)
    {
        int curIdx, len;
        if (PlayerSmth.Cloth == smth)
        {
            curIdx = PlayerState.ClothIndex;
            len = Clothes.Sprites.Count;
        } else {
            curIdx = PlayerState.PillowIndex;
            len = Pillows.Sprites.Count;
        }
        
        var incrIdx = next ? 1 : -1;
        curIdx += incrIdx;
        if (curIdx < 0)
        {
            curIdx = len - 1;
        } else if (curIdx >= len)
        {
            curIdx = 0;
        }

        if (PlayerSmth.Cloth == smth)
        {
            PlayerState.ClothIndex = curIdx;
            _playerClothSpriteRenderer.sprite = Clothes.Sprites[curIdx];
        } else {
            PlayerState.PillowIndex = curIdx;
            _playerPillowSpriteRenderer.sprite = Pillows.GetActive(PillowState.Idle);
        }
    }

    private void ClickAnim()
    {
        const float animationWindow = 0.1f;
        const float squizeVal = 0.05f;
        
        var steps = new Vector2[]
        {
            new Vector2(0, squizeVal*1.5f),
            new Vector2(squizeVal, -squizeVal*2)
        };
        
        _playerPillowSpriteRenderer.sprite = Pillows.GetActive(PillowState.Up);

        var PetT = _playerObj.transform;
        var PilT = _playerPillowSpriteRenderer.transform;
        var PilST = _playerPillowShadowSpriteRenderer.transform;
        
        Utils.Animate(Vector2.zero, steps[0], animationWindow, (v) => { PetT.localScale += v; });
        Utils.Animate(Vector2.zero, new Vector2(0, squizeVal*2), animationWindow, (v) =>
        {
            PetT.localPosition += 15 * v;
            PilT.localPosition += 5 * v;

            v.x = v.y;
            v.z = v.y;
            v.y = 0;
            PilST.localScale -= 4*v;
        });
        
        Utils.InvokeDelayed(() =>
        {
            _playerPillowSpriteRenderer.sprite = Pillows.GetActive(PillowState.Idle);
            
            Utils.Animate(steps[0], steps[1], animationWindow, (v) => { PetT.localScale += v; });
            Utils.Animate(new Vector2(0, squizeVal*2), Vector2.zero, animationWindow, (v) =>
            {
                PetT.localPosition += 15 * v;
                PilT.localPosition += 5 * v;
                
                v.x = v.y;
                v.z = v.y;
                v.y = 0;
                PilST.localScale -= 4*v;
            });
            Utils.InvokeDelayed(() =>
            {
                Utils.Animate(steps[1], Vector3.zero, animationWindow, (v) => { PetT.localScale += v; });
                Utils.InvokeDelayed(() =>
                {
                    _playerPillowSpriteRenderer.sprite = Pillows.GetActive(PillowState.Idle);
                    _inAnim = false;
                }, animationWindow);
            }, animationWindow);
        }, animationWindow);
    }

    public void ClickOnPlayer()
    {
        if (_inAnim)
        {
            return;
        }
        _inAnim = true;
        ClickAnim();

        if (Random.value < 0.5f)
        {
            _isDirLeft = !_isDirLeft;

            Vector3 scale;

            scale = _playerObj.transform.localScale;
            scale.x = -scale.x;
            _playerObj.transform.localScale = scale;
            
            scale = _playerPillowSpriteRenderer.transform.localScale;
            scale.x = -scale.x;
            _playerPillowSpriteRenderer.transform.localScale = scale;
            
            scale = _playerPillowShadowSpriteRenderer.transform.localScale;
            scale.x = -scale.x;
            _playerPillowShadowSpriteRenderer.transform.localScale = scale;
        }
    }
}
