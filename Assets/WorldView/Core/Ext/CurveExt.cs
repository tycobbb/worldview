using UnityEngine;

/// unity curve extensions
public static class CurveExt {
    // -- queries --
    /// sample a random value from the curve
    public static float Sample(this AnimationCurve c) {
        return c.Evaluate(Random.value);
    }
}