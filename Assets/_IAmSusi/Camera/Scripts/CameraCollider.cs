using Cinemachine;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// </summary>
    [AddComponentMenu("")] // Hide in menu
    [SaveDuringPlay]
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public class CameraCollider : CinemachineExtension
    {
        /// <summary>Objects on these layers will be detected.</summary>
        [Header("Obstacle Detection")]
        [SerializeField]
        [Tooltip("Objects on these layers will be detected")]
        private LayerMask collideAgainst = 1;
    
        [SerializeField]
        [Tooltip("How fast to return to distance after collision, higher is faster")]
        [Range(1, 100)]
        private float returnAfterCollisionRate = 5f;

        /// <summary>Objects on these layers will never obstruct view of the target.</summary>
        [SerializeField]
        [Tooltip("Objects on these layers will never obstruct view of the target")]
        private LayerMask transparentLayers = 0;

        public bool FoundCollision(ICinemachineCamera virtualCamera)
        {
            var extra = this.GetExtraState<VirtualCameraExtraState>(virtualCamera);
            return Time.time - extra.lastCollideTime < 0.1f;
        }

        void OnValidate()
        {
        }

        /// <summary>
        /// Per-vcam extra state info
        /// </summary>
        class VirtualCameraExtraState
        {
            public float lastCollideTime;
            public float currentReturnRate;
            public float previousAdjustment = 5f;
        }

        private RaycastHit[] raycastBuffer = new RaycastHit[4];

        /// <summary>Callback to preform the zoom adjustment</summary>
        /// <param name="virtualCamera">The virtual camera being processed</param>
        /// <param name="stage">The current pipeline stage</param>
        /// <param name="state">The current virtual camera state</param>
        /// <param name="deltaTime">The current applicable deltaTime</param>
        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase virtualCamera,
            CinemachineCore.Stage stage,
            ref CameraState state,
            float deltaTime)
        {
            var extra = this.GetExtraState<VirtualCameraExtraState>(virtualCamera);

            // Set the zoom after the body has been positioned, but before the aim,
            // so that composer can compose using the updated field of view.
            if (stage == CinemachineCore.Stage.Body)
            {
                Vector3 cameraPos = state.CorrectedPosition;
                Vector3 lookAtPos = state.ReferenceLookAt;

                int layerMask = this.collideAgainst & ~this.transparentLayers;

                Vector3 displacement = Vector3.zero;
                Vector3 targetToCamera = cameraPos - lookAtPos;
                Ray targetToCameraRay = new Ray(lookAtPos, targetToCamera);
                Vector3 targetToCameraNormalized = targetToCamera.normalized;

                float distanceToCamera = targetToCamera.magnitude;
                if (distanceToCamera > Epsilon)
                {
                    float desiredDistToCamera = distanceToCamera;

                    int numFound = Physics.SphereCastNonAlloc(
                        lookAtPos, 0.1f, targetToCameraNormalized, this.raycastBuffer, distanceToCamera + 1f, // Trace back a bit, in case we 'will' collide next frame
                        layerMask, QueryTriggerInteraction.Ignore);
                    if (numFound > 0)
                    {
                        float bestDistance = distanceToCamera;
                        for (int i = 0; i < numFound; ++i)
                        {
                            var castHitInfo = this.raycastBuffer[i];
                            Vector3 castPoint = castHitInfo.point; //Vector3.Project(castHitInfo.point, targetToCameraNormalized);
                            float dist = Vector3.Distance(lookAtPos, castPoint);
                            if (dist < bestDistance)
                            {
                                bestDistance = dist;
                            }
                        }

                        if (bestDistance < distanceToCamera)
                        {
                            extra.lastCollideTime = Time.time;
                            extra.currentReturnRate = 0f;
                            desiredDistToCamera = bestDistance;
                        }
                    }

                    if (extra.previousAdjustment > desiredDistToCamera)
                    {
                        extra.previousAdjustment = desiredDistToCamera;
                    }
                    else
                    {
                        // Add an extra lerp up to full value to make it buttery
                        extra.currentReturnRate = Mathf.Lerp(extra.currentReturnRate, this.returnAfterCollisionRate, 5f * Time.deltaTime);
                        extra.previousAdjustment = Mathf.Lerp(extra.previousAdjustment, desiredDistToCamera, extra.currentReturnRate * Time.deltaTime);
                    }

                    displacement = targetToCameraRay.GetPoint(extra.previousAdjustment) - cameraPos;
                }

                state.PositionCorrection += displacement;
            }
        }
    }
}