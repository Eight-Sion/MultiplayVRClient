using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarAnimationService
{
    /// <summary> キャラのアニメーション管理 </summary>
    public Animator _CharacterAnimator = null;
    public void SetAnimation(string name)
    {
        _CharacterAnimator.Play(name, 0);
    }
    public void SetSpeed(float speed)
    {
        _CharacterAnimator.speed = speed;
    }
    #region TEST ANIMATION
    public void SetIdleAnimation()
    {
        _CharacterAnimator.Play("idle", 0);
    }
    public void SetBowApologyAnimation()
    {
        _CharacterAnimator.Play("bow-apology", 0);
    }
    public void SetBowThanksAnimation()
    {
        _CharacterAnimator.Play("bow-thanks", 0);
    }
    public void SetPraiseAnimation()
    {
        _CharacterAnimator.Play("praise", 0);
    }
    public void SetStandUpAnimation()
    {
        _CharacterAnimator.Play("standup", 0);
    }
    public void SetSitDownAnimation()
    {
        _CharacterAnimator.Play("sitdown", 0);
    }
    public void SetWalkAnimation()
    {
        _CharacterAnimator.Play("walk", 0);
    }
    #endregion
}

