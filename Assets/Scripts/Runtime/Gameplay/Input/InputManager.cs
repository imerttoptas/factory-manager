using UnityEngine;

namespace Runtime.Gameplay.Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private SpriteRenderer mapSpriteRenderer; 
        
        private Vector3 difference;
        private Vector3 dragOrigin;
        private float mapMaxX, mapMinX, mapMaxY, mapMinY;

        private void Awake()
        {
            CalculateMapBorderPositions();
        }

        void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                dragOrigin = cam.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            }
            if (UnityEngine.Input.GetMouseButton(0))
            {
                difference = dragOrigin - cam.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                cam.transform.position = ClampPosition(cam.transform.position + difference);
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                IClickableObject clickedObject = TryGetClickedObject();
                clickedObject?.OnClick();
            }
        }
        
        private IClickableObject TryGetClickedObject()
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero);
            
            if (hit.collider != null && hit.collider.TryGetComponent(out IClickableObject clickedObject))
            {
                if (clickedObject.CanBeClicked)
                {
                    return clickedObject;
                }
            }

            return null;
        }

        private void CalculateMapBorderPositions()
        {
            mapMaxX = mapSpriteRenderer.transform.position.x + mapSpriteRenderer.bounds.size.x / 2f;
            mapMinX = mapSpriteRenderer.transform.position.x - mapSpriteRenderer.bounds.size.x / 2f;
            mapMaxY = mapSpriteRenderer.transform.position.y + mapSpriteRenderer.bounds.size.y / 2f;
            mapMinY = mapSpriteRenderer.transform.position.y - mapSpriteRenderer.bounds.size.y / 2f;
        }

        private Vector3 ClampPosition(Vector3 targetPosition)
        {
            float camHeight = cam.orthographicSize;
            float camWidth = cam.orthographicSize * cam.aspect;

            float maxX = mapMaxX - camWidth;
            float minX = mapMinX + camWidth;
            float maxY = mapMaxY - camHeight;
            float minY = mapMinY + camHeight;

            float targetX = Mathf.Clamp(targetPosition.x, minX, maxX);
            float targetY = Mathf.Clamp(targetPosition.y, minY, maxY);
            return new Vector3(targetX, targetY, targetPosition.z);
        }
        

    }
}
