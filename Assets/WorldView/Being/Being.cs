using UnityEngine;

public class Being: MonoBehaviour {
    // -- config --
    [Header("config")]
    [Tooltip("the musical key")]
    [SerializeField] Root m_KeyOf = Root.C;

    [Tooltip("the musical interval")]
    [SerializeField] Interval m_Interval = Interval.I;

    // -- nodes --
    [Header("nodes")]
    [Tooltip("this being's music")]
    [SerializeField] Musicker m_Music;

    // -- props --
    /// if this being is audible
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

    void Update() {
        m_IsAudible = false;
    }

    void FixedUpdate() {
        Play();
    }

    // -- commands --
    /// play the audio loop
    void Play() {
        m_Music.ToggleLoop(m_Loop, m_IsAudible, m_Key);
    }

    /// if this being is audible
    public void SetAudible(bool isAudible) {
        m_IsAudible = isAudible;
    }
}
