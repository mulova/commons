using System;
using System.Ex;
using mulova.commons;
using UnityEditor;
using UnityEngine;

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
        #if UNITY_2018_1_OR_NEWER
        public SceneView.CameraMode camMode;
        #else
        public DrawCameraMode camMode;
        #endif

        public void Collect()
        {
            var view = EditorUtil.sceneView;
            if (view != null)
            {
                size = view.size;
                in2dMode = view.in2DMode;
                rot = view.rotation;
                pivot = view.pivot;
                ortho = view.orthographic;
                fov = ortho ? view.camera.orthographicSize : view.camera.fieldOfView;
                rotationLocked = view.isRotationLocked;
                #if UNITY_2018_1_OR_NEWER
                camMode = view.cameraMode;
                #else
                camMode = view.renderMode;
                #endif
            }
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
                view.rotation = rot;
                view.pivot = pivot;
                view.orthographic = ortho;
            
                if (!in2dMode)
                {
                    var svRot = view.GetFieldValue<object>("svRot");
                    ReflectionUtil.Invoke(svRot, "ViewFromNiceAngle", view, !in2dMode);
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
            return id;
        }
    }
}