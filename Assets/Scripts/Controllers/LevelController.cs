using UnityEngine;

public class LevelController : MonoBehaviour {

  #region public members

    public Weapon[] weapons;
    public FloatConst grenadeAmmo;
    public WeaponHandler weaponHandler;

  #endregion

  #region MonoBehaviour methods

    private void Update() {
      _CheckForInputs();
    }

  #endregion

  #region private methods

    private void _CheckForInputs () {
      // TODO: Temp stuff; to be removed later
      // press 1 ~ 4 to switch between sword, pistol, laser & grenade launcher
      if (Input.GetKeyDown(KeyCode.Alpha1)) {
        _SetWeapon(1);
      } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
        _SetWeapon(2);
      } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
        _SetWeapon(3);
      } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
        _SetWeapon(4);
      } else if (Input.GetKeyDown(KeyCode.Alpha0)) {
        _SetWeapon(0); // drop weapon
      }
    }

    private void _SetWeapon (int type) {
      if (type > 0 && type <= weapons.Length) {
        weaponHandler.weapon = weapons[type - 1];
      } else {
        Debug.Log("Invalid weapon selected");
        weaponHandler.weapon = null;
      }
    }

  #endregion
}