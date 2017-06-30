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
        public Player Upside;
        public Player Downside;

        public bool IsUpside = false;

        void Awake()
        {
            Upside.gameObject.SetActive(false);
        }

        void Update()
        {
            if (Input.GetButtonUp("Jump"))
            {
                ChangeSide();
            }
        }

        void ChangeSide()
        {
            IsUpside = !IsUpside;

            Upside.gameObject.SetActive(IsUpside);
            Downside.gameObject.SetActive(!IsUpside);
        }   
    }
}
