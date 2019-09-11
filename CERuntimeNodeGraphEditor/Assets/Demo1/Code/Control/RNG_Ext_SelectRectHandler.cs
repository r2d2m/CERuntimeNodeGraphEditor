using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace.Control
{
    public class RNG_Ext_SelectRectHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        public RectTransform SelectRectRender;

        private Vector2 mDragDownPos;
        private float mNowWidth;
        private float mNowHeight;
        private bool mIsDuringDrag;

        private void Start() { SelectRectRender.gameObject.SetActive(false); }

        public void OnDrag(PointerEventData eventData)
        {
            if (!mIsDuringDrag) return;
            UpdateSelectArea(mDragDownPos, eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            mIsDuringDrag = false;
            SelectRectRender.gameObject.SetActive(false);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //如果鼠标中键或者右键被按下则不能选择
            if (Input.GetMouseButton(1) || Input.GetMouseButton(2)) return;
            mIsDuringDrag = true;
            SelectRectRender.gameObject.SetActive(true);
            mDragDownPos = eventData.position;
        }

        private void UpdateSelectArea(Vector3 _posA, Vector3 _posB)
        {
            var minX = Mathf.Min(_posA.x, _posB.x);
            var maxX = Mathf.Max(_posA.x, _posB.x);
            var minY = Mathf.Min(_posA.y, _posB.y);
            var maxY = Mathf.Max(_posA.y, _posB.y);

            mNowWidth = Mathf.Max(maxX - minX, 1);
            mNowHeight = Mathf.Max(maxY - minY, 1);

            SelectRectRender.position = new Vector3(minX, minY, 0);
            SelectRectRender.sizeDelta = new Vector2(mNowWidth, mNowHeight);
        }
    }
}