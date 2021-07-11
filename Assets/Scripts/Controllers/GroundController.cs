using UnityEngine;

public class GroundController : MonoBehaviour {
  
  #region public members

    public float baseMovementSpeed = -2f;
    public float limit = -14f; // position.x after crossing which block is to be placed at the right-most end
    // public float buffer = 25f; // distance by which block is to be moved (reset) after it crosses `limit`
    public GameObject zombiePrefab;
    // public GameObject groundPrefab;
    public float width = 10f; // width of ground block
    public float buffer = 10f;

  #endregion

  #region private members

    private bool m_ObstaclesInitialized = false;
    private bool m_NextBlockInitialized = false;
    private float cameraWidth; // right most end of user's view

  #endregion


  private void Start () {
    Camera m_camera = Camera.main;

    cameraWidth = m_camera.aspect * 2f * m_camera.orthographicSize;
    Debug.Log("cameraWidth: " + cameraWidth);
  }

  private void FixedUpdate () {
    _Move();
    _CheckForEnd();
    _CheckIfOutsideViewport();
  }

  #region private methods

    private void _Move() {
      // speed to be applied in current frame
      float speedPerFrame = baseMovementSpeed * GameService.GetWorldMovementMultiplier() * Time.fixedDeltaTime;

      // move self
      transform.Translate(speedPerFrame, 0, 0);

      // // place self at the end (on right side) if self has moved too far to the left
      // if (transform.position.x < limit) {
      //   _Initialize();
      // }
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

    private void _Initialize () {
      // TODO: regenerate obstacles
      // generate obstacles if not already done
      if (!m_ObstaclesInitialized) {
        // _InitObstacles();
        m_ObstaclesInitialized = true;
      }

      // reposition self
      transform.position = new Vector2(transform.position.x + buffer, 0);
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