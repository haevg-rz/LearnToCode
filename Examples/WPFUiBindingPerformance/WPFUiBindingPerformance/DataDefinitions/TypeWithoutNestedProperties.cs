using System.Collections.Generic;

namespace WPFUiBindingPerformance.DataDefinitions
{
    public class TypeWithoutNestedProperties : NestedProperties
    {
        public string TypeName { get; set; }
        private List<TypeWithoutNestedProperties> typeWithoutNestedPropertiesesList { get; set; }

        public List<TypeWithoutNestedProperties> GetTypeWithoutNestedPropertieses(string typeName, int amount)
        {
            this.typeWithoutNestedPropertiesesList = new List<TypeWithoutNestedProperties>();
            for (int i = 0; i < amount; i++)
            {
                this.typeWithoutNestedPropertiesesList.Add(new TypeWithoutNestedProperties
                {
                    TypeName = typeName,
                    ID = i,
                    State = "existent" + i,
                    IsCrucial = false
                });
            }

            return this.typeWithoutNestedPropertiesesList;
        }
    }
}