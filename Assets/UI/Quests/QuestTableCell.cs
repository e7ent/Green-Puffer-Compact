﻿using GreenPuffer.Quests;
using UnityEngine;
using UnityEngine.UI;

namespace GreenPuffer.UI
{
#pragma warning disable 0649
    class QuestTableCell : TableCell
    {
        [SerializeField]
        private Image icon;
        [SerializeField]
        private Text title, description, progressText, rewardText;
        [SerializeField]
        private Button rewardButton;

        private Quest quest;

        public void OnButtonClicked()
        {
            quest.TryProvideReward();
            rewardButton.interactable = !quest.AlreadyProvide;
        }

        public override void Load(object data)
        {
            quest = data as Quest;

            var descriptor = quest.Descriptor;
            icon.sprite = descriptor.Icon;
            title.text = descriptor.Title;
            description.text = descriptor.Description;
            rewardText.text = descriptor.RewardCoin + " Coin";
            progressText.text = quest.CurrentValue + "/" + quest.GoalValue;
            rewardButton.gameObject.SetActive(quest.Complate);
            rewardButton.interactable = !quest.AlreadyProvide;
        }
    }
}