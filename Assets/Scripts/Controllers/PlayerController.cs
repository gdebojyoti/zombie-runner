using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  #region public members

    public int positionMultiplier = 1;
  
  #endregion

  #region private members
  
    private int m_laneId = 0; // one of: -1, 0, 1; -1 = top lane, 1 = bottom lane
    private Rigidbody2D m_rb;

  #endregion

  void Start() {
    this.m_rb = GetComponent<Rigidbody2D>();
  }

  void Update() {
    _CheckForInputs();
  }

  #region private methods

    private void _CheckForInputs () {
      if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
        _SwitchLanes(1);
      }
      if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
        _SwitchLanes(-1);
      }
    }

    // move up or down lanes
    private void _SwitchLanes (int delta) {
      if (delta == -1) {
        m_laneId = Mathf.Max(m_laneId - 1, -1);
      } else if (delta == 1) {
        m_laneId = Mathf.Min(m_laneId + 1, 1);
      }

      transform.position = new Vector2(transform.position.x, m_laneId * positionMultiplier);
    }

  #endregion
}
