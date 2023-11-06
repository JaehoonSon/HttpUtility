# HttpUtility
A highly versatile tool to support fluent HttpClient workflow and custom class for efficient Thread-Safe collections.

## Usage
```
var que = new ConcurrentQueue<T>();
var client = new UniversalClient();
// Filter duplicate values on-creation
using (var NDC = new NonDuplicateConcurrentQueue<T>(ref que))
{
	NDC.AddToQueue("1.1.1.1:7858");
}
```
