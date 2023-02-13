using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceController : MonoBehaviour {

    [SerializeField]
    public List<UserInterfaceMenu> Menus;

    // Update is called once per frame
    void Update()
    {
        this.CheckForKeyPressed();
    }

    private void CheckForKeyPressed() {
        foreach (var userInterface in Menus) {
            if(Input.GetKeyDown(userInterface.Key)) {
                if(userInterface.Menu.activeSelf) {
                    userInterface.Menu.SetActive(false);
                }
                else {
                    userInterface.Menu.SetActive(true);
                }
            }
        }
    }
}
