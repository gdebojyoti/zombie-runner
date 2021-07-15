using UnityEngine;

public class PickupController : MonoBehaviour {
  public Weapon weapon;
  public WeaponHandler weaponHandler;

  private void OnTriggerEnter2D(Collider2D other) {
    string tag = other.gameObject.tag;

    // TODO: use constants for values like "Player"
    if (tag == "Player") {
      // set player's active weapon
      weaponHandler.weapon = weapon;

      // destroy self
      Destroy(gameObject);
    }
  }
}