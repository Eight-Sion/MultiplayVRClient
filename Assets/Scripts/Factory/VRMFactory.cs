using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.ServerShared.MessagePackObjects;
using System.Linq;
using UnityEngine.Events;

namespace Assets.Scripts.Service
{
    public class VRMFactory: MonoBehaviour
    {
        Dictionary<string, (GameObject head, GameObject left, GameObject right, GameObject main)> _models = new Dictionary<string, (GameObject head, GameObject left, GameObject right, GameObject main)>();

        public GameObject PlayerParentObject;
        public GameObject PlayerHeadObject;
        public GameObject PlayerLeftHandObject;
        public GameObject PlayerRightHandObject;
        public GameObject GuestParentObject;
        public GameObject ComParentObject;

        public void Create(Player player, AvatarType type, UnityAction<GameObject> avatarAction = null)
            => Create(player.Name, player.ModelName, type, avatarAction);
        public void Create(string id, string modelName, AvatarType type, UnityAction<GameObject> avatarAction = null)
        {
            var path = Path.Combine(Application.dataPath, "Models", "Avatar", modelName, modelName + ".vrm");
            StartCoroutine(VRMService.LoadVrmCoroutine(path, go =>
            {
                var headTarget = new GameObject();
                var leftHandTarget = new GameObject();
                var rightHandTarget = new GameObject();
                switch (type)
                {
                    case AvatarType.Player:
                        go.transform.parent = PlayerParentObject.transform;
                        Debug.Log(go.transform.childCount);
                        VRMService.SetupVRIK(go, PlayerHeadObject, PlayerLeftHandObject, PlayerRightHandObject);
                        if (!_models.ContainsKey(id)) _models.Add(id, (PlayerHeadObject, PlayerLeftHandObject, PlayerRightHandObject, go));
                        break;

                    case AvatarType.Guest:
                        headTarget.transform.position = new Vector3(0f, 1.5f, 0f);
                        leftHandTarget.transform.position = new Vector3(-0.5f, 0.8f, 0f);
                        rightHandTarget.transform.position = new Vector3(0.5f, 0.8f, 0f);
                        go.transform.parent = GuestParentObject.transform;
                        VRMService.SetupVRIK(go, headTarget, leftHandTarget, rightHandTarget);
                        if (!_models.ContainsKey(id)) _models.Add(id, (headTarget, leftHandTarget, rightHandTarget, go));
                        break;

                    case AvatarType.Com:
                        headTarget.transform.position = new Vector3(0f, 1.5f, 0f);
                        leftHandTarget.transform.position = new Vector3(-0.5f, 0.8f, 0f);
                        rightHandTarget.transform.position = new Vector3(0.5f, 0.8f, 0f);
                        go.transform.parent = ComParentObject.transform;
                        if (!_models.ContainsKey(id)) _models.Add(id, (headTarget, leftHandTarget, rightHandTarget, go));
                        break;
                }
                avatarAction?.Invoke(go);
            }));
        }
        public (GameObject head, GameObject left, GameObject right, GameObject main) Read(string id)
        {
            return _models.FirstOrDefault(model => model.Key == id).Value;
        }
        
        public void UpdateGuest(Player player)
            => UpdateGuest(player.Name, player.HeadPosition, player.LeftPosition, player.RightPosition, player.HeadRotation, player.LeftRotation, player.RightRotation);
        public void UpdateGuest(string id, Vector3 headPosition, Vector3 leftPosition, Vector3 rightPosition, Quaternion headRotate, Quaternion leftRotate, Quaternion rightRotate)
        {
            if (!_models.ContainsKey(id)) return;
            var target = _models[id];
            target.head.transform.position = new Vector3(headPosition.x, headPosition.y, headPosition.z);
            target.head.transform.rotation = new Quaternion(headRotate.x, headRotate.y, headRotate.z, headRotate.w);
            target.left.transform.position = new Vector3(leftPosition.x, leftPosition.y, leftPosition.z);
            target.left.transform.rotation = new Quaternion(leftRotate.x, leftRotate.y, leftRotate.z, leftRotate.w);
            target.right.transform.position = new Vector3(rightPosition.x, rightPosition.y, rightPosition.z);
            target.right.transform.rotation = new Quaternion(rightRotate.x, rightRotate.y, rightRotate.z, rightRotate.w);
        }
        public void UpdateCom(string id, Vector3 position)
        {
            if (!_models.ContainsKey(id)) return;
            var target = _models[id];
            target.head.transform.position = new Vector3(position.x, position.y, position.z);
        }
        public void Delete(string id)
        {
            if (_models.ContainsKey(id)) {
                Destroy(_models[id].main.transform.parent.gameObject);
                _models.Remove(id);
            }
        }
        public bool Contains(string id) => _models.ContainsKey(id);
    }
    public enum AvatarType
    {
        Player,
        Guest,
        Com
    }
}