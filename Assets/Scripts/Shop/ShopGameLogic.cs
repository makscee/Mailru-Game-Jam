using UnityEngine;

public enum PlayerSmth
{
    Cloth,
    Pillow
}

class PlayerParts
{
    public SpriteRenderer Body;
    public SpriteRenderer Hands;
    public SpriteRenderer Nose;
    public SpriteRenderer Tail;
    public SpriteRenderer Eyes;
    public SpriteRenderer Pillow;
    public SpriteRenderer Shadow;
}

public class ShopGameLogic : MonoBehaviour
{
    private GameObject _playerView;
    private GameObject _playerObj;

    public static ShopGameLogic Instance;

    private PlayerParts _playerParts;
    private Transform[] mainPartsT;

    [SerializeField] private Camera _camera;

    private bool _isDirLeft = true;

    private bool _inAnim;
    
    private void Awake()
    {
        Instance = this;
        
        _playerParts = new PlayerParts();
        
        if (_camera == null)
        {
            _camera = Camera.main;
        }

        _playerView = (GameObject) Instantiate(Resources.Load("PlayerView", typeof(GameObject)));

        _playerObj = (GameObject) Instantiate(Resources.Load("ShopPlayerIO", typeof(GameObject)));
        _playerView.transform.parent = _playerObj.transform;
        
       _playerObj.GetComponent<ShopPlayerIO>().ShopGameLogic = this;
        
        _playerObj.transform.position = new Vector3(0, 0, -2f);
        
        PlayerState.LoadFromPrefs();
        
        Clothes.Load();
        Pillows.Load();
        
        /*var playerCloth = _playerView.transform.Find("Cloth");
        _playerClothSpriteRenderer = playerCloth.GetComponent<SpriteRenderer>();
        if (PlayerState.ClothIndex >= Clothes.Sprites.Count)
        {
            PlayerState.ClothIndex = 0;
        }
        _playerClothSpriteRenderer.sprite = Clothes.Sprites[PlayerState.ClothIndex];
        playerCloth.gameObject.SetActive(false);*/
        
        _playerParts.Body = _playerView.transform.Find("Body").GetComponent<SpriteRenderer>();
        _playerParts.Hands = _playerView.transform.Find("Hands").GetComponent<SpriteRenderer>();
        _playerParts.Nose = _playerView.transform.Find("Nose").GetComponent<SpriteRenderer>();
        _playerParts.Tail = _playerView.transform.Find("Tail").GetComponent<SpriteRenderer>();
        _playerParts.Eyes = _playerView.transform.Find("Eyes").GetComponent<SpriteRenderer>();
        _playerParts.Pillow = _playerView.transform.Find("Pillow").GetComponent<SpriteRenderer>();
        _playerParts.Shadow = _playerView.transform.Find("Shadow").GetComponent<SpriteRenderer>();
        
        mainPartsT = new Transform[]
        {
            _playerParts.Body.transform,
            _playerParts.Hands.transform,
            _playerParts.Nose.transform,
            _playerParts.Tail.transform,
            _playerParts.Eyes.transform,
        };
        
        if (PlayerState.PillowIndex >= Pillows.Sprites.Count)
        {
            PlayerState.PillowIndex = 0;
        }
        _playerParts.Pillow.sprite = Pillows.Sprites[PlayerState.PillowIndex][PillowState.Idle];
        //playerPillow.gameObject.SetActive(true);
        
        _playerParts.Shadow = _playerView.transform.Find("Shadow").GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        breath(1);
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
            //_playerParts.Body.sprite = Clothes.Sprites[curIdx];
        } else {
            PlayerState.PillowIndex = curIdx;
            _playerParts.Pillow.sprite = Pillows.GetActive(PillowState.Idle);
        }
    }

    private void ClickAnim()
    {
        const float animationWindow = 0.1f;
        const float pillowAnimationWindow = animationWindow/1.5f;
        const float squizeVal = 0.05f;

        const float PetTSpeed = 18f;
        const float PillowSpeed = 7f;
        const float ShadowSpeed = -4f;
        
        var steps = new Vector2[]
        {
            new Vector2(0, squizeVal*1.5f),
            new Vector2(squizeVal, -squizeVal*2)
        };

        var PillowT = _playerParts.Pillow.transform;
        var ShadowT = _playerParts.Shadow.transform;

        // Up
        
        Utils.Animate(Vector2.zero, steps[0], animationWindow, (v) =>
        {
            foreach (var t in mainPartsT)
            {
              t.localScale += v;
              t.localPosition += PetTSpeed * v;
            }
        });
        Utils.Animate(Vector2.zero, new Vector2(0, squizeVal), pillowAnimationWindow, (v) =>
        {
            PillowT.localPosition += PillowSpeed * v;

            v.x = v.y;
            v.z = v.y;
            v.y = 0;
            PillowT.localScale += ShadowSpeed * v;
            ShadowT.localScale += ShadowSpeed*v;
        });
        
        // Down
        Utils.InvokeDelayed(() =>
        {
            Utils.Animate(new Vector2(0, squizeVal), Vector2.zero, pillowAnimationWindow, (v) =>
            {
                PillowT.localPosition += PillowSpeed * v;
                
                v.x = v.y;
                v.z = v.y;
                v.y = 0;
                PillowT.localScale += ShadowSpeed*v;
                ShadowT.localScale += ShadowSpeed*v;
            });
        }, pillowAnimationWindow);
        
        Utils.InvokeDelayed(() =>
        {
            Utils.Animate(steps[0], steps[1], animationWindow, (v) =>
            {
                foreach (var t in mainPartsT)
                {
                    t.localScale += v;
                }
            });
            Utils.Animate(steps[0], Vector2.zero, animationWindow, (v) =>
            {
                foreach (var t in mainPartsT)
                {
                    t.localPosition += PetTSpeed * v;
                }
            });
            
            Utils.InvokeDelayed(() =>
            {
                // Restore
                
                Utils.Animate(steps[1], Vector3.zero, animationWindow, (v) =>
                {
                    foreach (var t in mainPartsT)
                    {
                        t.localScale += v;
                    }
                });
                Utils.InvokeDelayed(() =>
                {
                    _inAnim = false;
                }, animationWindow);
            }, animationWindow);
        }, animationWindow);
    }

    private void breath(float dir)
    {
        const float over = 1f;
        
        Utils.Animate(Vector2.zero, new Vector2(0, 0.08f), over, (v) =>
        {
            v *= dir;
            
            _playerParts.Body.transform.localScale += v;
            _playerParts.Body.transform.localPosition += v;
            _playerParts.Eyes.transform.localPosition += 2*v;
            _playerParts.Nose.transform.localPosition += 2*v;
        });
        
        Utils.InvokeDelayed(() =>
        {
            breath(-dir);
        }, over);
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

            Vector3 scale = _playerObj.transform.localScale;
            scale.x = -scale.x;
            _playerObj.transform.localScale = scale;
        }
    }
}
