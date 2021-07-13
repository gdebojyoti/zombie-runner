using UnityEngine;

public class LevelController : MonoBehaviour {
  public Weapon[] weapons;

  private void Start() {
    foreach (Weapon weapon in weapons) {
      Debug.Log(weapon.weaponName + " is a " + weapon.type + " weapon");
    }
  }
}