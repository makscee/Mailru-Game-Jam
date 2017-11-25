using UnityEngine;

public class Effects
{
    private static readonly Prefab Prefab = new Prefab("ExplosionEffect");
    public static void ExplosionEffect(Vector2 position, Color color, int sortOrder = 5)
    {
        var he = Prefab.Instantiate();
        he.transform.position = position;
        var ps = he.GetComponent<ParticleSystem>();
        ps.GetComponent<Renderer>().sortingOrder = sortOrder;

        var main = ps.main;
        main.startColor = color;
        Object.Destroy(he, 5f);
    }
}