using UnityEngine;
using UnityEngine.InputSystem;

/// the player
public class Player: MonoBehaviour {
    // -- tuning --
    [Header("tuning")] [Tooltip("the move speed in percent per second")] [SerializeField]
    float m_MoveSpeed = 0.5f;

    // -- nodes --
    [Header("nodes")] [Tooltip("the input system input")] [SerializeField]
    PlayerInput m_Input;

    // -- props --
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
    }

    void FixedUpdate() {
        // move player
        Move();
    }

    // -- commands --
    /// read move input
    void ReadMove() {
    }

    // move player
    void Move() {
    }
}