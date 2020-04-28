using Assets.Scripts.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioPlayManager : MonoBehaviour
{
    public VRMFactory VRMFactory;
    public string AnimateAvatarName = "ShopMember";
    AvatarAnimationService avatarAnimation = new AvatarAnimationService();
    public RuntimeAnimatorController animatorController;
    // Start is called before the first frame update
    void Start()
    {
        VRMFactory.Create(AnimateAvatarName, "Estuary_School", AvatarType.Com, avatar => {
            var animator = avatar.GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;
            avatarAnimation._CharacterAnimator = animator;
            avatarAnimation.SetWalkAnimation();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
