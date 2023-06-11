using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        var playerInput = gameObject.AddComponent<PlayerInput>();

        InputActionAsset inputActionAsset = ScriptableObject.CreateInstance("InputActionAsset") as InputActionAsset;
        playerInput.actions = inputActionAsset;

        var inputActionMap = new InputActionMap("ChangerMap");
        inputActionAsset.AddActionMap(inputActionMap);

        var inputAction = inputActionMap.AddAction("ChangerAction", type: InputActionType.Button, binding: "<Mouse>/leftButton");
        inputAction.Enable();

        playerInput.currentActionMap = inputActionMap;

        inputAction.started += PlayGame;

        inputActionAsset.Enable();
    }
    public void PlayGame(InputAction.CallbackContext context)
    {
        Time.timeScale = 1f;
        Debug.Log("Playing!");
        SceneManager.LoadScene("Level_V0");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
