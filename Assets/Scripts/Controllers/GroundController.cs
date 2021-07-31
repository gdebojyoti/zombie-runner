using UnityEngine;

public class GroundController : MonoBehaviour {
  
  #region public members

    public float baseMovementSpeed = -2f;
    public GameObject groundPrefab;
    public GameObject obstaclePrefab;
    public GameObject pickupPrefab;
    public float width = 10f; // width of ground block (i.e., self)
    public Vector2[] pickupPositions = new Vector2[]{
      new Vector2(3,-1)
    };
    public Weapon[] weaponPickups;

  #endregion

  #region private members

    private bool m_NextBlockInitialized = false;
    private float m_cameraWidth; // width of camera, used to decide when to create & delete ground blocks
    private string m_obstacleName = "obstacle";
    private string m_pickupName = "pickup";
    private Vector2[] m_obstaclePositions = new Vector2[]{
      new Vector2(1,0),
      new Vector2(3,1),
      new Vector2(6,0),
      new Vector2(8,-1),
      new Vector2(9,1)
    };

  #endregion

  #region MonoBehaviour methods

    private void Start () {
      // calculate camera width (TODO: can be moved to GameService)
      Camera m_camera = Camera.main;
      m_cameraWidth = m_camera.aspect * 2f * m_camera.orthographicSize;

      _ClearOldObstacles();
      _InitObstacles();
      _InitPickups();
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
        if (child.name == m_obstacleName || child.name == m_pickupName) {
          Destroy(child.gameObject);
        }
      }
    }

    private void _InitObstacles () {
      // generate obstacles only if block is outside camera view
      if (transform.position.x < m_cameraWidth / 2) {
        return;
      }

      foreach(Vector2 position in m_obstaclePositions) {
        GameObject obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity, transform);
        obstacle.transform.localPosition = position;
        obstacle.name = m_obstacleName;
      }
    }

    private void _InitPickups () {
      // generate pickups only if block is outside camera view
      if (transform.position.x < m_cameraWidth / 2) {
        return;
      }

      foreach(Vector2 position in pickupPositions) {
        Weapon weapon = _AssignRandomWeapon();

        if (weapon == null) {
          Debug.Log("weapon not found");
          return;
        }

        GameObject pickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity, transform);
        pickup.transform.localPosition = position;
        pickup.name = m_pickupName;
        
        SpriteRenderer sr = pickup.GetComponent<SpriteRenderer>();
        sr.sprite = weapon.icon;

        PickupController pc = pickup.GetComponent<PickupController>();
        pc.weapon = weapon;
      }

      // assign a random weapon to the pickup
      Weapon _AssignRandomWeapon () {
        if (weaponPickups.Length == 0) {
          return null;
        }
        return weaponPickups[Random.Range(0, weaponPickups.Length)];
      }
    }

  #endregion
}
