using RootMotion.FinalIK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRM;

namespace Assets.Scripts.Service
{
    public static class VRMService
    {
        public static IEnumerator LoadVrmCoroutine(string path, Action<GameObject> onLoaded)
        {
            var www = new WWW(path);
            yield return www;
            VRMImporter.LoadVrmAsync(path, onLoaded);
        }
        public static (Transform head, Transform left, Transform right, Transform main) SetupVRIK(GameObject avatar, GameObject headTarget, GameObject leftHandTarget, GameObject rightHandTarget)
        {
            var vrIK = avatar.AddComponent<VRIK>();
            
            vrIK.AutoDetectReferences();

            vrIK.solver.leftArm.stretchCurve = new AnimationCurve();
            vrIK.solver.rightArm.stretchCurve = new AnimationCurve();

            vrIK.solver.spine.headTarget = headTarget.transform;
            vrIK.solver.leftArm.target = leftHandTarget.transform;
            vrIK.solver.rightArm.target = rightHandTarget.transform;

            vrIK.solver.locomotion.footDistance = 0.1f;
            return (vrIK.solver.spine.headTarget, vrIK.solver.leftArm.target, vrIK.solver.rightArm.target, vrIK.transform);
        }
    }
}
