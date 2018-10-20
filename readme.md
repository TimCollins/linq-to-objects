A reimplementation of LINQ To Objects. See [here](https://codeblog.jonskeet.uk/category/edulinq/).

The approach is:
* Write unit tests against existing LINQ implementation
* Verify those tests pass
* Remove existing LINQ implementation
* Make the tests pass against the reimplementation