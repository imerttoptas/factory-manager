using UnityEngine;

namespace Runtime.Pooling
{
    public enum ObjectType
    {
        [InspectorName("Grid/Cell")] Cell = 0,
        [InspectorName("Unit/Cube")] Cube = 1,
        [InspectorName("Unit/Box")] Box = 2,
        [InspectorName("Unit/Stone")] Stone = 3,
        [InspectorName("Unit/Vase")] Vase = 4,
        [InspectorName("Unit/TNT")] TNT = 5,
        [InspectorName("UI/ItemUIObject")] ItemUIObject = 100,
        [InspectorName("UI/OrderUIObject")] OrderUIObject = 101,
        [InspectorName("UI/Panels/BillOfMaterialPanel")] BillOfMaterialPanel = 102,
    }
}