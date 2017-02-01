# LinqExtensions
Useful extensions for C# Linq

## Examples

#### Action - iterates over list and performs action
```c#
var list = new [] {1, 2, 3};

list.Action(item => PrintItem(item));
```

#### Distinct - unique items based on the given property of an object.
```c#
class Paper {
  Paper(int width, int height) {Width = width, Height = height}
  int Width {get;set;}
  int Height {get;set;}
}

var list = new [] {new Paper(1,1), new Paper(1,3), new Paper(1,3)}; // notice repeat of Paper(1,3)

var distinctItems = list.Distinct(item => item.Height);
// should contain only 2 items - Paper(1,1) and Paper(1,3)
```

#### Distinct - unique items based on the given property of an object, takes the max/min value.
```c#
class Paper {
  Paper(int width, int height) {Width = width, Height = height}
  int Width {get;set;}
  int Height {get;set;}
}

var list = new [] {new Paper(1,1), new Paper(1,3), new Paper(2,3)};

var distinctItems = list.Distinct(item => item.Height, item => item.Width);
// distinct items based on property Height if there are multiple then take highest based on Width
// should contain only 2 items - Paper(1,1) and Paper(2,3) e.g Because 2 > 1 it took Paper(2,3) over Paper(1,3)

var distinctItems = list.Distinct(item => item.Height, item => item.Width, takeMin: true);
// distinct items based on property Height if there are multiple then take lowest based on Width
// should contain only 2 items - Paper(1,1) and Paper(1,3) e.g Because 1 < 2 it took Paper(1,3) over Paper(2,3)
```
