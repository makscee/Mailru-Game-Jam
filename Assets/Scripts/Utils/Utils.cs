using System;
using System.Collections;
using UnityEngine;

public static class Utils
{
    public static int IntX(this Vector3 v)
    {
        return (int)v.x;
    }

    public static float ToFloat(this string s)
    {
        if (string.IsNullOrEmpty(s)) return 0f;
        float f;
        var r = float.TryParse(s, out f);
        if (!r) f = 0f;
        return f;
    }

    public static float Interpolate(float from, float to, float over, float t)
    {
        var delta = Time.deltaTime;
        if (t + delta > over)
        {
            delta = Math.Max(0, over - t);
        }
        return (to - from) / over * delta;
    }

    public static void Animate(Vector3 from, Vector3 to, float over, Action<Vector3> onChange, MonoBehaviour obj = null, bool fullValue = false, float delay = 0f)
    {
        obj = obj == null ? CameraScript.Instance : obj;
        obj.StartCoroutine(Animation(from, to, over, onChange, fullValue, delay));
    }

    public static void Animate(Color from, Color to, float over, Action<Color> onChange, MonoBehaviour obj = null, bool fullValue = false, float delay = 0f)
    {
        obj = obj == null ? CameraScript.Instance : obj;
        var fromVec = new Vector3(from.r, from.g, from.b);
        var toVec = new Vector3(to.r, to.g, to.b);
        obj.StartCoroutine(Animation(fromVec, toVec, over, v => onChange(new Color(v.x, v.y, v.z)), fullValue, delay));
    }

    public static void Animate(float from, float to, float over, Action<float> onChange, MonoBehaviour obj = null, bool fullValue = false, float delay = 0f)
    {
        obj = obj == null ? CameraScript.Instance : obj;
        obj.StartCoroutine(Animation(new Vector3(from, 0), new Vector3(to, 0), over, v => onChange(v.x), fullValue, delay));
    }

    private static IEnumerator Animation(Vector3 from, Vector3 to, float over, Action<Vector3> action, bool fullValue, float delay)
    {
        yield return new WaitForSeconds(delay);
        var t = 0f;
        var result = from;
        while (t < over)
        {
            var x = Interpolate(from.x, to.x, over, t);
            var y = Interpolate(from.y, to.y, over, t);
            var z = Interpolate(from.z, to.z, over, t);
            var temp = new Vector3(x, y, z);
            result += temp;
            action(fullValue ? result : temp);
            t += Time.deltaTime;
            yield return null;
        }
        action(fullValue ? to : to - result);
    }

    public static void InvokeDelayed(Action a, float delay, MonoBehaviour obj = null, bool repeat = false)
    {
        obj = obj == null ? CameraScript.Instance : obj;
        obj.StartCoroutine(Delay(a, delay, repeat));
    }

    private static IEnumerator Delay(Action a, float delay, bool repeat)
    {
        yield return new WaitForSeconds(delay);
        a();
        while (repeat)
        {
            yield return new WaitForSeconds(delay);
            a();
        }
    }
}