using UnityEngine;

public class FillEffect : MonoBehaviour
{
    public RectTransform Bar;
    public ParticleSystem Partcles;

    private float _prevHeight;
    private void Update()
    {
        var curHeight = Bar.position.y + Bar.rect.height;
        if (_prevHeight != curHeight)
        {
            Partcles.Play();
            _prevHeight = curHeight;
            transform.position = new Vector3(transform.position.x, curHeight, 0);
        }
        else
        {
            Partcles.Stop();
        }
    }
}