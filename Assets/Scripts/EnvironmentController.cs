using UnityEngine;

public class EnvironmentController : MonoBehaviour {
  
  public float baseMovementSpeed = -1f;


  private void FixedUpdate() {
    _Move();
  }

  #region private methods

    private void _Move() {
      // speed to be applied in current frame
      float speedPerFrame = baseMovementSpeed * DetailsService.GetWorldMovementMultiplier() * Time.deltaTime;

      // // move rigidbody
      // m_rb.velocity = new Vector2(speedPerFrame, 0);

      // move self
      transform.Translate(speedPerFrame, 0, 0);
    }

    #endregion
}