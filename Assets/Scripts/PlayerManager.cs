using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using UnityEngine;

namespace EpicGameJam
{
    public class PlayerManager : MonoSingleton<PlayerManager>
    {
        public Player UpsidePlayer;
        public Player DownsidePlayer;

        public Cloud UpsideCloud;
        public Cloud DownsideCloud;

        public bool IsUpside = false;

        void Awake()
        {
            UpsidePlayer.gameObject.SetActive(false);
        }

        void Update()
        {
            if (Input.GetButtonUp("Jump") && GameManager.instance.GameIsRunning)
            {
                PopCloud();
                ChangeSide();
            }
        }

        void ChangeSide()
        {
            IsUpside = !IsUpside;

            UpsidePlayer.gameObject.SetActive(IsUpside);
            DownsidePlayer.gameObject.SetActive(!IsUpside);
        }

        void PopCloud()
        {
            if (IsUpside)
            {
                UpsideCloud.gameObject.SetActive(true);
                UpsideCloud.ResetCloud();
            }
            else
            {
                DownsideCloud.gameObject.SetActive(true);
                DownsideCloud.ResetCloud();
            }
        }
    }
}
