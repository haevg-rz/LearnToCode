using System.Collections.Generic;

namespace WPFUiBindingPerformance.DataDefinitions
{
    public class TypeWithNestedProperties
    {
        public string TypeName { get; set; }
        public List<NestedProperties> NestedPropertiesList { get; set; }

        public TypeWithNestedProperties(string typeName, int numberOfNestedProperties)
        {
            this.TypeName = typeName;
            this.NestedPropertiesList = new List<NestedProperties>();

            for (int i = 0; i < numberOfNestedProperties; i++)
            {
                this.NestedPropertiesList.Add(new NestedProperties
                {
                    ID = i,
                    State = "existent" + i,
                    IsCrucial = false
                });
            }
        }
    }
}