using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Viewport: MonoBehaviour {
    // -- tuning --
    [Header("tuning")]
    [Tooltip("the collapse curve")]
    [SerializeField] AnimationCurve m_Collapse;

    // -- nodes --
    [Header("nodes")]
    [Tooltip("the canvas")]
    [SerializeField] CanvasScaler m_Canvas;

    [Tooltip("the viewport image")]
    [SerializeField] RectTransform m_Viewport;

    // -- props --
    /// the start width
    float m_StartWidth;

    /// percent complete in the collapse anim
    float m_Percent;

    // -- lifecycle --
    void Awake() {
        // set props
        m_StartWidth = m_Canvas.referenceResolution.x;
    }

    void Start() {
        Play();
    }

    void Update() {
        Collapse();
    }

    // -- commands --
    /// start the collapse animation
    void Play() {
        var duration = m_Collapse.keys[m_Collapse.length - 1].time;

        // start animation
        var pct = new Lens<float>(
            ( ) => m_Percent,
            (v) => m_Percent = v
        );

        var _ = pct
            .TweenTo(0.0f, 1.0f, duration)
            .SetEase(m_Collapse);
    }

    /// collapse the viewport
    void Collapse() {
        var t = m_Viewport;
        var pct = m_Percent;

        // get the new width
        var w0 = m_StartWidth;
        var w1 = Mathf.Lerp(w0, 1.0f, pct);

        // get the insets
        var dl = (w0 - w1) / 2;
        var dr = dl;

        // make sure the end is pixel-perfect
        if (pct == 1.0f) {
            dl = Mathf.Floor(dl);
            dr = w0 - dl - 1.0f;
        }

        // update state
        var min = t.offsetMin;
        min.x = dl;
        t.offsetMin = min;

        var max = t.offsetMax;
        max.x = -dr;
        t.offsetMax = max;
    }
}
