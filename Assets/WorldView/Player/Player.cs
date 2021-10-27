using UnityEngine;
using UnityEngine.InputSystem;

/// the player
public class Player: MonoBehaviour {
    // -- tuning --
    [Header("tuning")]
    [Tooltip("the look speed, a torque")]
    [SerializeField] float m_LookSpeed = 0.5f;

    // -- nodes --
    [Header("nodes")]
    [Tooltip("the camera armature")]
    [SerializeField] Rigidbody m_Armature;

    [Tooltip("the input system input")]
    [SerializeField] PlayerInput m_Input;

    // -- props --
    /// the current look magnitude
    float m_Look;

    /// the player's inputs
    PlayerActions m_Actions;

    // -- lifecycle --
    void Awake() {
        // set props
        m_Actions = new PlayerActions(m_Input);
    }

    void Update() {
        // read input
        ReadMove();
        ReadLook();
    }

    void FixedUpdate() {
        // move player
        Move();
        Look();
    }

    // -- commands --
    /// read move input
    void ReadMove() {
    }

    /// read move input
    void ReadLook() {
        var look = m_Actions.Look;
        m_Look = Vector2.Dot(look, Vector2.up);
    }

    /// move player
    void Move() {
    }

    /// move camera
    void Look() {
        var torque = m_Look * m_LookSpeed * Time.deltaTime * Vector3.left;
        m_Armature.AddRelativeTorque(torque);
    }
}