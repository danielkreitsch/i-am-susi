/* 
 * This file is part of Unity-Procedural-IK-Wall-Walking-Spider on github.com/PhilS94
 * Copyright (C) 2020 Philipp Schofield - All Rights Reserved
 */

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Game.Avatar.SpiderImpl {
    /* Simple class for some general game settings. */
    sealed class GeneralSettings : MonoBehaviour {
        void Awake() {
            // Lock Cursor in Build
#if !UNITY_EDITOR
            Cursor.lockState = CursorLockMode.Locked;
#endif
        }

        void Update() {
            //On Press reset scene
            if (Keyboard.current.escapeKey.wasPressedThisFrame) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}