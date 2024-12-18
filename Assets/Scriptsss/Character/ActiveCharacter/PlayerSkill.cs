﻿using DG.Tweening;
using QuachDai.NinjaSchool.Animations;
using QuachDai.NinjaSchool.Sound;
using System;
using System.Collections;
using UnityEngine;
namespace QuachDai.NinjaSchool.Character
{
    public class PlayerSkill : MonoBehaviour
    {
        [SerializeField] SkillAnimation skillAnimation;
        [SerializeField] FrameSkill frameSkill;
        [SerializeField] SkillRecoveryTime skillRecoveryTime;
        bool isActtack, isSkillLv5;
        Player player => Player.Instance;
        TextTemplate textTemplate => TextTemplate.Instance;
        AnimatorSystem animatorSystem => AnimatorSystem.Instance;
        SoundSystem soundSystem => SoundSystem.Instance;
        ClipSystem clipSystem => ClipSystem.Instance;
        private void Start()
        {
            isActtack = true;
            isSkillLv5 = true;
        }
        public IDSkill GetIDSkill()
        {
            return frameSkill.IDSkill;
        }
        public void SetFrameSkill(FrameSkill _frameSkill)
        {
            frameSkill = _frameSkill;
        }
        public void SetSkillRecoveryTimes(SkillRecoveryTime _skillRecoveryTime)
        {
            skillRecoveryTime = _skillRecoveryTime;
        }
        public bool IsActtack()
        {
            return frameSkill.isActack;
        }
        public void SkillAttack(Action _damaged, Action _addExp)
        {
            StartCoroutine(_SkillAttack());
            IEnumerator _SkillAttack()
            {
                soundSystem.PlayOneShotSound(clipSystem.skillClip[clipSystem.GetIndex()]);
                ManaUseSkill();
                animatorSystem.SetBool(player.GetAnimator(), "IsAttack", true);
                isActtack = false;
                player.SetMp(frameSkill.mp * (-1));
                skillRecoveryTime.isTime = true;
                yield return new WaitForSeconds(0.23f);
                animatorSystem.SetBool(player.GetAnimator(), "IsAttack", false);
                _damaged?.Invoke();
                _addExp?.Invoke();
                skillAnimation.AnimationSkill(frameSkill);
                yield return new WaitForSeconds(frameSkill.timeSkill-0.1f);
                isActtack = true;
                skillRecoveryTime.isTime = false;
            }
        }


        public void InCreaseHPMP()
        {
            float point;
            point = frameSkill.increasedHPMP +
                (frameSkill.skillDamage * Mathf.Pow(frameSkill.skillLevel, frameSkill.coefficient));
            player.SetHp(point);
            player.SetMp(point);
        }
        public void SkillLevel5()
        {
            ManaUseSkill();
            if (!isSkillLv5)
            {
                textTemplate.SetText(TagScript.hoiChieu);
                return;
            }
            if (isSkillLv5)
                StartCoroutine(_UseSkillLv5());
            IEnumerator _UseSkillLv5()
            {
                skillAnimation.AnimationSkillLv5_15(frameSkill);
                isSkillLv5 = false;
                InvokeRepeating(nameof(InCreaseHPMP), 0, 0.5f);
                skillRecoveryTime.isTime = true;
                yield return new WaitForSeconds(1.5f);
                CancelInvoke(nameof(InCreaseHPMP));
                yield return new WaitForSeconds(frameSkill.timeSkill - 1.5f);
                skillRecoveryTime.isTime = false;
                isSkillLv5 = true;
            }
        }

        public void ManaUseSkill()
        {
            if (player.GetMp() < frameSkill.mp)
            {
                Debug.Log("khong du Mana de su dung  " + player.GetMp());
                return;
            }

        }
        public float GetSkillDamage()
        {
            return frameSkill.skillDamage +
                (frameSkill.skillDamage * Mathf.Pow(frameSkill.skillLevel, frameSkill.coefficient));
        }
        public float duration;
        public float strength;
        public int vibrato = 10;
        public float randomness = 90;
        public bool snapping = false;
        public bool fadeOut = true;
        private Vector3 posStart;
        bool isLoopShakePosition;
        public void DOCameraShake()
        {
            Camera.main.transform.DOShakePosition(duration, strength, vibrato, randomness, snapping, fadeOut);
        }
    }
}
