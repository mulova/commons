using System;
using System.Ex;
using mulova.commons;
using UnityEditor;
using UnityEngine;
using static UnityEditor.SceneView;

namespace mulova.unicore
{
    [Serializable]
    public class SceneCamProperty
    {
        public string id;
        public bool in2dMode;
        public float size;
        public bool ortho;
        public Vector3 pivot;
        public Quaternion rot;
        public bool rotationLocked;
        public float fov;
        public CameraSettings cameraSettings;
        #if UNITY_2018_1_OR_NEWER
        public SceneView.CameraMode camMode;
        #else
        public DrawCameraMode camMode;
        #endif

        public bool Collect()
        {
            var changed = false;
            var view = EditorUtil.sceneView;
            if (view != null)
            {
                if (size != view.size)
                {
                    size = view.size;
                    changed = true;
                }
                if (in2dMode != view.in2DMode)
                {
                    in2dMode = view.in2DMode;
                    changed = true;
                }
                if (rot != view.rotation)
                {
                    rot = view.rotation;
                    changed = true;
                }
                if (pivot != view.pivot)
                {
                    pivot = view.pivot;
                    changed = true;
                }
                if (ortho != view.orthographic)
                {
                    ortho = view.orthographic;
                    changed = true;
                }
                if (cameraSettings != view.cameraSettings)
                {
                    cameraSettings = view.cameraSettings;
                    changed = true;
                }
                var newFov = ortho ? view.camera.orthographicSize : view.camera.fieldOfView;
                if (fov != newFov)
                {
                    fov = newFov;
                    changed = true;
                }
                if (rotationLocked != view.isRotationLocked)
                {
                    rotationLocked = view.isRotationLocked;
                    changed = true;
                }
#if UNITY_2018_1_OR_NEWER
                if (camMode != view.cameraMode)
                {
                    camMode = view.cameraMode;
                    changed = true;
                }
#else
                if (camMode != view.renderMode)
                {
                    camMode = view.renderMode;
                    changed = true;
                }
#endif
            }
            return changed;
        }
        
        public bool valid
        {
            get
            {
                return size != 0f;
            }
        }
        
        public void Apply()
        {
            if (size != 0f)
            {
                var view = EditorUtil.sceneView;
                view.size = size;
                view.in2DMode = in2dMode;
                if (!in2dMode)
                {
                    view.rotation = rot;
                }
                view.pivot = pivot;
                view.orthographic = ortho;
                view.cameraSettings = cameraSettings;
            
                if (!in2dMode)
                {
#if UNITY_2021_2_OR_NEWER
#else
                    var svRot = view.GetFieldValue<object>("svRot");
                    ReflectionUtil.Invoke(svRot, "ViewFromNiceAngle", view, !in2dMode);
#endif
                }

                if (ortho)
                {
                    view.camera.orthographicSize = fov;
                }
                else
                {
                    view.camera.fieldOfView = fov;
                }
                view.isRotationLocked = rotationLocked;
#if UNITY_2018_1_OR_NEWER
                view.cameraMode = camMode;
#else
                view.renderMode = camMode;
#endif
            } else
            {
                Debug.LogWarning("SceneCam is not initialized");
            }
        }

        public override string ToString()
        {
            return $"{id}: size {size}  fov {fov}  pivot {pivot}";
        }

        public override bool Equals(object obj)
        {
            if (UnityEngine.Object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj is SceneCamProperty that)
            {
                return id == that.id
                    && in2dMode == that.in2dMode
                    && size == that.size
                    && ortho == that.ortho
                    && pivot == that.pivot
                    && rot == that.rot
                    && rotationLocked == that.rotationLocked
                    && fov == that.fov
                    && camMode == that.camMode;
            } else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            var h = id.GetHashCode();
            h += h * 37 + in2dMode.GetHashCode();
            h += h * 37 + size.GetHashCode();
            h += h * 37 + ortho.GetHashCode();
            h += h * 37 + pivot.GetHashCode();
            h += h * 37 + rot.GetHashCode();
            h += h * 37 + rotationLocked.GetHashCode();
            h += h * 37 + fov.GetHashCode();
            h += h * 37 + camMode.GetHashCode();
            return h;
        }
    }
}