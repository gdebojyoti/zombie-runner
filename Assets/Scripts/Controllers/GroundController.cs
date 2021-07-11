using UnityEngine;

public class GroundController : MonoBehaviour {
  
  #region public members

    public float baseMovementSpeed = -2f;
    public GameObject zombiePrefab;
    public float width = 10f; // width of ground block (i.e., self)

  #endregion

  #region private members

    private bool m_NextBlockInitialized = false;
    private float cameraWidth; // width of camera, used to decide when to create & delete ground blocks

  #endregion

  #region MonoBehaviour methods

    private void Start () {
      // calculate camera width (TODO: can be moved to GameService)
      Camera m_camera = Camera.main;
      cameraWidth = m_camera.aspect * 2f * m_camera.orthographicSize;
    }

    private void FixedUpdate () {
      _Move();
      _CheckForEnd();
      _CheckIfOutsideViewport();
    }

  #endregion

  #region private methods

    private void _Move() {
      // speed to be applied in current frame
      float speedPerFrame = baseMovementSpeed * GameService.GetWorldMovementMultiplier() * Time.fixedDeltaTime;

      // move self
      transform.Translate(speedPerFrame, 0, 0);
    }

    // check whether new ground block needs to be instantiated
    private void _CheckForEnd () {
      if (m_NextBlockInitialized) {
        return;
      }
      if ((transform.position.x + width) < cameraWidth) {
        _CreateNextGround();
      }
    }

    private void _CheckIfOutsideViewport () {
      if ((transform.position.x + width) < -cameraWidth) {
        Destroy(gameObject);
      }
    }

    // instantiate new ground block
    private void _CreateNextGround () {
      // instantiate new ground from self
      GameObject newGround = Instantiate(
        gameObject,
        new Vector2(transform.position.x + width - .06f, transform.position.y),
        Quaternion.identity
      );

      // set name & parent
      newGround.name = "Ground";
      newGround.transform.SetParent(transform.parent);

      // update flag
      m_NextBlockInitialized = true;
    }

    private void _InitObstacles () {
      GameObject child1 = Instantiate(zombiePrefab, transform.position, Quaternion.identity);
      child1.transform.SetParent(transform);
      child1.transform.localPosition = new Vector2(1, 0);
      
      GameObject child2 = Instantiate(zombiePrefab, transform.position, Quaternion.identity);
      child2.transform.SetParent(transform);
      child2.transform.localPosition = new Vector2(3, 0);
    }

  #endregion
}