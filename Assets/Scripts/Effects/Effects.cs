using UnityEngine;

public class Effects
{
    private static readonly Prefab Explosion = new Prefab("ExplosionEffect");
    private static readonly Prefab Hearts = new Prefab("HeartsEffect");
    
    public static void ExplosionEffect(Vector2 position, int sortOrder = 5)
    {
        var he = Explosion.Instantiate();
        he.transform.position = position;
        var ps = he.GetComponent<ParticleSystem>();
        ps.GetComponent<Renderer>().sortingOrder = sortOrder;

        Object.Destroy(he, 5f);
    }
    public static void HeartsEffect(Vector2 position, int sortOrder = 5)
    {
        var he = Hearts.Instantiate();
        he.transform.position = position;
        var ps = he.GetComponent<ParticleSystem>();
        ps.GetComponent<Renderer>().sortingOrder = sortOrder;

        Object.Destroy(he, 3f);
    }
}