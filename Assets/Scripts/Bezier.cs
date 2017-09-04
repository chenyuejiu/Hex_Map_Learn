using UnityEngine;
using System.Collections;

public static class Bezier {

    public static Vector3 Getpoint(Vector3 a, Vector3 b, Vector3 c, float t ) {
        float r = 1f - t;
        return r * r * a + 2f * r * t * b + t * t * c;
    }
}
