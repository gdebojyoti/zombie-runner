using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  #region public members

    public int positionMultiplier = 1;
    public GameObject playerGo;
    public float movementSpeed = 20f;
  
  #endregion

  #region private members
  
    private int m_laneId = 0; // one of: -1, 0, 1; -1 = top lane, 1 = bottom lane
    private Rigidbody2D m_rb;
    private float m_movementSpeedMultiplier = 1f;

  #endregion

  void Start() {
    m_rb = GetComponent<Rigidbody2D>();
  }

  void FixedUpdate() {
    _Move();
  }

  void Update() {
    _CheckForInputs();
  }

  #region private methods

    private void _Move() {
      // speed to be applied in current frame
      float speedPerFrame = movementSpeed * m_movementSpeedMultiplier * Time.deltaTime;

      // // move rigidbody
      // m_rb.velocity = new Vector2(speedPerFrame, 0);

      // move self
      transform.Translate(speedPerFrame, 0, 0);
    }

    private void _CheckForInputs () {
      if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
        _SwitchLanes(1);
      }
      if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
        _SwitchLanes(-1);
      }
      // if (Input.GetKeyUp(KeyCode.Space)) {}
    }

    // move up or down lanes
    private void _SwitchLanes (int delta) {
      if (delta == -1) {
        m_laneId = Mathf.Max(m_laneId - 1, -1);
      } else if (delta == 1) {
        m_laneId = Mathf.Min(m_laneId + 1, 1);
      }

      playerGo.transform.position = new Vector2(playerGo.transform.position.x, m_laneId * positionMultiplier);
    }

  #endregion
}
