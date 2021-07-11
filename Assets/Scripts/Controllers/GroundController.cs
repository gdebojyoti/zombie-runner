using UnityEngine;

public class GroundController : MonoBehaviour {
  
  #region public members

    public float baseMovementSpeed = -2f;
    public GameObject groundPrefab;
    public GameObject zombiePrefab;
    public float width = 10f; // width of ground block (i.e., self)

  #endregion

  #region private members

    private bool m_NextBlockInitialized = false;
    private float m_cameraWidth; // width of camera, used to decide when to create & delete ground blocks
    private string m_enemyName = "enemy";

  #endregion

  #region MonoBehaviour methods

    private void Start () {
      // calculate camera width (TODO: can be moved to GameService)
      Camera m_camera = Camera.main;
      m_cameraWidth = m_camera.aspect * 2f * m_camera.orthographicSize;

      _ClearOldObstacles();
      _InitObstacles();
    }

    private void FixedUpdate () {
      _Move();
      _IsGroundNeeded();
      _DidLeaveViewport();
    }

  #endregion

  #region private methods

    private void _Move() {
      // speed to be applied in current frame
      float speedPerFrame = baseMovementSpeed * GameService.GetWorldMovementMultiplier() * Time.fixedDeltaTime;

      // move self
      transform.Translate(speedPerFrame, 0, 0);
    }

    // check whether new ground block needs to be created
    private void _IsGroundNeeded () {
      if (m_NextBlockInitialized) {
        return;
      }
      if ((transform.position.x + width) < m_cameraWidth) {
        _CreateNextGround();
      }
    }

    // check whether current ground block has left camera viewport
    private void _DidLeaveViewport () {
      if ((transform.position.x + width) < -m_cameraWidth) {
        Destroy(gameObject);
      }
    }

    // instantiate new ground block
    private void _CreateNextGround () {
      // instantiate new ground from self
      GameObject newGround = Instantiate(
        groundPrefab,
        new Vector2(transform.position.x + width - .06f, transform.position.y),
        Quaternion.identity,
        transform.parent
      );

      newGround.name = "Ground";

      // update flag
      m_NextBlockInitialized = true;
    }

    private void _ClearOldObstacles () {
      foreach (Transform child in transform) {
        if (child.name == m_enemyName) {
          Destroy(child.gameObject);
        }
      }
    }

    private void _InitObstacles () {
      // generate obstacles only if block is outside camera view
      if (transform.position.x < m_cameraWidth / 2) {
        return;
      }

      GameObject child1 = Instantiate(zombiePrefab, transform.position, Quaternion.identity, transform);
      child1.transform.localPosition = new Vector2(1, 0);
      child1.name = m_enemyName;
      
      GameObject child2 = Instantiate(zombiePrefab, transform.position, Quaternion.identity, transform);
      child2.transform.localPosition = new Vector2(3, 0);
      child2.name = m_enemyName;
    }

  #endregion
}
