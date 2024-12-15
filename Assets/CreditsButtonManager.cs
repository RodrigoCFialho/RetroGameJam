using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;

    public void Back() {
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
