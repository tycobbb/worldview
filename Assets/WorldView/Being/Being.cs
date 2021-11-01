using DG.Tweening;
using UnityEngine;

/// a being in the world
public class Being: MonoBehaviour {
    // -- config --
    [Header("config")]
    [Tooltip("the musical key")]
    [SerializeField] Root m_KeyOf = Root.C;

    // -- config --
    [Header("tuning")]
    [Tooltip("the base color")]
    [SerializeField] Color m_Color = new Colors.Hsv(0.0f, 0.45f, 1.0f).ToRgb();

    [Tooltip("the initial radius from center")]
    [SerializeField] AnimationCurve m_Radius;

    [Tooltip("the period of the movement")]
    [SerializeField] AnimationCurve m_Period;

    [Tooltip("the range of the movement")]
    [SerializeField] AnimationCurve m_Range;

    [Tooltip("the tone this plays")]
    [SerializeField] AnimationCurve m_Tone;

    [Tooltip("the audio range")]
    [SerializeField] AnimationCurve m_SingRange;

    // -- nodes --
    [Header("nodes")]
    [Tooltip("this being's music")]
    [SerializeField] Musicker m_Music;

    [Tooltip("this being's renderer")]
    [SerializeField] MeshRenderer m_Renderer;

    // -- props --
    /// the start position
    Vector3 m_Pos;

    /// the time offset
    float m_Offset;

    /// the sampled range
    float m_RangeVal;

    /// if the being is audible
    bool m_IsAudible;

    /// the musical key
    Key m_Key;

    /// the loop when audible
    Loop m_Loop;

    // -- lifecycle --
    void Awake() {
        // set musical props
        m_Key = new Key(m_KeyOf);
    }

    void Start() {
        Sample();
    }

    void Update() {
        m_IsAudible = false;
    }

    void FixedUpdate() {
        Move();
        Sing();
    }

    // -- commands --
    /// sample random elements
    void Sample() {
        var t = transform;
        var p = t.position;

        // sample start pos
        var r = m_Radius.Sample();
        var a = 2.0f * Mathf.PI * Random.value;
        p.y = r * Mathf.Cos(a);
        p.z = r * Mathf.Sin(a);

        m_Pos = t.position = p;

        // sample color
        var color = m_Color.ToHsv().Hue(Random.value).ToRgb();
        m_Renderer.material.color = color;

        // sample range
        m_Music.SetMaxDistance(m_SingRange.Sample());

        // sample loop
        m_Loop = new Loop(
            fade: 1.5f,
            blend: 0.6f,
            new Tone(m_Tone.SampleInt()).Octave(Random.Range(0, 2))
        );

        // start movement
        Play(m_Range.Sample(), m_Period.Sample());
    }

    /// start the being's movement
    void Play(float range, float period) {
        // store range
        m_RangeVal = range;

        // tween offset
        var offset = new Lens<float>(
            ( ) => m_Offset,
            (v) => m_Offset = v
        );

        var _ = offset
            .TweenTo(-1.0f, 1.0f, period)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutCubic);
    }

    /// move the being
    void Move() {
        var t = transform;
        transform.position = m_Pos + m_Offset * m_RangeVal * Vector3.right;
    }

    /// play the audio loop
    void Sing() {
        m_Music.ToggleLoop(m_Loop, m_IsAudible, m_Key);
    }

    /// if this being is audible
    public void SetAudible(bool isAudible) {
        m_IsAudible = isAudible;
    }
}
