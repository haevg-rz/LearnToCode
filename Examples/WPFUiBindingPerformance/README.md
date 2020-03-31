# WPFUiBindingPerformance

This Project is designed to show how much the binding performance is dependent on the data structure.

There are three types defined:
  -Nested type: containing a property to represent a type name and a List<T>, where T is it's own type with own properties.
  -Nonnested type: all the properties from List<T> are on the same level as the nonnested type's name property (no internal list)
  -Silent observable collection: modified observable collection, which raises the collection changed event only once, after all data has been added

In the UI you can determin the amount of properties you want to generate. The time shown is the time for generation and filling the binded observable collection. After clicking the "Load" button you can observe how long it takes for the data to show up in the UI and for it to actually become responsive to further actions.

This project shows:
1) If you have little data, than you can choose any type. Nested type may be a better representation of your data and the actual context and may reduce redundancy in properties.
	Trivial Example: Type = University; List<T>, wehere T = Faculty
2) If you have large amount of data, than you have to consider restructuring it. Otherwise you may experience long lasting lags, while the data ist being rendered. This may lead to property redundancy and loss of structure hierarchy
	Trivial Example: same context as example from 1) but without internal list, meaning that each object of this type will contain the property UniversityName (redundancy) and the Faculty properties. This will reduce binding lag
3) If you have huge amount of data, than also consider using the silent collection. WPF events are usually fast, but if you raise 500.000 of them or even more, than it will have a negative impact. Silent collection will raise this event only once, no matter the amount of entries.
