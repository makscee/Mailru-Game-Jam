using UnityEngine;

public class Effects
{
    private static readonly Prefab Prefab = new Prefab("ExplosionEffect");
    public static void ExplosionEffect(Vector2 position, Color color)
    {
        var he = Prefab.Instantiate();
        he.transform.position = position;
        var ps = he.GetComponent<ParticleSystem>();

        var main = ps.main;
        main.startColor = color;
        Object.Destroy(he, 5f);
    }
}