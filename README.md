# MoreCollections
More collections for C# is an effort to fill gaps in the C# System.Collections namespace.

Current collections:
* Double-Ended Queue (Deque)

Planned collections
* Priority Queue
* More tbd...

# Deque

There's currently multiple Deque implementations, Benchmarks are being written and run. One will be the final winner but the others will remain available under ``AllCollections``.

All of them follow the following:
| Operation | Complexity |
| --------- | ---------- |
| PushFront | O(1)       |
| PushBack  | O(1)       |
| PopFront  | O(1)       |
| PopBack   | O(1)       |
| Index     | O(1)       |

Only some implementations have constant pointers, however this is not important in ``safe`` code.
