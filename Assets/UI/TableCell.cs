using UnityEngine;

namespace GreenPuffer.UI
{
    abstract class TableCell : MonoBehaviour
    {
        public abstract void Load(object data);
    }
}