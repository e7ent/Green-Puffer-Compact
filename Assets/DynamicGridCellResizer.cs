using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GreenPuffer.UI
{
    [ExecuteInEditMode]
    class DynamicGridCellResizer : MonoBehaviour
    {
        [SerializeField]
        private GridLayoutGroup gridLayoutGroup;
        [SerializeField]
        [Tooltip("If the value is 0, nothing happens.")]
        private int row = 0, colmn = 0;

        private RectTransform rectTransform;

        private void Update()
        {
            if (gridLayoutGroup != null)
            {
                if (rectTransform == null)
                {
                    rectTransform = gridLayoutGroup.transform as RectTransform;
                    if (rectTransform == null)
                    {
                        return;
                    }
                }
                var rect = rectTransform.rect;
                gridLayoutGroup.cellSize = new Vector2(rect.width / colmn, rect.height / row);
            }
        }
    }
}