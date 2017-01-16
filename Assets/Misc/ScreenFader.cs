using System;
using UnityEngine;
using DG.Tweening;

namespace GreenPuffer.Misc
{
    public class ScreenFader : MonoBehaviour
    {
        private static ScreenFader instance;
        private Tween tween;
        private Color color;
        private Texture2D texture;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            texture = new Texture2D(1, 1, TextureFormat.Alpha8, false);
        }

        private void OnGUI()
        {
            GUI.color = color;
            GUI.depth = int.MinValue;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
        }

        public void FadeTo(Color start, Color end, float duration = 1, Action finished = null)
        {
            if (tween != null)
            {
                if (!tween.IsComplete())
                {
                    tween.Complete(true);
                }
            }
            color = start;
            tween = DOTween.To(() => color, x => color = x, color, 1);
            tween.SetUpdate(false);
        }
        
        public static void FadeIn(Color start, Color end, float duration = 1, Action finished = null)
        {
            instance.FadeTo(start, end, duration, finished);
        }
    }
}