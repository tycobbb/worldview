using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public static class Tween {
    /// tween the lens
    public static TweenerCore<float, float, FloatOptions> Start(
        Lens<float> prop,
        float src,
        float dst,
        float duration
    ) {
        // set the initial value
        prop.Val = src;

        // create the tween
        var tween = DOTween.To(
            ( ) => prop.Val,
            (v) => prop.Val = v,
            dst,
            duration
        );

        return tween;
    }
}