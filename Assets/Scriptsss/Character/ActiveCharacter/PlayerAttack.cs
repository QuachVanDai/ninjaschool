﻿using DG.Tweening;
using QuachDai.NinjaSchool.Monsters;
using UnityEngine;
using UnityEngine.UI;
namespace QuachDai.NinjaSchool.Character
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] PlayerSkill playerSkill;
        public Monster monster;
        public Text[] expText;

        private float distance;
        private SetPlayer setPlayer = new SetPlayer();
        private SetMonster setMonster = new SetMonster();
        Player player => Player.Instance;
        PlayerController2D playerController2D => PlayerController2D.Instance;
        private void Start()
        {
            posStartText = expText[0].rectTransform.anchoredPosition;

        }
        private void Update()
        {
            if (GameManager.Instance.IsPlayGame == false) return;
            if (playerController2D.IsGround() == false) return;
            if (monster != null)
            {
                distance = Vector2.Distance(transform.position, monster.GetPosition());
                if (distance > 7)
                {
                    InforMonster.Instance.SetActive(false);
                    monster = null;
                    return;
                }

                if (playerController2D.getInputSpace())
                {

                    if (distance > 6)
                    {
                        TextTemplate.Instance.SetText(TagScript.khoangCach);
                        return;
                    }

                    //if player use skilllv5 or skilllv15, player cannot attack.
                    if (playerSkill.GetIDSkill() == IDSkill.SkillLv5)
                    {
                        TextTemplate.Instance.SetText(TagScript.useSkill);
                        return;
                    }
                    if (playerSkill.IsActtack() && monster.currHp > 0)
                        PlayerAttackMonster();
                }
            }
        }
    
        float damage;
        public void PlayerAttackMonster()
        {
            damage = player.GetDamage() + playerSkill.GetSkillDamage();
            Debug.Log(damage + " " + setMonster.getExpMonsterDictionary(monster.level) + " " +
                   setPlayer.getExpPlayerDictionary(player.GetLevel()));
            playerSkill.SkillAttack(Damgaed, AddExp);
        }
        public void Damgaed()
        {
            if (monster != null)
                monster.monsterAttacked.Attacked((int)damage);
        }
        double exp;
        public void AddExp()
        {
            if (monster == null || monster.currHp<=0) return;
            if (monster.currHp <= damage)
                 damage = monster.currHp;
            exp = (damage * setMonster.getExpMonsterDictionary(monster.level) * 100) /
                setPlayer.getExpPlayerDictionary(player.GetLevel());
            TextMove(((int)exp).ToString());
            if (player.GetPercentExp() + exp >= 99.99f)
            {
                if (player.GetLevel() == 20)
                    player.SetPercentExp(99.99f);
                else
                    player.SetPercentExp(0);
                player.IncreaseLevel();
                player.GetLevelText();
            }
            else
                player.IncreasePercentExp((float)exp);
            player.SetPercentExpText(player.GetPercentExp());
        }
        public Vector3 posStartText;

        public void TextMove(string _damage)
        {
            for (int i = 0; i < expText.Length; i++)
            {
                if (!expText[i].gameObject.activeSelf)
                {
                    expText[i].text = _damage;
                    expText[i].rectTransform.anchoredPosition = posStartText;
                    expText[i].gameObject.SetActive(true);
                    var t = expText[i].rectTransform.DOAnchorPosY(expText[i].rectTransform.localPosition.y + 2.5f, 0.5f)
               .OnComplete(() =>
               {
                   expText[i].gameObject.SetActive(false);
               });
                    return;
                }
            }


        }
        public SkillAnimation skillAnimation;
        public void FindMonster(Monster _monster)
        {
            monster = _monster.GetComponent<Monster>();
            skillAnimation.spritePosMonster = monster.spriteMonsterAttacked;
        }
        void OnDrawGizmos()
        {
            if (monster == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, monster.transform.position);
        }
    }
}