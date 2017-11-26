using UnityEngine;

public class PetAnim
{
    private readonly Transform _playerView;

    public readonly PlayerParts PlayerParts;
    private readonly Transform[] _mainPartsT;
    
    private int _breathLoop;
    
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

        _mainPartsT = new Transform[]
        {
            PlayerParts.Body.transform,
            PlayerParts.Suit.transform,
            PlayerParts.Paws.transform,
            PlayerParts.Nose.transform,
            PlayerParts.Tail.transform,
            PlayerParts.Eyes.transform,
        };
    }

    public void StartBreath()
    {
        Breath(1);
    }
    
    public void UpdateFace()
    {
        PlayerView.EyeType = (EyeType)Random.Range((int)EyeType.Blink1, Eyes.Count());
        PlayerView.NoseType = (NoseType)Random.Range(0, Noses.Count());
        PlayerView.SuitType = (SuitType)Random.Range(0, Suits.Count());
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

        const float PetTSpeed = 18f;
        const float PillowSpeed = 7f;
        const float ShadowSpeed = -4f;
        
        var steps = new Vector2[]
        {
            new Vector2(0, squizeVal*1.5f),
            new Vector2(squizeVal, -squizeVal*2)
        };

        var pillowT = PlayerParts.Pillow.transform;
        var shadowT = PlayerParts.Shadow.transform;
        
        PlayerView.SetPaws(true);

        // Up
        
        Utils.Animate(Vector2.zero, steps[0], animationWindow, (v) =>
        {
            foreach (var t in _mainPartsT)
            {
              t.localScale += v;
              t.localPosition += PetTSpeed * v;
            }
        });
        Utils.Animate(Vector2.zero, new Vector2(0, squizeVal), pillowAnimationWindow, (v) =>
        {
            pillowT.localPosition += PillowSpeed * v;

            v.x = v.y;
            v.z = v.y;
            v.y = 0;
            pillowT.localScale += ShadowSpeed * v;
            shadowT.localScale += ShadowSpeed*v;
        });
        
        // Down
        Utils.InvokeDelayed(() =>
        {
            Utils.Animate(new Vector2(0, squizeVal), Vector2.zero, pillowAnimationWindow, (v) =>
            {
                pillowT.localPosition += PillowSpeed * v;
                
                v.x = v.y;
                v.z = v.y;
                v.y = 0;
                pillowT.localScale += ShadowSpeed*v;
                shadowT.localScale += ShadowSpeed*v;
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
                    t.localPosition += PetTSpeed * v;
                }
            });
            
            Utils.InvokeDelayed(() =>
            {
                // Restore
                PlayerView.SetPaws(false);
                
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

            PlayerParts.Paws.transform.localPosition += v;
            
            v.x = v.y;
            v.y = 0;
            v.z = 0;
            PlayerParts.Tail.transform.localPosition += 2*v;
        });
        
        Utils.InvokeDelayed(() =>
        {
            // blinking
            var rnd = Random.Range(4, 5);
            if ((++_breathLoop % rnd) == (rnd-1))
            {
                _breathLoop = 0;
                
                PlayerView.EyeType = EyeType.Blink0;
                Utils.InvokeDelayed(() =>
                {
                    UpdateFace();
                    Breath(-dir);
                }, 0.15f);
                return;
            }
        
            Breath(-dir);
        }, over);
    }
}
