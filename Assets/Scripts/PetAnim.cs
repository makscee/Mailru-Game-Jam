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
    public SpriteRenderer Crown;
}

public class PetAnim
{
    private readonly Transform _playerView;

    public readonly PlayerParts PlayerParts;
    public PlayerView View;
    private readonly Transform[] _mainPartsT;
    
    private int _blinkIter;
    private int _changeFaceIter;
    private EyeType _lastEyeType;
    
    private bool _inJump;

    public PetAnim(Transform playerView)
    {
        _playerView = playerView;

        PlayerParts = new PlayerParts();

        Suits.Load();
        Tails.Load();
        Eyes.Load();
        Noses.Load();
        Paws.Load();

        PlayerParts.Body = _playerView.transform.Find("Body").GetComponent<SpriteRenderer>();
        PlayerParts.Suit = _playerView.transform.Find("Suit").GetComponent<SpriteRenderer>();
        PlayerParts.Paws = _playerView.transform.Find("Paws").GetComponent<SpriteRenderer>();
        PlayerParts.Nose = _playerView.transform.Find("Nose").GetComponent<SpriteRenderer>();
        PlayerParts.Tail = _playerView.transform.Find("Tail").GetComponent<SpriteRenderer>();
        PlayerParts.Eyes = _playerView.transform.Find("Eyes").GetComponent<SpriteRenderer>();
        PlayerParts.Pillow = _playerView.transform.Find("Pillow").GetComponent<SpriteRenderer>();
        PlayerParts.Shadow = _playerView.transform.Find("Shadow").GetComponent<SpriteRenderer>();
        PlayerParts.Crown = _playerView.transform.Find("Crown").GetComponent<SpriteRenderer>();

        _mainPartsT = new Transform[]
        {
            PlayerParts.Body.transform,
            PlayerParts.Suit.transform,
            PlayerParts.Paws.transform,
            PlayerParts.Nose.transform,
            PlayerParts.Tail.transform,
            PlayerParts.Eyes.transform,
            PlayerParts.Crown.transform,
        };
    }

    public void StartBreath()
    {
        Breath(1);
    }
    
    public void UpdateFace()
    {
        View.EyeType = (EyeType)Random.Range((int)EyeType.Blink1, Eyes.Count());
        View.NoseType = (NoseType)Random.Range(0, Noses.Count());
    }

    public void SetFace(EyeType e, NoseType n)
    {
        View.EyeType = e;
        View.NoseType = n;
    }

    public bool Jump()
    {
        if (_inJump)
        {
            return false;
        }
        _inJump = true;
        
        if (Random.value < 0.5f)
        {
            Vector3 scale = _playerView.transform.localScale;
            scale.x = -scale.x;
            _playerView.transform.localScale = scale;
        }
        
        const float animationWindow = 0.1f;
        const float pillowAnimationWindow = animationWindow/1.5f;
        const float squizeVal = 0.05f;

        const float petTSpeed = 18f;
        const float pillowSpeed = 7f;
        const float shadowSpeed = -4f;
        
        var steps = new Vector2[]
        {
            new Vector2(0, squizeVal*1.5f),
            new Vector2(squizeVal, -squizeVal*2)
        };

        var pillowT = PlayerParts.Pillow.transform;
        var shadowT = PlayerParts.Shadow.transform;
        
        View.SetPaws(true);

        // Up
        
        Utils.Animate(Vector2.zero, steps[0], animationWindow, (v) =>
        {
            foreach (var t in _mainPartsT)
            {
              t.localScale += v;
              t.localPosition += petTSpeed * v;
            }
        });
        Utils.Animate(Vector2.zero, new Vector2(0, squizeVal), pillowAnimationWindow, (v) =>
        {
            pillowT.localPosition += pillowSpeed * v;

            v.x = v.y;
            v.z = v.y;
            v.y = 0;
            pillowT.localScale += shadowSpeed * v;
            shadowT.localScale += shadowSpeed*v;
        });
        
        // Down
        Utils.InvokeDelayed(() =>
        {
            Utils.Animate(new Vector2(0, squizeVal), Vector2.zero, pillowAnimationWindow, (v) =>
            {
                pillowT.localPosition += pillowSpeed * v;
                
                v.x = v.y;
                v.z = v.y;
                v.y = 0;
                pillowT.localScale += shadowSpeed*v;
                shadowT.localScale += shadowSpeed*v;
            });
        }, pillowAnimationWindow);
        
        Utils.InvokeDelayed(() =>
        {
            Utils.Animate(steps[0], steps[1], animationWindow, (v) =>
            {
                foreach (var t in _mainPartsT)
                {
                    t.localScale += v;
                }
            });
            Utils.Animate(steps[0], Vector2.zero, animationWindow, (v) =>
            {
                foreach (var t in _mainPartsT)
                {
                    t.localPosition += petTSpeed * v;
                }
            });
            
            Utils.InvokeDelayed(() =>
            {
                // Restore
                View.SetPaws(false);
                
                Utils.Animate(steps[1], Vector3.zero, animationWindow, (v) =>
                {
                    foreach (var t in _mainPartsT)
                    {
                        t.localScale += v;
                    }
                });
                Utils.InvokeDelayed(() =>
                {
                    _inJump = false;
                }, animationWindow);
            }, animationWindow);
        }, animationWindow);

        return true;
    }

    private void Breath(float dir)
    {
        const float over = 1f;
        
        Utils.Animate(Vector2.zero, new Vector2(0, 0.08f), over, (v) =>
        {
            v *= dir;
            
            PlayerParts.Body.transform.localScale += v;
            PlayerParts.Body.transform.localPosition += v;
            
            PlayerParts.Suit.transform.localScale += v;
            PlayerParts.Suit.transform.localPosition += v;
            
            PlayerParts.Eyes.transform.localPosition += 2*v;
            PlayerParts.Nose.transform.localPosition += 2*v;
            
            PlayerParts.Crown.transform.localPosition -= 3*v;

            PlayerParts.Paws.transform.localPosition += v;
            
            v.x = v.y;
            v.y = 0;
            v.z = 0;
            PlayerParts.Tail.transform.localPosition += 2*v;
        });
        
        Utils.InvokeDelayed(() =>
        {
            ++_changeFaceIter;
            ++_blinkIter;
            
            // change face
            if (_changeFaceIter >= 4)
            {
                _changeFaceIter = 0;
                View.EyeType = EyeType.Blink0;
                Utils.InvokeDelayed(() =>
                {
                    UpdateFace();
                    Breath(-dir);
                }, 0.15f);
                return;
            }
            
            // blinking
            if (_blinkIter >= 6)
            {
                _blinkIter = 0;

                _lastEyeType = View.EyeType;
                View.EyeType = EyeType.Blink0;
                Utils.InvokeDelayed(() =>
                {
                    View.EyeType = _lastEyeType;
                    Breath(-dir);
                }, 0.15f);
                return;
            }
        
            Breath(-dir);
        }, over);
    }
}
