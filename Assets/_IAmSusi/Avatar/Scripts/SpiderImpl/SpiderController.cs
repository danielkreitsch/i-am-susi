/* 
 * This file is part of Unity-Procedural-IK-Wall-Walking-Spider on github.com/PhilS94
 * Copyright (C) 2020 Philipp Schofield - All Rights Reserved
 */

using Game.Input;
using UnityEngine;

namespace Game.Avatar.SpiderImpl {
    /*
     * This class needs a reference to the Spider class and calls the walk and turn functions depending on player input.
     * So in essence, this class translates player input to spider movement. The input direction is relative to a camera and so a 
     * reference to one is needed.
     */

    [DefaultExecutionOrder(-1)] // Make sure the players input movement is applied before the spider itself will do a ground check and possibly add gravity
    [RequireComponent(typeof(Spider))]
    sealed class SpiderController : MonoBehaviour {

        Spider spider;

        [SerializeField]
        AvatarCamera attachedCamera;

        Controls controls;

        void Awake() {
            OnValidate();
        }

        [ContextMenu(nameof(OnValidate))]
        void OnValidate() {
            if (!spider) {
                TryGetComponent(out spider);
            }
            if (!attachedCamera) {
                attachedCamera = FindObjectOfType<AvatarCamera>();
            }
        }

        void OnEnable() {
            controls = new();
            controls.Enable();
        }
        void OnDisable() {
            controls.Disable();
            controls.Dispose();
        }

        bool isJumping => controls.Avatar.Jump.IsPressed();
        bool isRunning => controls.Avatar.Dash.IsPressed();
        Vector2 movement => controls.Avatar.Move.ReadValue<Vector2>();

        void FixedUpdate() {
            //** Movement **//
            var input = getInput();

            if (isRunning) {
                spider.run(input);
            } else {
                spider.walk(input);
            }

            //var tempCamTargetRotation = smoothCam.camTarget.rotation;
            //var tempCamTargetPosition = smoothCam.camTarget.position;
            spider.turn(input);
            //smoothCam.setTargetRotation(tempCamTargetRotation);
            //smoothCam.setTargetPosition(tempCamTargetPosition);
        }

        void Update() {
            //Hold down Space to deactivate ground checking. The spider will fall while space is hold.
            spider.setGroundcheck(!isJumping);
        }

        Vector3 getInput() {
            var up = spider.transform.up;
            var right = spider.transform.right;
            var input = Vector3.ProjectOnPlane(attachedCamera.forward, up).normalized * movement.y + (Vector3.ProjectOnPlane(attachedCamera.right, up).normalized * movement.x);
            var fromTo = Quaternion.AngleAxis(Vector3.SignedAngle(up, spider.getGroundNormal(), right), right);
            input = fromTo * input;
            float magnitude = input.magnitude;
            return (magnitude <= 1) ? input : input /= magnitude;
        }
    }
}