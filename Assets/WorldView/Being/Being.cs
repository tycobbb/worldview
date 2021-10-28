using DG.Tweening;
using UnityEngine;

/// a being in the world
public class Being: MonoBehaviour {
    // -- config --
    [Header("config")]
    [Tooltip("the musical key")]
    [SerializeField] Root m_KeyOf = Root.C;

    [Tooltip("the musical interval")]
    [SerializeField] Interval m_Interval = Interval.I;

    [Tooltip("the beings color")]
    [SerializeField] Color m_Color = Color.red;

    // -- nodes --
    [Header("nodes")]
    [Tooltip("this being's music")]
    [SerializeField] Musicker m_Music;

    [Tooltip("this being's renderer")]
    [SerializeField] MeshRenderer m_Renderer;

    // -- props --
    /// the start position
    Vector3 m_Start;

    /// the position offset
    float m_Offset;

    /// if the being is audible
    bool m_IsAudible;

    /// the musical key
    Key m_Key;

    /// the loop when audible
    Loop m_Loop;

    // -- lifecycle --
    void Awake() {
        // set musical props
        m_Key = new Key(
            m_KeyOf
        );

        m_Loop = new Loop(
            fade: 1.5f,
            blend: 0.6f,
            new Tone(m_Interval)
        );
    }

    void Start() {
        // set props
        m_Start = transform.position;

        // set color
        m_Renderer.material.color = m_Color;

        // start movement
        var offset = new Lens<float>(
            ( ) => m_Offset,
            (v) => m_Offset = v
        );

        var _ = Tween
            .Start(offset, 0.0f, 1.0f, 1.0f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutCubic);
    }

    void Update() {
        m_IsAudible = false;
    }

    void FixedUpdate() {
        Move();
        Play();
    }

    // -- commands --
    /// move the being
    void Move() {
        var t = transform;
        t.position = m_Start + m_Offset * Vector3.right;
    }

    /// play the audio loop
    void Play() {
        m_Music.ToggleLoop(m_Loop, m_IsAudible, m_Key);
    }

    /// if this being is audible
    public void SetAudible(bool isAudible) {
        m_IsAudible = isAudible;
    }
}
